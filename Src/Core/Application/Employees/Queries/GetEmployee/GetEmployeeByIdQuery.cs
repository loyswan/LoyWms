using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyWms.Application.Common.Exceptions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Employees.Dtos;
using LoyWms.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoyWms.Application.Employees.Queries.GetEmployee;

//public class GetEmployeeByIdQuery
//    : IRequest<Response<EmployeeDetailDto>>
//{
//    public long Id { get; set; }

//}
//public class GetEmployeeByIdQueryHandler
//    : IRequestHandler<GetEmployeeByIdQuery, Response<EmployeeDetailDto>>
//{
//    private readonly IEmployeeRepositoryAsync _employeeRepository;
//    private readonly IMapper _mapper;
//    public GetEmployeeByIdQueryHandler(
//        IEmployeeRepositoryAsync employeeRepository,
//        IMapper mapper)
//    {
//        _employeeRepository = employeeRepository;
//        _mapper = mapper;
//    }

//    public async Task<Response<EmployeeDetailDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
//    {

//        var employee = await _employeeRepository.GetAsQueryable(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();
//        if (employee == null)
//        {
//            throw new ApiException($"Employee Not Found.");
//        }
//        var manager = await _employeeRepository.GetAsQueryable(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();
//        if (manager == null)
//        {
//            throw new ApiException($"Employee {employee.Name} 's manager Not Found.");
//        }

//        return new Response<EmployeeDetailDto>(_mapper.Map<Employee, EmployeeDetailDto>(employee));
//    }
//}