using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Customers.Commands.Create;
using LoyWms.Application.Customers.Dtos;
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
            [FromQuery] int pagenumber,
            [FromQuery] int pagesize)
        {
            if (pagenumber == 0 && pagesize == 0) //无参数时，显示全部
            {
                var queryall = new GetCustomersQuery() { };
                return await Mediator.Send(queryall);
            }
            else //有参数时，分页
            {
                pagenumber = pagenumber == 0 ? 1 : pagenumber;
                pagesize = pagesize == 0 ? 10: pagesize;

                var query = new GetCustomersWithPaginationQuery() { PageNumber = pagenumber, PageSize = pagesize };
                return await Mediator.Send(query);
            }
        }

        //[HttpGet("GetAll")]
        //public async Task<Response<IEnumerable<CustomerDto>>> GetAll()
        //{
        //    var query = new GetCustomersQuery() { };
        //    return await Mediator.Send(query);
        //}
        //[HttpGet("{id}")]
        //public async Task<Response<ProductDto>> Get(long id)
        //{
        //    //var query = new GetProductDetailQuery() { id = id };
        //    //return await Mediator.Send(query);
        //}

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<Response<CustomerDto>> Post(CreateCustomerCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
