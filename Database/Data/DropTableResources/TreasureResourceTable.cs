
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.DropTableResources {

    public class TreasureResourceTable : IDataGenerator {

        private const string DROP_TABLE_ID = DropTables.TREASURE; 

        //Drop chance decreases as rarity increases
        private readonly List<DropTableResource> drops = new(){
            new(){
                DropTableId = DROP_TABLE_ID,
                ResourceId = Treasures.ROD_WOODEN,
                Rarity = 10
            },
            new(){
                DropTableId = DROP_TABLE_ID,
                ResourceId = Treasures.BINDING_LINEN,
                Rarity = 15
            },
            new(){
                DropTableId = DROP_TABLE_ID,
                ResourceId = Treasures.CUBE_BRASS,
                Rarity = 50
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<DropTableResource>().HasData(drops);
        }
    }
}