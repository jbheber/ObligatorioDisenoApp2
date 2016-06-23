using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class Transaction: ISoftDelete, Identificable
    {
        /// <summary>
        /// Database generated Id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Referenced Stock
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Stock in the transaction.
        /// </summary>
        public virtual Stock Stock { get; set; }

        /// <summary>
        /// Stock Quantity
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Total Value
        /// </summary>
        public double TotalValue { get; set; }

        /// <summary>
        /// Transaction creation Date
        /// </summary>
        public DateTimeOffset TransactionDate { get; set; }

        /// <summary>
        /// Transfer type, buy or sell
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// reference to the portfolio
        /// </summary>
        public long PortfolioId { get; set; }

        /// <summary>
        /// Portfolio which the transaction belongs to
        /// </summary>
        public virtual Portfolio Portfolio { get; set; }

        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        public Transaction()
        {
            this.IsDeleted = false;
        }
    }

    /// <summary>
    /// Enum class used to determin if the transacion was to sell or buy stock.
    /// </summary>
    public enum TransactionType
    {
        Sell,
        Buy
    }
}
