
using System.Text.Json.Serialization;

namespace Models.Nodes {

    public class SelectNode {
        
        [JsonPropertyName("node_id")]
        public string NodeId {get; set;}
    }
}