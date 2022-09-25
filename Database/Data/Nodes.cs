
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
                Quantity = 4
            },
            new(){
                NaturalId = FLINT,
                DisplayName = "Flint",
                Quantity = 6
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Node>().HasData(nodes);
        }
    }
}