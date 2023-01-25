
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.Resources {

    public class Ores : IDataGenerator {

        //All Ore Ids have the "Ore" prefix
        public const string ORE_COAL = "Ore.Coal";
        public const string ORE_TIN = "Ore.Tin";
        public const string ORE_COPPER = "Ore.Copper";

        private readonly List<Resource> ores = new(){
            new(){
                NaturalId = ORE_COAL,
                DisplayName = "Coal",
                ResourceTypeId = ResourceTypes.ORE,
                ExperienceAwarded = 32
            },
            new(){
                NaturalId = ORE_TIN,
                DisplayName = "Tin",
                ResourceTypeId = ResourceTypes.ORE,
                ExperienceAwarded = 48
            },
            new(){
                NaturalId = ORE_COPPER,
                DisplayName = "Copper",
                ResourceTypeId = ResourceTypes.ORE,
                ExperienceAwarded = 64
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Resource>().HasData(ores);
        }
    }
}