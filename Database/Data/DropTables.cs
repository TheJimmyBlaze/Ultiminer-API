
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data {

    public class DropTables : IDataGenerator {

        //All DropTable Ids have the "Table" prefix
        public const string STONE = "Table.Stone";
        public const string TREASURE = "Table.Treasure";
        public const string GEMS = "Table.Gems";

        private readonly List<DropTable> tables = new(){
            new(){
                NaturalId = STONE,
                DisplayName = "Stone"
            },
            new(){
                NaturalId = TREASURE,
                DisplayName = "Treasure"
            },
            new(){
                NaturalId = GEMS,
                DisplayName = "Gem"
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<DropTable>().HasData(tables);
        }
    }
}