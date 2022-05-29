using AutoMapper;
using LoyWms.Application.Common.Exceptions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Response<Product>>
{
    public long Id { get; set; }
    //产品名称
    public string Name { get; set; }
    //产品规格
    public string? Size { get; set; }
    //供应商Id
    public long SupplierId { get; set; }
    //当前供应价格
    public decimal Rate { get; private set; }
    //产品说明
    public string? Description { get; set; }


    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<Product>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        public UpdateProductCommandHandler(
            IProductRepositoryAsync productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetAsQueryable(p => p.Id == request.Id).FirstOrDefault();
            if (product == null)
            {
                throw new ApiException($"Product Not Found.");
            }

            product.Name = request.Name;
            product.Size = request.Size;
            product.SupplierId = request.SupplierId;
            product.Rate = request.Rate;
            product.Description = request.Description;

            await _productRepository.UpdateAsync(product);
            return new Response<Product>(product);
        }
    }
}


