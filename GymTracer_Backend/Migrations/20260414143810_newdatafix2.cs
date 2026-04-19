using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class newdatafix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Code", "CreatedAt", "RevokedAt", "UserId" },
                values: new object[] { 6L, new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a999"), new DateTime(2026, 5, 1, 12, 30, 0, 0, DateTimeKind.Unspecified), null, 12L });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "IsActive", "IsStudent", "MaxUsage", "Price", "Tax_key", "TrainingId", "Type" },
                values: new object[,]
                {
                    { 42L, "Napijegy Diák", true, true, 1ul, 1250ul, 27.00m, null, 1 },
                    { 43L, "Havi bérlet Diák", true, true, null, 9000ul, 27.00m, null, 2 },
                    { 44L, "10 alkalmas bérlet Diák", true, true, 10ul, 11000ul, 27.00m, null, 3 }
                });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CardId", "UseDate" },
                values: new object[] { 3L, new DateTime(2026, 5, 1, 7, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 8L, 0, new DateTime(2026, 5, 1, 8, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CardId", "UseDate" },
                values: new object[] { 12L, new DateTime(2026, 5, 1, 10, 5, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CardId", "UseDate" },
                values: new object[] { 5L, new DateTime(2026, 5, 2, 16, 45, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 1L, 0, new DateTime(2026, 5, 2, 17, 20, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 9L, 2, new DateTime(2026, 5, 3, 9, 10, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Gate", "UseDate" },
                values: new object[] { 1, new DateTime(2026, 5, 3, 11, 55, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 13L, 0, new DateTime(2026, 5, 4, 6, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 7L, 2, new DateTime(2026, 5, 4, 18, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 4L, 1, new DateTime(2026, 5, 5, 8, 40, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 11L, 0, new DateTime(2026, 5, 5, 19, 5, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "UsageLogs",
                columns: new[] { "Id", "CardId", "Gate", "UseDate" },
                values: new object[,]
                {
                    { 13L, 10L, 1, new DateTime(2026, 5, 6, 15, 35, 0, 0, DateTimeKind.Unspecified) },
                    { 14L, 8L, 0, new DateTime(2026, 5, 7, 7, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 15L, 3L, 2, new DateTime(2026, 5, 7, 12, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 16L, 1L, 1, new DateTime(2026, 5, 8, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17L, 5L, 0, new DateTime(2026, 5, 8, 16, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 18L, 12L, 2, new DateTime(2026, 5, 9, 10, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 19L, 2L, 1, new DateTime(2026, 5, 9, 11, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 20L, 9L, 0, new DateTime(2026, 5, 10, 8, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 21L, 13L, 2, new DateTime(2026, 5, 10, 17, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 22L, 7L, 1, new DateTime(2026, 5, 11, 19, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 23L, 4L, 0, new DateTime(2026, 5, 11, 20, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 24L, 11L, 2, new DateTime(2026, 5, 12, 7, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 26L, 10L, 0, new DateTime(2026, 5, 13, 16, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 27L, 8L, 2, new DateTime(2026, 5, 13, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28L, 3L, 1, new DateTime(2026, 5, 14, 9, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 29L, 1L, 0, new DateTime(2026, 5, 14, 10, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 30L, 5L, 2, new DateTime(2026, 5, 15, 8, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 31L, 12L, 1, new DateTime(2026, 5, 15, 12, 35, 0, 0, DateTimeKind.Unspecified) },
                    { 32L, 2L, 0, new DateTime(2026, 5, 16, 14, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 33L, 9L, 2, new DateTime(2026, 5, 16, 17, 55, 0, 0, DateTimeKind.Unspecified) },
                    { 34L, 13L, 1, new DateTime(2026, 5, 17, 11, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 35L, 7L, 0, new DateTime(2026, 5, 17, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 36L, 4L, 2, new DateTime(2026, 5, 18, 7, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 37L, 11L, 1, new DateTime(2026, 5, 18, 9, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 39L, 10L, 2, new DateTime(2026, 5, 19, 19, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 40L, 8L, 1, new DateTime(2026, 5, 20, 8, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 41L, 3L, 0, new DateTime(2026, 5, 20, 11, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 42L, 1L, 2, new DateTime(2026, 5, 21, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43L, 5L, 1, new DateTime(2026, 5, 21, 17, 35, 0, 0, DateTimeKind.Unspecified) },
                    { 44L, 12L, 0, new DateTime(2026, 5, 22, 6, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 45L, 2L, 2, new DateTime(2026, 5, 22, 9, 55, 0, 0, DateTimeKind.Unspecified) },
                    { 46L, 9L, 1, new DateTime(2026, 5, 23, 14, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 47L, 13L, 0, new DateTime(2026, 5, 23, 18, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 48L, 7L, 2, new DateTime(2026, 5, 24, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 49L, 4L, 1, new DateTime(2026, 5, 24, 12, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 50L, 11L, 0, new DateTime(2026, 5, 25, 8, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 52L, 10L, 1, new DateTime(2026, 5, 26, 19, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 53L, 8L, 0, new DateTime(2026, 5, 26, 20, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 54L, 3L, 2, new DateTime(2026, 5, 27, 7, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 55L, 1L, 1, new DateTime(2026, 5, 27, 11, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 56L, 5L, 0, new DateTime(2026, 5, 28, 15, 35, 0, 0, DateTimeKind.Unspecified) },
                    { 57L, 12L, 2, new DateTime(2026, 5, 28, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58L, 2L, 1, new DateTime(2026, 5, 29, 9, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 59L, 9L, 0, new DateTime(2026, 5, 29, 13, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 60L, 13L, 2, new DateTime(2026, 5, 30, 8, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 61L, 7L, 1, new DateTime(2026, 5, 30, 10, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 62L, 4L, 0, new DateTime(2026, 5, 31, 16, 55, 0, 0, DateTimeKind.Unspecified) },
                    { 63L, 11L, 2, new DateTime(2026, 5, 31, 19, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 65L, 10L, 0, new DateTime(2026, 6, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66L, 8L, 2, new DateTime(2026, 6, 2, 15, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 67L, 3L, 1, new DateTime(2026, 6, 2, 18, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 68L, 1L, 0, new DateTime(2026, 6, 3, 9, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 69L, 5L, 2, new DateTime(2026, 6, 3, 11, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 70L, 12L, 1, new DateTime(2026, 6, 4, 16, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 71L, 2L, 0, new DateTime(2026, 6, 4, 19, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 72L, 9L, 2, new DateTime(2026, 6, 5, 8, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 73L, 13L, 1, new DateTime(2026, 6, 5, 14, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 74L, 7L, 0, new DateTime(2026, 6, 6, 17, 35, 0, 0, DateTimeKind.Unspecified) },
                    { 75L, 4L, 2, new DateTime(2026, 6, 6, 20, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 76L, 11L, 1, new DateTime(2026, 6, 7, 7, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 78L, 10L, 2, new DateTime(2026, 6, 8, 15, 55, 0, 0, DateTimeKind.Unspecified) },
                    { 79L, 8L, 1, new DateTime(2026, 6, 8, 18, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 80L, 3L, 0, new DateTime(2026, 6, 9, 9, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 81L, 1L, 2, new DateTime(2026, 6, 9, 12, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 82L, 5L, 1, new DateTime(2026, 6, 10, 16, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 83L, 12L, 0, new DateTime(2026, 6, 10, 19, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 84L, 2L, 2, new DateTime(2026, 6, 11, 8, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 85L, 9L, 1, new DateTime(2026, 6, 11, 11, 35, 0, 0, DateTimeKind.Unspecified) },
                    { 86L, 13L, 0, new DateTime(2026, 6, 12, 15, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 87L, 7L, 2, new DateTime(2026, 6, 12, 18, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 88L, 4L, 1, new DateTime(2026, 6, 13, 9, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 89L, 11L, 0, new DateTime(2026, 6, 14, 14, 25, 0, 0, DateTimeKind.Unspecified) },
                    { 91L, 10L, 1, new DateTime(2026, 6, 16, 19, 55, 0, 0, DateTimeKind.Unspecified) },
                    { 92L, 8L, 0, new DateTime(2026, 6, 17, 8, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 93L, 3L, 2, new DateTime(2026, 6, 18, 11, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 94L, 1L, 1, new DateTime(2026, 6, 19, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95L, 5L, 0, new DateTime(2026, 6, 20, 18, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 96L, 12L, 2, new DateTime(2026, 6, 22, 9, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 97L, 2L, 1, new DateTime(2026, 6, 24, 12, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 98L, 9L, 0, new DateTime(2026, 6, 26, 16, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 99L, 13L, 2, new DateTime(2026, 6, 28, 19, 35, 0, 0, DateTimeKind.Unspecified) },
                    { 100L, 7L, 1, new DateTime(2026, 6, 30, 8, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 12L, 6L, 2, new DateTime(2026, 5, 6, 14, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 25L, 6L, 1, new DateTime(2026, 5, 12, 15, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 38L, 6L, 0, new DateTime(2026, 5, 19, 16, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 51L, 6L, 2, new DateTime(2026, 5, 25, 16, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 64L, 6L, 1, new DateTime(2026, 6, 1, 7, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 77L, 6L, 0, new DateTime(2026, 6, 7, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 90L, 6L, 2, new DateTime(2026, 6, 15, 17, 10, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CardId", "UseDate" },
                values: new object[] { 1L, new DateTime(2026, 5, 1, 14, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 2L, 1, new DateTime(2026, 5, 1, 14, 5, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CardId", "UseDate" },
                values: new object[] { 3L, new DateTime(2026, 5, 1, 14, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CardId", "UseDate" },
                values: new object[] { 4L, new DateTime(2026, 5, 1, 14, 20, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 5L, 2, new DateTime(2026, 5, 1, 14, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 1L, 3, new DateTime(2026, 5, 1, 15, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Gate", "UseDate" },
                values: new object[] { 3, new DateTime(2026, 5, 1, 15, 45, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 3L, 4, new DateTime(2026, 5, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 6L, 1, new DateTime(2026, 5, 1, 16, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 7L, 2, new DateTime(2026, 5, 1, 16, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 8L, 1, new DateTime(2026, 5, 1, 16, 45, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
