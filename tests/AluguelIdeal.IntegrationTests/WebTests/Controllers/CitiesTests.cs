using AluguelIdeal.Application.Dtos.Cities;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Infrastructure.Database.Migrations.Testing;
using FluentAssertions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.IntegrationTests.WebTests.Controllers
{
    public class CitiesTests : IntegrationTestBase
    {
        private readonly string _requestUri = "api/cities";

        public CitiesTests(CustomWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Fact (DisplayName = "GET api/cities should return all cities in database")]
        public async Task Should_ReturnAllCities()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            QueryResult<CityDto> expectedResult = new QueryResult<CityDto>()
            {
                Items = SeedForTestingMaintenance.Cities.Select(city => new CityDto(city))
            };

            // Act
            (HttpStatusCode obtainedStatusCode, QueryResult<CityDto> obtainedResult) = await DoGetRequest<QueryResult<CityDto>>(_requestUri);

            // Assert
            obtainedResult.Should().Be(expectedResult);
            obtainedStatusCode.Should().Be(expectedStatusCode);
        }
    }
}
