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

namespace LoyWms.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Response<long>>
{
    public long Id { get; set; }


    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<long>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        public DeleteProductCommandHandler(
            IProductRepositoryAsync productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Response<long>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetAsQueryable(p => p.Id == command.Id).FirstOrDefault();
            if (product == null)
            {
                throw new ApiException($"Product Not Found.");
            }

            await _productRepository.DeleteAsync(product);
            return new Response<long>(product.Id);
        }
    }
}


