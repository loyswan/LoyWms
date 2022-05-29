using LoyWms.Application.WeatherForecasts.Dtos;
using LoyWms.Application.WeatherForecasts.Queries.GetWeaherForecasts;
using LoyWms.Application.Common.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LoyWms.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WeatherForecastController : ApiControllerBase
{


    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<Response<IEnumerable<WeatherForecastDto>>> Get()
    {
        var cmd = new GetWeatherForecastsQuery();
        return await Mediator.Send(cmd);
    }
}
