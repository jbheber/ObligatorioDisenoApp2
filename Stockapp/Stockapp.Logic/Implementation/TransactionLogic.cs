using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Data.Extensions;

namespace Stockapp.Logic.Implementation
{
    public class TransactionLogic : ITransactionLogic
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IPortfolioLogic portfolioLogic;

        public TransactionLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
            portfolioLogic = new PortfolioLogic(UnitOfWork);
        }

        public bool RegisterTransaction(Transaction transaction)
        {
            if (portfolioLogic.UpdatePortfolio(transaction) == false)
                return false;
            UnitOfWork.TransactionRepository.Insert(transaction);
            UnitOfWork.Save();
            return true;
        }

        public IEnumerable<Transaction> GetTransacions(DateTimeOffset from, DateTimeOffset to, long stockId, string transactionType = null)
         {
             var transactions = UnitOfWork.TransactionRepository.Get(null, null, "Stock");
             if (transactions.IsEmpty())
                 return null;

            var filteredTransactions = new List<Transaction>();
            foreach(var transaction in transactions)
            {
                if (transaction.TransactionDate.Date >= from.Date && transaction.TransactionDate.Date <= to.Date) { 
                    filteredTransactions.Add(transaction);
                }
            }
            if (stockId != 0)
                filteredTransactions = filteredTransactions.Where(x => x.StockId == stockId).ToList();
 
             if (transactionType != null)
                filteredTransactions = filteredTransactions.Where(x => x.Type.ToString() == transactionType).ToList();
 
             return filteredTransactions;
         }

        public bool UpdateTransaction(Transaction transaction)
        {
            UnitOfWork.TransactionRepository.Update(transaction);
            UnitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
