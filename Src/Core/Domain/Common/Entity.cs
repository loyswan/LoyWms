namespace LoyWms.Domain.Common;

public interface IEntity
{
    IdType Id { get; }
}

public abstract class Entity : IEntity
{
    public IdType Id { get; set; } = LoyId.Next().Id;
}
