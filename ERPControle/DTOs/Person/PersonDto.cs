using System.ComponentModel.DataAnnotations;

namespace DTOs.Person
{
    public class PersonDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O idade é obrigatória.")]
        public int Age { get; set; }
    }
}