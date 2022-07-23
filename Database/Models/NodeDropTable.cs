
namespace Database.Models {

    public class NodeDropTable {

        public string NodeId {get; set;}
        public string DropTableId {get; set;}

        public int TableRarity {get; set;}
        public int MinDropRarity {get; set;}
        public int MaxDropRarity {get; set;}

        public Node Node {get; set;}
        public DropTable DropTable {get; set;}
    }
}