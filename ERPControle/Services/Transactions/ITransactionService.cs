using DTOs.Transactions;
using Models.Transactions;

namespace Services.Transactions
{
    public interface ITransactionService
    {
        /// Cria uma nova transação a partir dos dados do DTO
        Task<Transaction> CreateTransactionAsync(TransactionDTO dto);

        /// Obtém uma transação pelo identificador
        Task<Transaction?> GetTransactionByIdAsync(int id);
    }
}