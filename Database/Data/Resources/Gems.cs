
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.Resources {

    public class Gems : IDataGenerator {

        //All Gem Ids have the "Gem" prefix
        public const string RAW_QUARTZ = "Gem.Raw.Quartz";
        public const string RAW_OPAL = "Gem.Raw.Opal";
        public const string RAW_SAPPHIRE = "Gem.Raw.Sapphire";
        public const string RAW_EMERALD = "Gem.Raw.Emerald";

        private readonly List<Resource> gems = new(){
            new(){
                NaturalId = RAW_QUARTZ,
                DisplayName = "Quartz",
                ResourceTypeId = ResourceTypes.GEM,
                ExperienceAwarded = 50
            },
            new(){
                NaturalId = RAW_OPAL,
                DisplayName = "Opal",
                ResourceTypeId = ResourceTypes.GEM,
                ExperienceAwarded = 65
            },
            new(){
                NaturalId = RAW_SAPPHIRE,
                DisplayName = "Sapphire",
                ResourceTypeId = ResourceTypes.GEM,
                ExperienceAwarded = 80
            },
            new(){
                NaturalId = RAW_EMERALD,
                DisplayName = "Emerald",
                ResourceTypeId = ResourceTypes.GEM,
                ExperienceAwarded = 95
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Resource>().HasData(gems);
        }
    }
}