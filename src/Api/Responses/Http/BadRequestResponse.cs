using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AluguelIdeal.Api.Controllers.Models.Responses.Http
{
    public class BadRequestResponse
    {
        [JsonPropertyName("errors")]
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
