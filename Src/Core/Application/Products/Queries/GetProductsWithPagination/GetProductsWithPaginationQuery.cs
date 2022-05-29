using AutoMapper;
using AutoMapper.QueryableExtensions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Products.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoyWms.Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQuery
    : IRequest<PagedResponse<IEnumerable<ProductDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
public class GetProductsWithPaginationQueryHandler
    : IRequestHandler<GetProductsWithPaginationQuery, PagedResponse<IEnumerable<ProductDto>>>
{
    private readonly IProductRepositoryAsync _productRepository;
    private readonly IMapper _mapper;
    public GetProductsWithPaginationQueryHandler(
        IProductRepositoryAsync productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<IEnumerable<ProductDto>>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        //跳过项目数
        var skipnum = (request.PageNumber - 1) * request.PageSize;
        //加载项目数
        var takenum = request.PageSize;

        var data = await _productRepository.GetAsQueryable()
            .AsNoTracking()
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .OrderBy(p => p.Id)
            .Skip(skipnum)
            .Take(takenum)
            .ToListAsync();

        return new PagedResponse<IEnumerable<ProductDto>>(data,request.PageNumber,request.PageSize);
    }
}
