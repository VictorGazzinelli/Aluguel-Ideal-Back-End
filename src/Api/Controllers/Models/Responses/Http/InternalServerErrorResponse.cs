using System.Text.Json.Serialization;

namespace AluguelIdeal.Api.Controllers.Models.Responses.Http
{
    public class InternalServerErrorResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
