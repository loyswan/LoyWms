using AutoMapper;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Customers.Dtos;
using LoyWms.Domain.Entities;
using MediatR;

namespace LoyWms.Application.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<Response<CustomerDto>>
{
    public string Name { get; set; }
    public string NickName { get; set; }
    public string? Description { get; set; }



    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Response<CustomerDto>>
    {
        private readonly ICustomerRepositoryAsync _customerRepository;
        private readonly IMapper _mapper;
        public CreateCustomerCommandHandler(
            ICustomerRepositoryAsync customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response<CustomerDto>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer()
            {
                Name = command.Name,
                NickName = command.NickName,
                Description = command.Description,
            };
            var rst = _mapper.Map<CustomerDto>(await _customerRepository.CreateAsync(customer));
            return new Response<CustomerDto>(rst);
        }
    }
}


