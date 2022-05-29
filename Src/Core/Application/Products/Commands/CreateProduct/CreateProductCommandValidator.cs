using FluentValidation;
using LoyWms.Application.Common.Interfaces.Repositories;

namespace LoyWms.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepositoryAsync _productRepository;

    public CreateProductCommandValidator(IProductRepositoryAsync productRepository)
    {
        _productRepository = productRepository;

        RuleFor(p => p.ProductNo)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.")
            .MustAsync(IsUniqueProductNo).WithMessage("{PropertyName} already exists.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Size)
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Rate)
            .GreaterThanOrEqualTo(0m).WithMessage("{PropertyName} must be no less than zero.");
    }

    private async Task<bool> IsUniqueProductNo(string no, CancellationToken cancellationToken)
    {
        return await _productRepository.IsUniqueProductNoAsync(no);
    }
}


