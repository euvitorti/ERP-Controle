using Data;
using Microsoft.EntityFrameworkCore;
using People.Services;
using Transactions.DTO;
using Transactions.Enum;
using Transactions.Model;

namespace Transactions.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPersonService _personService;

        public TransactionService(ApplicationDbContext context, IPersonService personService)
        {
            _context = context;
            _personService = personService;
        }

        public async Task<Transaction> CreateTransactionAsync(TransactionDto transactionDTO)
        {
            // Consulta a pessoa no banco de dados antes de salvar para verificar a idade
            var person = await _personService.GetPersonByIdAsync(transactionDTO.IdPessoa);

            // Retorna null para o controller caso seja menor de idade
            if (person.Age < 18 && transactionDTO.Tipo != TransactionType.Despesa)
                return null;

            // Mapeia os dados do DTO para a entidade Transaction
            var transaction = new Transaction
            {
                Description = transactionDTO.Detalhes,
                Value = transactionDTO.Valor,
                Type = transactionDTO.Tipo,
                PersonId = transactionDTO.IdPessoa
            };

            _context.Transactions.Add(transaction);
            
            // Salva no banco de dados
            await _context.SaveChangesAsync();

            return transaction;
        }

        // Consulta no banco de dados pelo id fornecido
        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}