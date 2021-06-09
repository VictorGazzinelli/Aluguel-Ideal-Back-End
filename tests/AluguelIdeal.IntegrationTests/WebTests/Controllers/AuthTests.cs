using AluguelIdeal.Api.Options.Jwt;
using AluguelIdeal.Application.Dtos.Auth;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.IntegrationTests.WebTests.Controllers
{
    public class AuthTests : IntegrationTestBase
    {
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly TokenValidationParameters validationParameters;
        public AuthTests(CustomWebApplicationFactory fixture) : base(fixture)
        {
            this.tokenHandler = new JwtSecurityTokenHandler();
            this.validationParameters = new CustomJwtTokenValidationParameters(configuration.GetSection("Secret").Value);
        }

        public async Task<HttpResponseMessage> DoAuthPostRequestAsync(object parameters)
        {
            string[] pairs = parameters.GetType().GetProperties().Select(x => x.Name + "=" + x.GetValue(parameters, null)).ToArray();
            string queryParams = string.Join("&", pairs);
            string requestUri = "/api/Auth?" + queryParams;
            HttpClient client = fixture.CreateClient();

            return await client.PostAsync(requestUri, new StringContent(string.Empty, Encoding.UTF8, MediaTypeNames.Application.Json));
        }

        public async Task<T> DoAuthPostDeserializedRequestAsync<T>(object parameters)
        {
            JsonSerializerOptions jsonSerializerOptionsIgnoreCase = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            HttpResponseMessage httpResponseMessage = await DoAuthPostRequestAsync(parameters);
            string jsonPayload = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(jsonPayload, jsonSerializerOptionsIgnoreCase);
        }

        [Theory(DisplayName = "given valid credentials for User, when on Post Request, should return valid bearer token")]
        [InlineData("admin@mail.com", "123456")]
        [InlineData("landlord@mail.com", "123456")]
        [InlineData("user@mail.com", "123456")]
        public async Task ValidUserCredentials_Post_ValidBearerToken(string username, string password)
        {
            // Arrange
            object parameters = new
            {
                grant_type = "password",
                client_id = "client_id",
                username,
                password
            };

            // Act
            AuthDto result = await DoAuthPostDeserializedRequestAsync<AuthDto>(parameters);
            Action validation = () => tokenHandler.ValidateToken(result.AccessToken, validationParameters, out _);

            // Assert
            validation.Should().NotThrow();
            result.ExpiresIn.Should().Be(8 * 3600);
        }

        [Theory(DisplayName = "given invalid credentials for User, when on Post Request, should return invalid request error")]
        [InlineData("password", "client_id", "admin@mail.com.br", "123456")]
        [InlineData("password", "client_id", "landlord@mail.com", "1234567")]
        [InlineData("password", "client_id", "user@mail", "123456")]
        [InlineData("code", "client_id", "user@mail.com", "123456")]
        [InlineData("password", "other_client_id", "user@mail.com", "123456")]
        public async Task InvalidValidUserCredentials_Post_InvalidRequest(string grant_type, string client_id, string username, string password)
        {
            // Arrange
            object parameters = new
            {
                grant_type,
                client_id,
                username,
                password
            };

            // Act
            HttpResponseMessage httpResponseMessage = await DoAuthPostRequestAsync(parameters);
            string jsonPayload = await httpResponseMessage.Content.ReadAsStringAsync();
            JsonElement result = JsonSerializer.Deserialize<JsonElement>(jsonPayload);

            // Assert
            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.GetProperty("error").GetString().Should().Be("invalid_request");
        }
    }
}
