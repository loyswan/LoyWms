using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Products.Commands.CreateProduct;
using LoyWms.Application.Products.Commands.DeleteProduct;
using LoyWms.Application.Products.Commands.UpdateProduct;
using LoyWms.Application.Products.Dtos;
using LoyWms.Application.Products.Queries.GetProdcutDetail;
using LoyWms.Application.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoyWms.WebApi.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ProductController : ApiControllerBase
    {


        [HttpGet]
        public async Task<Response<IEnumerable<ProductDto>>> Get(
            [FromQuery] int pagenumber = 1,
            [FromQuery] int pagesize = 10)
        {
            var query = new GetProductsWithPaginationQuery() { PageNumber = pagenumber, PageSize = pagesize };
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<Response<ProductDetailDto>> Get(long id)
        {
            var query = new GetProductDetailQuery() { ProductId = id };
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Authorize]
        public async Task<Response<ProductDto>> Post(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Authorize]
        public async Task<Response<ProductDto>> Put(UpdateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<Response<long>> Delete(long id)
        {
            var command = new DeleteProductCommand() { Id = id };
            return await Mediator.Send(command);
        }
    }
}
