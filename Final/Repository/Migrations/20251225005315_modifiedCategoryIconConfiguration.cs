using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class modifiedCategoryIconConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7A9FJSKE-11DD-EB38-JS88-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1a87178-0161-4eff-b854-7f8d91677af2", "AQAAAAIAAYagAAAAEHFaVFJr1sexSeNBk3GwuXwyTzcibfJoLl7INp42D4xX3OJcIKTacA6oHypiiqkOnw==", "483ee666-5c97-4324-be52-3f6010273959" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e732c29f-0b78-4d50-9a1b-f287c6e6f72f", "AQAAAAIAAYagAAAAEIs6BGeVh6PXJVrWaVWNhpY4ioLcKTsdyTwiO4GyDPxb5WB5kPyJNy+ZXklLPoX8jA==", "4490801f-eab8-4aee-a912-a0fc4a968672" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
    }
}
