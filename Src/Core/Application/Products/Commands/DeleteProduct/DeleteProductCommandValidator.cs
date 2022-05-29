using FluentValidation;
using LoyWms.Application.Common.Interfaces.Repositories;

namespace LoyWms.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly IProductRepositoryAsync _productRepository;

    public DeleteProductCommandValidator(IProductRepositoryAsync productRepository)
    {
        _productRepository = productRepository;
        RuleFor(p => p.Id)
            .GreaterThan(0).WithMessage("{PropertyName} Must Greater Than zero.");

    }

}


