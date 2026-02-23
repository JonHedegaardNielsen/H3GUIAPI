using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H3GUIAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ImageFilePageDataId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ImageFiles",
                columns: table => new
                {
                    ImageFilePathDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    RelativePath = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFiles", x => x.ImageFilePathDataId);
                    table.ForeignKey(
                        name: "FK_ImageFiles_Products_ImageFilePathDataId",
                        column: x => x.ImageFilePathDataId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "ImageFilePageDataId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
