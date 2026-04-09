using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace GymTracer.Auth
{
    public class TokenHandler
    {
        private readonly AuthOptions authSettings;
        public TokenHandler(IOptions<AuthOptions> authOptions)
        {
            this.authSettings = authOptions.Value;
        }
        public void GenerateTokenData(out string tokenString, out DateTime createdAt, out DateTime revokedAt)
        {
            tokenString = GenerateTokenString();
            createdAt = DateTime.UtcNow;
            revokedAt = DateTime.UtcNow.AddMinutes(authSettings.ExpirationInMinutes);
        }

        public DateTime Now()
        {
            return DateTime.UtcNow;
        }

        private string GenerateTokenString()
        {
            return RandomNumberGenerator.GetHexString(authSettings.TokenLength);
        }
    }
}
