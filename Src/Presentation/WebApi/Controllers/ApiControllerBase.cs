using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoyWms.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator =>
        _mediator ??= this.HttpContext.RequestServices.GetRequiredService<ISender>();
}
