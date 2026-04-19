using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTracer.Migrations
{
    /// <inheritdoc />
    public partial class newdatafix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$pbkdf2$sha256$10$9EpqZbBCER6wbi+1cQZIrA==$b27NmGCjC5ll/Xx9tp4atkezneCrwoHuQ7/972kLuDAlXaiTqkHXUwT809/wU+Zr");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Password",
                value: "$pbkdf2$sha256$10$X2mISW/tTvaFPjHNmiVzHg==$MTCqu9sBR+HyMZ5L+WAsuWdu2VK90AncgJpJh/igzGSZxvbudqA8D+tKfHwrXpDm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Password",
                value: "$pbkdf2$sha256$10$UwmbS+nW7ONxybIb7ZKkwg==$PKmOZkVG5LCfFYHCEG5XtprjBI1Hh8ZoxXoIolKRVWaGX8uubwipc7OACmqhu34p");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Password",
                value: "$pbkdf2$sha256$10$OMmHJZpe1+i+mqmppuZl7g==$e5iKNiO0PRFvk4cH2rhX0MfwLjm8NYuQpIpZ1M/T3E/0TejxJV3f8PQCPIH1qoWo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 8L,
                column: "Password",
                value: "$pbkdf2$sha256$10$RzwqSi7MgrYQEcgSthPT7A==$db3Q+KxFdWEHFCemUKpLK7ygLIlIlChcJD+JJE0ltzXhNxobieAX2KjfnMVGJnyG");
        }
    }
}
