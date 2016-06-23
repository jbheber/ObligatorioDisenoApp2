using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class StockHistory : ISoftDelete, Identificable
    {
        /// <summary>
        /// Database Generated Id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Date of change
        /// </summary>
        public DateTimeOffset DateOfChange { get; set; }

        /// <summary>
        /// Value before being modified.
        /// </summary>
        public double RecordedValue { get; set; }

        /// <summary>
        /// Reference to the stock.
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Parent Stock
        /// </summary>
        public virtual Stock Stock { get; set; }

        /// <summary>
        /// Soft delete.
        /// </summary>
        public bool IsDeleted { get; set; }

        public StockHistory()
        {
            DateOfChange = DateTimeOffset.Now;
            IsDeleted = false;
            RecordedValue = 0;
        }

        public StockHistory(Stock stock)
        {
            DateOfChange = DateTimeOffset.Now;
            IsDeleted = false;
            Stock = stock;
            RecordedValue = stock.UnityValue;
        }

    }
}
