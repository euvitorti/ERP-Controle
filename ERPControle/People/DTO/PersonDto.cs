using System.ComponentModel.DataAnnotations;

namespace People.DTO
{
    public class PersonDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O idade é obrigatória.")]
        public int Idade { get; set; }
    }
}