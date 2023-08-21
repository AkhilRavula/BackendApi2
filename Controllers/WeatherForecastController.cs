using System.Collections;
using BackendApi2.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi2.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

   private readonly ILoggerManager _logger;
   private readonly IRepositoryWrapper _repositoryWrapper;
    public WeatherForecastController( ILoggerManager logger,IRepositoryWrapper repositoryWrapper )
    {
        _logger = logger;
        _repositoryWrapper = repositoryWrapper;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogDebug("lollipop");

        IEnumerable employees =_repositoryWrapper.employee.FindALLwithRelatedEntities("Skills").ToList();
       // IEnumerable skills = _repositoryWrapper.skill.FindALLwithRelatedEntities("Emp").ToList();
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
