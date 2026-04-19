using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class newdatafix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 42L,
                column: "Description",
                value: "Napijegy Diák");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Description",
                value: "Havi bérlet Diák");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Description",
                value: "10 alkalmas bérlet Diák");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 42L,
                column: "Description",
                value: "Napijegy");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Description",
                value: "Havi bérlet");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Description",
                value: "10 alkalmas bérlet");
        }
    }
}
