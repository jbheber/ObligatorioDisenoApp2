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
    public class PortfolioLogic : IPortfolioLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public PortfolioLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public void CreatePortfolio(Player player)
        {
            throw new NotImplementedException();
        }

        public Portfolio FetchPlayerPortfolio(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePortfolio(Portfolio portfolio, Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
