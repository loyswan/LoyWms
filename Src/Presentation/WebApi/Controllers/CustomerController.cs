using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Customers.Commands.Create;
using LoyWms.Application.Customers.Commands.Delete;
using LoyWms.Application.Customers.Commands.Update;
using LoyWms.Application.Customers.Dtos;
using LoyWms.Application.Customers.Queries.GetCustomer;
using LoyWms.Application.Customers.Queries.GetCustomers;
using LoyWms.Application.Customers.Queries.GetCustomersWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoyWms.WebApi.Controllers
{

    [ApiController]
    //[Route("[controller]")]
    public class CustomerController : ApiControllerBase
    {

        [HttpGet]
        public async Task<Response<IEnumerable<CustomerDto>>> Get(
            [FromQuery] int? pagenumber,
            [FromQuery] int? pagesize)
        {
            if (!pagenumber.HasValue && !pagesize.HasValue) //无参数时，显示全部
            {
                var queryall = new GetCustomersQuery() { };
                return await Mediator.Send(queryall);
            }
            else //有参数时，分页
            {
                pagenumber = pagenumber.HasValue ? pagenumber : 1;
                pagesize = pagesize.HasValue ? pagesize : 10;

                var query = new GetCustomersWithPaginationQuery() { PageNumber = pagenumber.Value, PageSize = pagesize.Value };
                return await Mediator.Send(query);
            }
        }

        [HttpGet("{id}")]
        public async Task<Response<CustomerDto>> Get(long id)
        {
            var query = new GetCustomerByIdQuery() { Id = id };
            return await Mediator.Send(query);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<Response<CustomerDto>> Post(CreateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Authorize]
        public async Task<Response<CustomerDto>> Put(UpdateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<Response<long>> Delete(long id)
        {
            var command = new DeleteCustomerCommand() { Id = id };
            return await Mediator.Send(command);
        }
    }
}
