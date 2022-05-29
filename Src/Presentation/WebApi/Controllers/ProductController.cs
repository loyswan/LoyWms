using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Products.Commands.CreateProduct;
using LoyWms.Application.Products.Dtos;
using LoyWms.Application.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace LoyWms.WebApi.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ProductController : ApiControllerBase
    {


        [HttpGet(Name = "GetProductsWithPagination")]
        public async Task<Response<IEnumerable<ProductDto>>> Get(
            [FromQuery] int pagenumber = 1,
            [FromQuery] int pagesize = 10)
        {
            var query = new GetProductsWithPaginationQuery() { PageNumber = pagenumber, PageSize = pagesize };
            return await Mediator.Send(query);
        }

        //[HttpGet("{id}")]
        //public async Task<Response<ProductDto>> Get(long id)
        //{
        //    //var query = new GetProductDetailQuery() { id = id };
        //    //return await Mediator.Send(query);
        //}

        // POST api/<controller>
        [HttpPost]
        //[Authorize]
        public async Task<Response<ProductDto>> Post(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
