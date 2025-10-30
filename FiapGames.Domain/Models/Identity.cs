using System.Security.Cryptography;
using System.Text;

namespace FiapGames.Domain.Models
{
    public class Identity : ModelBase
    {
        public string Nome { get; private set; }
        public Usuario? Usuario { get; private set; }
        public int UsuarioId { get; private set; }
        public string Token { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public DateTime? UltimoAcesso { get; private set; }

        protected Identity()
        {
            Nome = string.Empty;
            Role = UserRole.Usuario;
            UltimoAcesso = null;
        }

        public Identity(string nome, string senha, UserRole role = UserRole.Usuario)
        {
            ArgumentNullException.ThrowIfNull(nome);
            ArgumentNullException.ThrowIfNull(senha);
            Nome = nome;
            SetPassword(senha);
            Role = role;
            UltimoAcesso = DateTime.UtcNow;
        }

        public void SetPassword(string senha)
        {
            ArgumentNullException.ThrowIfNull(senha);
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
            return Convert.ToHexString(hash);
        }
    }


    public enum UserRole
    {
        Usuario = 0,
        Admin = 1
    }

}