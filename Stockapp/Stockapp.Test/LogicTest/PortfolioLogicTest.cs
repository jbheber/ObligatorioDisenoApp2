using Moq;
using Stockapp.Data;
using Stockapp.Data.Entities;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stockapp.Test.LogicTest
{
    public class PortfolioLogicTest
    {
        [Fact]
        public void FetchPortfolioTest()
        {
            var player = new Player()
            {
                Portfolio = new Portfolio()
                {
                    AvailableActions = new List<Actions>()
                }
            };
            var playerId = player.Id;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(un => un.PlayerRepository.GetById(It.IsAny<long>())).Returns(player);
            mockUnitOfWork.Setup(un => un.ActionsRepository.Get(It.IsAny<Expression<Func<Actions,bool>>>(), null, It.IsAny<string>())).Returns(() => new List<Actions>());

            var portfolio = portfolioLogic.FetchPlayerPortfolio(player.Id);

            Assert.NotNull(portfolio);
        }


        [Fact]
        public void FetchPortfolioTest2()
        {
            var player = new Player()
            {
                
                Portfolio = new Portfolio()
                {
                    
                    AvailableMoney = 1000,
                    ActionsValue = 2000
                }
            };
            var playerId = player.Id;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.PlayerRepository.GetById(It.IsAny<long>())).Returns(player);
            mockUnitOfWork.Setup(un => un.ActionsRepository.Get(It.IsAny<Expression<Func<Actions, bool>>>(), null, It.IsAny<string>())).Returns(() => new List<Actions>());

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            var portfolio = portfolioLogic.FetchPlayerPortfolio(playerId);

            Assert.Equal(portfolio.TotalMoney, (player.Portfolio.ActionsValue + player.Portfolio.AvailableMoney));
        }

        [Fact]
        public void UpdatePortfolioTest()
        {
            var portfolio = new Portfolio()
            {
                Id = 1,
                TotalMoney = 1000000,
                AvailableMoney = 100000,
                AvailableActions = new List<Actions>() {
                    new Actions()
                    {
                        Id = 1,
                        QuantityOfActions = 1000,
                        StockId = 1,
                        PortfolioId = 1
                    }
                },
                Transactions = new List<Transaction>()
            };
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Get(It.IsAny<Expression<Func<Portfolio, bool>>>(), null, It.IsAny<string>()))
                .Returns(new List<Portfolio>() { portfolio });
            mockUnitOfWork.Setup(un => un.StockRepository.GetById(It.IsAny<long>())).Returns(new Stock
            {
                Id = 1,
                Code = "GOO",
                Description = "GooGoo",
                Name = "GOO",
                QuantiyOfActions = 10000,
                UnityValue = 1.67
            });
            mockUnitOfWork.Setup(un => un.TransactionRepository.Insert(It.IsAny<Transaction>()));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.GetById(It.IsAny<long>())).Returns(portfolio);
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Update(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.Save());
            var transaction = new Transaction()
            {
                Stock = new Stock(),
                StockQuantity = 20,
                TotalValue = 1000,
                Type = TransactionType.Buy,
                TransactionDate = DateTimeOffset.Now,
                Portfolio = portfolio
            };

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            Assert.True(portfolioLogic.UpdatePortfolio(transaction));
        }
    }
}
