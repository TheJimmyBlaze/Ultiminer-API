
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.NodeLootTables {

    public class StoneNodeTable : IDataGenerator {

        private const string NODE_ID = Nodes.STONE;

        private readonly List<NodeLootTable> tables = new(){
            new(){
                NodeId = NODE_ID,
                LootTableId = LootTables.STONE,
                TableRarity = 10,
                MinRarity = 0,
                MaxRarity = 10
            },
            new(){
                NodeId = NODE_ID,
                LootTableId = LootTables.TREASURE,
                TableRarity = 100,
                MinRarity = 0,
                MaxRarity = 15
            },
            new(){
                NodeId = NODE_ID,
                LootTableId = LootTables.GEMS,
                TableRarity = 200,
                MinRarity = 0,
                MaxRarity = 10
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<NodeLootTable>().HasData(tables);
        }
    }
}