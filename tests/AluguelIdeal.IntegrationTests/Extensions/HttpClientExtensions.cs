using AluguelIdeal.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AluguelIdeal.IntegrationTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpClient> UsingUserAsync(this HttpClient httpClient, IAuthService authService)
        {
            (string bearerToken, int _ ) = await authService.CreateBearerTokenAsync("user@mail.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: JwtBearerDefaults.AuthenticationScheme, bearerToken);

            return httpClient;
        }
    }
}
