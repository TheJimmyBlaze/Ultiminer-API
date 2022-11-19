
using System.Text.Json.Serialization;

namespace Models.Nodes {

    public class UnlockedNodes {

        [JsonPropertyName("unlocked_nodes")]
        public List<UserNode> Unlocked {get; set;}
        [JsonPropertyName("next_unlock")]
        public UserNode NextUnlock {get; set;}
    }
}