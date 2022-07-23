using Database.Data;
using Database.Data.DropTableResources;
using Database.Data.NodeDropTables;
using Database.Data.Resources;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database {

    public class UltiminerContext : DbContext {

        public DbSet<User> Users {get; set;}

        public DbSet<Node> Nodes {get; set;}
        public DbSet<Resource> Resources {get; set;}
        public DbSet<UserResource> UserResources {get; set;}
        
        public DbSet<DropTable> DropTables {get; set;}
        public DbSet<DropTableResource> DropTableResources {get; set;}
        public DbSet<NodeDropTable> NodeDropTables {get; set;}

        private readonly List<IDataGenerator> generators = new() {

            //Nodes
            new Nodes(),

            //Resources
            new Stones(),
            new Treasures(),
            new Gems(),

            //Drop Tables
            new DropTables(),

            //Drop Table Resources
            new StoneResourceTable(),
            new TreasureResourceTable(),
            new GemResourceTable(),

            //Node Drop tables
            new StoneNodeTable(),
            new SlateNodeTable()
        };

        public UltiminerContext(DbContextOptions<UltiminerContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<UserResource>().HasKey(userResource => new {userResource.UserId, userResource.ResourceId});
            builder.Entity<DropTableResource>().HasKey(dropTableResource => new {dropTableResource.DropTableId, dropTableResource.ResourceId});
            builder.Entity<NodeDropTable>().HasKey(nodeDropTable => new {nodeDropTable.NodeId, nodeDropTable.DropTableId});

            generators.ForEach(generator => generator.Generate(builder));
        }
    }
}