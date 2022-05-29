using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Customers.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoyWms.Application.Customers.Queries.GetCustomers;


public class GetCustomersQuery
    : IRequest<Response<IEnumerable<CustomerDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
public class GetCustomersQueryHandler
    : IRequestHandler<GetCustomersQuery, Response<IEnumerable<CustomerDto>>>
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

    public async Task<Response<IEnumerable<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {

        var data = await _customersRepository.GetAsQueryable()
            .AsNoTracking()
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .OrderBy(p => p.Id)
            .ToListAsync();

        return new Response<IEnumerable<CustomerDto>>(data);
    }
}
