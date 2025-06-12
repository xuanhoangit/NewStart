using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVYDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class _162025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductSubColor__OutFitKey",
                table: "ProductSubColors",
                newName: "ProductSubColor__OutfitKey");

            migrationBuilder.AlterColumn<string>(
                name: "ProductSubColor__OutfitKey",
                table: "ProductSubColors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubColors_ProductSubColor__OutfitKey",
                table: "ProductSubColors",
                column: "ProductSubColor__OutfitKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSubColors_ProductSubColor__OutfitKey",
                table: "ProductSubColors");

            migrationBuilder.RenameColumn(
                name: "ProductSubColor__OutfitKey",
                table: "ProductSubColors",
                newName: "ProductSubColor__OutFitKey");

            migrationBuilder.AlterColumn<string>(
                name: "ProductSubColor__OutFitKey",
                table: "ProductSubColors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
