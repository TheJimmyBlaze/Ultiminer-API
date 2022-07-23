
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.Resources {

    public class Stones : IDataGenerator {

        //All Stone Ids have the "Stone" prefix
        public const string STONE = "Stone.Simple";
        public const string SLATE = "Stone.Slate";

        private readonly List<Resource> stones = new(){
            new(){
                NaturalId = STONE,
                DisplayName = "Stone"
            },
            new(){
                NaturalId = SLATE,
                DisplayName = "Slate"
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Resource>().HasData(stones);
        }
    }
}