using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVYDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class addOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItem__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartItem__Quantity = table.Column<int>(type: "int", nullable: false),
                    CartItem__CreatedByCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartItem__ProductSubColorId = table.Column<int>(type: "int", nullable: false),
                    CartItem__Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItem__Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Customers_CartItem__CreatedByCustomerId",
                        column: x => x.CartItem__CreatedByCustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Order__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order__CreatedByCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order__Status = table.Column<int>(type: "int", nullable: false),
                    Order__PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    Order__AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order__PaymentCode = table.Column<long>(type: "bigint", nullable: false),
                    Order__CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order__PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Order__Id);
                    table.ForeignKey(
                        name: "FK_Order_Customers_Order__CreatedByCustomerId",
                        column: x => x.Order__CreatedByCustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItem__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItem__OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderItem__Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderItem__ProductSubColorId = table.Column<int>(type: "int", nullable: false),
                    OrderItem__Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItem__Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderItem__OrderId",
                        column: x => x.OrderItem__OrderId,
                        principalTable: "Order",
                        principalColumn: "Order__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_ProductSubColors_OrderItem__ProductSubColorId",
                        column: x => x.OrderItem__ProductSubColorId,
                        principalTable: "ProductSubColors",
                        principalColumn: "ProductSubColor__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartItem__CreatedByCustomerId",
                table: "CartItem",
                column: "CartItem__CreatedByCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Order__CreatedByCustomerId",
                table: "Order",
                column: "Order__CreatedByCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderItem__OrderId",
                table: "OrderItem",
                column: "OrderItem__OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderItem__ProductSubColorId",
                table: "OrderItem",
                column: "OrderItem__ProductSubColorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Customers");
        }
    }
}
