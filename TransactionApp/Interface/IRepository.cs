using TransactionApp.Models;

namespace TransactionApp.Interface
{
    public interface IRepository
    { 
        void Add(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(Transaction transaction);
        IEnumerable<Client> GetAllClient();
        IEnumerable<TransactionType> GetAllTransactionType();
        IEnumerable<Transaction> GetAllTransaction();
    }
}
