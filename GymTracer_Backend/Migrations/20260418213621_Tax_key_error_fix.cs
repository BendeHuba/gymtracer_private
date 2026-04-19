using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class Tax_key_error_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Tax_key_positive",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Email",
                value: "janos.kovacs@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Email",
                value: "anna.nagy@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Email",
                value: "peter.szabo@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Email",
                value: "maria.toth@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Email",
                value: "laszlo.kiss@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Email",
                value: "eva.varga@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Email",
                value: "gabor.molnar@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Email",
                value: "judit.farkas@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Email",
                value: "zoltan.balogh@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Email",
                value: "andrea.papp@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Email",
                value: "miklos.takacs@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Email",
                value: "katalin.juhasz@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Email",
                value: "csaba.lakatos@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Email",
                value: "krisztina.meszaros@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Email",
                value: "attila.simon@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Email",
                value: "agnes.fekete@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Email",
                value: "zsolt.toth@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Email",
                value: "ildiko.szilagyi@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Email",
                value: "balazs.torok@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Email",
                value: "dora.feher@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 28L,
                column: "Email",
                value: "tamas.racz@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 29L,
                column: "Email",
                value: "viktoria.kis@email.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 30L,
                column: "Email",
                value: "gergo.gal@email.com");

            migrationBuilder.AddCheckConstraint(
                name: "Tax_key_positive",
                table: "Tickets",
                sql: "`Tax_key` >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Tax_key_positive",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Email",
                value: "janos.kovacs@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Email",
                value: "anna.nagy@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Email",
                value: "peter.szabo@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Email",
                value: "maria.toth@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Email",
                value: "laszlo.kiss@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Email",
                value: "eva.varga@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Email",
                value: "gabor.molnar@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Email",
                value: "judit.farkas@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Email",
                value: "zoltan.balogh@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Email",
                value: "andrea.papp@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Email",
                value: "miklos.takacs@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Email",
                value: "katalin.juhasz@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Email",
                value: "csaba.lakatos@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Email",
                value: "krisztina.meszaros@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Email",
                value: "attila.simon@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Email",
                value: "agnes.fekete@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Email",
                value: "zsolt.toth@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Email",
                value: "ildiko.szilagyi@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Email",
                value: "balazs.torok@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Email",
                value: "dora.feher@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 28L,
                column: "Email",
                value: "tamas.racz@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 29L,
                column: "Email",
                value: "viktoria.kis@gmail.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 30L,
                column: "Email",
                value: "gergo.gal@gmail.com");

            migrationBuilder.AddCheckConstraint(
                name: "Tax_key_positive",
                table: "Tickets",
                sql: "\"Tax_key\" >= 0");
        }
    }
}
