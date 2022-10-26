
using System.ComponentModel.DataAnnotations;

namespace Database.Models {

    public class ResourceType {

        [Key]
        public string NaturalId {get; set;}
    }
}