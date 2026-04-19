using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class newdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1985, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "admin@gym.hu", "Adminisztrátor Anna", "admin123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 3 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1995, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "ricsi.staff@gym.hu", "Recepciós Ricsi", "ricsistaff1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(1990, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "elemer.edzo@gym.hu", "Edző Elemér", "elemeredzo1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1992, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "eszter.edzo@gym.hu", "Edző Eszter", "eszteredzo1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1988, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "erik.edzo@gym.hu", "Edző Erik", "erikedzo123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(1994, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "edit.edzo@gym.hu", "Edző Edit", "editedzo123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1991, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "endre.edzo@gym.hu", "Edző Endre", "endreedzo12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(2001, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), "janos.kovacs@gmail.com", "Kovács János", "janos.kovacs12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(1998, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 9, 20, 0, 0, DateTimeKind.Unspecified), "anna.nagy@gmail.com", "Nagy Anna", "anna.nagy12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(2003, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), "peter.szabo@gmail.com", "Szabó Péter", "peter.szabo12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1995, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 9, 40, 0, 0, DateTimeKind.Unspecified), "maria.toth@gmail.com", "Tóth Mária", "maria.toth123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1990, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 9, 45, 0, 0, DateTimeKind.Unspecified), "laszlo.kiss@gmail.com", "Kiss László", "laszlo.kiss12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 13L, true, new DateTime(1987, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 9, 50, 0, 0, DateTimeKind.Unspecified), "eva.varga@gmail.com", "Varga Éva", "eva.varga1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 14L, true, new DateTime(1992, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 9, 55, 0, 0, DateTimeKind.Unspecified), "gabor.molnar@gmail.com", "Molnár Gábor", "gabor.molnar123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 15L, true, new DateTime(1999, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "judit.farkas@gmail.com", "Farkas Judit", "judit.farkas123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 16L, true, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 10, 0, 0, DateTimeKind.Unspecified), "zoltan.balogh@gmail.com", "Balogh Zoltán", "zoltan.balogh12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 17L, true, new DateTime(1985, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 15, 0, 0, DateTimeKind.Unspecified), "andrea.papp@gmail.com", "Papp Andrea", "andrea.papp1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 18L, true, new DateTime(1994, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 20, 0, 0, DateTimeKind.Unspecified), "miklos.takacs@gmail.com", "Takács Miklós", "miklos.takacs12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 19L, true, new DateTime(1996, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 25, 0, 0, DateTimeKind.Unspecified), "katalin.juhasz@gmail.com", "Juhász Katalin", "katalin.juhasz1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 20L, true, new DateTime(1991, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "csaba.lakatos@gmail.com", "Lakatos Csaba", "csaba.lakatos12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 21L, true, new DateTime(2002, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 35, 0, 0, DateTimeKind.Unspecified), "krisztina.meszaros@gmail.com", "Mészáros Krisztina", "krisztinameszaros123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 22L, true, new DateTime(1989, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 40, 0, 0, DateTimeKind.Unspecified), "attila.simon@gmail.com", "Simon Attila", "attila.simon12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 23L, true, new DateTime(1997, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 45, 0, 0, DateTimeKind.Unspecified), "agnes.fekete@gmail.com", "Fekete Ágnes", "agnes.fekete12300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 24L, true, new DateTime(1986, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 50, 0, 0, DateTimeKind.Unspecified), "zsolt.toth@gmail.com", "Tóth Zsolt", "zsolt.toth123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 25L, true, new DateTime(1993, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 10, 55, 0, 0, DateTimeKind.Unspecified), "ildiko.szilagyi@gmail.com", "Szilágyi Ildikó", "ildiko.szilagyi123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 26L, true, new DateTime(1998, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), "balazs.torok@gmail.com", "Török Balázs", "balazs.torok1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 27L, true, new DateTime(2004, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 11, 5, 0, 0, DateTimeKind.Unspecified), "dora.feher@gmail.com", "Fehér Dóra", "dora.feher123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 28L, true, new DateTime(1995, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 11, 10, 0, 0, DateTimeKind.Unspecified), "tamas.racz@gmail.com", "Rácz Tamás", "tamas.racz123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 29L, true, new DateTime(1992, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 11, 15, 0, 0, DateTimeKind.Unspecified), "viktoria.kis@gmail.com", "Kis Viktória", "viktoria.kis123000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 },
                    { 30L, true, new DateTime(1990, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 11, 20, 0, 0, DateTimeKind.Unspecified), "gergo.gal@gmail.com", "Gál Gergő", "gergo.gal1230000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0 }
                });
            migrationBuilder.UpdateData(
               table: "Cards",
               keyColumn: "Id",
               keyValue: 7L,
               columns: new[] { "Code", "CreatedAt", "RevokedAt", "UserId" },
               values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a007"), new DateTime(2026, 5, 1, 12, 30, 0, 0, DateTimeKind.Unspecified), null, 14L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a008"), new DateTime(2026, 5, 1, 12, 35, 0, 0, DateTimeKind.Unspecified), 15L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a009"), new DateTime(2026, 5, 1, 12, 40, 0, 0, DateTimeKind.Unspecified), 16L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a010"), new DateTime(2026, 5, 1, 12, 45, 0, 0, DateTimeKind.Unspecified), 17L });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Code", "CreatedAt", "RevokedAt", "UserId" },
                values: new object[,]
                {
                    { 11L, new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a011"), new DateTime(2026, 5, 1, 12, 50, 0, 0, DateTimeKind.Unspecified), null, 18L },
                    { 12L, new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a012"), new DateTime(2026, 5, 1, 12, 55, 0, 0, DateTimeKind.Unspecified), null, 19L },
                    { 13L, new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a013"), new DateTime(2026, 5, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), null, 20L }
                });


            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a001"), new DateTime(2026, 5, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 8L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a002"), new DateTime(2026, 5, 1, 12, 5, 0, 0, DateTimeKind.Unspecified), 9L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a003"), new DateTime(2026, 5, 1, 12, 10, 0, 0, DateTimeKind.Unspecified), 10L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a004"), new DateTime(2026, 5, 1, 12, 15, 0, 0, DateTimeKind.Unspecified), 11L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("ae25016a-edd0-4e2a-85cf-5fb04f53a005"), new DateTime(2026, 5, 1, 12, 20, 0, 0, DateTimeKind.Unspecified), 12L });


            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 8L, new DateTime(2026, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "INV-202605-001", 2000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 9L, new DateTime(2026, 5, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), "INV-202605-002", 18000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), 10L, new DateTime(2026, 5, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), "INV-202605-003", 2500ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 11L, new DateTime(2026, 5, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "INV-202605-004", 22000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 15, 0, 0, DateTimeKind.Unspecified), 12L, new DateTime(2026, 5, 1, 12, 15, 0, 0, DateTimeKind.Unspecified), "INV-202605-005", 1800ul });

           
            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Napijegy", 2500ul });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "IsStudent", "MaxUsage", "Price", "Type" },
                values: new object[] { "Havi bérlet", false, null, 18000ul, 2 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Description", "MaxUsage", "Price", "Type" },
                values: new object[] { "10 alkalmas bérlet", 10ul, 22000ul, 3 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Description", "IsStudent", "MaxUsage", "Price", "TrainingId", "Type" },
                values: new object[] { "Reggeli Jóga Jegy", false, 1ul, 2000ul, 1L, 0 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Description", "IsStudent", "MaxUsage", "Price", "TrainingId", "Type" },
                values: new object[] { "Reggeli Jóga Diák", true, 1ul, 1500ul, 1L, 0 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Description", "IsStudent", "MaxUsage", "Price", "TrainingId", "Type" },
                values: new object[] { "CrossFit Kezdő Jegy", false, 1ul, 2500ul, 2L, 0 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "CrossFit Kezdő Diák", true, 1800ul, 2L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Haladó TRX Jegy", false, 2200ul, 3L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Haladó TRX Diák", true, 1600ul, 3L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Zumba Fit Jegy", false, 1800ul, 4L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Zumba Fit Diák", true, 1400ul, 4L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Súlyemelés Jegy", false, 2500ul, 5L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Súlyemelés Diák", true, 1800ul, 5L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Spinning 1 Jegy", false, 2000ul, 6L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Spinning 1 Diák", true, 1500ul, 6L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Pilates Jegy", false, 2000ul, 7L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Pilates Diák", true, 1500ul, 7L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Kettlebell Jegy", false, 2200ul, 8L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Kettlebell Diák", true, 1600ul, 8L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Box edzés Jegy", false, 2500ul, 9L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Box edzés Diák", true, 1800ul, 9L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "HIIT Jegy", false, 2000ul, 10L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "HIIT Diák", true, 1500ul, 10L });

            

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString" },
                values: new object[] { new DateTime(2026, 5, 1, 8, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 8, 5, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A360" });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A361", 8L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 10, 15, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A362", 9L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A363", 10L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 10, 45, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A364", 11L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A365", 12L });

           
            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ApplicationDate", "Presence", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 10, 0, 0, DateTimeKind.Unspecified), false, 8L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ApplicationDate", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 15, 0, 0, DateTimeKind.Unspecified), false, 1L, 9L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ApplicationDate", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 20, 0, 0, DateTimeKind.Unspecified), 2L, 10L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "ApplicationDate", "OnWaitinglist", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 25, 0, 0, DateTimeKind.Unspecified), false, 2L, 11L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "ApplicationDate", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), false, 3L, 12L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "ApplicationDate", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 35, 0, 0, DateTimeKind.Unspecified), false, 3L, 13L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "ApplicationDate", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 40, 0, 0, DateTimeKind.Unspecified), 4L, 14L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "ApplicationDate", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 45, 0, 0, DateTimeKind.Unspecified), false, 4L, 15L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "ApplicationDate", "OnWaitinglist", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 50, 0, 0, DateTimeKind.Unspecified), false, 5L, 16L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "ApplicationDate", "OnWaitinglist", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 55, 0, 0, DateTimeKind.Unspecified), true, false, 5L, 17L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime" },
                values: new object[] { "Frissítő napindító jóga", new DateTime(2026, 5, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "yoga_morning.jpg", 15ul, "Reggeli Jóga", new DateTime(2026, 5, 5, 7, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Alapok elsajátítása", new DateTime(2026, 5, 6, 18, 30, 0, 0, DateTimeKind.Unspecified), "crossfit_basic.jpg", 10ul, "CrossFit Kezdő", new DateTime(2026, 5, 6, 17, 0, 0, 0, DateTimeKind.Unspecified), 4L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Saját testsúlyos edzés", new DateTime(2026, 5, 7, 19, 0, 0, 0, DateTimeKind.Unspecified), "trx_pro.jpg", 12ul, "Haladó TRX", new DateTime(2026, 5, 7, 18, 0, 0, 0, DateTimeKind.Unspecified), 5L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Description", "EndTime", "MaxParticipant", "Name", "StartTime" },
                values: new object[] { "Táncos kardió", new DateTime(2026, 5, 8, 20, 0, 0, 0, DateTimeKind.Unspecified), 20ul, "Zumba Fit", new DateTime(2026, 5, 8, 19, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Erőfejlesztés", new DateTime(2026, 5, 10, 17, 30, 0, 0, DateTimeKind.Unspecified), "weightlifting.jpg", 8ul, "Súlyemelés", new DateTime(2026, 5, 10, 16, 0, 0, 0, DateTimeKind.Unspecified), 7L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Kardió tekerés", new DateTime(2026, 5, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), "spinning1.jpg", 15ul, "Spinning 1", new DateTime(2026, 5, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), 3L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Core izmok erősítése", new DateTime(2026, 5, 14, 18, 0, 0, 0, DateTimeKind.Unspecified), "pilates.jpg", 15ul, "Pilates", new DateTime(2026, 5, 14, 17, 0, 0, 0, DateTimeKind.Unspecified), 4L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Dinamikus erőnléti", new DateTime(2026, 5, 15, 19, 30, 0, 0, DateTimeKind.Unspecified), "kettlebell.jpg", 12ul, "Kettlebell", new DateTime(2026, 5, 15, 18, 30, 0, 0, DateTimeKind.Unspecified), 5L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Technika és állóképesség", new DateTime(2026, 5, 18, 20, 30, 0, 0, DateTimeKind.Unspecified), "box.jpg", 10ul, "Box edzés", new DateTime(2026, 5, 18, 19, 0, 0, 0, DateTimeKind.Unspecified), 6L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Magas intenzitású intervall", new DateTime(2026, 5, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), "hiit.jpg", 20ul, "HIIT", new DateTime(2026, 5, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), 7L });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "Active", "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[,]
                {
                    { 11L, true, "Törzsizomzat fejlesztése", new DateTime(2026, 5, 22, 18, 30, 0, 0, DateTimeKind.Unspecified), "core.jpg", 15ul, "Core tréning", new DateTime(2026, 5, 22, 17, 30, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 12L, true, "Nyújtás és lazítás", new DateTime(2026, 5, 25, 20, 0, 0, 0, DateTimeKind.Unspecified), "stretching.jpg", 20ul, "Stretching", new DateTime(2026, 5, 25, 19, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 13L, true, "Összetett gyakorlatok", new DateTime(2026, 5, 27, 19, 0, 0, 0, DateTimeKind.Unspecified), "functional.jpg", 12ul, "Funkcionális edzés", new DateTime(2026, 5, 27, 18, 0, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 14L, true, "Klasszikus aerobik", new DateTime(2026, 5, 29, 18, 0, 0, 0, DateTimeKind.Unspecified), "aerobik.jpg", 25ul, "Aerobik", new DateTime(2026, 5, 29, 17, 0, 0, 0, DateTimeKind.Unspecified), 6L },
                    { 15L, true, "Szülés utáni regeneráció", new DateTime(2026, 6, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "babamama.jpg", 10ul, "Baba-mama torna", new DateTime(2026, 6, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), 7L },
                    { 16L, true, "Tartásjavító torna", new DateTime(2026, 6, 5, 17, 0, 0, 0, DateTimeKind.Unspecified), "gerinc.jpg", 15ul, "Gerinctorna", new DateTime(2026, 6, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 17L, true, "Állóképesség fejlesztés", new DateTime(2026, 6, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), "cardio.jpg", 20ul, "Kardió mix", new DateTime(2026, 6, 10, 18, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 18L, true, "Nehéz súlyok", new DateTime(2026, 6, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), "powerlifting.jpg", 8ul, "Erőemelés", new DateTime(2026, 6, 15, 17, 30, 0, 0, DateTimeKind.Unspecified), 5L },
                    { 19L, true, "Haladó tekerés", new DateTime(2026, 6, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), "spinning2.jpg", 15ul, "Spinning 2", new DateTime(2026, 6, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), 6L },
                    { 20L, true, "Nyílt nap a személyi edzésre", new DateTime(2026, 6, 25, 19, 0, 0, 0, DateTimeKind.Unspecified), "pt.jpg", 5ul, "Személyi edzés Demo", new DateTime(2026, 6, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), 7L }
                });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 1L,
                column: "UseDate",
                value: new DateTime(2026, 5, 1, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Gate", "UseDate" },
                values: new object[] { 1, new DateTime(2026, 5, 1, 14, 5, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Gate", "UseDate" },
                values: new object[] { 2, new DateTime(2026, 5, 1, 14, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Gate", "UseDate" },
                values: new object[] { 1, new DateTime(2026, 5, 1, 14, 20, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 5L,
                column: "UseDate",
                value: new DateTime(2026, 5, 1, 14, 30, 0, 0, DateTimeKind.Unspecified));

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
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 2L, 3, new DateTime(2026, 5, 1, 15, 45, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 3L, 4, new DateTime(2026, 5, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) });


            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CardId", "UseDate" },
                values: new object[] { 7L, new DateTime(2026, 5, 1, 16, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "UsageLogs",
                columns: new[] { "Id", "CardId", "Gate", "UseDate" },
                values: new object[] { 11L, 8L, 1, new DateTime(2026, 5, 1, 16, 45, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 10, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), 4L, 0ul, 8L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 11, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 2L, 9L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 11, 35, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 1L, 0ul, 10L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 3L, 0ul, 11L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 6, 18, 30, 0, 0, DateTimeKind.Unspecified), 7L, 0ul, 12L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreationDate", "ExpirationDate", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 35, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 0ul, 13L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 13, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 10, 17, 30, 0, 0, DateTimeKind.Unspecified), 12L, 14L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreationDate", "ExpirationDate", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 13, 35, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 15L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 14, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 12, 19, 0, 0, 0, DateTimeKind.Unspecified), 14L, 0ul, 16L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 14, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 14, 18, 0, 0, 0, DateTimeKind.Unspecified), 17L, 17L });

            


           
            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[,]
                {
                    { 11L, new DateTime(2026, 5, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), 18L, new DateTime(2026, 5, 1, 14, 30, 0, 0, DateTimeKind.Unspecified), "INV-202605-011", 18000ul },
                    { 12L, new DateTime(2026, 5, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), 19L, new DateTime(2026, 5, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), "INV-202605-012", 2500ul }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Description", "IsActive", "IsStudent", "MaxUsage", "Price", "Tax_key", "TrainingId", "Type" },
                values: new object[,]
                {
                    { 27L, "Stretching Diák", true, true, 1ul, 1000ul, 27.00m, 12L, 0 },
                    { 28L, "Funkcionális Jegy", true, false, 1ul, 2200ul, 27.00m, 13L, 0 },
                    { 29L, "Funkcionális Diák", true, true, 1ul, 1600ul, 27.00m, 13L, 0 },
                    { 30L, "Aerobik Jegy", true, false, 1ul, 1800ul, 27.00m, 14L, 0 },
                    { 31L, "Aerobik Diák", true, true, 1ul, 1400ul, 27.00m, 14L, 0 },
                    { 32L, "Baba-mama Jegy", true, false, 1ul, 2000ul, 27.00m, 15L, 0 },
                    { 33L, "Baba-mama Diák", true, true, 1ul, 1500ul, 27.00m, 15L, 0 },
                    { 34L, "Gerinctorna Jegy", true, false, 1ul, 1800ul, 27.00m, 16L, 0 },
                    { 35L, "Gerinctorna Diák", true, true, 1ul, 1400ul, 27.00m, 16L, 0 },
                    { 36L, "Kardió mix Jegy", true, false, 1ul, 2000ul, 27.00m, 17L, 0 },
                    { 37L, "Kardió mix Diák", true, true, 1ul, 1500ul, 27.00m, 17L, 0 },
                    { 38L, "Erőemelés Jegy", true, false, 1ul, 2500ul, 27.00m, 18L, 0 },
                    { 39L, "Erőemelés Diák", true, true, 1ul, 1800ul, 27.00m, 18L, 0 },
                    { 40L, "Spinning 2 Jegy", true, false, 1ul, 2200ul, 27.00m, 19L, 0 },
                    { 41L, "Spinning 2 Diák", true, true, 1ul, 1600ul, 27.00m, 19L, 0 },
                    { 42L, "Személyi edzés Jegy", true, false, 1ul, 5000ul, 27.00m, 20L, 0 },
                    { 43L, "Személyi edzés Diák", true, true, 1ul, 4000ul, 27.00m, 20L, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tokens",
                columns: new[] { "Id", "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[,]
                {
                    { 11L, new DateTime(2026, 5, 1, 12, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 12, 15, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A370", 17L },
                    { 12L, new DateTime(2026, 5, 1, 12, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 12, 30, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A371", 18L }
                });

            migrationBuilder.InsertData(
                table: "TrainingUsers",
                columns: new[] { "Id", "ApplicationDate", "OnWaitinglist", "Presence", "TrainingId", "UserId" },
                values: new object[] { 11L, new DateTime(2026, 5, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), false, false, 6L, 18L });

            migrationBuilder.InsertData(
                table: "UserTickets",
                columns: new[] { "Id", "CreationDate", "ExpirationDate", "PaymentId", "TicketId", "UsageAmount", "UserId" },
                values: new object[,]
                {
                    { 11L, new DateTime(2026, 5, 1, 14, 35, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 11L, 2L, 0ul, 18L },
                    { 12L, new DateTime(2026, 5, 1, 15, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), 12L, 1L, 0ul, 19L }
                });

            migrationBuilder.UpdateData(
               table: "Payments",
               keyColumn: "Id",
               keyValue: 6L,
               columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
               values: new object[] { new DateTime(2026, 5, 1, 12, 30, 0, 0, DateTimeKind.Unspecified), 13L, new DateTime(2026, 5, 1, 12, 30, 0, 0, DateTimeKind.Unspecified), "INV-202605-006", 18000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 14L, new DateTime(2026, 5, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), "INV-202605-007", 2500ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), 15L, new DateTime(2026, 5, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "INV-202605-008", 22000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), 16L, new DateTime(2026, 5, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), "INV-202605-009", 2000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2026, 5, 1, 14, 15, 0, 0, DateTimeKind.Unspecified), 17L, new DateTime(2026, 5, 1, 14, 15, 0, 0, DateTimeKind.Unspecified), "INV-202605-010", 1500ul });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Core tréning Jegy", false, 1800ul, 11L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Core tréning Diák", true, 1400ul, 11L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Stretching Jegy", false, 1500ul, 12L });

            migrationBuilder.UpdateData(
               table: "Tokens",
               keyColumn: "Id",
               keyValue: 7L,
               columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
               values: new object[] { new DateTime(2026, 5, 1, 11, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 11, 15, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A366", 13L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A367", 14L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 11, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 11, 45, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A368", 15L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), "575D6F6985153B428B4BEF8FD525BEA1F78E502B9DA6654A1EB774997C06B9F30FF132D9A44F296E176767A148175538579C459051474120FB85530D89C7A369", 16L });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), new DateTime(2023, 1, 20, 10, 0, 0, 0, DateTimeKind.Utc), 1L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"), new DateTime(2023, 1, 21, 11, 0, 0, 0, DateTimeKind.Utc), 2L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"), new DateTime(2023, 1, 22, 12, 0, 0, 0, DateTimeKind.Utc), 3L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"), new DateTime(2023, 1, 23, 13, 0, 0, 0, DateTimeKind.Utc), 4L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b"), new DateTime(2023, 1, 24, 14, 0, 0, 0, DateTimeKind.Utc), 5L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Code", "CreatedAt", "RevokedAt", "UserId" },
                values: new object[] { new Guid("a7b8c9d0-e1f2-3a4b-5c6d-7e8f9a0b1c2d"), new DateTime(2023, 1, 26, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("b8c9d0e1-f2a3-4b5c-6d7e-8f9a0b1c2d3e"), new DateTime(2023, 1, 27, 17, 0, 0, 0, DateTimeKind.Utc), 8L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("c9d0e1f2-a3b4-5c6d-7e8f-9a0b1c2d3e4f"), new DateTime(2023, 1, 28, 18, 0, 0, 0, DateTimeKind.Utc), 9L });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Code", "CreatedAt", "UserId" },
                values: new object[] { new Guid("d0e1f2a3-b4c5-6d7e-8f9a-0b1c2d3e4f5a"), new DateTime(2023, 1, 29, 19, 0, 0, 0, DateTimeKind.Utc), 10L });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Code", "CreatedAt", "RevokedAt", "UserId" },
                values: new object[] { 6L, new Guid("f6a7b8c9-d0e1-2f3a-4b5c-6d7e8f9a0b1c"), new DateTime(2023, 1, 25, 15, 0, 0, 0, DateTimeKind.Utc), null, 6L });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1L, new DateTime(2023, 1, 30, 10, 0, 0, 0, DateTimeKind.Utc), "REC-00001", 5000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2L, new DateTime(2023, 2, 4, 11, 0, 0, 0, DateTimeKind.Utc), "REC-00002", 45000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3L, new DateTime(2023, 2, 9, 12, 0, 0, 0, DateTimeKind.Utc), "REC-00003", 20000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4L, new DateTime(2023, 2, 14, 13, 0, 0, 0, DateTimeKind.Utc), "REC-00004", 3000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), 5L, new DateTime(2023, 2, 19, 14, 0, 0, 0, DateTimeKind.Utc), "REC-00005", 40000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), 6L, new DateTime(2023, 2, 24, 15, 0, 0, 0, DateTimeKind.Utc), "REC-00006", 2500ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7L, new DateTime(2023, 2, 28, 16, 0, 0, 0, DateTimeKind.Utc), "REC-00007", 22500ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), 8L, null, "REC-00008", 3500ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 9L, new DateTime(2023, 3, 9, 18, 0, 0, 0, DateTimeKind.Utc), "REC-00009", 15000ul });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "DueDate", "IssuerId", "PaymentDate", "ReceiptNumber", "TotalPrice" },
                values: new object[] { new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 10L, new DateTime(2023, 3, 14, 19, 0, 0, 0, DateTimeKind.Utc), "REC-00010", 400000ul });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Standard Daily Pass", 5000ul });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "IsStudent", "MaxUsage", "Price", "Type" },
                values: new object[] { "Student Daily Pass", true, 1ul, 2500ul, 1 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Description", "MaxUsage", "Price", "Type" },
                values: new object[] { "Monthly Full Pass", null, 45000ul, 2 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Description", "IsStudent", "MaxUsage", "Price", "TrainingId", "Type" },
                values: new object[] { "Student Monthly Pass", true, null, 22500ul, null, 2 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Description", "IsStudent", "MaxUsage", "Price", "TrainingId", "Type" },
                values: new object[] { "10-Occasion Pass", false, 10ul, 40000ul, null, 3 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Description", "IsStudent", "MaxUsage", "Price", "TrainingId", "Type" },
                values: new object[] { "Student 10-Occasion", true, 10ul, 20000ul, null, 3 });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Yoga Basics", false, 6000ul, 1L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - Yoga Basics", true, 3000ul, 1L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - HIIT Blast", false, 6000ul, 2L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - HIIT Blast", true, 3000ul, 2L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Pilates Core", false, 6000ul, 3L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - Pilates Core", true, 3000ul, 3L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Zumba", false, 6000ul, 4L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - Zumba", true, 3000ul, 4L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - CrossFit", false, 6000ul, 5L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - CrossFit", true, 3000ul, 5L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Spin Class", false, 6000ul, 6L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - Spin Class", true, 3000ul, 6L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Boxing", false, 6000ul, 7L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - Boxing", true, 3000ul, 7L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Stretching", false, 6000ul, 8L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - Stretching", true, 3000ul, 8L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Powerlifting", false, 6000ul, 9L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - Powerlifting", true, 3000ul, 9L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Standard Training Ticket - Aqua Aerobics", false, 6000ul, 10L });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "Description", "IsStudent", "Price", "TrainingId" },
                values: new object[] { "Student Training Ticket - Aqua Aerobics", true, 3000ul, 10L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString" },
                values: new object[] { new DateTime(2023, 4, 1, 8, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_abc123def456" });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 2, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_bcd234efg567", 2L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 3, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_cde345fgh678", 3L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 4, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_def456ghi789", 4L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 5, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), "token_efg567hij890", 5L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 6, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_fgh678ijk901", 6L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 7, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_ghi789jkl012", 7L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 8, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_hij890klm123", 8L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 9, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_ijk901lmn234", 9L });

            migrationBuilder.UpdateData(
                table: "Tokens",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "RevokedAt", "TokenString", "UserId" },
                values: new object[] { new DateTime(2023, 4, 10, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "token_jkl012mno345", 10L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ApplicationDate", "Presence", "UserId" },
                values: new object[] { new DateTime(2023, 4, 25, 10, 0, 0, 0, DateTimeKind.Utc), true, 1L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ApplicationDate", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 4, 26, 11, 0, 0, 0, DateTimeKind.Utc), true, 2L, 2L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ApplicationDate", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 4, 27, 12, 0, 0, 0, DateTimeKind.Utc), 3L, 4L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "ApplicationDate", "OnWaitinglist", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 4, 28, 13, 0, 0, 0, DateTimeKind.Utc), true, 4L, 5L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "ApplicationDate", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 4, 29, 14, 0, 0, 0, DateTimeKind.Utc), true, 5L, 7L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "ApplicationDate", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 4, 30, 15, 0, 0, 0, DateTimeKind.Utc), true, 6L, 8L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "ApplicationDate", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 5, 1, 16, 0, 0, 0, DateTimeKind.Utc), 7L, 9L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "ApplicationDate", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 5, 2, 17, 0, 0, 0, DateTimeKind.Utc), true, 8L, 10L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "ApplicationDate", "OnWaitinglist", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 5, 3, 18, 0, 0, 0, DateTimeKind.Utc), true, 9L, 1L });

            migrationBuilder.UpdateData(
                table: "TrainingUsers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "ApplicationDate", "OnWaitinglist", "Presence", "TrainingId", "UserId" },
                values: new object[] { new DateTime(2023, 5, 4, 19, 0, 0, 0, DateTimeKind.Utc), false, true, 10L, 2L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime" },
                values: new object[] { "Intro to Yoga", new DateTime(2023, 5, 1, 9, 0, 0, 0, DateTimeKind.Utc), "yoga.jpg", 20ul, "Yoga Basics", new DateTime(2023, 5, 1, 8, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "High Intensity Interval Training", new DateTime(2023, 5, 1, 11, 0, 0, 0, DateTimeKind.Utc), "hiit.jpg", 15ul, "HIIT Blast", new DateTime(2023, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc), 6L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Core strengthening", new DateTime(2023, 5, 2, 9, 0, 0, 0, DateTimeKind.Utc), "pilates.jpg", 15ul, "Pilates Core", new DateTime(2023, 5, 2, 8, 0, 0, 0, DateTimeKind.Utc), 3L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Description", "EndTime", "MaxParticipant", "Name", "StartTime" },
                values: new object[] { "Dance cardio", new DateTime(2023, 5, 2, 19, 0, 0, 0, DateTimeKind.Utc), 25ul, "Zumba Dance", new DateTime(2023, 5, 2, 18, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Learn the basics of CrossFit", new DateTime(2023, 5, 3, 18, 0, 0, 0, DateTimeKind.Utc), "crossfit.jpg", 10ul, "CrossFit Intro", new DateTime(2023, 5, 3, 17, 0, 0, 0, DateTimeKind.Utc), 3L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Indoor cycling", new DateTime(2023, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), "spin.jpg", 20ul, "Spin Class", new DateTime(2023, 5, 4, 7, 0, 0, 0, DateTimeKind.Utc), 6L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Pad work and technique", new DateTime(2023, 5, 5, 20, 0, 0, 0, DateTimeKind.Utc), "boxing.jpg", 12ul, "Boxing Fundamentals", new DateTime(2023, 5, 5, 19, 0, 0, 0, DateTimeKind.Utc), 3L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Recovery session", new DateTime(2023, 5, 6, 11, 0, 0, 0, DateTimeKind.Utc), "stretch.jpg", 20ul, "Stretching & Mobility", new DateTime(2023, 5, 6, 10, 0, 0, 0, DateTimeKind.Utc), 6L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Squat, Bench, Deadlift", new DateTime(2023, 5, 7, 17, 30, 0, 0, DateTimeKind.Utc), "power.jpg", 8ul, "Powerlifting 101", new DateTime(2023, 5, 7, 16, 0, 0, 0, DateTimeKind.Utc), 3L });

            migrationBuilder.UpdateData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Description", "EndTime", "Image", "MaxParticipant", "Name", "StartTime", "TrainerId" },
                values: new object[] { "Aerobic workout", new DateTime(2023, 5, 8, 10, 0, 0, 0, DateTimeKind.Utc), "aerobics.jpg", 15ul, "Aerobics", new DateTime(2023, 5, 8, 9, 0, 0, 0, DateTimeKind.Utc), 6L });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 1L,
                column: "UseDate",
                value: new DateTime(2023, 5, 1, 7, 45, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Gate", "UseDate" },
                values: new object[] { 2, new DateTime(2023, 5, 1, 9, 50, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Gate", "UseDate" },
                values: new object[] { 1, new DateTime(2023, 5, 2, 7, 40, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Gate", "UseDate" },
                values: new object[] { 0, new DateTime(2023, 5, 2, 17, 50, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 5L,
                column: "UseDate",
                value: new DateTime(2023, 5, 3, 16, 45, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 6L, 1, new DateTime(2023, 5, 4, 6, 50, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 7L, 2, new DateTime(2023, 5, 5, 18, 45, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 8L, 1, new DateTime(2023, 5, 6, 9, 55, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CardId", "Gate", "UseDate" },
                values: new object[] { 9L, 0, new DateTime(2023, 5, 7, 15, 45, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "UsageLogs",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CardId", "UseDate" },
                values: new object[] { 10L, new DateTime(2023, 5, 8, 8, 50, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2023, 1, 30, 10, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 31, 23, 59, 59, 0, DateTimeKind.Utc), 1L, 1ul, 1L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UserId" },
                values: new object[] { new DateTime(2023, 2, 4, 11, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 4, 23, 59, 59, 0, DateTimeKind.Utc), 3L, 2L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2023, 2, 9, 12, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 5, 9, 23, 59, 59, 0, DateTimeKind.Utc), 6L, 5ul, 3L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2023, 2, 14, 13, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 2, 15, 23, 59, 59, 0, DateTimeKind.Utc), 7L, 1ul, 4L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2023, 2, 19, 14, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 5, 19, 23, 59, 59, 0, DateTimeKind.Utc), 5L, 2ul, 5L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreationDate", "ExpirationDate", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2023, 2, 24, 15, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 2, 25, 23, 59, 59, 0, DateTimeKind.Utc), 1ul, 6L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UserId" },
                values: new object[] { new DateTime(2023, 2, 28, 16, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 28, 23, 59, 59, 0, DateTimeKind.Utc), 4L, 7L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreationDate", "ExpirationDate", "UserId" },
                values: new object[] { new DateTime(2023, 3, 5, 8, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 3, 6, 12, 0, 0, 0, DateTimeKind.Utc), 8L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UsageAmount", "UserId" },
                values: new object[] { new DateTime(2023, 3, 9, 18, 5, 0, 0, DateTimeKind.Utc), new DateTime(2023, 4, 9, 23, 59, 59, 0, DateTimeKind.Utc), 2L, 4ul, 9L });

            migrationBuilder.UpdateData(
                table: "UserTickets",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreationDate", "ExpirationDate", "TicketId", "UserId" },
                values: new object[] { new DateTime(2023, 3, 14, 19, 5, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 14, 23, 59, 59, 0, DateTimeKind.Utc), 8L, 10L });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), "alice@example.com", "Alice Smith", "hashed_pw_1", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1985, 10, 22, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 11, 9, 30, 0, 0, DateTimeKind.Utc), "bob@example.com", "Bob Jones", "hashed_pw_2", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(1992, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 12, 10, 15, 0, 0, DateTimeKind.Utc), "charlie@example.com", "Charlie Brown", "hashed_pw_3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1988, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 13, 11, 45, 0, 0, DateTimeKind.Utc), "diana@example.com", "Diana Prince", "hashed_pw_4", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1995, 7, 19, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 14, 14, 20, 0, 0, DateTimeKind.Utc), "evan@example.com", "Evan Wright", "hashed_pw_5", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(1993, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 15, 16, 5, 0, 0, DateTimeKind.Utc), "fiona@example.com", "Fiona Gallagher", "hashed_pw_6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1980, 9, 30, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 16, 18, 50, 0, 0, DateTimeKind.Utc), "george@example.com", "George Miller", "hashed_pw_7", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(1998, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 17, 7, 10, 0, 0, DateTimeKind.Utc), "hannah@example.com", "Hannah Abbott", "hashed_pw_8" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(1975, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 18, 12, 35, 0, 0, DateTimeKind.Utc), "ian@example.com", "Ian Malcolm", "hashed_pw_9" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password" },
                values: new object[] { new DateTime(1999, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 19, 15, 25, 0, 0, DateTimeKind.Utc), "jane@example.com", "Jane Doe", "hashed_pw_10" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1999, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 19, 15, 25, 0, 0, DateTimeKind.Utc), "tesztelek@example.com", "Teszt Elek", "hashed_pw_10", 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "BirthDate", "CreationDate", "Email", "Name", "Password", "Role" },
                values: new object[] { new DateTime(1999, 8, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 1, 19, 15, 25, 0, 0, DateTimeKind.Utc), "gitaron@example.com", "Git Áron", "hashed_pw_10", 3 });
        }
    }
}
