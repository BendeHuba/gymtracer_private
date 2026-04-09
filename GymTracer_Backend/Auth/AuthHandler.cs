using GymTracer.Context;
using GymTracer.models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace GymTracer.Auth
{
    public class AuthHandler : AuthenticationHandler<AuthOptions>
    {
        private readonly GymTracerDbContext dbContext;
        private readonly TokenHandler tokenHandler;

        public AuthHandler(
            IOptionsMonitor<AuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            TokenHandler tokenHandler,
            GymTracerDbContext dbContext) : base(options, logger, encoder)
        {
            this.dbContext = dbContext;
            this.tokenHandler = tokenHandler;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Context.Request.Headers.Authorization;
            if (authorizationHeader.Count == 0)
                return await Task.FromResult(AuthenticateResult.NoResult());

            //Authorization: Bearer TOKEN----
            var tokenString = authorizationHeader.ToString().Replace("Bearer ","").Trim();

            return await HandleToken(tokenString);
        }

        private async Task<AuthenticateResult> HandleToken(string tokenString)
        {
            Token? sessionToken = dbContext.Set<Token>().Include(s => s.User).SingleOrDefault(s => s.TokenString == tokenString);

            if (sessionToken is null || sessionToken.RevokedAt <= tokenHandler.Now())
                return await Task.FromResult(AuthenticateResult.Fail("Authentication failed"));

            sessionToken.RevokedAt = tokenHandler.Now().AddMinutes(Options.ExpirationInMinutes);
            dbContext.Update(sessionToken);
            dbContext.SaveChanges();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, sessionToken.User.Role.ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, sessionToken.User.Id.ToString()));
            claims.Add(new Claim("SessionToken", sessionToken.TokenString));

            var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties(), Scheme.Name);
            Context.Response.Headers.Append("session", JsonSerializer.Serialize(new
            {
                validTo = sessionToken.RevokedAt
            }));
            Context.Response.Headers.Append("access-control-expose-headers", "session");

            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
