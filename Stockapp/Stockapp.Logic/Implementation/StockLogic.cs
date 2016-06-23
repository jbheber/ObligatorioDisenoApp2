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

        public bool CorrectStock(Stock stock)
        {
            stock.Code = stock.Code.ToUpper();
            if (stock.Code.Length <= 6)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Stock> GetStocks(string name = "", string description = "")
        {
            var stocks = UnitOfWork.StockRepository.Get(null, null, "StockNews,StockHistory");
            if (stocks.IsEmpty())
                return null;

            var filteredStocks = stocks.ToList();
            if (name != string.Empty || name != "")
                filteredStocks = stocks.Where(s => s.Name.ToLower().Contains(name.ToLower())).ToList();

            if (description != string.Empty || description != "")
                filteredStocks = filteredStocks.Where(s => s.Description.ToLower().Contains(description.ToLower())).ToList();

            foreach (var stock in filteredStocks)
            {
                if (stock.StockHistory.IsNotEmpty())
                {
                    foreach (var history in stock.StockHistory)
                        history.Stock = null;
                    stock.StockHistory = stock.StockHistory.OrderByDescending(x => x.DateOfChange).ToList();
                }
                if (stock.StockNews.IsNotEmpty())
                {
                    foreach (var news in stock.StockNews)
                        news.ReferencedStocks = null;
                    stock.StockNews = stock.StockNews.OrderByDescending(x => x.PublicationDate).ToList();
                }
            }

            return filteredStocks;
        }

        public bool CreateStock(Stock stock)
        {
            var existingStocks = UnitOfWork.StockRepository.Get();

            if (existingStocks.IsNotEmpty() && existingStocks.Any(s => s.Code == stock.Code || s.Name == stock.Name))
                return false;

            if (CorrectStock(stock))
            {
                UnitOfWork.StockRepository.Insert(stock);
                UnitOfWork.Save();
            }
            return true;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return UnitOfWork.StockRepository.Get();
        }

        public Stock GetStock(long stockId)
        {
            return UnitOfWork.StockRepository.Get(s => s.Id == stockId, null, "StockHistory").SingleOrDefault();
        }

        public bool UpdateStock(Stock stock)
        {
            UnitOfWork.StockRepository.Update(stock);
            UnitOfWork.Save();
            return true;
        }

        public bool DeleteStock(Stock stock)
        {
            UnitOfWork.StockRepository.Delete(stock);
            UnitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
