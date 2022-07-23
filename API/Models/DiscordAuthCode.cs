using System.Text.Json.Serialization;

namespace Models {

    public class DiscordAuthCode {

        [JsonPropertyName("auth_code")]
        public string AuthCode {get; set;}
    }
}