
using System.Text.Json.Serialization;

namespace Models.Mining {

    public class NewExperience {

        [JsonPropertyName("new_exp")]
        public int NewExp {get; set;}
        [JsonPropertyName("total_exp")]
        public int TotalExp {get; set;}
    }
}