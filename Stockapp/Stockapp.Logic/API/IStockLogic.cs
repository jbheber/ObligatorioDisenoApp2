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
        bool CorrectStock(Stock stock);
        IEnumerable<Stock> GetStocks(string name = "", string description = "");
        bool CreateStock(Stock stock);

        bool UpdateStock(Stock stock);

        IEnumerable<Stock> GetAllStocks();

        Stock GetStock(long stockId);
        bool DeleteStock(Stock stock);
        void Dispose();

    }
}
