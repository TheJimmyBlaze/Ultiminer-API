
using System.ComponentModel.DataAnnotations;

namespace Database.Models {

    public class Node {

        [Key]
        public string NaturalId {get; set;}
        public string DisplayName {get; set;}

        public int Quantity {get; set;}

        public List<NodeDropTable> DropTables {get; set;}
    }
}