
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data {

    public class Nodes : IDataGenerator {

        //All Node Ids have the "Node" prefix
        public const string STONE = "Node.Stone";
        public const string SLATE = "Node.Slate";

        private readonly List<Node> nodes = new(){
            new(){
                NaturalId = STONE,
                DisplayName = "Stone",
                Quantity = 4
            },
            new(){
                NaturalId = SLATE,
                DisplayName = "Slate",
                Quantity = 6
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Node>().HasData(nodes);
        }
    }
}