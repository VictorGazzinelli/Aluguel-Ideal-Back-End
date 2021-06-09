using AluguelIdeal.Application.Services;
using AluguelIdeal.IntegrationTests.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;
using Xunit.Priority;

namespace AluguelIdeal.IntegrationTests
{
    [Collection("CustomWebApplicationFactory collection")]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [DefaultPriority(0)]
    public abstract class IntegrationTestBase
    {
        protected CustomWebApplicationFactory fixture;
        protected IConfiguration configuration;
        protected IAuthService authService;
        protected IntegrationTestBase(CustomWebApplicationFactory fixture)
        {
            this.fixture = fixture;
            configuration = (IConfiguration)fixture.Services.GetService(typeof(IConfiguration));
            authService = (IAuthService)fixture.Services.GetService(typeof(IAuthService));
        }

        protected async Task<(HttpStatusCode statusCode, JsonElement jsonResponse)> DoPostRequest(string requestUri, CancellationToken cancellationToken = default,  object parameters = null, string userEmail = null) =>
            await DoRequest(HttpMethod.Post, requestUri, cancellationToken, parameters, userEmail);

        protected async Task<(HttpStatusCode statusCode, T response)> DoPostRequest<T>(string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null) =>
            await DoRequest<T>(HttpMethod.Post, requestUri, cancellationToken, parameters, userEmail);

        protected async Task<(HttpStatusCode statusCode, JsonElement jsonResponse)> DoGetRequest(string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null) =>
            await DoRequest(HttpMethod.Get, requestUri, cancellationToken, parameters, userEmail);

        protected async Task<(HttpStatusCode statusCode, T response)> DoGetRequest<T>(string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null) =>
            await DoRequest<T>(HttpMethod.Get, requestUri, cancellationToken, parameters, userEmail);

        protected async Task<(HttpStatusCode statusCode, JsonElement jsonResponse)> DoPutRequest(string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null) =>
            await DoRequest(HttpMethod.Put, requestUri, cancellationToken, parameters, userEmail);

        protected async Task<(HttpStatusCode statusCode, T response)> DoPutRequest<T>(string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null) =>
            await DoRequest<T>(HttpMethod.Put, requestUri, cancellationToken, parameters, userEmail);

        protected async Task<(HttpStatusCode statusCode, JsonElement jsonResponse)> DoDeleteRequest(string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null) =>
            await DoRequest(HttpMethod.Delete, requestUri, cancellationToken, parameters, userEmail);

        protected async Task<(HttpStatusCode statusCode, T response)> DoDeleteRequest<T>(string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null) =>
            await DoRequest<T>(HttpMethod.Delete, requestUri, cancellationToken, parameters, userEmail);

        protected async Task<(HttpStatusCode statusCode, JsonElement jsonResponse)> DoRequest(HttpMethod httpMethod, string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null)
        {
            HttpClient client = fixture.CreateClient();

            if (userEmail != null)
                client = await client.AddProfileAsync(userEmail, authService);

            HttpResponseMessage httpResponse = httpMethod switch
            {
                HttpMethod m when m == HttpMethod.Post => await client.DoPostRequest(requestUri, parameters, cancellationToken),
                HttpMethod m when m == HttpMethod.Put => await client.DoPutRequest(requestUri, parameters, cancellationToken),
                HttpMethod m when m == HttpMethod.Delete => await client.DoDeleteRequest(requestUri, parameters, cancellationToken),
                HttpMethod m when m == HttpMethod.Get => await client.DoGetRequest(requestUri, parameters, cancellationToken),
                _ => throw new NotImplementedException(),
            };

            JsonElement jsonResponse = new JsonElement();
            string jsonPayload = await httpResponse.Content.ReadAsStringAsync();
            if (!jsonPayload.Equals(string.Empty))
            {
                JsonSerializerOptions jsonSerializerOptionsIgnoreCase = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                jsonResponse = JsonSerializer.Deserialize<JsonElement>(jsonPayload, jsonSerializerOptionsIgnoreCase);
            }

            return (httpResponse.StatusCode, jsonResponse);
        }

        protected async Task<(HttpStatusCode statusCode, T response)> DoRequest<T>(HttpMethod httpMethod, string requestUri, CancellationToken cancellationToken = default, object parameters = null, string userEmail = null)
        {
            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                HttpClient client = fixture.CreateClient();

                if (userEmail != null)
                    client = await client.AddProfileAsync(userEmail, authService);

                HttpResponseMessage httpResponse = httpMethod switch
                {
                    HttpMethod m when m == HttpMethod.Post => await client.DoPostRequest(requestUri, parameters, cancellationToken),
                    HttpMethod m when m == HttpMethod.Put => await client.DoPutRequest(requestUri, parameters, cancellationToken),
                    HttpMethod m when m == HttpMethod.Delete => await client.DoDeleteRequest(requestUri, parameters, cancellationToken),
                    HttpMethod m when m == HttpMethod.Get => await client.DoGetRequest(requestUri, parameters, cancellationToken),
                    _ => throw new NotImplementedException(),
                };

                string jsonPayload = await httpResponse.Content.ReadAsStringAsync();
                JsonSerializerOptions jsonSerializerOptionsIgnoreCase = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                T response = JsonSerializer.Deserialize<T>(jsonPayload, jsonSerializerOptionsIgnoreCase);

                return (httpResponse.StatusCode, response);
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}
