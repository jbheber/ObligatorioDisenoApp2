using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Data.Extensions;
using Stockapp.Data.Entities;

namespace Stockapp.Logic.Implementation
{
    public class PortfolioLogic : IPortfolioLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public PortfolioLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public Portfolio FetchPlayerPortfolio(long playerId)
        {
            var player = UnitOfWork.PlayerRepository.GetById(playerId);
            return FetchPlayerPortfolio(player);
        }

        public Portfolio FetchPlayerPortfolio(Player player)
        {
            if (player.Portfolio == null)
                throw new Exception("El jugador no tiene portfolio");

            var portfolio = player.Portfolio;
            portfolio.AvailableActions = Actions(portfolio.Id).ToList();
            portfolio.ActionsValue = portfolio.AvailableActions.Sum(a => a.QuantityOfActions * a.Stock.UnityValue);
            portfolio.TotalMoney = portfolio.AvailableMoney + portfolio.ActionsValue;
            return portfolio;
        }

        private IEnumerable<StockHistory> OrderStockHistory(Stock stock)
        {
            var stockHistories = stock.StockHistory;
            if (stockHistories.IsNotEmpty())
	        {
                return stockHistories.OrderByDescending(s => s.DateOfChange);
	        }
            return stockHistories;
        }

        private double GetPurchasedStock(long stockId)
        {
            var actions = UnitOfWork.ActionsRepository.Get(a => a.StockId == stockId, null, "Stock");
            if (actions.IsNotEmpty())
            {
                return actions.Sum(a => a.QuantityOfActions);
            }
            return 0;
        }

        private IEnumerable<Actions> Actions(long portfolioId)
        {
            var actions = UnitOfWork.ActionsRepository.Get(a => a.PortfolioId == portfolioId, null, "Stock.StockHistory");
            if (actions.IsNotEmpty())
            {
                foreach (var action in actions)
                {
                    Stock actualStock = action.Stock;
                    IEnumerable<StockHistory> stockHistories = OrderStockHistory(actualStock);
                    if (stockHistories.IsNotEmpty())
                    {
                        StockHistory previousStock = stockHistories.First();
                        actualStock.NetVariation = actualStock.UnityValue - previousStock.RecordedValue;
                        if (actualStock.UnityValue != 0)
                        {
                            actualStock.PercentageVariation = (actualStock.NetVariation * 100) / actualStock.UnityValue;
                            if (actualStock.PercentageVariation == 100)
                            {
                                actualStock.PercentageVariation = 0;
                            }
                        }
                        actualStock.MarketCapital = GetPurchasedStock(actualStock.Id) * actualStock.UnityValue;
                    }
                    action.Portfolio = null;
                    action.Stock.StockHistory = null;
                }
            }
            return actions;
        }

        public bool UpdatePortfolio(Transaction transaction)
        {
            var portfolio = UnitOfWork.PortfolioRepository.Get(p => p.Id == transaction.PortfolioId, null, "AvailableActions").SingleOrDefault();
            var stock = UnitOfWork.StockRepository.GetById(transaction.StockId);
            if (portfolio == null || transaction == null)
                return false;
            if (transaction.Type == TransactionType.Buy && portfolio.AvailableMoney < transaction.TotalValue)
                return false;
            if (transaction.Type == TransactionType.Sell && portfolio.AvailableActions.SafeCount() != 0 && portfolio.AvailableActions.FirstOrDefault(a => a.StockId == stock.Id).QuantityOfActions < transaction.StockQuantity)
                return false;

            if (transaction.Type == TransactionType.Buy)
            {
                portfolio.AvailableMoney -= transaction.TotalValue;
                stock.QuantiyOfActions -= transaction.StockQuantity;
            }
            if (transaction.Type == TransactionType.Sell)
            {
                portfolio.AvailableMoney += transaction.TotalValue;
                stock.QuantiyOfActions += transaction.StockQuantity;
            }
            // Si no tiene ese stock en su lista de acciones
            if (portfolio.AvailableActions.Where(a => a.StockId == transaction.StockId).SafeCount() == 0)
            {
                portfolio.AvailableActions.Add(new Actions()
                {
                    PortfolioId = transaction.PortfolioId,
                    QuantityOfActions = transaction.StockQuantity,
                    StockId = stock.Id
                });
            }
            else
            {
                var actionUpdated = portfolio.AvailableActions.Where(a => a.StockId == transaction.StockId).SingleOrDefault();
                actionUpdated.QuantityOfActions += transaction.Type == TransactionType.Sell ? -transaction.StockQuantity : transaction.StockQuantity;
            }
            UnitOfWork.PortfolioRepository.Update(portfolio);
            UnitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
