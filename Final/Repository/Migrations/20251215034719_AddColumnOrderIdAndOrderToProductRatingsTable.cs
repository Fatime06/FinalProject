using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnOrderIdAndOrderToProductRatingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductRatings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3fef9309-8a69-40ab-94e2-6931861e2326", "AQAAAAIAAYagAAAAEBwd2RiZF2/cOBCc4y9hw0cSt1Yv3zJ2vm2jz9iDXuKqEYxsdf1zKHPnB/zluuNF1g==", "bba2c89a-e8a6-4f70-8640-032fb3566714" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_OrderId",
                table: "ProductRatings",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Orders_OrderId",
                table: "ProductRatings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Orders_OrderId",
                table: "ProductRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_ProductRatings_OrderId",
                table: "ProductRatings");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductRatings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a054c896-b053-4699-9d3a-835a58f42616", "AQAAAAIAAYagAAAAEKlsSbMyFmiM9hhJrH1rXjLSEsSmxmPTa3a97kF7YlbYIaAqhZ/4nZQKtl2Il82yGQ==", "a3363b9a-7e7e-43a8-9162-e1655fe1d512" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
