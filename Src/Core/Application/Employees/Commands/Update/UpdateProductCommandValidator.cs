using FluentValidation;
using LoyWms.Application.Common.Interfaces.Repositories;

namespace LoyWms.Application.Customers.Commands.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{


    public UpdateCustomerCommandValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0).WithMessage("{PropertyName} Must Greater Than zero.");

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


