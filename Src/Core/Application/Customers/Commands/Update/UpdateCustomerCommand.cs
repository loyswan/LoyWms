using LoyWms.Application.Common.Exceptions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Domain.Entities;
using MediatR;

namespace LoyWms.Application.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<Response<Customer>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string NickName { get; set; }
    public string? Description { get; set; }


    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<Customer>>
    {
        private readonly ICustomerRepositoryAsync _repository;
        public UpdateCustomerCommandHandler(
            ICustomerRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<Response<Customer>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _repository.GetAsQueryable(p => p.Id == request.Id).FirstOrDefault();
            if (customer == null)
            {
                throw new ApiException($"Customer Not Found.");
            }

            customer.Name = request.Name;
            customer.NickName = request.NickName;
            customer.Description = request.Description;

            await _repository.UpdateAsync(customer);
            return new Response<Customer>(customer);
        }
    }
}


