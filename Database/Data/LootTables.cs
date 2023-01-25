
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data {

    public class LootTables : IDataGenerator {

        //All Loot Table Ids have the "Table" prefix
        public const string STONE = "Table.Stone";
        public const string ORE = "Table.Ore";
        public const string TREASURE = "Table.Treasure";
        public const string GEMS = "Table.Gems";

        private readonly List<LootTable> tables = new(){
            new(){
                NaturalId = STONE,
                DisplayName = "Stone"
            },
            new(){
                NaturalId = ORE,
                DisplayName = "Ore"
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
            builder.Entity<LootTable>().HasData(tables);
        }
    }
}