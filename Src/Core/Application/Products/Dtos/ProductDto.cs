using AutoMapper;
using LoyWms.Application.Common.Mappings;
using LoyWms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Application.Products.Dtos;

public class ProductDto : IMapFrom<Product>
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

    //当前供应价格
    public double CurrentSupplyPrice { get; private set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>();
    }
}
