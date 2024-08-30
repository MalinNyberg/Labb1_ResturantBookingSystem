using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb1_ResturantBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "Id", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "John Doe", "1234567890" },
                    { 2, "Jane Smith", "0987654321" },
                    { 3, "Malin Nyberg", "0701234567" },
                    { 4, "Fredrik Jansson", "0707654321" }
                });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "MenuId", "IsAvailable", "NameOfDish", "Price" },
                values: new object[,]
                {
                    { 1, false, "Spaghetti Bolognese", 12.99m },
                    { 2, false, "Caesar Salad", 9.99m },
                    { 3, false, "Pizza Vesuvio", 8.99m },
                    { 4, false, "Swedish Meatballs - Italian style", 10.99m }
                });

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "TableId", "NumberOfSeats", "TableNumber" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 2, 2 },
                    { 3, 6, 3 },
                    { 4, 4, 4 },
                    { 5, 8, 5 }
                });

            migrationBuilder.InsertData(
                table: "bookings",
                columns: new[] { "BookingId", "CustomerId", "Date", "NumberOfPeople", "TableId", "Time" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 31, 22, 24, 15, 375, DateTimeKind.Local).AddTicks(138), 0, 1, new TimeSpan(0, 18, 0, 0, 0) },
                    { 2, 2, new DateTime(2024, 9, 1, 22, 24, 15, 375, DateTimeKind.Local).AddTicks(195), 0, 2, new TimeSpan(0, 19, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "bookings",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "bookings",
                keyColumn: "BookingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "MenuId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "MenuId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "MenuId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "menus",
                keyColumn: "MenuId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "TableId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "TableId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "TableId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "TableId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "TableId",
                keyValue: 2);
        }
    }
}
