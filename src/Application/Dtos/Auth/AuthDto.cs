using System.Text.Json.Serialization;

namespace AluguelIdeal.Application.Dtos.Auth
{
    public class AuthDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

    }
}
