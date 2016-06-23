using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class Stock : ISoftDelete, Identificable
    {
        /// <summary>
        /// Database generated Id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Stock Code. Maximum 6 character, all upper case.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Stock brief description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Stock quantity of actions.
        /// </summary>
        public double QuantiyOfActions { get; set; }

        /// <summary>
        /// Single stock value.
        /// </summary>
        public double UnityValue { get; set; }

        /// <summary>
        /// News which contain current stock.
        /// </summary>
        public virtual ICollection<StockNews> StockNews { get; set; }

        /// <summary>
        /// All recorded changes for current stock.
        /// </summary>
        public virtual ICollection<StockHistory> StockHistory { get; set; }

        /// <summary>
        /// Net variation.
        /// </summary>
        [NotMapped]
        public double NetVariation { get; set; }

        /// <summary>
        /// Percetage variation
        /// </summary>
        [NotMapped]
        public double PercentageVariation { get; set; }

        /// <summary>
        /// Market Capital.
        /// </summary>
        [NotMapped]
        public double MarketCapital { get; set; }

        /// <summary>
        /// Used for soft(logic) delete.
        /// </summary>
        public bool IsDeleted { get; set; }

        public Stock()
        {
            IsDeleted = false;
            UnityValue = 0;
        }

        public override string ToString()
        {
            return Code;
        }

    }
}
