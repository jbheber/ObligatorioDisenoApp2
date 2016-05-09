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
        public IEnumerable<StockHistoryLogic> FetchStockHistories(Stock stock, int from = 0, int to = 20)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStockHistory(StockHistory history)
        {
            throw new NotImplementedException();
        }
    }
}
