using AutoMapper;
using LoyWms.Application.Common.Exceptions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Employees.Dtos;
using LoyWms.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoyWms.Application.Employees.Queries.GetEmployee;




//public class GetEmployeeDetailQuery : IRequest<Response<EmployeeDetailDto>>
//{
//    public long Id { get; set; }
//}
//public class GetEmployeeDetailQueryHandler
//    : IRequestHandler<GetEmployeeDetailQuery, Response<EmployeeDetailDto>>
//{
//    private readonly IEmployeeRepositoryAsync _employeeRepository;
//    private readonly IMapper _mapper;
//    public GetEmployeeDetailQueryHandler(
//        IEmployeeRepositoryAsync employeeRepository,
//        IMapper mapper)
//    {
//        _employeeRepository = employeeRepository;
//        _mapper = mapper;
//    }

//    public async Task<Response<EmployeeDetailDto>> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
//    {

//        var employee = await _employeeRepository.GetAsQueryable(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();
//        if (employee == null)
//        {
//            throw new ApiException($"Employee Not Found.");
//        }
//        employee.Manager = await _employeeRepository.GetManagerAsync(employee);

//        return new Response<EmployeeDetailDto>(_mapper.Map<Employee, EmployeeDetailDto>(employee));
//    }
//}
