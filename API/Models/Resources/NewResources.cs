
using System.Text.Json.Serialization;

namespace Models.Resources {

    public class NewResources : ResourceTotal {

        [JsonPropertyName("new_resources")]
        public List<ResourceStack> NewResource {get; set;}
    }
}