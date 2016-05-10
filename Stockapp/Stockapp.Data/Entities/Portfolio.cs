using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class Portfolio : ISoftDelete, Identificable
    {
        /// <summary>
        /// Database generated Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Player available liquid money
        /// </summary>
        public double AvailableMoney { get; set; }

        /// <summary>
        /// Properties used only for data calculations
        /// </summary>
        [NotMapped]
        public double ActionsValue { get; set; }

        /// <summary>
        /// Properties used only for data calculations
        /// </summary>
        [NotMapped]
        public double TotalMoney { get; set; }

        /// <summary>
        /// Player's transactions
        /// </summary>
        public virtual IEnumerable<Transaction> Transactions { get; set; }

        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        public Portfolio()
        {
            IsDeleted = false;
            this.ActionsValue = 0;
            this.AvailableMoney = 0;
            this.TotalMoney = 0;
            this.Transactions = new List<Transaction>();
        }

    }
}
