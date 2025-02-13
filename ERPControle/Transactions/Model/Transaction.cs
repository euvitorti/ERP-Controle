using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using People.Model;
using Transactions.Enum;

namespace Transactions.Model
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Value { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        // Chave estrangeira para a Person
        [Required]
        public int PersonId { get; set; }

        // Apenas para relacionamento, sem o jsonignore ocorre um erro de serialização
        // Person possui uma coleção de Transaction
        // Cada Transaction possui uma propriedade Person
        // Gera um loop infinito na serialização, pois contêm a mesma pessoa e assim por diante
        [ForeignKey(nameof(PersonId))]
        [JsonIgnore]
        public Person Person { get; set; }
    }
}
