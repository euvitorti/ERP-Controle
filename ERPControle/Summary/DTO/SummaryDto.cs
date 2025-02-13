namespace Summary.DTO
{
    public class SummaryDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public int Idade { get; set; }

        public decimal ReceitaTotal { get; set; }

        public decimal DespesaTotal { get; set; }

        public decimal Saldo { get; set; }
    }
}
