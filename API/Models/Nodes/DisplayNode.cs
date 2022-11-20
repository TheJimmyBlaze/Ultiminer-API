
using System.Text.Json.Serialization;

namespace Models.Nodes {

    public class DisplayNode {

        [JsonPropertyName("node_id")]
        public string NodeId {get; set;}
        [JsonPropertyName("display_name")]
        public string DisplayName {get; set;}
        [JsonPropertyName("level_required")]
        public int LevelRequired {get; set;}
    }
}