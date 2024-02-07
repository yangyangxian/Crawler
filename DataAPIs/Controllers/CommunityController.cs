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
            IList<AdministrativeDistrict> districts = CommunityApplications.GetAdministrativeDistricts();

            List<Community> communities = new List<Community>();
            foreach (AdministrativeDistrict district in districts)
            {
                List<Community> communitiesByDistrict = await CommunityApplications.ReadCommunities(district.CommunityMainPageURL);

                communities = communities.Concat(communitiesByDistrict).ToList();
            }

            int updatedCount = CommunityApplications.AddOrUpdateCommunities(communities);

            return updatedCount;
        }
    }
}
