using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class Portfolio
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public double AvailableMoney { get; set; }

        public double ActionsValue { get; set; }

        public virtual IEnumerable<Action> Actions { get; set; }
    }
}
