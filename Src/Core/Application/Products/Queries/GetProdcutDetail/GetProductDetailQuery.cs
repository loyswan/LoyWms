using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Products.Dtos;
using MediatR;

namespace LoyWms.Application.Products.Queries.GetProdcutDetail;

//public class GetProductDetailQuery
//    : IRequest<Response<ProductDetailDto>>
//{
//    public long ProductId { get; set; }
//}

//public class GetProductDetailQueryHandler
//    : IRequestHandler<GetProductDetailQuery, ProductDetailDto>
//{
//    private readonly IProductRepositoryAsync _productRepository;
//    private readonly IRepository<Customer> _customerRepo;
//    private readonly IMapper _mapper;

//    public GetProductDetailQueryHandler(
//        IRepository<Product> repo,
//        IRepository<Customer> customerRepo,
//        IMapper mapper)
//    {
//        _repo = repo;
//        _customerRepo = customerRepo;
//        _mapper = mapper;
//    }

//    public async Task<ProductDetailDto> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
//    {
//        var product = await _productRepository.GetAsync(p => p.Id == request.ProductId);
//        var supplier = await _customerRepo.GetAsync(c => c.Id == product.SupplierId);
//        var rst = new ProductDetailDto(product, supplier);
//        return rst;
//    }
//}
