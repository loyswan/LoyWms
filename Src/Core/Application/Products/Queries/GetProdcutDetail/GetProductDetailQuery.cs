using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Products.Dtos;
using LoyWms.Domain.Entities;
using MediatR;

namespace LoyWms.Application.Products.Queries.GetProdcutDetail;

public class GetProductDetailQuery
    : IRequest<Response<ProductDetailDto>>
{
    public long ProductId { get; set; }
}

public class GetProductDetailQueryHandler
    : IRequestHandler<GetProductDetailQuery, Response<ProductDetailDto>>
{
    private readonly IProductRepositoryAsync _productRepository;
    private readonly ICustomerRepositoryAsync _customerRepository;
    private readonly IMapper _mapper;

    public GetProductDetailQueryHandler(
        IProductRepositoryAsync productRepository,
        ICustomerRepositoryAsync customerRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Response<ProductDetailDto>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(p => p.Id == request.ProductId);
        var supplier = await _customerRepository.GetAsync(c => c.Id == product.SupplierId);
        ProductDetailDto dto = new ProductDetailDto();
        var rst = _mapper.Map(product,dto);
        var s = _mapper.Map(supplier,rst);
        return new Response<ProductDetailDto>(s);
    }
}
