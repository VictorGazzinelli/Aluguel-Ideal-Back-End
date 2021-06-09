using AluguelIdeal.Application.Dtos.Districts;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Infrastructure.Database.Migrations.Testing;
using FluentAssertions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.IntegrationTests.WebTests.Controllers
{
    public class DistrictsTests : IntegrationTestBase
    {
        private static string _requestUri = "api/districts";

        public DistrictsTests(CustomWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Fact(DisplayName = "GET api/districts should return all Districts in database")]
        public async Task Should_ReturnAllDistricts()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            QueryResult<DistrictDto> expectedResult = new QueryResult<DistrictDto>()
            {
                Items = SeedForTestingMaintenance.Districts.Select(district => new DistrictDto(district))
            };

            // Act
            (HttpStatusCode obtainedStatusCode, QueryResult<DistrictDto> obtainedResult) = await DoGetRequest<QueryResult<DistrictDto>>(_requestUri);

            // Assert
            obtainedResult.Should().Be(expectedResult);
            obtainedStatusCode.Should().Be(expectedStatusCode);
        }
    }
}
