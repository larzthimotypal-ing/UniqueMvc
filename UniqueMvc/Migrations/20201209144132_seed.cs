using Microsoft.EntityFrameworkCore.Migrations;

namespace UniqueMvc.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Access", "HomeAddress", "Name" },
                values: new object[] { "16446968-4cef-46d6-83c5-d3a00de4f250", 0, "6548ad42-dbf0-41ee-b25d-8fd9750657c3", "ApplicationUser", "james@gmail.com", true, false, null, "JAMES@GMAIL.COM", "ADMINJAMES", "AQAAAAEAACcQAAAAEPj8NbZrHiPxTgTWqYvfS9Udr8rieJiX6jTg+dlEeRgF1eHBmHs9SzuyE8mwVTfO0w==", null, false, "dad6efa6-5f90-4cd6-9abb-34a6992d8e25", false, "adminJames", "Admin", null, "James" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16446968-4cef-46d6-83c5-d3a00de4f250");
        }
    }
}
