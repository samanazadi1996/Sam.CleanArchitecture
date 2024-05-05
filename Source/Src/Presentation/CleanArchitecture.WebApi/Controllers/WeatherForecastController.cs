using CleanArchitecture.Application.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.WebApi.Controllers
    {
    [Authorize]
    [ApiController]
    //[Route("[controller]")]
    [Route("api/[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class WeatherForecastController(IAccountServices accountServices) : ControllerBase
        {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

      
        [HttpGet]
        public IActionResult Get()
            {
            if (Request != null && Request?.Headers != null && !Request.Headers.Authorization.IsNullOrEmpty())
                {
                var token = Request.Headers.Authorization.ToString().Split(' ')[1];//bcz "Bearer asdjadsjlassdohujiqwerljwerjlitokenStaysHere"
                //Console.WriteLine($"[{token}]");

                //type1
                var type1Check = accountServices.AuthenticateWithGoogle(token).Result;

                //type2
                var type2Check = accountServices.AuthenticateByJwtTokenOfGoogleType2(Request.Headers.Authorization).Result;//this is working with some tweak of key

                return Ok(type1Check);
                //await
                }
            return Ok("validated success");
            }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //    {
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //        })
        //    .ToArray();
        //    }
        }
    }
