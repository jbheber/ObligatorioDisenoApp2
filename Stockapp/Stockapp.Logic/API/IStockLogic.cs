using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface IStockLogic
    {
        bool CreateStock(Stock stock);

        bool UpdateStock(Stock stock);

        IEnumerable<Stock> GetAllStocks();

        Stock GetStock(Guid stockId);
    }
}
