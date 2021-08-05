using AluguelIdeal.Application.Enums;
using AluguelIdeal.Application.Services;
using Ardalis.SmartEnum.JsonNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.IntegrationTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpClient> AddProfileAsync(this HttpClient httpClient, string userEmail, IAuthService authService)
        {
            (string bearerToken, int _) = await authService.CreateBearerTokenAsync(userEmail);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: JwtBearerDefaults.AuthenticationScheme, bearerToken);

            return httpClient;
        }

        private static string BuildAsQueryString(object parameters) =>
            string.Join("&", parameters.GetType().GetProperties().Select(x => x.Name + "=" + x.GetValue(parameters, null)).ToArray());

        private static HttpContent BuildAsHttpContent(object parameters)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new SmartEnumValueConverter<ResidenceType, int>());

            return parameters != null ?
                new StringContent(JsonConvert.SerializeObject(parameters, settings), Encoding.UTF8, MediaTypeNames.Application.Json):
                new StringContent(string.Empty, Encoding.UTF8, MediaTypeNames.Application.Json);
        }
        public static async Task<HttpResponseMessage> DoGetRequest(this HttpClient httpClient, string requestUri, object parameters = null, CancellationToken cancellationToken = default)
        {
            if(parameters != null)
            {
                string queryParams = BuildAsQueryString(parameters);
                requestUri += $"?{queryParams}";
            }

            return await httpClient.GetAsync(requestUri, cancellationToken);
        }

        public static async Task<HttpResponseMessage> DoDeleteRequest(this HttpClient httpClient, string requestUri, object parameters = null, CancellationToken cancellationToken = default)
        {
            if (parameters != null)
            {
                string queryParams = BuildAsQueryString(parameters);
                requestUri += $"?{queryParams}";
            }

            return await httpClient.DeleteAsync(requestUri, cancellationToken);
        }

        public static async Task<HttpResponseMessage> DoPostRequest(this HttpClient httpClient, string requestUri, object parameters = null, CancellationToken cancellationToken = default)
        {
            HttpContent httpConent = BuildAsHttpContent(parameters);

            return await httpClient.PostAsync(requestUri, httpConent, cancellationToken);
        }

        public static async Task<HttpResponseMessage> DoPutRequest(this HttpClient httpClient, string requestUri, object parameters = null, CancellationToken cancellationToken = default)
        {
            HttpContent httpConent = BuildAsHttpContent(parameters);

            return await httpClient.PutAsync(requestUri, httpConent, cancellationToken);
        }
    }
}
