using FluentValidation;
using LoyWms.Application.Common.Interfaces.Repositories;

namespace LoyWms.Application.Customers.Commands.Delete;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    private readonly ICustomerRepositoryAsync _repository;

    public DeleteCustomerCommandValidator(ICustomerRepositoryAsync repository)
    {
        _repository = repository;
        RuleFor(p => p.Id)
            .GreaterThan(0).WithMessage("{PropertyName} Must Greater Than zero.");

    }

}


