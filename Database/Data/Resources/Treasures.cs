
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.Resources {

    public class Treasures : IDataGenerator {

        //All Treasure Ids have the 'Treasure" prefix
        public const string ROD_WOODEN = "Treasure.Rod.Wooden";
        public const string BINDING_LINEN = "Treasure.Binding.Linen";
        public const string CUBE_BRASS = "Treasure.Cube.Brass";

        private readonly List<Resource> treasures = new(){
            new(){
                NaturalId = ROD_WOODEN,
                DisplayName = "Wooden Rod",
                ResourceTypeId = ResourceTypes.TREASURE,
                ExperienceAwarded = 20
            },
            new(){
                NaturalId = BINDING_LINEN,
                DisplayName = "Linen Scrap",
                ResourceTypeId = ResourceTypes.TREASURE,
                ExperienceAwarded = 25
            },
            new(){
                NaturalId = CUBE_BRASS,
                DisplayName = "Brass Cube",
                ResourceTypeId = ResourceTypes.TREASURE,
                ExperienceAwarded = 75
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<Resource>().HasData(treasures);
        }
    } 
}