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
    public class PortfolioLogic : IPortfolioLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public PortfolioLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public bool CreatePortfolio(Player player)
        {
            var existingPortfolio = UnitOfWork
                .PortfolioRepository.Get(null, null, "")
                .isNotEmpty();
            if (existingPortfolio)
                return false;
            var portfolio = new Portfolio()
            {
                Player = player
            };
            UnitOfWork.PortfolioRepository.Insert(portfolio);
            UnitOfWork.Save();
            return true;
        }

        public Portfolio FetchPlayerPortfolio(Guid playerId)
        {
            var portfolios = UnitOfWork
               .PortfolioRepository.Get(x => x.Player.Id == playerId, null, "Player,Transactions");

            if (portfolios.isEmpty())
                throw new Exception("El jugador no tiene portfolio");

            var portfolio = portfolios.SingleOrDefault();
            portfolio.ActionsValue = portfolio.Transactions.Sum(t => t.TotalValue);
            portfolio.TotalMoney = portfolio.AvailableMoney + portfolio.ActionsValue;

            return portfolio;
        }

        public bool UpdatePortfolio(Transaction transaction)
        {
            var portfolio = UnitOfWork.PortfolioRepository.GetById(transaction.PortfolioId);

            if (portfolio == null)
                return false;

            portfolio.AvailableMoney += (transaction.Type == TransactionType.Buy) ?
                -transaction.TotalValue : transaction.TotalValue;

            UnitOfWork.PortfolioRepository.Update(portfolio);
            UnitOfWork.Save();
            return true;
        }
    }
}
