
using System.Text.Json.Serialization;

namespace Models.Resources {

    public class ResourceTotal {

        [JsonPropertyName("total_resources")]
        public List<ResourceStack> TotalResources {get; set;}
    }
}