
using System.ComponentModel.DataAnnotations;

namespace Database.Models {

    public class Resource {

        [Key]
        public string NaturalId {get; set;}
        public string DisplayName {get; set;}

        public int ExperienceAwarded {get; set;}
    }
}