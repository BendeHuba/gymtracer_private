using Microsoft.AspNetCore.Authentication;

namespace GymTracer.Auth
{
    public class AuthOptions : AuthenticationSchemeOptions
    {
        public const string SectionName = "AuthHandler";
        public double ExpirationInMinutes { get; set; } = 5;
        public int TokenLength { get; set; } = 128;
    }
}
