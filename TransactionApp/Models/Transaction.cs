using System.ComponentModel.DataAnnotations;

namespace TransactionApp.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        //[Required] didn't use this, because when using modal it will need  ajax to display validation
        public decimal Amount { get; set; }
        //[Required]
        public int TransactionTypeID { get; set; }
        //[Required]
        public int ClientID { get; set; }
        public string? Comment { get; set; }
    }
}
