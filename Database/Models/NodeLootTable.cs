
namespace Database.Models {

    public class NodeLootTable {

        public string NodeId {get; set;}
        public string LootTableId {get; set;}

        public int TableRarity {get; set;}
        public int MinRarity {get; set;}
        public int MaxRarity {get; set;}

        public Node Node {get; set;}
        public LootTable LootTable {get; set;}
    }
}