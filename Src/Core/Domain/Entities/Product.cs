using LoyWms.Domain.Common;

namespace LoyWms.Domain.Entities;

public class Product : AuditableEntity, IAggregateRoot
{
    public Product() : base()
    {

    }

    //产品编号 唯一性
    public string ProductNo { get; set; }
    //产品名称
    public string Name { get; set; }
    //产品规格
    public string? Size { get; set; }
    //供应商Id
    public long SupplierId { get; set; }
    //当前供应价格
    public decimal Rate { get; set; }
    //产品说明
    public string Description { get; set; }


}
