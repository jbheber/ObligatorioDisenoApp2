using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class Action: ISoftDelete
    {
        public Guid Id { get; set; }

        public string Stock { get; set; }

        public double UnityValue { get; set; }

        public double Var { get; set; }

        public double PercentageVar { get; set; }

        public double MarketCapital { get; set; }

        public int Quantity { get; set; }

        public double Value { get; set; }

        public Guid PortfolioId { get; set; }

        public virtual Portfolio Portfolio { get; set; }

        public bool IsDeleted { get; set; }
    }
}
