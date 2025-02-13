using Data;
using DTOs.Transactions;
using Enum;
using Microsoft.EntityFrameworkCore;
using Models.Transactions;
using Services.Persons;

namespace Services.Transactions
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

        public async Task<Transaction> CreateTransactionAsync(TransactionDTO transactionDTO)
        {
            // Verificar se a pessoa é menor de idade
            var person = await _personService.GetPersonByIdAsync(transactionDTO.PersonId);

            if (person.Age < 18 && transactionDTO.Type != TransactionType.Despesa)
                return null;

            // Mapeia os dados do DTO para a entidade Transaction
            var transaction = new Transaction
            {
                Description = transactionDTO.Description,
                Value = transactionDTO.Value,
                Type = transactionDTO.Type,
                PersonId = transactionDTO.PersonId
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}