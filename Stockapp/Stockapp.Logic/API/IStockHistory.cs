using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface IStockHistory
    {
        /// <summary>
        /// Fetchs a group of histories.
        /// </summary>
        /// <param name="from">Default is 0</param>
        /// <param name="to">Default is 20</param>
        /// <returns></returns>
        IEnumerable<StockHistoryLogic> FetchStockHistories(Stock stock, int from = 0, int to = 20);

        /// <summary>
        /// Updates the stock hisoty.
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        bool UpdateStockHistory(StockHistory history);
    }
}
