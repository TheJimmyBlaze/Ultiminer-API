using System;
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
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.NaturalId);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    NaturalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceAwarded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.NaturalId);
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
                    { "Table.Stone", "Stone" },
                    { "Table.Treasure", "Treasure" }
                });

            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "NaturalId", "DisplayName", "Quantity" },
                values: new object[,]
                {
                    { "Node.Flint", "Flint", 6 },
                    { "Node.Stone", "Stone", 4 }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "NaturalId", "DisplayName", "ExperienceAwarded" },
                values: new object[,]
                {
                    { "Gem.Raw.Opal", "Opal", 65 },
                    { "Gem.Raw.Quartz", "Quartz", 50 },
                    { "Stone.Flint", "Flint", 10 },
                    { "Stone.Simple", "Stone", 5 },
                    { "Treasure.Binding.Linen", "Linen Scrap", 25 },
                    { "Treasure.Cube.Brass", "Brass Cube", 75 },
                    { "Treasure.Rod.Wooden", "Wooden Rod", 20 }
                });

            migrationBuilder.InsertData(
                table: "LootTableResources",
                columns: new[] { "LootTableId", "ResourceId", "Rarity" },
                values: new object[,]
                {
                    { "Table.Gems", "Gem.Raw.Opal", 15 },
                    { "Table.Gems", "Gem.Raw.Quartz", 10 },
                    { "Table.Stone", "Stone.Flint", 15 },
                    { "Table.Stone", "Stone.Simple", 10 },
                    { "Table.Treasure", "Treasure.Binding.Linen", 15 },
                    { "Table.Treasure", "Treasure.Cube.Brass", 50 },
                    { "Table.Treasure", "Treasure.Rod.Wooden", 10 }
                });

            migrationBuilder.InsertData(
                table: "NodeLootTables",
                columns: new[] { "LootTableId", "NodeId", "MaxRarity", "MinRarity", "TableRarity" },
                values: new object[,]
                {
                    { "Table.Gems", "Node.Flint", 15, 0, 200 },
                    { "Table.Stone", "Node.Flint", 15, 0, 10 },
                    { "Table.Treasure", "Node.Flint", 50, 0, 100 },
                    { "Table.Gems", "Node.Stone", 10, 0, 200 },
                    { "Table.Stone", "Node.Stone", 10, 0, 10 },
                    { "Table.Treasure", "Node.Stone", 15, 0, 100 }
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
                name: "UserResources");

            migrationBuilder.DropTable(
                name: "LootTables");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
