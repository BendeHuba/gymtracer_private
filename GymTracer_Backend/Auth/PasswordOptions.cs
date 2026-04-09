using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace GymTracer.Auth
{
    public class PasswordOptions
    {
        public const string SectionName = "PasswordHandler";

        public string AlgorithmName { get; set; } = "SHA256";
        public int Iterations { get; set; } = 10;
        public int HashLength { get; set; } = 32;
        public int SaltLength { get; set; } = 16;
    }
}
