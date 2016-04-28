using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class StockNews: ISoftDelete
    {
        public Guid Id { get; set; }

        public virtual IEnumerable<Stock> ReferencedStocks { get; set; }

        public DateTimeOffset PublicationDate { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsDeleted { get; set; }
    }
}
