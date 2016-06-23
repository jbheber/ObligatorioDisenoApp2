using Stockapp.Data.Entities;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

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
        public virtual ICollection<Transaction> Transactions { get; set; }

        /// <summary>
        /// Available actions
        /// </summary>
        public virtual ICollection<Actions> AvailableActions { get; set; }

        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        public Portfolio()
        {
            IsDeleted = false;
            Transactions = new List<Transaction>();
            AvailableActions = new List<Actions>();
            ActionsValue = Transactions.Sum(t => t.TotalValue);
            TotalMoney = AvailableMoney + ActionsValue;
        }

    }
}
