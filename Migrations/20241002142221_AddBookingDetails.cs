using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb1_ResturantBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "bookings",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "bookings",
                keyColumn: "BookingId",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "bookings");

            migrationBuilder.InsertData(
                table: "bookings",
                columns: new[] { "BookingId", "CustomerId", "Date", "NumberOfPeople", "TableId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 3, 11, 38, 26, 645, DateTimeKind.Local).AddTicks(7207), 0, 1 },
                    { 2, 2, new DateTime(2024, 10, 4, 11, 38, 26, 645, DateTimeKind.Local).AddTicks(7260), 0, 2 }
                });
        }
    }
}
