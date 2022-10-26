
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data.Resources {

    public class ResourceTypes : IDataGenerator {

        public const string STONE = "Stone";
        public const string GEM = "Gem";
        public const string TREASURE = "Treasure";

        private readonly List<ResourceType> types = new(){
            new(){
                NaturalId=STONE
            },
            new(){
                NaturalId=GEM
            },
            new(){
                NaturalId=TREASURE
            }
        };

        public void Generate(ModelBuilder builder) {
            builder.Entity<ResourceType>().HasData(types);
        }
    }   
}