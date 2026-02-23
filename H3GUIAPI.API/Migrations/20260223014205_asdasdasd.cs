using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H3GUIAPI.API.Migrations
{
    /// <inheritdoc />
    public partial class asdasdasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_Products_ImageFilePathDataId",
                table: "ImageFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageFiles",
                table: "ImageFiles");

            migrationBuilder.RenameTable(
                name: "ImageFiles",
                newName: "ImageFilesDatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageFilesDatas",
                table: "ImageFilesDatas",
                column: "ImageFilePathDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFilesDatas_Products_ImageFilePathDataId",
                table: "ImageFilesDatas",
                column: "ImageFilePathDataId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFilesDatas_Products_ImageFilePathDataId",
                table: "ImageFilesDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageFilesDatas",
                table: "ImageFilesDatas");

            migrationBuilder.RenameTable(
                name: "ImageFilesDatas",
                newName: "ImageFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageFiles",
                table: "ImageFiles",
                column: "ImageFilePathDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_Products_ImageFilePathDataId",
                table: "ImageFiles",
                column: "ImageFilePathDataId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
