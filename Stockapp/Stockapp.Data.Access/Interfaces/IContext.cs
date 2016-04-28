using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data.Access.Interfaces
{
    /// <summary>
    /// Context interface
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// All Users from database
        /// </summary>
        IDbSet<User> Users { get; set; }

        /// <summary>
        /// All Players from database
        /// </summary>
        IDbSet<Player> Players { get; set; }

        /// <summary>
        /// All Administrators from database
        /// </summary>
        IDbSet<Admin> Admins { get; set; }

        /// <summary>
        /// All portfolios from database
        /// </summary>
        IDbSet<Portfolio> Portfolios { get; set; }

        /// <summary>
        /// All Actions from database
        /// </summary>
        IDbSet<Stock> Stocks { get; set; }

        /// <summary>
        /// All transactions from database.
        /// </summary>
        IDbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// All stock recorded changes from database.
        /// </summary>
        IDbSet<StockHistory> StockHistories { get; set; }

        /// <summary>
        /// All stock recorded news from database.
        /// </summary>
        IDbSet<StockNews> StockNews { get; set; }

        /// <summary>
        /// All non deleted Users
        /// </summary>
        IQueryable<User> UsersGet { get; }

        /// <summary>
        /// All non deleted Players
        /// </summary>
        IQueryable<Player> PlayersGet { get; }

        /// <summary>
        /// All non deleted Administrators
        /// </summary>
        IQueryable<Admin> AdminsGet { get; }

        /// <summary>
        /// All non deleted Portfolios
        /// </summary>
        IQueryable<Portfolio> PortfoliosGet { get; }

        /// <summary>
        /// All non deleted Stocks
        /// </summary>
        IQueryable<Stock> StocksGet { get; }

        /// <summary>
        /// All non deleted Transactions
        /// </summary>
        IQueryable<Transaction> TransactionsGet { get; }

        /// <summary>
        /// All non deleted stock recorded changes.
        /// </summary>
        IQueryable<StockHistory> StockHistoriesGet { get; }

        /// <summary>
        /// All non deleted recorded stock news.
        /// </summary>
        IQueryable<StockNews> StockNewsGet { get; }
    }
}
