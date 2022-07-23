
namespace Database.Models {

    public class DropTableResource {

        public string DropTableId {get; set;}
        public string ResourceId {get; set;}

        public int Rarity {get; set;}

        public DropTable DropTable {get; set;}
        public Resource Resource {get; set;}
    }
}