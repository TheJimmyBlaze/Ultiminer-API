﻿// <auto-generated />
using System;
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
    [Migration("20230125093038_initial")]
    partial class initial
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
                            NaturalId = "Table.Ore",
                            DisplayName = "Ore"
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
                            ResourceId = "Stone.Flint",
                            Rarity = 15
                        },
                        new
                        {
                            LootTableId = "Table.Stone",
                            ResourceId = "Stone.Soap",
                            Rarity = 20
                        },
                        new
                        {
                            LootTableId = "Table.Ore",
                            ResourceId = "Ore.Coal",
                            Rarity = 10
                        },
                        new
                        {
                            LootTableId = "Table.Ore",
                            ResourceId = "Ore.Tin",
                            Rarity = 15
                        },
                        new
                        {
                            LootTableId = "Table.Ore",
                            ResourceId = "Ore.Copper",
                            Rarity = 20
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
                            ResourceId = "Treasure.Tablet.Stone",
                            Rarity = 25
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
                        },
                        new
                        {
                            LootTableId = "Table.Gems",
                            ResourceId = "Gem.Raw.Sapphire",
                            Rarity = 20
                        },
                        new
                        {
                            LootTableId = "Table.Gems",
                            ResourceId = "Gem.Raw.Emerald",
                            Rarity = 25
                        });
                });

            modelBuilder.Entity("Database.Models.MiningStats", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastMine")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextMine")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalMineActions")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("MiningStats");
                });

            modelBuilder.Entity("Database.Models.Node", b =>
                {
                    b.Property<string>("NaturalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LevelRequired")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("NaturalId");

                    b.ToTable("Nodes");

                    b.HasData(
                        new
                        {
                            NaturalId = "Node.Stone",
                            DisplayName = "Stone",
                            LevelRequired = 0,
                            Quantity = 6
                        },
                        new
                        {
                            NaturalId = "Node.Flint",
                            DisplayName = "Flint",
                            LevelRequired = 3,
                            Quantity = 6
                        },
                        new
                        {
                            NaturalId = "Node.Coal",
                            DisplayName = "Coal",
                            LevelRequired = 5,
                            Quantity = 8
                        },
                        new
                        {
                            NaturalId = "Node.Tin",
                            DisplayName = "Tin",
                            LevelRequired = 8,
                            Quantity = 5
                        },
                        new
                        {
                            NaturalId = "Node.Copper",
                            DisplayName = "Copper",
                            LevelRequired = 10,
                            Quantity = 5
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
                            NodeId = "Node.Flint",
                            LootTableId = "Table.Stone",
                            MaxRarity = 15,
                            MinRarity = 0,
                            TableRarity = 10
                        },
                        new
                        {
                            NodeId = "Node.Flint",
                            LootTableId = "Table.Treasure",
                            MaxRarity = 50,
                            MinRarity = 0,
                            TableRarity = 100
                        },
                        new
                        {
                            NodeId = "Node.Flint",
                            LootTableId = "Table.Gems",
                            MaxRarity = 15,
                            MinRarity = 0,
                            TableRarity = 200
                        },
                        new
                        {
                            NodeId = "Node.Coal",
                            LootTableId = "Table.Stone",
                            MaxRarity = 10,
                            MinRarity = 0,
                            TableRarity = 10
                        },
                        new
                        {
                            NodeId = "Node.Coal",
                            LootTableId = "Table.Ore",
                            MaxRarity = 10,
                            MinRarity = 0,
                            TableRarity = 50
                        },
                        new
                        {
                            NodeId = "Node.Coal",
                            LootTableId = "Table.Treasure",
                            MaxRarity = 50,
                            MinRarity = 0,
                            TableRarity = 200
                        },
                        new
                        {
                            NodeId = "Node.Coal",
                            LootTableId = "Table.Gems",
                            MaxRarity = 20,
                            MinRarity = 0,
                            TableRarity = 300
                        },
                        new
                        {
                            NodeId = "Node.Tin",
                            LootTableId = "Table.Stone",
                            MaxRarity = 20,
                            MinRarity = 0,
                            TableRarity = 10
                        },
                        new
                        {
                            NodeId = "Node.Tin",
                            LootTableId = "Table.Ore",
                            MaxRarity = 15,
                            MinRarity = 15,
                            TableRarity = 50
                        },
                        new
                        {
                            NodeId = "Node.Tin",
                            LootTableId = "Table.Treasure",
                            MaxRarity = 50,
                            MinRarity = 0,
                            TableRarity = 200
                        },
                        new
                        {
                            NodeId = "Node.Tin",
                            LootTableId = "Table.Gems",
                            MaxRarity = 25,
                            MinRarity = 0,
                            TableRarity = 300
                        },
                        new
                        {
                            NodeId = "Node.Copper",
                            LootTableId = "Table.Stone",
                            MaxRarity = 20,
                            MinRarity = 0,
                            TableRarity = 10
                        },
                        new
                        {
                            NodeId = "Node.Copper",
                            LootTableId = "Table.Ore",
                            MaxRarity = 20,
                            MinRarity = 20,
                            TableRarity = 50
                        },
                        new
                        {
                            NodeId = "Node.Copper",
                            LootTableId = "Table.Treasure",
                            MaxRarity = 50,
                            MinRarity = 0,
                            TableRarity = 200
                        },
                        new
                        {
                            NodeId = "Node.Copper",
                            LootTableId = "Table.Gems",
                            MaxRarity = 25,
                            MinRarity = 0,
                            TableRarity = 300
                        });
                });

            modelBuilder.Entity("Database.Models.Resource", b =>
                {
                    b.Property<string>("NaturalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExperienceAwarded")
                        .HasColumnType("int");

                    b.Property<string>("ResourceTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NaturalId");

                    b.HasIndex("ResourceTypeId");

                    b.ToTable("Resources");

                    b.HasData(
                        new
                        {
                            NaturalId = "Stone.Simple",
                            DisplayName = "Stone",
                            ExperienceAwarded = 15,
                            ResourceTypeId = "Stone"
                        },
                        new
                        {
                            NaturalId = "Stone.Flint",
                            DisplayName = "Flint",
                            ExperienceAwarded = 25,
                            ResourceTypeId = "Stone"
                        },
                        new
                        {
                            NaturalId = "Stone.Soap",
                            DisplayName = "Soap Stone",
                            ExperienceAwarded = 35,
                            ResourceTypeId = "Stone"
                        },
                        new
                        {
                            NaturalId = "Ore.Coal",
                            DisplayName = "Coal",
                            ExperienceAwarded = 32,
                            ResourceTypeId = "Ore"
                        },
                        new
                        {
                            NaturalId = "Ore.Tin",
                            DisplayName = "Tin",
                            ExperienceAwarded = 48,
                            ResourceTypeId = "Ore"
                        },
                        new
                        {
                            NaturalId = "Ore.Copper",
                            DisplayName = "Copper",
                            ExperienceAwarded = 64,
                            ResourceTypeId = "Ore"
                        },
                        new
                        {
                            NaturalId = "Treasure.Rod.Wooden",
                            DisplayName = "Wooden Rod",
                            ExperienceAwarded = 20,
                            ResourceTypeId = "Treasure"
                        },
                        new
                        {
                            NaturalId = "Treasure.Binding.Linen",
                            DisplayName = "Linen Scrap",
                            ExperienceAwarded = 20,
                            ResourceTypeId = "Treasure"
                        },
                        new
                        {
                            NaturalId = "Treasure.Tablet.Stone",
                            DisplayName = "Stone Tablet",
                            ExperienceAwarded = 40,
                            ResourceTypeId = "Treasure"
                        },
                        new
                        {
                            NaturalId = "Treasure.Cube.Brass",
                            DisplayName = "Brass Cube",
                            ExperienceAwarded = 75,
                            ResourceTypeId = "Treasure"
                        },
                        new
                        {
                            NaturalId = "Gem.Raw.Quartz",
                            DisplayName = "Quartz",
                            ExperienceAwarded = 50,
                            ResourceTypeId = "Gem"
                        },
                        new
                        {
                            NaturalId = "Gem.Raw.Opal",
                            DisplayName = "Opal",
                            ExperienceAwarded = 65,
                            ResourceTypeId = "Gem"
                        },
                        new
                        {
                            NaturalId = "Gem.Raw.Sapphire",
                            DisplayName = "Sapphire",
                            ExperienceAwarded = 80,
                            ResourceTypeId = "Gem"
                        },
                        new
                        {
                            NaturalId = "Gem.Raw.Emerald",
                            DisplayName = "Emerald",
                            ExperienceAwarded = 95,
                            ResourceTypeId = "Gem"
                        });
                });

            modelBuilder.Entity("Database.Models.ResourceType", b =>
                {
                    b.Property<string>("NaturalId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NaturalId");

                    b.ToTable("ResourceType");

                    b.HasData(
                        new
                        {
                            NaturalId = "Stone"
                        },
                        new
                        {
                            NaturalId = "Ore"
                        },
                        new
                        {
                            NaturalId = "Gem"
                        },
                        new
                        {
                            NaturalId = "Treasure"
                        });
                });

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.Models.UserLevel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("LevelExperience")
                        .HasColumnType("int");

                    b.Property<int>("TotalExperience")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("UserLevel");
                });

            modelBuilder.Entity("Database.Models.UserNode", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SelectedNodeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("SelectedNodeId");

                    b.ToTable("UserNodes");
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

            modelBuilder.Entity("Database.Models.MiningStats", b =>
                {
                    b.HasOne("Database.Models.User", "User")
                        .WithOne("MiningStats")
                        .HasForeignKey("Database.Models.MiningStats", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Models.NodeLootTable", b =>
                {
                    b.HasOne("Database.Models.LootTable", "LootTable")
                        .WithMany()
                        .HasForeignKey("LootTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Node", "Node")
                        .WithMany("LootTables")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LootTable");

                    b.Navigation("Node");
                });

            modelBuilder.Entity("Database.Models.Resource", b =>
                {
                    b.HasOne("Database.Models.ResourceType", "ResourceType")
                        .WithMany()
                        .HasForeignKey("ResourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ResourceType");
                });

            modelBuilder.Entity("Database.Models.UserLevel", b =>
                {
                    b.HasOne("Database.Models.User", "User")
                        .WithOne("Level")
                        .HasForeignKey("Database.Models.UserLevel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.Models.UserNode", b =>
                {
                    b.HasOne("Database.Models.Node", "SelectedNode")
                        .WithMany()
                        .HasForeignKey("SelectedNodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.User", "User")
                        .WithOne("Node")
                        .HasForeignKey("Database.Models.UserNode", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SelectedNode");

                    b.Navigation("User");
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
                    b.Navigation("LootTables");
                });

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Navigation("Level")
                        .IsRequired();

                    b.Navigation("MiningStats")
                        .IsRequired();

                    b.Navigation("Node")
                        .IsRequired();

                    b.Navigation("Resources");
                });
#pragma warning restore 612, 618
        }
    }
}