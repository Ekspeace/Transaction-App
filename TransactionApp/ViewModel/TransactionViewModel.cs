using TransactionApp.Models;

namespace TransactionApp.ViewModel
{
    public class TransactionViewModel
    {
        public Client client { get; set; }
        public TransactionType transactionType { get; set; }
        public Transaction transaction{ get; set; }
        public IEnumerable<Client> clients { get; set; } = Enumerable.Empty<Client>();
        public IEnumerable<TransactionType> transactionTypes { get; set; } = Enumerable.Empty<TransactionType>();

        public IEnumerable<Transaction> transactions { get; set; } = Enumerable.Empty<Transaction>();
    }
}
