using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class trainingDataModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Description", "Image", "Name" },
                values: new object[] { "Aerobic workout", "aerobics.jpg", "Aerobics" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Description", "Image", "Name" },
                values: new object[] { "Pool workout", "aqua.jpg", "Aqua Aerobics" });
        }
    }
}
