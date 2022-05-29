using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace LoyWms.Domain.Common;

public readonly struct LoyId :
    IEquatable<LoyId>,
    IComparable<LoyId>,
    IComparable,
    IFormattable
{
    public static readonly LoyId Empty = new LoyId(0);

    private readonly long _id;
    public long Id => _id;

    private static readonly object _lock = new Object();

    static ILoyIdGenerator? _generator;
    static IMillisecondsProvider? _millisecondsProvider;

    static IMillisecondsProvider MillisecondsProvider => _millisecondsProvider ??= new DateTimeMillisecondsProvider();
    static ILoyIdGenerator IdGenerator
    {
        get
        {
            if (_generator == null)
            {
                lock (_lock)
                {
                    _generator ??= new LoyIdGenerator(MillisecondsProvider);
                }
            }
            return _generator;
        }
    }


    public LoyId(in long id)
    {
        this._id = id;
    }
    public LoyId(in int id)
    {
        this._id = id;
    }
    public LoyId(in string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("must not be null or empty", nameof(value));

        if (long.TryParse(value, out long id))
        {
            this._id = id;
        }
        else
        {
            this._id = id;
        }
    }

    public int CompareTo(LoyId other)
    {
        if (_id == other._id) return 0;
        return _id < other._id ? -1 : 1;
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
            return 1;
        if (obj is LoyId id)
            return CompareTo(id);

        throw new ArgumentException("Argument must be a LoyId");
    }

    public bool Equals(LoyId other)
    {
        return _id == other._id;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return _id.ToString(format, formatProvider);
    }

    public override string ToString()
    {
        return _id.ToString();
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (obj.GetType() != typeof(LoyId))
            return false;
        return Equals((LoyId)obj);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LoyId Next()
    {
        return IdGenerator.Next();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LoyId[] Next(int count)
    {
        var ids = new LoyId[count];

        IdGenerator.Next(ids, 0, count);

        return ids;
    }

    public static ArraySegment<LoyId> Next(LoyId[] ids, int index, int count)
    {
        return IdGenerator.Next(ids, index, count);
    }



    public static bool operator ==(in LoyId left, in LoyId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(in LoyId left, in LoyId right)
    {
        return !(left == right);
    }

    public static bool operator <(in LoyId left, in LoyId right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(in LoyId left, in LoyId right)
    {
        return left.CompareTo(right) > 0;
    }
}


public interface ILoyIdGenerator
{
    LoyId Next();

    ArraySegment<LoyId> Next(LoyId[] ids, int index, int count);

}

public interface IMillisecondsProvider
{
    long Milliseconds { get; }
}

public class LoyIdGenerator : ILoyIdGenerator
{
    readonly IMillisecondsProvider _tickProvider;

    #region 配置
    //基准时间  起始时间2022-01-01 00:00:00
    private const long BaseTimetamp = 1640966400000L;
    //序列号位数 默认10位
    private const int SequenceBits = 10;
    //序列号ID最大值（默认按序列号位数计算最大值,10位为0-1023）
    private const int SequenceMask = -1 ^ (-1 << SequenceBits);
    //时间毫秒左移10位（根据序列号位数）
    private const int TimestampLeftShift = SequenceBits;
    //产生的ID格式[最高1位0][53位时间Tick][10位序列号]，时间Tick是以基准时间开始
    #endregion 配置

    //记录上次生成Id时的Timestamp
    private long _lastTimestamp = -1L;
    //序号 
    private int _sequence = 0;
    //相对基准时间的时间戳
    private long Timestamp = 0;

    private readonly object _lock = new Object();
    public LoyIdGenerator(IMillisecondsProvider tickProvider)
    {
        _tickProvider = tickProvider;
    }


    public LoyId Next()
    {
        var nowTimestamp = _tickProvider.Milliseconds;
        lock (_lock)
        {
            if (nowTimestamp > _lastTimestamp)
            {
                _sequence = 0;//重置 
                _lastTimestamp = nowTimestamp;
            }
            _sequence = (_sequence + 1) & SequenceMask;
            if (_sequence == 0)//发生溢出 
            {
                _lastTimestamp++;//直接进入下一毫秒   
                while (nowTimestamp < _lastTimestamp)//强制等待至下一毫秒
                {
                    nowTimestamp = _tickProvider.Milliseconds;
                }
            }
            Timestamp = _lastTimestamp - BaseTimetamp;

        }
        return new LoyId((Timestamp << TimestampLeftShift) | (long)_sequence);
    }


    public ArraySegment<LoyId> Next(LoyId[] ids, int index, int count)
    {
        if (index + count > ids.Length)
            throw new ArgumentOutOfRangeException(nameof(count));

        var nowTimestamp = _tickProvider.Milliseconds;
        lock (_lock)
        {
            if (nowTimestamp > _lastTimestamp)
            {
                _sequence = 0;//重置 从0开始累加
                _lastTimestamp = nowTimestamp;
            }

            var limit = index + count;
            for (var i = index; i < limit; i++)
            {
                _sequence = (_sequence + 1) & SequenceMask;
                if (_sequence == 0) // 溢出
                {
                    _lastTimestamp++;//直接进入下一毫秒 
                }
                Timestamp = _lastTimestamp - BaseTimetamp;
                ids[i] = new LoyId((Timestamp << TimestampLeftShift) | (long)_sequence);
            }
        }
        return new ArraySegment<LoyId>(ids, index, count);
    }
}

public class DateTimeMillisecondsProvider : IMillisecondsProvider
{
    public long Milliseconds
    {
        get
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }

}


