using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStartTimeEndTimeUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trainings_EndTime",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_StartTime",
                table: "Trainings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trainings_EndTime",
                table: "Trainings",
                column: "EndTime",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_StartTime",
                table: "Trainings",
                column: "StartTime",
                unique: true);
        }
    }
}
