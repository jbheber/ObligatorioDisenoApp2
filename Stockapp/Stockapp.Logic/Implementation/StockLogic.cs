using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Data.Extensions;

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
            var existingStocks = UnitOfWork.StockRepository.Get();

            if (existingStocks.isNotEmpty() && existingStocks.Any(s => s.Code == stock.Code || s.Name == stock.Name))
                return false;

            UnitOfWork.StockRepository.Insert(stock);
            UnitOfWork.Save();
            return true;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return UnitOfWork.StockRepository.Get();
        }

        public Stock GetStock(Guid stockId)
        {
           return UnitOfWork.StockRepository.GetById(stockId);
        }

        public bool UpdateStock(Stock stock)
        {
            UnitOfWork.StockRepository.Update(stock);
            UnitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
