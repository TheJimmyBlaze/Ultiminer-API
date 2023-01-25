
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.LootTableResources {

    public class OreResourceTable : IDataGenerator {

        private const string LOOT_TABLE_ID = LootTables.ORE;

        //Loot chance decreases as rarity increases
        private readonly List<LootTableResource> loot = new(){
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Ores.ORE_COAL,
                Rarity = 10
            },
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Ores.ORE_TIN,
                Rarity = 15
            },
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Ores.ORE_COPPER,
                Rarity = 20
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<LootTableResource>().HasData(loot);
        }
    }
}