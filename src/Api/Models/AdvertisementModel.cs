using System.Text.Json.Serialization;

namespace AluguelIdeal.Api.Controllers.Models.Advertisement
{
    public class AdvertisementModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
