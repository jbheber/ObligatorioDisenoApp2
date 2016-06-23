using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class StockNews : ISoftDelete, Identificable
    {
        /// <summary>
        /// Database Generated Id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Stocks that are mentioned.
        /// </summary>
        public virtual IEnumerable<Stock> ReferencedStocks { get; set; }

        /// <summary>
        /// Date the news was created.
        /// </summary>
        public DateTimeOffset PublicationDate { get; set; }

        /// <summary>
        /// News Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// News content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        public StockNews()
        {
            IsDeleted = false;
        }

    }
}
