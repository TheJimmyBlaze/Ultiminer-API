using Database.Data;
using Database.Data.LootTableResources;
using Database.Data.NodeLootTables;
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database {

    public class UltiminerContext : DbContext {

        public DbSet<User> Users {get; set;}

        public DbSet<Node> Nodes {get; set;}
        public DbSet<Resource> Resources {get; set;}
        public DbSet<UserResource> UserResources {get; set;}
        
        public DbSet<LootTable> LootTables {get; set;}
        public DbSet<LootTableResource> LootTableResources {get; set;}
        public DbSet<NodeLootTable> NodeLootTables {get; set;}

        private readonly List<IDataGenerator> generators = new() {

            //Nodes
            new Nodes(),

            //Resources
            new Stones(),
            new Treasures(),
            new Gems(),

            //Loot Tables
            new LootTables(),

            //Loot Table Resources
            new StoneResourceTable(),
            new TreasureResourceTable(),
            new GemResourceTable(),

            //Node Loot tables
            new StoneNodeTable(),
            new SlateNodeTable()
        };

        public UltiminerContext(DbContextOptions<UltiminerContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<UserResource>().HasKey(userResource => new {userResource.UserId, userResource.ResourceId});
            builder.Entity<LootTableResource>().HasKey(lootTableResource => new {lootTableResource.LootTableId, lootTableResource.ResourceId});
            builder.Entity<NodeLootTable>().HasKey(nodeLootTable => new {nodeLootTable.NodeId, nodeLootTable.LootTableId});

            generators.ForEach(generator => generator.Generate(builder));
        }
    }
}