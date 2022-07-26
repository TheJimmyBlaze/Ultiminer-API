
using System.ComponentModel.DataAnnotations;

namespace Database.Models {

    public class LootTable {

        [Key]
        public string NaturalId {get; set;}
        public string DisplayName {get; set;}

        public List<LootTableResource> Resources {get; set;}
    }
}