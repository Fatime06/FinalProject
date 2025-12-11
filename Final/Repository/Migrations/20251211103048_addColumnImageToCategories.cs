using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class addColumnImageToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3570003a-e0f6-41e1-bdc6-06a3855785aa", "AQAAAAIAAYagAAAAEN3EZdkrU3axUWwcJCFu6RHA4yqihnoeuH/yKZyazYStb0G0F9R/HoCaxshE814dqA==", "6a5360c9-1acd-48f6-afc4-c0750929d097" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d3cddef-2124-45f1-8f52-be71be4acc33", "AQAAAAIAAYagAAAAEJOWKVfg4GaqGPbvFBhGfyKy0+nqGnz8l/bZm+Gi0u86KO9FbGiGxGgp/i5hhPyhDA==", "160e19e4-9a64-4722-9248-1767e9eef0e5" });
        }
    }
}
