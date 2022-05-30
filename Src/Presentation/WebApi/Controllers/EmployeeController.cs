using LoyWms.Application.Common.Wrappers;
using LoyWms.Application.Employees.Dtos;
using LoyWms.Application.Employees.Queries.GetEmployee;
using Microsoft.AspNetCore.Mvc;

namespace LoyWms.WebApi.Controllers
{
    [ApiController]
    public class EmployeeController :  ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<Response<EmployeeDetailDto>> Get(long id)
        {
            var query = new GetEmployeeDetailQuery() { Id = id };
            return await Mediator.Send(query);
        }
    }
}
