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
        public Guid Id { get; set; }

        public Guid StockId { get; set; }

        [ForeignKey("StockId")]
        public virtual Stock Stock { get; set; }

        public double NetVariation { get; set; }

        public double PercentageVariation { get; set; }

        public double MarketCapital { get; set; }

        public int StockQuantity { get; set; }

        public double TotalValue { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        public TransactionType Type { get; set; }

        public Guid PortfolioId { get; set; }

        [ForeignKey("PortfolioId")]
        public virtual Portfolio Portfolio { get; set; }

        public bool IsDeleted { get; set; }
    }

    public enum TransactionType
    {
        Sell,
        Buy
    }
}
