
using System.Text.Json.Serialization;

namespace Models.Mining {

    public class MiningResult {

        [JsonPropertyName("resources")]
        public NewResources Resources {get; set;}
        [JsonPropertyName("exp")]
        public NewExperience Exp {get; set;}
        [JsonPropertyName("next_mine")]
        public DateTime NextMine {get; set;}
    }
}