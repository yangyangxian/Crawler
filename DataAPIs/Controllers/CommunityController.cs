using Microsoft.AspNetCore.Mvc;
using Yang.Entities;
using Yang.SpiderApplication.Seashell;

namespace DataAPIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunityController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CommunityController> _logger;
        private CommunityApplications CommunityApplications { get; set; } = new CommunityApplications();

        public CommunityController()
        {
        }

        [HttpGet(Name = "GetCommunity")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(Name = "RefreshCommunityData")]
        public async Task<int> RefreshCommunityData()
        {
            int updatedCount = await CommunityApplications.GetAndSaveCommunityInfo();

            return updatedCount;
        }
    }
}
