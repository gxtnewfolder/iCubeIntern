using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iCubeTrain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30c76baf-23e2-499d-abba-ee76b6d04a9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3776a439-2ef9-422d-bc53-b7aecc81a244");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5225fcc0-47f2-4c7e-b508-d9e65cd0c95b", null, "User", "USER" },
                    { "6b5b6d25-1d7f-45ca-b531-319cf246a1fe", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "30c76baf-23e2-499d-abba-ee76b6d04a9d", null, "User", "USER" },
                    { "3776a439-2ef9-422d-bc53-b7aecc81a244", null, "Admin", "ADMIN" }
                });
        }
    }
}
