using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iCubeTrain.Migrations
{
    /// <inheritdoc />
    public partial class FirstinMAC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5225fcc0-47f2-4c7e-b508-d9e65cd0c95b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b5b6d25-1d7f-45ca-b531-319cf246a1fe");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bac18b85-c01b-424a-bb7a-55f963da11c3", null, "Admin", "ADMIN" },
                    { "f0b3260c-87c6-42b7-9dbd-84505848695f", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bac18b85-c01b-424a-bb7a-55f963da11c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0b3260c-87c6-42b7-9dbd-84505848695f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5225fcc0-47f2-4c7e-b508-d9e65cd0c95b", null, "User", "USER" },
                    { "6b5b6d25-1d7f-45ca-b531-319cf246a1fe", null, "Admin", "ADMIN" }
                });
        }
    }
}
