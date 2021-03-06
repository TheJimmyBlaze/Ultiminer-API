using System.Text.Json.Serialization;

namespace Models.Authentication {

    public class DiscordIdentity {

        [JsonPropertyName("id")]
        public string Id {get; set;}
        [JsonPropertyName("username")]
        public string Username {get; set;}
        [JsonPropertyName("discriminator")]
        public string Discriminator {get; set;}
        [JsonPropertyName("avatar")]
        public string AvatarHash {get; set;}
    }
}