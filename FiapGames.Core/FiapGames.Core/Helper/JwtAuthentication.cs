using System.Security.Cryptography;

namespace FiapGames.Core.Helper
{
    public class JwtAuthenticationGenerate
    {
        public static string GenerateSecret(int byteLength = 32)
        {
            var bytes = new byte[byteLength];
            RandomNumberGenerator.Fill(bytes);
            return Base64UrlEncode(bytes);
        }

        private static string Base64UrlEncode(byte[] bytes)
        {
            var s = Convert.ToBase64String(bytes);
            return s.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }
    }
}
