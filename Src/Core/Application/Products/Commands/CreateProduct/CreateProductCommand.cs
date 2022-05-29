using AutoMapper;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Products.Dtos;
using LoyWms.Domain.Entities;
using MediatR;

namespace LoyWms.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Response<ProductDto>>
{

    //产品编号 唯一性
    public string ProductNo { get; set; }
    //产品名称
    public string Name { get; set; }
    //产品规格
    public string? Size { get; set; }
    //供应商Id
    public long SupplierId { get; set; }
    //当前供应价格
    public decimal Rate { get; set; }
    //产品说明
    public string? Description { get; set; }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDto>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(
            IProductRepositoryAsync productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductDto>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            var product = new Product()
            {
                ProductNo = command.ProductNo,
                Name = command.Name,
                Size = command.Size,
                SupplierId = command.SupplierId,
                Rate = command.Rate,
                Description = command.Description,
            };
            var rst = _mapper.Map<ProductDto>(await _productRepository.CreateAsync(product));
            return new Response<ProductDto>(rst);
        }
    }
}


