using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface IStockNewsLogic
    {
        /// <summary>
        /// Register a new Stock news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        bool RegisterStockNews(StockNews news);

        /// <summary>
        /// Deletes a specified stock news.
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        bool DeleteStockNews(StockNews news);

        void Dispose();

    }
}
