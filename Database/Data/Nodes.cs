
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data {

    public class Nodes : IDataGenerator {

        //All Node Ids have the "Node" prefix
        public const string STONE = "Node.Stone";
        public const string FLINT = "Node.Flint";
        public const string COAL = "Node.Coal";
        public const string TIN = "Node.Tin";
        public const string COPPER = "Node.Copper";

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
            },
            new(){
                NaturalId = COAL,
                DisplayName = "Coal",
                LevelRequired = 5,
                Quantity = 8
            },
            new(){
                NaturalId = TIN,
                DisplayName = "Tin",
                LevelRequired = 8,
                Quantity = 5
            },
            new(){
                NaturalId = COPPER,
                DisplayName = "Copper",
                LevelRequired = 10,
                Quantity = 5
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Node>().HasData(nodes);
        }
    }
}