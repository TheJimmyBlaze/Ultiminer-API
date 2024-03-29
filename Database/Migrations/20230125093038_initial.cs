﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultiminer_Database.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LootTables",
                columns: table => new
                {
                    NaturalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LootTables", x => x.NaturalId);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    NaturalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelRequired = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.NaturalId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceType",
                columns: table => new
                {
                    NaturalId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceType", x => x.NaturalId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "NodeLootTables",
                columns: table => new
                {
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LootTableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableRarity = table.Column<int>(type: "int", nullable: false),
                    MinRarity = table.Column<int>(type: "int", nullable: false),
                    MaxRarity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeLootTables", x => new { x.NodeId, x.LootTableId });
                    table.ForeignKey(
                        name: "FK_NodeLootTables_LootTables_LootTableId",
                        column: x => x.LootTableId,
                        principalTable: "LootTables",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodeLootTables_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    NaturalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExperienceAwarded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.NaturalId);
                    table.ForeignKey(
                        name: "FK_Resources_ResourceType_ResourceTypeId",
                        column: x => x.ResourceTypeId,
                        principalTable: "ResourceType",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MiningStats",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastMine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextMine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalMineActions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiningStats", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_MiningStats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLevel",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    TotalExperience = table.Column<int>(type: "int", nullable: false),
                    LevelExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLevel", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserLevel_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNodes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SelectedNodeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNodes", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserNodes_Nodes_SelectedNodeId",
                        column: x => x.SelectedNodeId,
                        principalTable: "Nodes",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LootTableResources",
                columns: table => new
                {
                    LootTableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LootTableResources", x => new { x.LootTableId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_LootTableResources_LootTables_LootTableId",
                        column: x => x.LootTableId,
                        principalTable: "LootTables",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LootTableResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserResources",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResources", x => new { x.UserId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_UserResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserResources_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LootTables",
                columns: new[] { "NaturalId", "DisplayName" },
                values: new object[,]
                {
                    { "Table.Gems", "Gem" },
                    { "Table.Ore", "Ore" },
                    { "Table.Stone", "Stone" },
                    { "Table.Treasure", "Treasure" }
                });

            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "NaturalId", "DisplayName", "LevelRequired", "Quantity" },
                values: new object[,]
                {
                    { "Node.Coal", "Coal", 5, 8 },
                    { "Node.Copper", "Copper", 10, 5 },
                    { "Node.Flint", "Flint", 3, 6 },
                    { "Node.Stone", "Stone", 0, 6 },
                    { "Node.Tin", "Tin", 8, 5 }
                });

            migrationBuilder.InsertData(
                table: "ResourceType",
                column: "NaturalId",
                values: new object[]
                {
                    "Gem",
                    "Ore",
                    "Stone",
                    "Treasure"
                });

            migrationBuilder.InsertData(
                table: "NodeLootTables",
                columns: new[] { "LootTableId", "NodeId", "MaxRarity", "MinRarity", "TableRarity" },
                values: new object[,]
                {
                    { "Table.Gems", "Node.Coal", 20, 0, 300 },
                    { "Table.Ore", "Node.Coal", 10, 0, 50 },
                    { "Table.Stone", "Node.Coal", 10, 0, 10 },
                    { "Table.Treasure", "Node.Coal", 50, 0, 200 },
                    { "Table.Gems", "Node.Copper", 25, 0, 300 },
                    { "Table.Ore", "Node.Copper", 20, 20, 50 },
                    { "Table.Stone", "Node.Copper", 20, 0, 10 },
                    { "Table.Treasure", "Node.Copper", 50, 0, 200 },
                    { "Table.Gems", "Node.Flint", 15, 0, 200 },
                    { "Table.Stone", "Node.Flint", 15, 0, 10 },
                    { "Table.Treasure", "Node.Flint", 50, 0, 100 },
                    { "Table.Gems", "Node.Stone", 10, 0, 200 },
                    { "Table.Stone", "Node.Stone", 10, 0, 10 },
                    { "Table.Treasure", "Node.Stone", 15, 0, 100 },
                    { "Table.Gems", "Node.Tin", 25, 0, 300 },
                    { "Table.Ore", "Node.Tin", 15, 15, 50 },
                    { "Table.Stone", "Node.Tin", 20, 0, 10 },
                    { "Table.Treasure", "Node.Tin", 50, 0, 200 }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "NaturalId", "DisplayName", "ExperienceAwarded", "ResourceTypeId" },
                values: new object[,]
                {
                    { "Gem.Raw.Emerald", "Emerald", 95, "Gem" },
                    { "Gem.Raw.Opal", "Opal", 65, "Gem" },
                    { "Gem.Raw.Quartz", "Quartz", 50, "Gem" },
                    { "Gem.Raw.Sapphire", "Sapphire", 80, "Gem" },
                    { "Ore.Coal", "Coal", 32, "Ore" },
                    { "Ore.Copper", "Copper", 64, "Ore" },
                    { "Ore.Tin", "Tin", 48, "Ore" },
                    { "Stone.Flint", "Flint", 25, "Stone" },
                    { "Stone.Simple", "Stone", 15, "Stone" },
                    { "Stone.Soap", "Soap Stone", 35, "Stone" },
                    { "Treasure.Binding.Linen", "Linen Scrap", 20, "Treasure" },
                    { "Treasure.Cube.Brass", "Brass Cube", 75, "Treasure" },
                    { "Treasure.Rod.Wooden", "Wooden Rod", 20, "Treasure" },
                    { "Treasure.Tablet.Stone", "Stone Tablet", 40, "Treasure" }
                });

            migrationBuilder.InsertData(
                table: "LootTableResources",
                columns: new[] { "LootTableId", "ResourceId", "Rarity" },
                values: new object[,]
                {
                    { "Table.Gems", "Gem.Raw.Emerald", 25 },
                    { "Table.Gems", "Gem.Raw.Opal", 15 },
                    { "Table.Gems", "Gem.Raw.Quartz", 10 },
                    { "Table.Gems", "Gem.Raw.Sapphire", 20 },
                    { "Table.Ore", "Ore.Coal", 10 },
                    { "Table.Ore", "Ore.Copper", 20 },
                    { "Table.Ore", "Ore.Tin", 15 },
                    { "Table.Stone", "Stone.Flint", 15 },
                    { "Table.Stone", "Stone.Simple", 10 },
                    { "Table.Stone", "Stone.Soap", 20 },
                    { "Table.Treasure", "Treasure.Binding.Linen", 15 },
                    { "Table.Treasure", "Treasure.Cube.Brass", 50 },
                    { "Table.Treasure", "Treasure.Rod.Wooden", 10 },
                    { "Table.Treasure", "Treasure.Tablet.Stone", 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LootTableResources_ResourceId",
                table: "LootTableResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeLootTables_LootTableId",
                table: "NodeLootTables",
                column: "LootTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceTypeId",
                table: "Resources",
                column: "ResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNodes_SelectedNodeId",
                table: "UserNodes",
                column: "SelectedNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResources_ResourceId",
                table: "UserResources",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LootTableResources");

            migrationBuilder.DropTable(
                name: "MiningStats");

            migrationBuilder.DropTable(
                name: "NodeLootTables");

            migrationBuilder.DropTable(
                name: "UserLevel");

            migrationBuilder.DropTable(
                name: "UserNodes");

            migrationBuilder.DropTable(
                name: "UserResources");

            migrationBuilder.DropTable(
                name: "LootTables");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ResourceType");
        }
    }
}
