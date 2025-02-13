using System.ComponentModel.DataAnnotations;

namespace Auth.DTO
{
    public class LoginDto
    {

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string? Senha { get; set; }
    }
}