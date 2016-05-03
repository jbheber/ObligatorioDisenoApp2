using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class StockHistory: ISoftDelete, Identificable
    {
        public Guid Id { get; set; }

        public DateTimeOffset DateOfChange { get; set; }

        public double RecordedValue { get; set; }

        public bool IsDeleted { get; set; }
    }
}
