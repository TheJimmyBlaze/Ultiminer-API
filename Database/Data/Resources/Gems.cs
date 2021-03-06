
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.Resources {

    public class Gems : IDataGenerator {

        //All Gem Ids have the "Gem" prefix
        public const string RAW_QUARTZ = "Gem.Raw.Quartz";
        public const string RAW_OPAL = "Gem.Raw.Opal";

        private readonly List<Resource> gems = new(){
            new(){
                NaturalId = RAW_QUARTZ,
                DisplayName = "Quartz"
            },
            new(){
                NaturalId = RAW_OPAL,
                DisplayName = "Opal"
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Resource>().HasData(gems);
        }
    }
}