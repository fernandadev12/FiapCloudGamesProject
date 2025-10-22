using System.Security.Cryptography;
using System.Text;

namespace FiapGames.Core.Models
{
    public class Identity : ModelBase
    {
        public string Nome { get; private set; }
        public string Token { get; private set; }
        public UserRole Role { get; private set; }
        public DateTime UltimoAcesso { get; private set; }

        protected Identity()
        {
            Nome = string.Empty;
            Token = string.Empty;
            Role = UserRole.Usuario;
            UltimoAcesso = DateTime.MinValue;
        }

        public Identity(string nome, string senha, UserRole role = UserRole.Usuario)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            SetPassword(senha ?? throw new ArgumentNullException(nameof(senha)));
            Role = role;
            UltimoAcesso = DateTime.UtcNow;
        }

        public void SetPassword(string senha)
        {
            if (senha is null) throw new ArgumentNullException(nameof(senha));
            Token = ComputeSha256Hash(senha);
        }

        public bool VerifyPassword(string senha)
        {
            if (senha is null) return false;
            return Token == ComputeSha256Hash(senha);
        }

        public void UpdateUltimoAcesso(DateTime dataHoraUtc)
        {
            UltimoAcesso = dataHoraUtc;
        }

        private static string ComputeSha256Hash(string raw)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(raw);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash); // .NET 5+ disponível em .NET 8
        }
    }


    public enum UserRole
    {
        Usuario = 0,
        Admin = 1
    }

}