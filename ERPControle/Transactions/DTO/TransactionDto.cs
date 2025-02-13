using System.ComponentModel.DataAnnotations;
using Transactions.Enum;

namespace Transactions.DTO
{
    public class TransactionDto
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Detalhes { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor é obrigatório.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O tipo de transação é obrigatório.")]
        public TransactionType Tipo { get; set; }

        [Required(ErrorMessage = "A pessoa é obrigatória.")]
        public int IdPessoa { get; set; }
    }
}
