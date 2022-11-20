
using System.Text.Json.Serialization;

namespace Models.Nodes {

    public class UnlockedNodes {

        [JsonPropertyName("unlocked")]
        public List<DisplayNode> Unlocked {get; set;}
        [JsonPropertyName("next_unlock")]
        public DisplayNode? NextUnlock {get; set;}
    }
}