using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class Portfolio: ISoftDelete, Identificable
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

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

        public virtual IEnumerable<Transaction> Transactions { get; set; }

        public bool IsDeleted { get; set; }
    }
}
