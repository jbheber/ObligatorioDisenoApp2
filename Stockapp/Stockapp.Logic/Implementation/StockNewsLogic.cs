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
    public class StockNewsLogic : IStockNewsLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public StockNewsLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public StockNews GetStockNews(long stockNewsId)
        {
            return UnitOfWork.StockNewsRepository.GetById(stockNewsId);
        }

        public bool DeleteStockNews(StockNews news)
        {
            UnitOfWork.StockNewsRepository.Delete(news);
            UnitOfWork.Save();
            return true;
        }

        public bool RegisterStockNews(StockNews news)
        {
            
            UnitOfWork.StockNewsRepository.Insert(news);
            foreach (var stock in news.ReferencedStocks)
            {
                var dbStock = UnitOfWork.StockRepository.Get(x => x.Id == stock.Id, null, "StockNews").SingleOrDefault();
                dbStock.StockNews.Add(news);
                UnitOfWork.StockRepository.Update(dbStock);
            }
            UnitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
