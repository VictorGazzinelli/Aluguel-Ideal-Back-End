using System.Text.Json.Serialization;

namespace AluguelIdeal.Api.Models
{
    public class AdvertisementModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
