
using System.Text.Json.Serialization;

namespace Api.Models
{

    public class GithubUser
    {
        [JsonPropertyName("email")]
        public required string Email { get; set; }

        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        [JsonPropertyName("verified")]
        public bool Verified { get; set; }

        [JsonPropertyName("visibility")]
        public required string Visibility { get; set; }
    }
}