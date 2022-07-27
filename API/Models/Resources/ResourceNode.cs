
using System.Text.Json.Serialization;

namespace Models.Resources {

    public class ResourceNode {
        
        [JsonPropertyName("node_id")]
        public string NodeId {get; set;}
    }
}