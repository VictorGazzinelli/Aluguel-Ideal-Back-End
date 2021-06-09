using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task WriteJsonResponseAsync(this HttpContext context, ErrorResponse errorResponse)
        {
            context.Response.StatusCode = errorResponse.StatusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            byte[] responseBody = JsonSerializer.SerializeToUtf8Bytes(errorResponse, typeof(ErrorResponse));
            await context.Response.Body.WriteAsync(responseBody, 0, responseBody.Length);
        }
    }
}
