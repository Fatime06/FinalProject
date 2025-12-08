using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25D6D5B2-DC97-4042-B56E-EB3F8123BB99", null, "SuperAdmin", "SUPERADMIN" },
                    { "9A17F51D-AED3-4C8C-BE55-EE3D6E8A0C01", null, "Member", "MEMBER" },
                    { "F8A43D91-1E74-4F8A-BC55-5D27A3F9989A", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Birthday", "ConcurrencyStamp", "CustomerNumber", "Email", "EmailConfirmed", "Gender", "Image", "IsVerifiedPurchase", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E", 0, "Bakı", new DateTime(2006, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "cd647881-ea2c-4227-b81a-a80fc5e5ca5b", null, "esedovaf4@gmail.com", true, 2, null, false, false, null, "Fatima", "ESEDOVAF4@GMAIL.COM", "_FATIMA", "AQAAAAIAAYagAAAAEKOS2m34vwvgSItPLA9roVZf3kuhY2VQgxXllqaKqlvcUm9oLydNigqxat0DJIc8Zg==", null, false, "3690718c-bf0e-476b-a162-19cd34ccacd0", "Asadova", false, "_fatima" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "25D6D5B2-DC97-4042-B56E-EB3F8123BB99", "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9A17F51D-AED3-4C8C-BE55-EE3D6E8A0C01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F8A43D91-1E74-4F8A-BC55-5D27A3F9989A");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "25D6D5B2-DC97-4042-B56E-EB3F8123BB99", "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25D6D5B2-DC97-4042-B56E-EB3F8123BB99");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
