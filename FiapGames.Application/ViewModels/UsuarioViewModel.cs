namespace FiapGames.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Nome { get; set; } = String.Empty;
        public string Senha { get; set; } = String.Empty;
        public string DataNascimento { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
    }
}
