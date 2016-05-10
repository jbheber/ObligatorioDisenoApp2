using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data.Entities
{
    public class GameSettings: ISoftDelete, Identificable
    {
        /// <summary>
        /// Database Generated Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User Initial Money
        /// </summary>
        public double InitialMoney { get; set; }

        /// <summary>
        /// Maximum number of transactions per day
        /// </summary>
        public int MaxTransactionsPerDay { get; set; }
        
        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}
