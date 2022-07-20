using System.Text.Json.Serialization;

namespace Models {

    public class UltiminerToken {

        [JsonPropertyName("access_token")]
        public string AccessToken {get; set;}
    }
}