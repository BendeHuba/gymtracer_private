using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class newdatafix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 28L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 29L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 30L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "Active", "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { 20L, true, "Nyílt nap a személyi edzésre", new DateTime(2026, 6, 25, 19, 0, 0, 0, DateTimeKind.Unspecified), "pt.jpg", 5ul, "Személyi edzés Demo", new DateTime(2026, 6, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), 7L });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "admin123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Password",
                value: "ricsistaff1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Password",
                value: "elemeredzo1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Password",
                value: "eszteredzo1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Password",
                value: "erikedzo123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Password",
                value: "editedzo123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Password",
                value: "endreedzo12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Password",
                value: "janos.kovacs12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Password",
                value: "anna.nagy12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Password",
                value: "peter.szabo12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Password",
                value: "maria.toth123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Password",
                value: "laszlo.kiss12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Password",
                value: "eva.varga1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Password",
                value: "gabor.molnar123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Password",
                value: "judit.farkas123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Password",
                value: "zoltan.balogh12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Password",
                value: "andrea.papp1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Password",
                value: "miklos.takacs12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Password",
                value: "katalin.juhasz1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Password",
                value: "csaba.lakatos12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Password",
                value: "krisztinameszaros123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Password",
                value: "attila.simon12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Password",
                value: "agnes.fekete12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Password",
                value: "zsolt.toth123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Password",
                value: "ildiko.szilagyi123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Password",
                value: "balazs.torok1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Password",
                value: "dora.feher123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 28L,
                column: "Password",
                value: "tamas.racz123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 29L,
                column: "Password",
                value: "viktoria.kis123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 30L,
                column: "Password",
                value: "gergo.gal1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "IsActive", "IsStudent", "MaxUsage", "Price", "Tax_key", "TrainingId", "Type" },
                values: new object[,]
                {
                    { 42L, "Személyi edzés Jegy", true, false, 1ul, 5000ul, 27.00m, 20L, 0 },
                    { 43L, "Személyi edzés Diák", true, true, 1ul, 4000ul, 27.00m, 20L, 0 }
                });
        }
    }
}
