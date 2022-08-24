using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FabricTrackerMobileApp.API.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fabrics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Brand = table.Column<string>(nullable: true),
                    Designer = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    MainCategoryId = table.Column<int>(nullable: false, defaultValue: "Unknown"),
                    SubCategoryId = table.Column<int>(nullable: false, defaultValue: "Unknown"),
                    TotalInches = table.Column<int>(nullable: true),
                    FatQtrQty = table.Column<int>(nullable: true),
                    FabricType = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: true),
                    BackgroundColor = table.Column<string>(nullable: true),
                    AccentColor1 = table.Column<string>(nullable: true),
                    AccentColor2 = table.Column<string>(nullable: true),
                    AccentColor3 = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValue: DateTime.Now),
                    DateModified = table.Column<DateTime>(nullable: true),
                    IsDiscontinued = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainCategories",
                columns: table => new
                {
                    MainCategoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MainCategoryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategories", x => x.MainCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubCategoryName = table.Column<string>(nullable: false),
                    MainCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategories_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "MainCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_MainCategoryId",
                table: "SubCategories",
                column: "MainCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fabrics");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "MainCategories");
        }
    }
}
