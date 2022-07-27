using System.Text.Json.Serialization;

namespace Models.Authentication {

    public class DiscordAuthCode {

        [JsonPropertyName("auth_code")]
        public string AuthCode {get; set;}
    }
}