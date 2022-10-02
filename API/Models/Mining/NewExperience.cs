
using System.Text.Json.Serialization;

namespace Models.Mining {

    public class NewExperience {

        [JsonPropertyName("new_exp")]
        public int NewExp {get; set;}
        [JsonPropertyName("level")]
        public int Level {get; set;}
        [JsonPropertyName("experience")]
        public int Experience {get; set;}
        [JsonPropertyName("next_level_experience")]
        public int NextLevelExperience {get; set;}
    }
}