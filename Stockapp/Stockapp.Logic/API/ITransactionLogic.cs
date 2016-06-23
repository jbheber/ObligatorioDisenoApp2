using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface ITransactionLogic
    {
        /// <summary>
        /// Register a new transaction
        /// </summary>
        /// <param name="transaction">New transaction</param>
        /// <returns></returns>
        bool RegisterTransaction(Transaction transaction);

        /// <summary>
        /// Gets transaction between dates. Has optional filter
        /// </summary>
        /// <param name="from">Get transaction from this date</param>
        /// <param name="to">Get transaction to this date</param>
        /// <param name="stock">Filter by a specified stock</param>
        /// <param name="transactionType">String to show if sell or buy</param>
        /// <returns></returns>
        IEnumerable<Transaction> GetTransacions(DateTimeOffset from, DateTimeOffset to, long stock = 0, string transactionType = null);
        bool UpdateTransaction(Transaction transaction);
        void Dispose();

    }
}
