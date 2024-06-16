using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iCubeTrain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25b732c7-4528-423f-ab74-2d8032067606");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a34adda0-b181-48fe-a510-1d4403cca92e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f618807-522f-40b2-ad81-2c2f5c43d976", null, "Admin", "ADMIN" },
                    { "e0e3c4a2-7e50-4310-913e-4437021395d9", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f618807-522f-40b2-ad81-2c2f5c43d976");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0e3c4a2-7e50-4310-913e-4437021395d9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25b732c7-4528-423f-ab74-2d8032067606", null, "Admin", "ADMIN" },
                    { "a34adda0-b181-48fe-a510-1d4403cca92e", null, "User", "USER" }
                });
        }
    }
}
