
using Database.Data.DropTableResources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.NodeDropTables {

    public class SlateNodeTable : IDataGenerator {

        private const string NODE_ID = Nodes.SLATE;

        private readonly List<NodeDropTable> tables = new(){
            new(){
                NodeId = NODE_ID,
                DropTableId = DropTables.STONE,
                TableRarity = 10,
                MinDropRarity = 0,
                MaxDropRarity = 15
            },
            new(){
                NodeId = NODE_ID,
                DropTableId = DropTables.TREASURE,
                TableRarity = 100,
                MinDropRarity = 0,
                MaxDropRarity = 50
            },
            new(){
                NodeId = NODE_ID,
                DropTableId = DropTables.GEMS,
                TableRarity = 200,
                MinDropRarity = 0,
                MaxDropRarity = 15
            }
        };

        public void Generate(ModelBuilder builder){
            builder.Entity<NodeDropTable>().HasData(tables);
        }
    }
}