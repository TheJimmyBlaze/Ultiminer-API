
using System.Text.Json.Serialization;

namespace Models.Resources {

    public class ResourceStack {

        [JsonPropertyName("resource_id")]
        public string ResourceId {get; set;}
        [JsonPropertyName("count")]
        public int Count {get; set;}
    }
}