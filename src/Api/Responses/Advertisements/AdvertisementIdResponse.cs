using System.Text.Json.Serialization;

namespace AluguelIdeal.Api.Responses.Advertisements
{
    public class AdvertisementIdResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
