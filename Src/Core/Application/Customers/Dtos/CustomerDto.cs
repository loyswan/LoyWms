using AutoMapper;
using LoyWms.Application.Common.Mappings;
using LoyWms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Application.Customers.Dtos;

public class CustomerDto : IMapFrom<Customer>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Description { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Customer, CustomerDto>();
    }
}
