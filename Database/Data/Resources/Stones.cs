
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.Resources {

    public class Stones : IDataGenerator {

        //All Stone Ids have the "Stone" prefix
        public const string STONE = "Stone.Simple";
        public const string FLINT = "Stone.Flint";

        private readonly List<Resource> stones = new(){
            new(){
                NaturalId = STONE,
                DisplayName = "Stone",
                ResourceTypeId = ResourceTypes.STONE,
                ExperienceAwarded = 5,
            },
            new(){
                NaturalId = FLINT,
                DisplayName = "Flint",
                ResourceTypeId = ResourceTypes.STONE,
                ExperienceAwarded = 10,
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Resource>().HasData(stones);
        }
    }
}