using System.ComponentModel.DataAnnotations;

namespace DTOs.User
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string? Password { get; set; }
    }
}