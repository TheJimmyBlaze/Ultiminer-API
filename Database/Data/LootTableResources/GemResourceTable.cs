
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.LootTableResources {

    public class GemResourceTable : IDataGenerator {

        private const string LOOT_TABLE_ID = LootTables.GEMS; 

        //Loot chance decreases as rarity increases
        private readonly List<LootTableResource> loot = new(){
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Gems.RAW_QUARTZ,
                Rarity = 10
            },
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Gems.RAW_OPAL,
                Rarity = 15
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<LootTableResource>().HasData(loot);
        }
    }
}