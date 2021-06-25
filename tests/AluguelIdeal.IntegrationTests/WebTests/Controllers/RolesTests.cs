using AluguelIdeal.Application.Dtos.Roles;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Infrastructure.Database.Migrations.Testing;
using FluentAssertions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.IntegrationTests.WebTests.Controllers
{
    public class RolesTests : IntegrationTestBase
    {
        private readonly string _requestUri = "api/roles";

        public RolesTests(CustomWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Fact (DisplayName = "GET api/roles should return all roles in database")]
        public async Task Should_ReturnAllRoles()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            QueryResult<RoleDto> expectedResult = new QueryResult<RoleDto>()
            {
                Items = SeedForTestingMaintenance.Roles.Select(role => new RoleDto(role))
            };

            // Act
            (HttpStatusCode obtainedStatusCode, QueryResult<RoleDto> obtainedResult) = await DoGetRequest<QueryResult<RoleDto>>(_requestUri, userEmail: "admin@mail.com");

            // Assert
            obtainedResult.Should().Be(expectedResult);
            obtainedStatusCode.Should().Be(expectedStatusCode);
        }
    }
}
