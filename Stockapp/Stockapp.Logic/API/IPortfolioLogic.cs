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
        /// Gets the portfolio for the player
        /// </summary>
        /// <param name="player">Player</param>
        /// <returns></returns>
        Portfolio FetchPlayerPortfolio(Player player);

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
        bool UpdatePortfolio(Transaction transaction);

        void Dispose();

    }
}
