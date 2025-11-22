
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class GithubAuthenticationResponse
    {
        [JsonPropertyName("access_token")]
        public required string Access_Token { get; set; }

        [JsonPropertyName("scope")]
        public required string Scope { get; set; }

        [JsonPropertyName("token_type")]
        public required string Token_Type { get; set; }
    }
}