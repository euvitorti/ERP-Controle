using System.ComponentModel.DataAnnotations;
using Enum;

namespace DTOs.Transactions
{
    public class TransactionDTO
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor é obrigatório.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "O tipo de transação é obrigatório.")]
        public TransactionType Type { get; set; }

        [Required(ErrorMessage = "A pessoa é obrigatória.")]
        public int PersonId { get; set; }
    }
}