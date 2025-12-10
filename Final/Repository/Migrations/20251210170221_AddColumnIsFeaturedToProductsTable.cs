using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnIsFeaturedToProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d3cddef-2124-45f1-8f52-be71be4acc33", "AQAAAAIAAYagAAAAEJOWKVfg4GaqGPbvFBhGfyKy0+nqGnz8l/bZm+Gi0u86KO9FbGiGxGgp/i5hhPyhDA==", "160e19e4-9a64-4722-9248-1767e9eef0e5" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_AppUserId",
                table: "Baskets",
                column: "AppUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48899af0-8f04-4ec2-98b9-220f3529f8a0", "AQAAAAIAAYagAAAAEMbyPUAL7oOCrsyT7jDpthhKQ6Xj1bnucOyFdoZ9ZGiPswZSE9riqpZHPD3WfcWpAw==", "2499c365-baaf-405a-9d2f-77c70ff0e9ac" });
        }
    }
}
