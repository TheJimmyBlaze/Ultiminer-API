
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.DropTableResources {

    public class StoneResourceTable : IDataGenerator {

        private const string DROP_TABLE_ID = DropTables.STONE; 

        //Drop chance decreases as rarity increases
        private readonly List<DropTableResource> drops = new(){
            new(){
                DropTableId = DROP_TABLE_ID,
                ResourceId = Stones.STONE,
                Rarity = 10
            },
            new(){
                DropTableId = DROP_TABLE_ID,
                ResourceId = Stones.SLATE,
                Rarity = 15
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<DropTableResource>().HasData(drops);
        }
    }
}