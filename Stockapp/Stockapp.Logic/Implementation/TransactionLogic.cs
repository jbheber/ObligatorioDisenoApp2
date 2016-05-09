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

        public TransactionLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public bool RegisterTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
