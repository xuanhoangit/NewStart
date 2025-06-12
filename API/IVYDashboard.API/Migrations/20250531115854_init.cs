using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVYDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category__Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category__Type = table.Column<int>(type: "int", nullable: false),
                    Category__Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Category__Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Collection__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Collection__Status = table.Column<int>(type: "int", nullable: false),
                    Collection__Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Collection__Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Color__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color__Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color__Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Color__Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumberConfirmed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product__Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product__Sold = table.Column<int>(type: "int", nullable: false),
                    Product__Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Product__SeasonId = table.Column<byte>(type: "tinyint", nullable: false),
                    Product__CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Product__UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product__Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubColors",
                columns: table => new
                {
                    SubColor__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubColor__Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubColor__Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubColor__Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubColors", x => x.SubColor__Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    SubCategory__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategory__Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategory__Status = table.Column<int>(type: "int", nullable: false),
                    SubCategory__CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.SubCategory__Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_SubCategory__CategoryId",
                        column: x => x.SubCategory__CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Category__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Employees_UserId",
                        column: x => x.UserId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Employees_UserId",
                        column: x => x.UserId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Employees_UserId",
                        column: x => x.UserId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCollections",
                columns: table => new
                {
                    ProductCollection__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCollection__ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCollection__CollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCollections", x => x.ProductCollection__Id);
                    table.ForeignKey(
                        name: "FK_ProductCollections_Collections_ProductCollection__CollectionId",
                        column: x => x.ProductCollection__CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Collection__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCollections_Products_ProductCollection__ProductId",
                        column: x => x.ProductCollection__ProductId,
                        principalTable: "Products",
                        principalColumn: "Product__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFavorites",
                columns: table => new
                {
                    ProductFavorite__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductFavorite__ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFavorites", x => x.ProductFavorite__Id);
                    table.ForeignKey(
                        name: "FK_ProductFavorites_Products_ProductFavorite__ProductId",
                        column: x => x.ProductFavorite__ProductId,
                        principalTable: "Products",
                        principalColumn: "Product__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Employees_UserId",
                        column: x => x.UserId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColorSubColors",
                columns: table => new
                {
                    ColorSubColor__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorSubColor__SubColorId = table.Column<int>(type: "int", nullable: false),
                    ColorSubColor__ColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorSubColors", x => x.ColorSubColor__Id);
                    table.ForeignKey(
                        name: "FK_ColorSubColors_Colors_ColorSubColor__ColorId",
                        column: x => x.ColorSubColor__ColorId,
                        principalTable: "Colors",
                        principalColumn: "Color__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorSubColors_SubColors_ColorSubColor__SubColorId",
                        column: x => x.ColorSubColor__SubColorId,
                        principalTable: "SubColors",
                        principalColumn: "SubColor__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubColors",
                columns: table => new
                {
                    ProductSubColor__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSubColor__Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductSubColor__Discount = table.Column<byte>(type: "tinyint", nullable: false),
                    ProductSubColor__CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductSubColor__UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductSubColor__Status = table.Column<byte>(type: "tinyint", nullable: false),
                    ProductSubColor__SubColorId = table.Column<int>(type: "int", nullable: false),
                    ProductSubColor__ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubColors", x => x.ProductSubColor__Id);
                    table.ForeignKey(
                        name: "FK_ProductSubColors_Products_ProductSubColor__ProductId",
                        column: x => x.ProductSubColor__ProductId,
                        principalTable: "Products",
                        principalColumn: "Product__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubColors_SubColors_ProductSubColor__SubColorId",
                        column: x => x.ProductSubColor__SubColorId,
                        principalTable: "SubColors",
                        principalColumn: "SubColor__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubCategories",
                columns: table => new
                {
                    ProductSubCategory__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSubCategory__ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductSubCategory__SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCategories", x => x.ProductSubCategory__Id);
                    table.ForeignKey(
                        name: "FK_ProductSubCategories_Products_ProductSubCategory__ProductId",
                        column: x => x.ProductSubCategory__ProductId,
                        principalTable: "Products",
                        principalColumn: "Product__Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubCategories_SubCategories_ProductSubCategory__SubCategoryId",
                        column: x => x.ProductSubCategory__SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategory__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubColorFiles",
                columns: table => new
                {
                    ProductSubColorFile__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSubColorFile__Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductSubColorFile__Index = table.Column<byte>(type: "tinyint", nullable: false),
                    ProductSubColorFile__ProductSubColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubColorFiles", x => x.ProductSubColorFile__Id);
                    table.ForeignKey(
                        name: "FK_ProductSubColorFiles_ProductSubColors_ProductSubColorFile__ProductSubColorId",
                        column: x => x.ProductSubColorFile__ProductSubColorId,
                        principalTable: "ProductSubColors",
                        principalColumn: "ProductSubColor__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Size__Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size__S = table.Column<int>(type: "int", nullable: true),
                    Size__M = table.Column<int>(type: "int", nullable: true),
                    Size__L = table.Column<int>(type: "int", nullable: true),
                    Size__XL = table.Column<int>(type: "int", nullable: true),
                    Size__XXl = table.Column<int>(type: "int", nullable: true),
                    Size__ProductSubColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Size__Id);
                    table.ForeignKey(
                        name: "FK_Sizes_ProductSubColors_Size__ProductSubColorId",
                        column: x => x.Size__ProductSubColorId,
                        principalTable: "ProductSubColors",
                        principalColumn: "ProductSubColor__Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorSubColors_ColorSubColor__ColorId",
                table: "ColorSubColors",
                column: "ColorSubColor__ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorSubColors_ColorSubColor__SubColorId",
                table: "ColorSubColors",
                column: "ColorSubColor__SubColorId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Employees",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Employees",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCollections_ProductCollection__CollectionId",
                table: "ProductCollections",
                column: "ProductCollection__CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCollections_ProductCollection__ProductId",
                table: "ProductCollections",
                column: "ProductCollection__ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFavorites_ProductFavorite__ProductId",
                table: "ProductFavorites",
                column: "ProductFavorite__ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategories_ProductSubCategory__ProductId",
                table: "ProductSubCategories",
                column: "ProductSubCategory__ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCategories_ProductSubCategory__SubCategoryId",
                table: "ProductSubCategories",
                column: "ProductSubCategory__SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubColorFiles_ProductSubColorFile__ProductSubColorId",
                table: "ProductSubColorFiles",
                column: "ProductSubColorFile__ProductSubColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubColors_ProductSubColor__ProductId",
                table: "ProductSubColors",
                column: "ProductSubColor__ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubColors_ProductSubColor__SubColorId",
                table: "ProductSubColors",
                column: "ProductSubColor__SubColorId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_Size__ProductSubColorId",
                table: "Sizes",
                column: "Size__ProductSubColorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_SubCategory__CategoryId",
                table: "SubCategories",
                column: "SubCategory__CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorSubColors");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ProductCollections");

            migrationBuilder.DropTable(
                name: "ProductFavorites");

            migrationBuilder.DropTable(
                name: "ProductSubCategories");

            migrationBuilder.DropTable(
                name: "ProductSubColorFiles");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "ProductSubColors");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubColors");
        }
    }
}
