using System.Text.Json.Serialization;

namespace Models.Authentication {

    public class UltiminerIdentity {

        [JsonPropertyName("id")]
        public string Id {get; set;}
        [JsonPropertyName("username")]
        public string Username {get; set;}
    }
}