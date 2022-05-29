using AutoMapper;
using LoyWms.Domain.Entities;

namespace LoyWms.Application.Products.Dtos;

public class ProductDetailDto
{
    public long Id { get; set; }
    //产品编号 唯一性
    public string ProductNo { get; set; }
    //产品名称
    public string Name { get; set; }
    //产品规格
    public string? Size { get; set; }
    //供应商Id
    public long? SupplierId { get; set; }
    //供应商名称
    public string SupplierName { get; set; }
    //供应商简称
    public string SupplierNickName { get; set; }
    //当前供应价格
    public decimal Rate { get; private set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDetailDto>();
    }
}
