using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface IPortfolioLogic
    {
        /// <summary>
        /// Creates a portfolio for a player
        /// </summary>
        /// <param name="player">Portfolio owner</param>
        void CreatePortfolio(Player player);

        /// <summary>
        /// Gets the portfolio for the player
        /// </summary>
        /// <param name="playerId">Player.Id</param>
        /// <returns></returns>
        Portfolio FetchPlayerPortfolio(Guid playerId);

        /// <summary>
        /// Updates the portfolio with a new transaction
        /// </summary>
        /// <param name="transaction"></param>
        void UpdatePortfolio(Portfolio portfolio, Transaction transaction);
    }
}
