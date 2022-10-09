
using System.Text.Json.Serialization;

namespace Models.Experience {

    public class NewExperience : ExperienceTotal{

        [JsonPropertyName("new_experience")]
        public int NewExp {get; set;}
    }
}