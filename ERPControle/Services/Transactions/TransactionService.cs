using Data;
using DTOs.Transactions;
using Microsoft.EntityFrameworkCore;
using Models.Transactions;

namespace Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateTransactionAsync(TransactionDTO transactionDTO)
        {
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