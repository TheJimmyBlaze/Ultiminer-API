
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.DropTableResources {

    public class GemResourceTable : IDataGenerator {

        private const string DROP_TABLE_ID = DropTables.GEMS; 

        //Drop chance decreases as rarity increases
        private readonly List<DropTableResource> drops = new(){
            new(){
                DropTableId = DROP_TABLE_ID,
                ResourceId = Gems.RAW_QUARTZ,
                Rarity = 10
            },
            new(){
                DropTableId = DROP_TABLE_ID,
                ResourceId = Gems.RAW_OPAL,
                Rarity = 15
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<DropTableResource>().HasData(drops);
        }
    }
}