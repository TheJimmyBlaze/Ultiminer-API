
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data {

    public class Nodes : IDataGenerator {

        //All Node Ids have the "Node" prefix
        public const string STONE = "Node.Stone";
        public const string FLINT = "Node.Flint";

        private readonly List<Node> nodes = new(){
            new(){
                NaturalId = STONE,
                DisplayName = "Stone",
                LevelRequired = 0,
                Quantity = 6
            },
            new(){
                NaturalId = FLINT,
                DisplayName = "Flint",
                LevelRequired = 3,
                Quantity = 6
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Node>().HasData(nodes);
        }
    }
}