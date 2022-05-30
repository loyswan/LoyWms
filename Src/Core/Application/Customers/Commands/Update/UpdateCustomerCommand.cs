using AutoMapper;
using LoyWms.Application.Common.Exceptions;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Customers.Dtos;
using LoyWms.Domain.Entities;
using MediatR;

namespace LoyWms.Application.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<Response<CustomerDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string NickName { get; set; }
    public string? Description { get; set; }


    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<CustomerDto>>
    {
        private readonly ICustomerRepositoryAsync _repository;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandHandler(
            ICustomerRepositoryAsync repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetAsync(c => c.Id == request.Id);
            if (customer == null)
            {
                throw new ApiException($"Customer Not Found.");
            }

            customer.Name = request.Name;
            customer.NickName = request.NickName;
            customer.Description = request.Description;

            await _repository.UpdateAsync(customer);
            return new Response<CustomerDto>(_mapper.Map<Customer, CustomerDto>(customer));
        }
    }
}


