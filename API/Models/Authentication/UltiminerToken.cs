using System.Text.Json.Serialization;

namespace Models.Authentication {

    public class UltiminerToken {

        [JsonPropertyName("access_token")]
        public string AccessToken {get; set;}
        [JsonPropertyName("expires_in")]
        public int ExpiresIn {get; set;}
        [JsonPropertyName("token_type")]
        public string TokenType {get; set;}
    }
}