using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVYDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class _3152025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductSubColor__OutFitKey",
                table: "ProductSubColors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSubColor__OutFitKey",
                table: "ProductSubColors");
        }
    }
}
