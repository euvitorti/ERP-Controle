using Transactions.DTO;
using Transactions.Model;

namespace Transactions.Services
{
    public interface ITransactionService
    {
        // Cria uma nova transação a partir dos dados do DTO
        Task<Transaction> CreateTransactionAsync(TransactionDto dto);

        // Obtém uma transação pelo identificador
        Task<Transaction?> GetTransactionByIdAsync(int id);
    }
}