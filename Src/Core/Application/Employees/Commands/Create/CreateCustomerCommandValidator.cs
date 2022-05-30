using FluentValidation;
using LoyWms.Application.Common.Interfaces.Repositories;

namespace LoyWms.Application.Customers.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{


    public CreateCustomerCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.NickName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");
    }


}


