using GymTracer.Auth;
using GymTracer.Context;
using GymTracer.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GymTracer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AuthController : ControllerBase
    {
        private readonly GymTracerDbContext dbContext;
        private readonly PasswordHandler passwordHandler;
        private readonly TokenHandler tokenHandler;

        public AuthController(GymTracerDbContext dbContext, PasswordHandler passwordHandler, TokenHandler tokenHandler)
        {
            this.dbContext = dbContext;
            this.passwordHandler = passwordHandler;
            this.tokenHandler = tokenHandler;
        }

        [GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        private static partial Regex EmailRegex();

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] dynamic user)
        {
            return this.Run(() =>
            {
                models.User User = models.User.Deserialize(user);

                if(User is null || string.IsNullOrEmpty(User.Name) || string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Password))
                    return BadRequest(new { error = "Hibás adatok!" });

                if(User.Password.Length < 8)
                    return BadRequest(new { error = "A jelszónak legalább 8 karakterből kell állnia" });

                if(!EmailRegex().Match(User.Email).Success)
                    return BadRequest(new { error = "Az email címnek validnak kell lennie" });

                if(User.Name.Trim() == "")
                    return BadRequest(new { error = "A névnek validnak kell lennie" });

                var isUsedEmail = dbContext.Users.Any(u => u.Email == User.Email);
                if(isUsedEmail)
                    return BadRequest(new { error = "Az email cím már használatban van" });

                dbContext.Users.Add(new models.User()
                {
                    Email = User.Email,
                    Name = User.Name,
                    Password = passwordHandler.HashPassword(User.Password),
                    CreationDate = DateTime.UtcNow,
                    Role = models.User_Role.customer,
                    Active = true,
                });
                dbContext.SaveChanges();

                return Ok(new { message = "Sikeres regisztráció!"} );
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] dynamic user)
        {
            return this.Run(() =>
            {
                models.User User = models.User.Deserialize(user);

                if (User is null || string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Password))
                    return BadRequest(new { error = "Hibás adatok!" });

                var dbUser = dbContext.Users.FirstOrDefault(u => u.Email == User.Email);
                if (dbUser is null || !dbUser.Active || !passwordHandler.ComparePasswords(User.Password, dbUser.Password))
                    return BadRequest(new { error = "Hibás email cím vagy jelszó!" });

                tokenHandler.GenerateTokenData(out string tokenString, out DateTime createdAt, out DateTime revokedAt);

                dbContext.Tokens.Add(new models.Token {
                    UserId = dbUser.Id,
                    CreatedAt = createdAt,
                    RevokedAt = revokedAt,
                    TokenString = tokenString
                });
                dbContext.SaveChanges();

                return Ok(new {
                    message = "Sikeres bejelentkezés!",
                    user = new {
                        id = dbUser.Id,
                        name = dbUser.Name,
                        email = dbUser.Email,
                        role = dbUser.Role,
                    },
                    token = tokenString,
                    validTo = revokedAt,
                });

            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return this.Run(() =>
            {
                string? tokenString = User.Claims.FirstOrDefault(c => c.Type == "SessionToken")?.Value;

                if(tokenString is null)
                    return BadRequest(new { error = "A token invalid!" });

                models.Token? token = dbContext.Tokens.FirstOrDefault(t => t.TokenString == tokenString);

                if (token is null)
                    return BadRequest(new { error = "A token invalid!" });

                DateTime now = tokenHandler.Now();

                // Ha még nem járt le a token
                if (token.RevokedAt > now)
                {
                    token.RevokedAt = now;
                    dbContext.Update(token);
                    dbContext.SaveChanges();
                }

                return Ok(new { message = "Sikeres kijelentkezés!" });
            });
        }
    }
}
