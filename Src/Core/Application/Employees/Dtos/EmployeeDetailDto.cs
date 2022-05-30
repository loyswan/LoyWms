using AutoMapper;
using LoyWms.Application.Common.Mappings;
using LoyWms.Domain.Entities;

namespace LoyWms.Application.Employees.Dtos;

public class EmployeeDetailDto : IMapFrom<Employee>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string EmployeeNo { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    //领导
    public EmployeeDto Manager { get; set; }

    public void Mapping(Profile profile)
    {

        profile.CreateMap<Employee, EmployeeDetailDto>()
            .ForMember(d => d.Manager.Id, o => o.MapFrom(e => e.ManagerId))
            .ForMember(d => d.Manager.Name, o => o.MapFrom(e => e.Manager.Name))
            .ForMember(d => d.Manager.EmployeeNo, o => o.MapFrom(e => e.Manager.EmployeeNo))
            .ForMember(d => d.Manager.Gender, o => o.MapFrom(e => e.Manager.Gender))
            .ForMember(d => d.Manager.BirthDate, o => o.MapFrom(e => e.Manager.BirthDate))
            .ForMember(d => d.Manager.HireDate, o => o.MapFrom(e => e.Manager.HireDate));
    }



}