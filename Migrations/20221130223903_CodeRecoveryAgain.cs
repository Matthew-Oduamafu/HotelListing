using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.Migrations
{
    /// <inheritdoc />
    public partial class CodeRecoveryAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29c0b275-a04f-4fa3-b113-c810bb332df4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "679397bd-a2fc-4d39-9b99-275706630c3e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3952187b-1ff3-4181-b34e-83656508331c", "d985d6fc-b44a-4f98-891d-6998d93172b0", "User", "USER" },
                    { "c8b4b026-1af4-458f-b4de-87548a50b20b", "ef6fda57-9459-4209-8341-7197bb5d692d", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3952187b-1ff3-4181-b34e-83656508331c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8b4b026-1af4-458f-b4de-87548a50b20b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29c0b275-a04f-4fa3-b113-c810bb332df4", "467d9a64-83d6-4616-a353-1d4cf8d189f6", "Administrator", "ADMINISTRATOR" },
                    { "679397bd-a2fc-4d39-9b99-275706630c3e", "a7b948f1-672b-4039-9723-a03a85d34843", "User", "USER" }
                });
        }
    }
}
