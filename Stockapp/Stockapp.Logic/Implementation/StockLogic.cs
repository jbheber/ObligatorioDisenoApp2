using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;

namespace Stockapp.Logic.Implementation
{
    public class StockLogic : IStockLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public StockLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public bool CreateStock(Stock stock)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            throw new NotImplementedException();
        }

        public Stock GetStock(Guid stockId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStock(Stock stock)
        {
            throw new NotImplementedException();
        }
    }
}
