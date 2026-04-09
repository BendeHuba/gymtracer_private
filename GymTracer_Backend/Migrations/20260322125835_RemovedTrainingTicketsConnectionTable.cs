using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTrainingTicketsConnectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingTickets");

            migrationBuilder.AddColumn<long>(
                name: "TrainingId",
                table: "Tickets",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1L,
                column: "TrainingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2L,
                column: "TrainingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3L,
                column: "TrainingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4L,
                column: "TrainingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5L,
                column: "TrainingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 6L,
                column: "TrainingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Description", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Yoga Basics", 1L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Description", "TrainingId" },
                values: new object[] { "Student Training Ticket - Yoga Basics", 1L });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "IsActive", "IsStudent", "MaxUsage", "Price", "Tax_key", "TrainingId", "Type" },
                values: new object[,]
                {
                    { 9L, "Standard Training Ticket - HIIT Blast", true, false, 1ul, 6000ul, 27.00m, 2L, 0 },
                    { 10L, "Student Training Ticket - HIIT Blast", true, true, 1ul, 3000ul, 27.00m, 2L, 0 },
                    { 11L, "Standard Training Ticket - Pilates Core", true, false, 1ul, 6000ul, 27.00m, 3L, 0 },
                    { 12L, "Student Training Ticket - Pilates Core", true, true, 1ul, 3000ul, 27.00m, 3L, 0 },
                    { 13L, "Standard Training Ticket - Zumba", true, false, 1ul, 6000ul, 27.00m, 4L, 0 },
                    { 14L, "Student Training Ticket - Zumba", true, true, 1ul, 3000ul, 27.00m, 4L, 0 },
                    { 15L, "Standard Training Ticket - CrossFit", true, false, 1ul, 6000ul, 27.00m, 5L, 0 },
                    { 16L, "Student Training Ticket - CrossFit", true, true, 1ul, 3000ul, 27.00m, 5L, 0 },
                    { 17L, "Standard Training Ticket - Spin Class", true, false, 1ul, 6000ul, 27.00m, 6L, 0 },
                    { 18L, "Student Training Ticket - Spin Class", true, true, 1ul, 3000ul, 27.00m, 6L, 0 },
                    { 19L, "Standard Training Ticket - Boxing", true, false, 1ul, 6000ul, 27.00m, 7L, 0 },
                    { 20L, "Student Training Ticket - Boxing", true, true, 1ul, 3000ul, 27.00m, 7L, 0 },
                    { 21L, "Standard Training Ticket - Stretching", true, false, 1ul, 6000ul, 27.00m, 8L, 0 },
                    { 22L, "Student Training Ticket - Stretching", true, true, 1ul, 3000ul, 27.00m, 8L, 0 },
                    { 23L, "Standard Training Ticket - Powerlifting", true, false, 1ul, 6000ul, 27.00m, 9L, 0 },
                    { 24L, "Student Training Ticket - Powerlifting", true, true, 1ul, 3000ul, 27.00m, 9L, 0 },
                    { 25L, "Standard Training Ticket - Aqua Aerobics", true, false, 1ul, 6000ul, 27.00m, 10L, 0 },
                    { 26L, "Student Training Ticket - Aqua Aerobics", true, true, 1ul, 3000ul, 27.00m, 10L, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TrainingId",
                table: "Tickets",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Trainings_TrainingId",
                table: "Tickets",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Trainings_TrainingId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TrainingId",
                table: "Tickets");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "TrainingTickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TicketId = table.Column<long>(type: "bigint", nullable: false),
                    TrainingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTickets_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingTickets_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Description",
                value: "Standard Training Ticket");

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Description",
                value: "Student Training Ticket");

            migrationBuilder.InsertData(
                table: "TrainingTickets",
                columns: new[] { "Id", "TicketId", "TrainingId" },
                values: new object[,]
                {
                    { 1L, 7L, 1L },
                    { 2L, 7L, 2L },
                    { 3L, 7L, 3L },
                    { 4L, 7L, 4L },
                    { 5L, 7L, 5L },
                    { 6L, 7L, 6L },
                    { 7L, 7L, 7L },
                    { 8L, 7L, 8L },
                    { 9L, 7L, 9L },
                    { 10L, 7L, 10L },
                    { 11L, 8L, 1L },
                    { 12L, 8L, 2L },
                    { 13L, 8L, 3L },
                    { 14L, 8L, 4L },
                    { 15L, 8L, 5L },
                    { 16L, 8L, 6L },
                    { 17L, 8L, 7L },
                    { 18L, 8L, 8L },
                    { 19L, 8L, 9L },
                    { 20L, 8L, 10L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTickets_TicketId",
                table: "TrainingTickets",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTickets_TrainingId_TicketId",
                table: "TrainingTickets",
                columns: new[] { "TrainingId", "TicketId" },
                unique: true);
        }
    }
}
