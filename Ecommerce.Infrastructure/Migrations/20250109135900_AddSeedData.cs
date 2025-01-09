using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "ExpiredRefreshToken", "Firstname", "IsDeleted", "Lastname", "Password", "PhoneNumber", "RefreshToken", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { 1, "System", new DateTimeOffset(new DateTime(2025, 1, 9, 20, 58, 58, 365, DateTimeKind.Unspecified).AddTicks(5332), new TimeSpan(0, 7, 0, 0, 0)), "coloshopclient1@gmail.com", null, "admin", false, "1", "MgEOGwtFd10=", "0812345555", null, null, null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
