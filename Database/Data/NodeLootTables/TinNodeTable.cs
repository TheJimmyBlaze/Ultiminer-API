
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.NodeLootTables {

    public class TinNodeTable : IDataGenerator {

        private const string NODE_ID = Nodes.TIN;

        private readonly List<NodeLootTable> tables = new(){
            new(){
                NodeId = NODE_ID,
                LootTableId = LootTables.STONE,
                TableRarity = 10,
                MinRarity = 0,
                MaxRarity = 20
            },
            new(){
                NodeId = NODE_ID,
                LootTableId = LootTables.ORE,
                TableRarity = 50,
                MinRarity = 15,
                MaxRarity = 15
            },
            new(){
                NodeId = NODE_ID,
                LootTableId = LootTables.TREASURE,
                TableRarity = 200,
                MinRarity = 0,
                MaxRarity = 50
            },
            new(){
                NodeId = NODE_ID,
                LootTableId = LootTables.GEMS,
                TableRarity = 300,
                MinRarity = 0,
                MaxRarity = 25
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<NodeLootTable>().HasData(tables);
        }
    }
}