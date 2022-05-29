using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LoyWms.Application.Common.Interfaces;

public interface ISpecification<T>
{
    // 查询条件子句
    Expression<Func<T, bool>> Criteria { get; }
    // Include子句
    Func<IQueryable<T>, IIncludableQueryable<T, object>> Include { get; }
    // OrderBy子句
    Expression<Func<T, object>> OrderBy { get; }
    // OrderByDescending子句
    Expression<Func<T, object>> OrderByDescending { get; }

    // 分页相关属性
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}
