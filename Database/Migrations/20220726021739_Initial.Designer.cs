// <auto-generated />
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ultiminer_Database.Migrations
{
    [DbContext(typeof(UltiminerContext))]
    [Migration("20220726021739_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Database.Models.LootTable", b =>
                {
                    b.Property<string>("NaturalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NaturalId");

                    b.ToTable("LootTables");

                    b.HasData(
                        new
                        {
                            NaturalId = "Table.Stone",
                            DisplayName = "Stone"
                        },
                        new
                        {
                            NaturalId = "Table.Treasure",
                            DisplayName = "Treasure"
                        },
                        new
                        {
                            NaturalId = "Table.Gems",
                            DisplayName = "Gem"
                        });
                });

            modelBuilder.Entity("Database.Models.LootTableResource", b =>
                {
                    b.Property<string>("LootTableId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ResourceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.HasKey("LootTableId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("LootTableResources");

                    b.HasData(
                        new
                        {
                            LootTableId = "Table.Stone",
                            ResourceId = "Stone.Simple",
                            Rarity = 10
                        },
                        new
                        {
                            LootTableId = "Table.Stone",
                            ResourceId = "Stone.Slate",
                            Rarity = 15
                        },
                        new
                        {
                            LootTableId = "Table.Treasure",
                            ResourceId = "Treasure.Rod.Wooden",
                            Rarity = 10
                        },
                        new
                        {
                            LootTableId = "Table.Treasure",
                            ResourceId = "Treasure.Binding.Linen",
                            Rarity = 15
                        },
                        new
                        {
                            LootTableId = "Table.Treasure",
                            ResourceId = "Treasure.Cube.Brass",
                            Rarity = 50
                        },
                        new
                        {
                            LootTableId = "Table.Gems",
                            ResourceId = "Gem.Raw.Quartz",
                            Rarity = 10
                        },
                        new
                        {
                            LootTableId = "Table.Gems",
                            ResourceId = "Gem.Raw.Opal",
                            Rarity = 15
                        });
                });

            modelBuilder.Entity("Database.Models.Node", b =>
                {
                    b.Property<string>("NaturalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("NaturalId");

                    b.ToTable("Nodes");

                    b.HasData(
                        new
                        {
                            NaturalId = "Node.Stone",
                            DisplayName = "Stone",
                            Quantity = 4
                        },
                        new
                        {
                            NaturalId = "Node.Slate",
                            DisplayName = "Slate",
                            Quantity = 6
                        });
                });

            modelBuilder.Entity("Database.Models.NodeLootTable", b =>
                {
                    b.Property<string>("NodeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LootTableId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MaxRarity")
                        .HasColumnType("int");

                    b.Property<int>("MinRarity")
                        .HasColumnType("int");

                    b.Property<int>("TableRarity")
                        .HasColumnType("int");

                    b.HasKey("NodeId", "LootTableId");

                    b.HasIndex("LootTableId");

                    b.ToTable("NodeLootTables");

                    b.HasData(
                        new
                        {
                            NodeId = "Node.Stone",
                            LootTableId = "Table.Stone",
                            MaxRarity = 10,
                            MinRarity = 0,
                            TableRarity = 10
                        },
                        new
                        {
                            NodeId = "Node.Stone",
                            LootTableId = "Table.Treasure",
                            MaxRarity = 15,
                            MinRarity = 0,
                            TableRarity = 100
                        },
                        new
                        {
                            NodeId = "Node.Stone",
                            LootTableId = "Table.Gems",
                            MaxRarity = 10,
                            MinRarity = 0,
                            TableRarity = 200
                        },
                        new
                        {
                            NodeId = "Node.Slate",
                            LootTableId = "Table.Stone",
                            MaxRarity = 15,
                            MinRarity = 0,
                            TableRarity = 10
                        },
                        new
                        {
                            NodeId = "Node.Slate",
                            LootTableId = "Table.Treasure",
                            MaxRarity = 50,
                            MinRarity = 0,
                            TableRarity = 100
                        },
                        new
                        {
                            NodeId = "Node.Slate",
                            LootTableId = "Table.Gems",
                            MaxRarity = 15,
                            MinRarity = 0,
                            TableRarity = 200
                        });
                });

            modelBuilder.Entity("Database.Models.Resource", b =>
                {
                    b.Property<string>("NaturalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NaturalId");

                    b.ToTable("Resources");

                    b.HasData(
                        new
                        {
                            NaturalId = "Stone.Simple",
                            DisplayName = "Stone"
                        },
                        new
                        {
                            NaturalId = "Stone.Slate",
                            DisplayName = "Slate"
                        },
                        new
                        {
                            NaturalId = "Treasure.Rod.Wooden",
                            DisplayName = "Wooden Rod"
                        },
                        new
                        {
                            NaturalId = "Treasure.Binding.Linen",
                            DisplayName = "Linen Scrap"
                        },
                        new
                        {
                            NaturalId = "Treasure.Cube.Brass",
                            DisplayName = "Brass Cube"
                        },
                        new
                        {
                            NaturalId = "Gem.Raw.Quartz",
                            DisplayName = "Quartz"
                        },
                        new
                        {
                            NaturalId = "Gem.Raw.Opal",
                            DisplayName = "Opal"
                        });
                });

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.Models.UserResource", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ResourceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("UserResources");
                });

            modelBuilder.Entity("Database.Models.LootTableResource", b =>
                {
                    b.HasOne("Database.Models.LootTable", "LootTable")
                        .WithMany("Resources")
                        .HasForeignKey("LootTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Resource", "Resource")
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LootTable");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Database.Models.NodeLootTable", b =>
                {
                    b.HasOne("Database.Models.LootTable", "LootTable")
                        .WithMany()
                        .HasForeignKey("LootTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Node", "Node")
                        .WithMany("LooTables")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LootTable");

                    b.Navigation("Node");
                });

            modelBuilder.Entity("Database.Models.UserResource", b =>
                {
                    b.HasOne("Database.Models.Resource", "Resource")
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.User", "User")
                        .WithMany("Resources")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resource");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Models.LootTable", b =>
                {
                    b.Navigation("Resources");
                });

            modelBuilder.Entity("Database.Models.Node", b =>
                {
                    b.Navigation("LooTables");
                });

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Navigation("Resources");
                });
#pragma warning restore 612, 618
        }
    }
}
