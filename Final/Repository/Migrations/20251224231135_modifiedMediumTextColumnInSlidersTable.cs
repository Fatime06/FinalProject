using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class modifiedMediumTextColumnInSlidersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MediumText",
                table: "Sliders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34d51fc3-e6a0-4bff-9e49-c650aaedc70c", "AQAAAAIAAYagAAAAEPc/eCJ4xCP/caboy5hm1TTPw753oCYhDxNXQq0661AbXKCdPxjnvE4IBpELLbZlvQ==", "7188f1b5-a7ed-4807-ae88-2a8bb2f924ca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MediumText",
                table: "Sliders",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3fef9309-8a69-40ab-94e2-6931861e2326", "AQAAAAIAAYagAAAAEBwd2RiZF2/cOBCc4y9hw0cSt1Yv3zJ2vm2jz9iDXuKqEYxsdf1zKHPnB/zluuNF1g==", "bba2c89a-e8a6-4f70-8640-032fb3566714" });
        }
    }
}
