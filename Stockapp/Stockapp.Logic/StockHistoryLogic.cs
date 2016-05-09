using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic
{
    public class StockHistoryLogic : IStockHistory
    {
        private readonly IUnitOfWork UnitOfWork;

        public StockHistoryLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public IEnumerable<StockHistory> FetchStockHistories(Stock stock, int from = 0, int to = 20)
        {
            var stockHistories = stock.StockHistory
                .OrderByDescending(x => x.DateOfChange)
                .Skip(from)
                .Take(to);

            return stockHistories;
        }

        public bool UpdateStockHistory(StockHistory history)
        {
            UnitOfWork.StockHistoryRepository.Update(history);
            UnitOfWork.Save();
            return true;
        }
    }
}
