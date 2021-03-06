
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.LootTableResources {

    public class StoneResourceTable : IDataGenerator {

        private const string LOOT_TABLE_ID = LootTables.STONE; 

        //Loot chance decreases as rarity increases
        private readonly List<LootTableResource> loot = new(){
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Stones.STONE,
                Rarity = 10
            },
            new(){
                LootTableId = LOOT_TABLE_ID,
                ResourceId = Stones.SLATE,
                Rarity = 15
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<LootTableResource>().HasData(loot);
        }
    }
}