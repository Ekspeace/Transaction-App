using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TransactionApp.Interface;
using TransactionApp.Models;
using TransactionApp.ViewModel;

namespace TransactionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index(int? client, int? transactionType, decimal? minAmount, decimal? maxAmount, string? sortOrder)
        {
            //these are viewbag which get sorting order
            ViewBag.NameSort = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.AmountSort = sortOrder == "amount" ? "amount_desc" : "amount";

            //get list of clients, transactions and transaction types and wrap them into TransactionViewModel since razor view only use only one model.
            var model = new TransactionViewModel();
            model.clients = _repository.GetAllClient();
            model.transactions = _repository.GetAllTransaction();
            model.transactionTypes = _repository.GetAllTransactionType();

            //check if client have a value, so that new list of transactions for the client is return.
            if (client is not null) { model.transactions = model.transactions.Where(t => t.ClientID == client); }
            //check if transactionType have a value, so that new list of transactions for the transaction type is return.
            if (transactionType is not null) { model.transactions = model.transactions.Where(t => t.TransactionTypeID == transactionType); }
            //check if minAmount have a value, so that new list of transactions between min and max amount is return.
            if (minAmount is not null) { model.transactions = model.transactions.Where(t => t.Amount >= minAmount && t.Amount <= maxAmount); }

            //check if sortOrder have a value, so that sorted list of transactions is return.
            if (sortOrder != null){ model.transactions = SortOrder(sortOrder, model.transactions, model.clients).ToList();}

            return View(model);
        }
        [HttpPost]
        public IActionResult Index(Transaction transaction)
        {
            var model = new TransactionViewModel();
            model.clients = _repository.GetAllClient();
            model.transactions = _repository.GetAllTransaction();
            model.transactionTypes = _repository.GetAllTransactionType();

            if (ModelState.IsValid)
            {
                if (transaction != null)
                {
                    try
                    {
                        //in case a user entered minus sign in amount for credit, this remove it because it will be handle in the stored procedure [InsertTransaction]
                        transaction.Amount = Math.Abs(transaction.Amount);
                        //check if transaction type is credit and if transaction amount is greater than client balance, then if true change it to (!true) false and display error
                        if (!(transaction.TransactionTypeID == 2 && model.clients.Where(c => c.ClientID == transaction.ClientID).First().ClientBalance <= transaction.Amount))
                        {
                            _repository.Add(transaction);
                            ViewBag.Message = "Transaction successfully added";
                            //To reload changes made
                            model.transactions = _repository.GetAllTransaction();
                            model.clients = _repository.GetAllClient();
                            return View(model);
                        }
                        else {
                            ViewBag.ErrorMessage = "Please note that if transaction is credit, then transaction amount cannot be greater than client balance";
                            return View(model);
                        }
                    }
                    catch(Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return View(model);
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please note some fields are required";
                return View(model);
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            var model = new TransactionViewModel();
            model.transaction = _repository.GetAllTransaction().Where(t => t.TransactionID == id).First();
            model.clients = _repository.GetAllClient();
            model.transactionTypes = _repository.GetAllTransactionType();

            if (model.transaction != null)
            {
                return PartialView("Edit", model);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            var model = new TransactionViewModel();
            model.clients = _repository.GetAllClient();
            model.transactions = _repository.GetAllTransaction();
            model.transactionTypes = _repository.GetAllTransactionType();

            if (ModelState.IsValid)
            {
                if (transaction != null)
                {
                    try
                    {
                        _repository.Update(transaction);
                        ViewBag.Message = "Transaction successfully Updated";
                        //To reload changes made
                        model.transactions = _repository.GetAllTransaction();
                        return View("Index",model);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return View("Index", model);
                    }
                }
            }
            else 
            {
                return View("Index", model);
            }
            return RedirectToAction("Index");
        }
        public PartialViewResult Delete(int id)
        {
            var model = new TransactionViewModel();
            model.transaction = _repository.GetAllTransaction().Where(t => t.TransactionID == id).First();
            model.clients = _repository.GetAllClient();
            model.transactionTypes = _repository.GetAllTransactionType();

            if (model.transaction != null)
            {
                return PartialView("Delete", model);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public IActionResult Delete(Transaction transaction)
        {
            var model = new TransactionViewModel();
            model.clients = _repository.GetAllClient();
            model.transactions = _repository.GetAllTransaction();
            model.transactionTypes = _repository.GetAllTransactionType();

            if (ModelState.IsValid)
            {
                if (transaction != null)
                {
                    try
                    {
                        _repository.Delete(transaction);
                        ViewBag.Message = "Transaction successfully Deleted";
                        //To reload changes made
                        model.transactions = _repository.GetAllTransaction();
                        return View("Index", model);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return View("Index", model);
                    }
                }
            }
            else
            {
                return View("Index", model);
            }
            return RedirectToAction("Index");
        }
        public IEnumerable<Transaction> SortOrder(string sortOrder, IEnumerable<Transaction> transactions, IEnumerable<Client> clients)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    transactions = from t in transactions join c in clients on t.ClientID equals c.ClientID orderby c.Name descending select t;
                    break;
                case "amount_desc":
                    transactions = transactions.OrderByDescending(t => t.Amount).ToList();
                    break;
                case "name":
                    transactions = from t in transactions join c in clients on t.ClientID equals c.ClientID orderby c.Name ascending select t;
                    break;
                case "amount":
                    transactions = transactions.OrderBy(t => t.Amount).ToList();
                    break;
                default:
                    break;
            }
            return transactions;
        }
    }
}