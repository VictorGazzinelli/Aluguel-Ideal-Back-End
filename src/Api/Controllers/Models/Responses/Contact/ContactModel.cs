using System.Text.Json.Serialization;

namespace AluguelIdeal.Api.Controllers.Models.Contact
{
    public class ContactModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
