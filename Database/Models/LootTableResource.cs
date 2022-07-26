
namespace Database.Models {

    public class LootTableResource {

        public string LootTableId {get; set;}
        public string ResourceId {get; set;}

        public int Rarity {get; set;}

        public LootTable LootTable {get; set;}
        public Resource Resource {get; set;}
    }
}