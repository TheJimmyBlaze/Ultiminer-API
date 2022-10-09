
using System.Text.Json.Serialization;

namespace Models.Experience {

    public class ExperienceTotal {

        [JsonPropertyName("level")]
        public int Level {get; set;}
        [JsonPropertyName("experience")]
        public int Experience {get; set;}
        [JsonPropertyName("next_level_experience")]
        public int NextLevelExperience {get; set;}
    }
}