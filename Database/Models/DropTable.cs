
using System.ComponentModel.DataAnnotations;

namespace Database.Models {

    public class DropTable {

        [Key]
        public string NaturalId {get; set;}
        public string DisplayName {get; set;}

        public List<DropTableResource> Resources {get; set;}
    }
}