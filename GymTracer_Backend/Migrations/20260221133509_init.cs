using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    IsStudent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Price = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Tax_key = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    MaxUsage = table.Column<ulong>(type: "bigint unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.CheckConstraint("Tax_key_positive", "\"Tax_key\" >= 0");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Code = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IssuerId = table.Column<long>(type: "bigint", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TotalPrice = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Users_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TokenString = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TrainerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MaxParticipant = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsageLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CardId = table.Column<long>(type: "bigint", nullable: false),
                    UseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageLogs_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TicketId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsageAmount = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTickets_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTickets_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TrainingTickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TrainingId = table.Column<long>(type: "bigint", nullable: false),
                    TicketId = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TrainingUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TrainingId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OnWaitinglist = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Presence = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingUsers_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "IsStudent", "MaxUsage", "Price", "Tax_key", "Type" },
                values: new object[,]
                {
                    { 1L, "Standard Daily Pass", false, 1ul, 5000ul, 27.00m, 1 },
                    { 2L, "Student Daily Pass", true, 1ul, 2500ul, 27.00m, 1 },
                    { 3L, "Monthly Full Pass", false, null, 45000ul, 27.00m, 2 },
                    { 4L, "Student Monthly Pass", true, null, 22500ul, 27.00m, 2 },
                    { 5L, "10-Occasion Pass", false, 10ul, 40000ul, 27.00m, 3 },
                    { 6L, "Student 10-Occasion", true, 10ul, 20000ul, 27.00m, 3 },
                    { 7L, "Standard Training Ticket", false, 1ul, 6000ul, 27.00m, 0 },
                    { 8L, "Student Training Ticket", true, 1ul, 3000ul, 27.00m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1L, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), "alice@example.com", "Alice Smith", "hashed_pw_1", 0 },
                    { 2L, new DateTime(1985, 10, 22, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 11, 9, 30, 0, 0, DateTimeKind.Utc), "bob@example.com", "Bob Jones", "hashed_pw_2", 0 },
                    { 3L, new DateTime(1992, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 12, 10, 15, 0, 0, DateTimeKind.Utc), "charlie@example.com", "Charlie Brown", "hashed_pw_3", 1 },
                    { 4L, new DateTime(1988, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 13, 11, 45, 0, 0, DateTimeKind.Utc), "diana@example.com", "Diana Prince", "hashed_pw_4", 0 },
                    { 5L, new DateTime(1995, 7, 19, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 14, 14, 20, 0, 0, DateTimeKind.Utc), "evan@example.com", "Evan Wright", "hashed_pw_5", 0 },
                    { 6L, new DateTime(1993, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 15, 16, 5, 0, 0, DateTimeKind.Utc), "fiona@example.com", "Fiona Gallagher", "hashed_pw_6", 1 },
                    { 7L, new DateTime(1980, 9, 30, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 16, 18, 50, 0, 0, DateTimeKind.Utc), "george@example.com", "George Miller", "hashed_pw_7", 0 },
                    { 8L, new DateTime(1998, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 17, 7, 10, 0, 0, DateTimeKind.Utc), "hannah@example.com", "Hannah Abbott", "hashed_pw_8", 0 },
                    { 9L, new DateTime(1975, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 18, 12, 35, 0, 0, DateTimeKind.Utc), "ian@example.com", "Ian Malcolm", "hashed_pw_9", 0 },
                    { 10L, new DateTime(1999, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 19, 15, 25, 0, 0, DateTimeKind.Utc), "jane@example.com", "Jane Doe", "hashed_pw_10", 0 },
                    { 11L, new DateTime(1999, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 19, 15, 25, 0, 0, DateTimeKind.Utc), "tesztelek@example.com", "Teszt Elek", "hashed_pw_10", 2 },
                    { 12L, new DateTime(1999, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 19, 15, 25, 0, 0, DateTimeKind.Utc), "gitaron@example.com", "Git Áron", "hashed_pw_10", 3 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Code", "CreatedAt", "RevokedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), new DateTime(2023, 1, 20, 10, 0, 0, 0, DateTimeKind.Utc), null, 1L },
                    { 2L, new Guid("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"), new DateTime(2023, 1, 21, 11, 0, 0, 0, DateTimeKind.Utc), null, 2L },
                    { 3L, new Guid("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"), new DateTime(2023, 1, 22, 12, 0, 0, 0, DateTimeKind.Utc), null, 3L },
                    { 4L, new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"), new DateTime(2023, 1, 23, 13, 0, 0, 0, DateTimeKind.Utc), null, 4L },
                    { 5L, new Guid("e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b"), new DateTime(2023, 1, 24, 14, 0, 0, 0, DateTimeKind.Utc), null, 5L },
                    { 6L, new Guid("f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c"), new DateTime(2023, 1, 25, 15, 0, 0, 0, DateTimeKind.Utc), null, 6L },
                    { 7L, new Guid("a7b8c9d0-e1f2-3a4b-5c6d-7e8f9a0b1c2d"), new DateTime(2023, 1, 26, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7L },
                    { 8L, new Guid("b8c9d0e1-f2a3-4b5c-6d7e-8f9a0b1c2d3e"), new DateTime(2023, 1, 27, 17, 0, 0, 0, DateTimeKind.Utc), null, 8L },
                    { 9L, new Guid("c9d0e1f2-a3b4-5c6d-7e8f-9a0b1c2d3e4f"), new DateTime(2023, 1, 28, 18, 0, 0, 0, DateTimeKind.Utc), null, 9L },
                    { 10L, new Guid("d0e1f2a3-b4c5-6d7e-8f9a-0b1c2d3e4f5a"), new DateTime(2023, 1, 29, 19, 0, 0, 0, DateTimeKind.Utc), null, 10L }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1L, new DateTime(2023, 1, 30, 10, 0, 0, 0, DateTimeKind.Utc), "REC-00001", 5000ul },
                    { 2L, new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2L, new DateTime(2023, 2, 4, 11, 0, 0, 0, DateTimeKind.Utc), "REC-00002", 45000ul },
                    { 3L, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3L, new DateTime(2023, 2, 9, 12, 0, 0, 0, DateTimeKind.Utc), "REC-00003", 20000ul },
                    { 4L, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4L, new DateTime(2023, 2, 14, 13, 0, 0, 0, DateTimeKind.Utc), "REC-00004", 3000ul },
                    { 5L, new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), 5L, new DateTime(2023, 2, 19, 14, 0, 0, 0, DateTimeKind.Utc), "REC-00005", 40000ul },
                    { 6L, new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), 6L, new DateTime(2023, 2, 24, 15, 0, 0, 0, DateTimeKind.Utc), "REC-00006", 2500ul },
                    { 7L, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7L, new DateTime(2023, 2, 28, 16, 0, 0, 0, DateTimeKind.Utc), "REC-00007", 22500ul },
                    { 8L, new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), 8L, null, "REC-00008", 3500ul },
                    { 9L, new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 9L, new DateTime(2023, 3, 9, 18, 0, 0, 0, DateTimeKind.Utc), "REC-00009", 15000ul },
                    { 10L, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 10L, new DateTime(2023, 3, 14, 19, 0, 0, 0, DateTimeKind.Utc), "REC-00010", 400000ul }
                });

            migrationBuilder.InsertData(
                table: "Tokens",
                columns: new[] { "Id", "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 4, 1, 8, 0, 0, 0, DateTimeKind.Utc), null, "token_abc123def456", 1L },
                    { 2L, new DateTime(2023, 4, 2, 9, 0, 0, 0, DateTimeKind.Utc), null, "token_bcd234efg567", 2L },
                    { 3L, new DateTime(2023, 4, 3, 10, 0, 0, 0, DateTimeKind.Utc), null, "token_cde345fgh678", 3L },
                    { 4L, new DateTime(2023, 4, 4, 11, 0, 0, 0, DateTimeKind.Utc), null, "token_def456ghi789", 4L },
                    { 5L, new DateTime(2023, 4, 5, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), "token_efg567hij890", 5L },
                    { 6L, new DateTime(2023, 4, 6, 13, 0, 0, 0, DateTimeKind.Utc), null, "token_fgh678ijk901", 6L },
                    { 7L, new DateTime(2023, 4, 7, 14, 0, 0, 0, DateTimeKind.Utc), null, "token_ghi789jkl012", 7L },
                    { 8L, new DateTime(2023, 4, 8, 15, 0, 0, 0, DateTimeKind.Utc), null, "token_hij890klm123", 8L },
                    { 9L, new DateTime(2023, 4, 9, 16, 0, 0, 0, DateTimeKind.Utc), null, "token_ijk901lmn234", 9L },
                    { 10L, new DateTime(2023, 4, 10, 17, 0, 0, 0, DateTimeKind.Utc), null, "token_jkl012mno345", 10L }
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[,]
                {
                    { 1L, "Intro to Yoga", new DateTime(2023, 5, 1, 9, 0, 0, 0, DateTimeKind.Utc), "yoga.jpg", 20ul, "Yoga Basics", new DateTime(2023, 5, 1, 8, 0, 0, 0, DateTimeKind.Utc), 3L },
                    { 2L, "High Intensity Interval Training", new DateTime(2023, 5, 1, 11, 0, 0, 0, DateTimeKind.Utc), "hiit.jpg", 15ul, "HIIT Blast", new DateTime(2023, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc), 6L },
                    { 3L, "Core strengthening", new DateTime(2023, 5, 2, 9, 0, 0, 0, DateTimeKind.Utc), "pilates.jpg", 15ul, "Pilates Core", new DateTime(2023, 5, 2, 8, 0, 0, 0, DateTimeKind.Utc), 3L },
                    { 4L, "Dance cardio", new DateTime(2023, 5, 2, 19, 0, 0, 0, DateTimeKind.Utc), "zumba.jpg", 25ul, "Zumba Dance", new DateTime(2023, 5, 2, 18, 0, 0, 0, DateTimeKind.Utc), 6L },
                    { 5L, "Learn the basics of CrossFit", new DateTime(2023, 5, 3, 18, 0, 0, 0, DateTimeKind.Utc), "crossfit.jpg", 10ul, "CrossFit Intro", new DateTime(2023, 5, 3, 17, 0, 0, 0, DateTimeKind.Utc), 3L },
                    { 6L, "Indoor cycling", new DateTime(2023, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), "spin.jpg", 20ul, "Spin Class", new DateTime(2023, 5, 4, 7, 0, 0, 0, DateTimeKind.Utc), 6L },
                    { 7L, "Pad work and technique", new DateTime(2023, 5, 5, 20, 0, 0, 0, DateTimeKind.Utc), "boxing.jpg", 12ul, "Boxing Fundamentals", new DateTime(2023, 5, 5, 19, 0, 0, 0, DateTimeKind.Utc), 3L },
                    { 8L, "Recovery session", new DateTime(2023, 5, 6, 11, 0, 0, 0, DateTimeKind.Utc), "stretch.jpg", 20ul, "Stretching & Mobility", new DateTime(2023, 5, 6, 10, 0, 0, 0, DateTimeKind.Utc), 6L },
                    { 9L, "Squat, Bench, Deadlift", new DateTime(2023, 5, 7, 17, 30, 0, 0, DateTimeKind.Utc), "power.jpg", 8ul, "Powerlifting 101", new DateTime(2023, 5, 7, 16, 0, 0, 0, DateTimeKind.Utc), 3L },
                    { 10L, "Pool workout", new DateTime(2023, 5, 8, 10, 0, 0, 0, DateTimeKind.Utc), "aqua.jpg", 15ul, "Aqua Aerobics", new DateTime(2023, 5, 8, 9, 0, 0, 0, DateTimeKind.Utc), 6L }
                });

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

            migrationBuilder.InsertData(
                table: "TrainingUsers",
                columns: new[] { "Id", "ApplicationDate", "OnWaitinglist", "Presence", "TrainingId", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 4, 25, 10, 0, 0, 0, DateTimeKind.Utc), false, true, 1L, 1L },
                    { 2L, new DateTime(2023, 4, 26, 11, 0, 0, 0, DateTimeKind.Utc), false, true, 2L, 2L },
                    { 3L, new DateTime(2023, 4, 27, 12, 0, 0, 0, DateTimeKind.Utc), false, false, 3L, 4L },
                    { 4L, new DateTime(2023, 4, 28, 13, 0, 0, 0, DateTimeKind.Utc), true, false, 4L, 5L },
                    { 5L, new DateTime(2023, 4, 29, 14, 0, 0, 0, DateTimeKind.Utc), false, true, 5L, 7L },
                    { 6L, new DateTime(2023, 4, 30, 15, 0, 0, 0, DateTimeKind.Utc), false, true, 6L, 8L },
                    { 7L, new DateTime(2023, 5, 1, 16, 0, 0, 0, DateTimeKind.Utc), false, false, 7L, 9L },
                    { 8L, new DateTime(2023, 5, 2, 17, 0, 0, 0, DateTimeKind.Utc), false, true, 8L, 10L },
                    { 9L, new DateTime(2023, 5, 3, 18, 0, 0, 0, DateTimeKind.Utc), true, false, 9L, 1L },
                    { 10L, new DateTime(2023, 5, 4, 19, 0, 0, 0, DateTimeKind.Utc), false, true, 10L, 2L }
                });

            migrationBuilder.InsertData(
                table: "UsageLogs",
                columns: new[] { "Id", "CardId", "Gate", "UseDate" },
                values: new object[,]
                {
                    { 1L, 1L, 1, new DateTime(2023, 5, 1, 7, 45, 0, 0, DateTimeKind.Utc) },
                    { 2L, 2L, 2, new DateTime(2023, 5, 1, 9, 50, 0, 0, DateTimeKind.Utc) },
                    { 3L, 3L, 1, new DateTime(2023, 5, 2, 7, 40, 0, 0, DateTimeKind.Utc) },
                    { 4L, 4L, 0, new DateTime(2023, 5, 2, 17, 50, 0, 0, DateTimeKind.Utc) },
                    { 5L, 5L, 2, new DateTime(2023, 5, 3, 16, 45, 0, 0, DateTimeKind.Utc) },
                    { 6L, 6L, 1, new DateTime(2023, 5, 4, 6, 50, 0, 0, DateTimeKind.Utc) },
                    { 7L, 7L, 2, new DateTime(2023, 5, 5, 18, 45, 0, 0, DateTimeKind.Utc) },
                    { 8L, 8L, 1, new DateTime(2023, 5, 6, 9, 55, 0, 0, DateTimeKind.Utc) },
                    { 9L, 9L, 0, new DateTime(2023, 5, 7, 15, 45, 0, 0, DateTimeKind.Utc) },
                    { 10L, 10L, 2, new DateTime(2023, 5, 8, 8, 50, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "UserTickets",
                columns: new[] { "Id", "CreationDate", "ExpirationDate", "PaymentId", "TicketId", "UsageAmount", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 1, 30, 10, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 31, 23, 59, 59, 0, DateTimeKind.Utc), 1L, 1L, 1ul, 1L },
                    { 2L, new DateTime(2023, 2, 4, 11, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 4, 23, 59, 59, 0, DateTimeKind.Utc), 2L, 3L, 0ul, 2L },
                    { 3L, new DateTime(2023, 2, 9, 12, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 5, 9, 23, 59, 59, 0, DateTimeKind.Utc), 3L, 6L, 5ul, 3L },
                    { 4L, new DateTime(2023, 2, 14, 13, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 2, 15, 23, 59, 59, 0, DateTimeKind.Utc), 4L, 7L, 1ul, 4L },
                    { 5L, new DateTime(2023, 2, 19, 14, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 5, 19, 23, 59, 59, 0, DateTimeKind.Utc), 5L, 5L, 2ul, 5L },
                    { 6L, new DateTime(2023, 2, 24, 15, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 2, 25, 23, 59, 59, 0, DateTimeKind.Utc), 6L, 2L, 1ul, 6L },
                    { 7L, new DateTime(2023, 2, 28, 16, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 28, 23, 59, 59, 0, DateTimeKind.Utc), 7L, 4L, 0ul, 7L },
                    { 8L, new DateTime(2023, 3, 5, 8, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 6, 12, 0, 0, 0, DateTimeKind.Utc), 8L, 3L, 0ul, 8L },
                    { 9L, new DateTime(2023, 3, 9, 18, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 4, 9, 23, 59, 59, 0, DateTimeKind.Utc), 9L, 2L, 4ul, 9L },
                    { 10L, new DateTime(2023, 3, 14, 19, 5, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 14, 23, 59, 59, 0, DateTimeKind.Utc), 10L, 8L, 0ul, 10L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Code",
                table: "Cards",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IssuerId",
                table: "Payments",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ReceiptNumber",
                table: "Payments",
                column: "ReceiptNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_TokenString",
                table: "Tokens",
                column: "TokenString",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainerId",
                table: "Trainings",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTickets_TicketId",
                table: "TrainingTickets",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTickets_TrainingId_TicketId",
                table: "TrainingTickets",
                columns: new[] { "TrainingId", "TicketId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingUsers_TrainingId_UserId",
                table: "TrainingUsers",
                columns: new[] { "TrainingId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingUsers_UserId",
                table: "TrainingUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageLogs_CardId",
                table: "UsageLogs",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_PaymentId",
                table: "UserTickets",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_TicketId",
                table: "UserTickets",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_UserId",
                table: "UserTickets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "TrainingTickets");

            migrationBuilder.DropTable(
                name: "TrainingUsers");

            migrationBuilder.DropTable(
                name: "UsageLogs");

            migrationBuilder.DropTable(
                name: "UserTickets");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
