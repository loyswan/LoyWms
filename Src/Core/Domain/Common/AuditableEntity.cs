namespace LoyWms.Domain.Common;

public interface IAuditableEntity
{
    IdType? CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    IdType? DeletedBy { get; set; }
    DateTime? DeletedOn { get; set; }
}

public interface ISoftDelete
{
    DateTime? DeletedOn { get; set; }
    IdType? DeletedBy { get; set; }
}


public abstract class AuditableEntity : 
    Entity, IAuditableEntity, ISoftDelete
{
    public DateTime CreatedOn { get; set; }

    public IdType? CreatedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public IdType? LastModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public IdType? DeletedBy { get; set; }

    protected AuditableEntity()
    {
        CreatedOn = DateTime.UtcNow;
        LastModifiedOn = DateTime.UtcNow;
    }
}
