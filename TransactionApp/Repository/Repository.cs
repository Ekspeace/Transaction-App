using Dapper;
using System.Data;
using System.Data.SqlClient;
using TransactionApp.Interface;
using TransactionApp.Models;

namespace TransactionApp.Repository
{
    public class Repository : IRepository
    {
        private readonly IConfiguration configuration;
        public Repository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Add(Transaction transaction)
        {
            var sql = "dbo.InsertTransaction";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                connection.Execute(sql, new { transaction.Amount, transaction.ClientID, transaction.TransactionTypeID, transaction.Comment }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(Transaction transaction)
        {
            var sql = "dbo.UpdateTransaction";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                connection.Execute(sql, new { transaction.TransactionID, transaction.Comment }, commandType: CommandType.StoredProcedure);
            }
        }
            public void Delete(Transaction transaction)
        {
            var sql = "dbo.DeleteTransaction";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
               connection.Execute(sql, new { transaction.TransactionID}, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Client> GetAllClient()
        {
            var sql = "dbo.SelectAllClients";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result =  connection.QueryAsync<Client>(sql, CommandType.StoredProcedure);
                return result.Result;
            }
        }

        public  IEnumerable<Transaction> GetAllTransaction()
        {
            var sql = "dbo.SelectAllTransactions";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = connection.QueryAsync<Transaction>(sql, CommandType.StoredProcedure);
                return result.Result;
            }
        }

        public IEnumerable<TransactionType> GetAllTransactionType()
        {
            var sql = "dbo.SelectAllTransactionTypes";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = connection.QueryAsync<TransactionType>(sql, CommandType.StoredProcedure);
                return result.Result;
            }
        }
    }
}
