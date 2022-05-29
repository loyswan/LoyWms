using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoyWms.Application.WeatherForecasts.Dtos;
using LoyWms.Application.Common.Wrappers;
using MediatR;

namespace LoyWms.Application.WeatherForecasts.Queries.GetWeaherForecasts;

public class GetWeatherForecastsQuery
    : IRequest<Response<IEnumerable<WeatherForecastDto>>>
{
}

public class GetWeatherForecastsQueryHandler
    : IRequestHandler<GetWeatherForecastsQuery, Response<IEnumerable<WeatherForecastDto>>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", 
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public GetWeatherForecastsQueryHandler()
    {
    }

    public Task<Response<IEnumerable<WeatherForecastDto>>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        var vm = Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });

        var res = new Response<IEnumerable<WeatherForecastDto>>(vm);

        return Task.FromResult(res);
    }
}
