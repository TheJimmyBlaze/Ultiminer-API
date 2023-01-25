
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.Resources {

    public class Stones : IDataGenerator {

        //All Stone Ids have the "Stone" prefix
        public const string STONE = "Stone.Simple";
        public const string FLINT = "Stone.Flint";
        public const string SOAP = "Stone.Soap";

        private readonly List<Resource> stones = new(){
            new(){
                NaturalId = STONE,
                DisplayName = "Stone",
                ResourceTypeId = ResourceTypes.STONE,
                ExperienceAwarded = 15,
            },
            new(){
                NaturalId = FLINT,
                DisplayName = "Flint",
                ResourceTypeId = ResourceTypes.STONE,
                ExperienceAwarded = 25,
            },
            new(){
                NaturalId = SOAP,
                DisplayName = "Soap Stone",
                ResourceTypeId = ResourceTypes.STONE,
                ExperienceAwarded = 35
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Resource>().HasData(stones);
        }
    }
}