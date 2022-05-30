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

namespace LoyWms.Application.Customers.Commands.Delete;

public class DeleteCustomerCommand : IRequest<Response<long>>
{
    public long Id { get; set; }


    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<long>>
    {
        private readonly ICustomerRepositoryAsync _repository;
        public DeleteCustomerCommandHandler(
            ICustomerRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<Response<long>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = _repository.GetAsQueryable(p => p.Id == command.Id).FirstOrDefault();
            if (customer == null)
            {
                throw new ApiException($"Customer Not Found.");
            }

            await _repository.DeleteAsync(customer);
            return new Response<long>(customer.Id);
        }
    }
}


