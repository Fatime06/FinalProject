using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class addColumnIconToCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7A9FJSKE-11DD-EB38-JS88-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6466fb8-c3a5-4e82-b701-0ddb48ac00c8", "AQAAAAIAAYagAAAAELjFlYZlcUEQV6lKcbm4FpH0I4M985cStE/aKtcD+yQjKXLoNdWEdrdsRQ1zavKAlw==", "0a5efd62-de80-4b63-baae-997b0f709ac2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "751ddba0-795c-46a9-8004-b79db15d0b72", "AQAAAAIAAYagAAAAEDOiCOxKtoYRuP6vwr/BAS2KbfN47jtEPcmqTxpQ7n8MpPtlUEVJnEjFWpbWvaO0sQ==", "2b73f278-7cc4-4b53-8230-765623c5d08a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7A9FJSKE-11DD-EB38-JS88-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe98656d-39d8-422b-8fdd-3503cc75c69e", "AQAAAAIAAYagAAAAEMQrV4K5EJm1+7fkkkulF7KO2QACqWfHg5SbzH+J4vRq1qpBuReqW4ARNw1ewtFmXw==", "ba23b2cd-9354-409c-8d25-0c0a70d812b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "326090eb-06cb-4427-99b1-e0f8c9d052ff", "AQAAAAIAAYagAAAAEE/m23AgU9d8rwm7n4ijVZEa72tEO0vplDlwl1Makb4xg3Syjaf0A1fVGsV3ueMotg==", "0c1c6f11-3527-4f1a-b471-3569deab85bf" });
        }
    }
}
