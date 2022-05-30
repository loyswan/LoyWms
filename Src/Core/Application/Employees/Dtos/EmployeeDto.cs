using AutoMapper;
using LoyWms.Application.Common.Mappings;
using LoyWms.Domain.Entities;

namespace LoyWms.Application.Employees.Dtos;

public class EmployeeDto : IMapFrom<Employee>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string EmployeeNo { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeDto>();
    }
}
