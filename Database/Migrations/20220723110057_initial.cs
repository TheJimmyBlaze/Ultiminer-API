using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ultiminer_Database.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DropTables",
                columns: table => new
                {
                    NaturalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropTables", x => x.NaturalId);
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
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "NodeDropTables",
                columns: table => new
                {
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DropTableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableRarity = table.Column<int>(type: "int", nullable: false),
                    MinDropRarity = table.Column<int>(type: "int", nullable: false),
                    MaxDropRarity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeDropTables", x => new { x.NodeId, x.DropTableId });
                    table.ForeignKey(
                        name: "FK_NodeDropTables_DropTables_DropTableId",
                        column: x => x.DropTableId,
                        principalTable: "DropTables",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodeDropTables_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DropTableResources",
                columns: table => new
                {
                    DropTableId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropTableResources", x => new { x.DropTableId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_DropTableResources_DropTables_DropTableId",
                        column: x => x.DropTableId,
                        principalTable: "DropTables",
                        principalColumn: "NaturalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DropTableResources_Resources_ResourceId",
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
                table: "DropTables",
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
                    { "Node.Slate", "Slate", 6 },
                    { "Node.Stone", "Stone", 4 }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "NaturalId", "DisplayName" },
                values: new object[,]
                {
                    { "Gem.Raw.Opal", "Opal" },
                    { "Gem.Raw.Quartz", "Quartz" },
                    { "Stone.Simple", "Stone" },
                    { "Stone.Slate", "Slate" },
                    { "Treasure.Binding.Linen", "Linen Scrap" },
                    { "Treasure.Cube.Brass", "Brass Cube" },
                    { "Treasure.Rod.Wooden", "Wooden Rod" }
                });

            migrationBuilder.InsertData(
                table: "DropTableResources",
                columns: new[] { "DropTableId", "ResourceId", "Rarity" },
                values: new object[,]
                {
                    { "Table.Gems", "Gem.Raw.Opal", 15 },
                    { "Table.Gems", "Gem.Raw.Quartz", 10 },
                    { "Table.Stone", "Stone.Simple", 10 },
                    { "Table.Stone", "Stone.Slate", 15 },
                    { "Table.Treasure", "Treasure.Binding.Linen", 15 },
                    { "Table.Treasure", "Treasure.Cube.Brass", 50 },
                    { "Table.Treasure", "Treasure.Rod.Wooden", 10 }
                });

            migrationBuilder.InsertData(
                table: "NodeDropTables",
                columns: new[] { "DropTableId", "NodeId", "MaxDropRarity", "MinDropRarity", "TableRarity" },
                values: new object[,]
                {
                    { "Table.Gems", "Node.Slate", 15, 0, 200 },
                    { "Table.Stone", "Node.Slate", 15, 0, 10 },
                    { "Table.Treasure", "Node.Slate", 50, 0, 100 },
                    { "Table.Gems", "Node.Stone", 10, 0, 200 },
                    { "Table.Stone", "Node.Stone", 10, 0, 10 },
                    { "Table.Treasure", "Node.Stone", 15, 0, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DropTableResources_ResourceId",
                table: "DropTableResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeDropTables_DropTableId",
                table: "NodeDropTables",
                column: "DropTableId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResources_ResourceId",
                table: "UserResources",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DropTableResources");

            migrationBuilder.DropTable(
                name: "NodeDropTables");

            migrationBuilder.DropTable(
                name: "UserResources");

            migrationBuilder.DropTable(
                name: "DropTables");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
