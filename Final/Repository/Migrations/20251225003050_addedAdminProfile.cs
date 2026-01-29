using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class addedAdminProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "326090eb-06cb-4427-99b1-e0f8c9d052ff", "AQAAAAIAAYagAAAAEE/m23AgU9d8rwm7n4ijVZEa72tEO0vplDlwl1Makb4xg3Syjaf0A1fVGsV3ueMotg==", "0c1c6f11-3527-4f1a-b471-3569deab85bf" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Birthday", "ConcurrencyStamp", "CustomerNumber", "Email", "EmailConfirmed", "Gender", "Image", "IsVerifiedPurchase", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7A9FJSKE-11DD-EB38-JS88-3D5AA6E5AB0E", 0, "Bakı", new DateTime(2006, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "fe98656d-39d8-422b-8fdd-3503cc75c69e", null, "esedovaf6@gmail.com", true, 2, null, false, false, null, "Fatya", "ESEDOVAF6@GMAIL.COM", "_FATYA", "AQAAAAIAAYagAAAAEMQrV4K5EJm1+7fkkkulF7KO2QACqWfHg5SbzH+J4vRq1qpBuReqW4ARNw1ewtFmXw==", null, false, "ba23b2cd-9354-409c-8d25-0c0a70d812b7", "Esedova", false, "_fatya" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "F8A43D91-1E74-4F8A-BC55-5D27A3F9989A", "7A9FJSKE-11DD-EB38-JS88-3D5AA6E5AB0E" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "F8A43D91-1E74-4F8A-BC55-5D27A3F9989A", "7A9FJSKE-11DD-EB38-JS88-3D5AA6E5AB0E" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7A9FJSKE-11DD-EB38-JS88-3D5AA6E5AB0E");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08ce80a7-9c26-4ffc-8940-e1f43889fb2a", "AQAAAAIAAYagAAAAEGe5KTWYngTubTxkEWk2uFDYXrBZXnk43DQOUIYPaRxv62wotlsFQhs59qKN1IJNlg==", "56e90858-13b3-4334-b14b-3fcdd53cfaa6" });
        }
    }
}
