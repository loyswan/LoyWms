using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyWms.Application.Common.Exceptions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Customers.Dtos;
using LoyWms.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoyWms.Application.Customers.Queries.GetCustomer;


public class GetCustomerByIdQuery
    : IRequest<Response<CustomerDto>>
{
    public long Id { get; set; }

}
public class GetCustomersQueryHandler
    : IRequestHandler<GetCustomerByIdQuery, Response<CustomerDto>>
{
    private readonly ICustomerRepositoryAsync _customersRepository;
    private readonly IMapper _mapper;
    public GetCustomersQueryHandler(
        ICustomerRepositoryAsync customersRepository,
        IMapper mapper)
    {
        _customersRepository = customersRepository;
        _mapper = mapper;
    }

    public async Task<Response<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {

        var customer = await _customersRepository.GetAsQueryable(c => c.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();
        if (customer == null)
        {
            throw new ApiException($"Customer Not Found.");
        }

        return new Response<CustomerDto>(_mapper.Map<Customer, CustomerDto>(customer));
    }
}
