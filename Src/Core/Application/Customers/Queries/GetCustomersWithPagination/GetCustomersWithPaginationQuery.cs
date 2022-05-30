using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Customers.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoyWms.Application.Customers.Queries.GetCustomersWithPagination;

public class GetCustomersWithPaginationQuery
    : IRequest<PagedResponse<IEnumerable<CustomerDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
public class GetCustomersWithPaginationQueryHandler
    : IRequestHandler<GetCustomersWithPaginationQuery, PagedResponse<IEnumerable<CustomerDto>>>
{
    private readonly ICustomerRepositoryAsync _customersRepository;
    private readonly IMapper _mapper;
    public GetCustomersWithPaginationQueryHandler(
        ICustomerRepositoryAsync customersRepository,
        IMapper mapper)
    {
        _customersRepository = customersRepository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<IEnumerable<CustomerDto>>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        //跳过项目数
        var skipnum = (request.PageNumber - 1) * request.PageSize;
        //加载项目数
        var takenum = request.PageSize;

        var data = await _customersRepository.GetAsQueryable()
            .AsNoTracking()
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .OrderBy(p => p.Id)
            .Skip(skipnum)
            .Take(takenum)
            .ToListAsync();

        return new PagedResponse<IEnumerable<CustomerDto>>(data,request.PageNumber,request.PageSize);
    }
}
