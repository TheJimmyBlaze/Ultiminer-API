
using System.Text.Json.Serialization;

namespace Models.Mining {

    public class NewResources {

        [JsonPropertyName("new_resources")]
        public List<ResourceStack> NewResource {get; set;}
        [JsonPropertyName("total_resources")]
        public List<ResourceStack> TotalResources {get; set;}
    }
}