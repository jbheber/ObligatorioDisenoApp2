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
            UnitOfWork.TransactionRepository.Update(transaction);
            UnitOfWork.Save();
            return true;
        }

        public IEnumerable<Transaction> GetTransacions(DateTimeOffset from, DateTimeOffset to, Stock stock = null, string transactionType = null)
        {
            var transactions = UnitOfWork.TransactionRepository.Get(null, null, "Portfolio,Stock");
            if (transactions.isEmpty())
                return null;

            if (stock != null)
                transactions.Where(x => x.StockId == stock.Id);

            if (transactionType != null)
                transactions.Where(x => x.Type.ToString() == transactionType);

            return transactions;
        }
    }
}
