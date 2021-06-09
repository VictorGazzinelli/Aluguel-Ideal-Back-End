using AluguelIdeal.Application.Interactors.Profiles.Commands;
using AluguelIdeal.Infrastructure.Database.Migrations.Testing;
using FluentAssertions;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.IntegrationTests.WebTests.Controllers
{
    public class ProfilesTests : IntegrationTestBase
    {
        private readonly string _requestUri = "api/profiles";

        public ProfilesTests(CustomWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Fact(DisplayName = "POST api/profiles should bind user to role")]
        public async Task Should_BindUserToRole()
        {
            // Assign
            BindProfileCommand command = new BindProfileCommand()
            {
                UserId = SeedForTestingMaintenance.Users.First(user => user.Email.Equals("user@mail.com")).Id,
                RoleId = SeedForTestingMaintenance.Roles.First(role => role.Name.Equals("Landlord")).Id,
            };
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;

            // Act
            (HttpStatusCode obtainedStatusCode, JsonElement _) = await DoPostRequest(_requestUri, userEmail: "admin@mail.com", parameters: command);

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
        }
    }
}
