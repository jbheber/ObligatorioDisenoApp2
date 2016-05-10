using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;

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
            throw new NotImplementedException();
        }
    }
}
