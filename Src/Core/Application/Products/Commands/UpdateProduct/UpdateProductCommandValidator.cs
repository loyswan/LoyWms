using FluentValidation;
using LoyWms.Application.Common.Interfaces.Repositories;

namespace LoyWms.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IProductRepositoryAsync _productRepository;

    public UpdateProductCommandValidator(IProductRepositoryAsync productRepository)
    {
        _productRepository = productRepository;
        RuleFor(p => p.Id)
            .GreaterThan(0).WithMessage("{PropertyName} Must Greater Than zero.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Size)
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
    }

}


