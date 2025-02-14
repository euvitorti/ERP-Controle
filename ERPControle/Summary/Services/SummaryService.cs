using Data;
using Microsoft.EntityFrameworkCore;
using Summary.DTO;
using Transactions.Enum;

namespace Services.Summary
{
    public class SummaryService : ISummaryService
    {
        private readonly ApplicationDbContext _context;

        public SummaryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SummaryDto>> GetPersonSummariesAsync()
        {
            // Carrega todas as pessoas com suas transações
            var persons = await _context.Persons
                .Include(p => p.Transactions)
                .ToListAsync();

            // Para cada pessoa, calcula a despesa, receita e o saldo
            var summaries = persons.Select(p =>
            {
                // Percorrendo a lista de transações para somar as despesas e as receitas respctivamente
                var totalIncome = p.Transactions
                    .Where(t => t.Type == TransactionType.Receita)
                    .Sum(t => t.Value);
                    
                var totalExpense = p.Transactions
                    .Where(t => t.Type == TransactionType.Despesa)
                    .Sum(t => t.Value);

                return new SummaryDto
                {
                    Id = p.Id,
                    Nome = p.Name,
                    Idade = p.Age,
                    ReceitaTotal = totalIncome,
                    DespesaTotal = totalExpense,
                    Saldo = totalIncome - totalExpense
                };
            }).ToList();

            return summaries;
        }

        // Obter o relatório geral das despesas, receitas e saldo de todas as pessoas
        public async Task<IEnumerable<GeneralSummaryDto>> GetOverallSummaryAsync()
        {
            // Somar a receita de cada pessoa
            var totalIncome = await _context.Transactions
                .Where(t => t.Type == TransactionType.Receita)
                .SumAsync(t => t.Value);

            // Somar a despesa de cada pessoa
            var totalExpense = await _context.Transactions
                .Where(t => t.Type == TransactionType.Despesa)
                .SumAsync(t => t.Value);

            var summary = new GeneralSummaryDto
            {
                ReceitaTotal = totalIncome,
                DespesaTotal = totalExpense,
                Saldo = totalIncome - totalExpense
            };

            return new List<GeneralSummaryDto> { summary };
        }
    }
}