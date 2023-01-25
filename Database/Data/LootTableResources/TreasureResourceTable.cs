
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.LootTableResources {

    public class TreasureResourceTable : IDataGenerator {

        private const string LOOT_TABLE_ID = LootTables.TREASURE; 

        //Loot chance decreases as rarity increases
        private readonly List<LootTableResource> loot = new(){
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Treasures.ROD_WOODEN,
                Rarity = 10
            },
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Treasures.BINDING_LINEN,
                Rarity = 15
            },
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Treasures.TABLET_STONE,
                Rarity = 25
            },
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Treasures.CUBE_BRASS,
                Rarity = 50
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<LootTableResource>().HasData(loot);
        }
    }
}