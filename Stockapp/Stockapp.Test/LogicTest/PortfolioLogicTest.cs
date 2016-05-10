using Moq;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Id = Guid.NewGuid(),
                Portfolio = new Portfolio()
            };
            var playerId = player.Id;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);
            mockUnitOfWork.Setup(un => un.PlayerRepository.GetById(It.IsAny<Guid>())).Returns(() => player);

            var portfolio = portfolioLogic.FetchPlayerPortfolio(player.Id);

            Assert.NotNull(portfolio);
        }


        [Fact]
        public void FetchPortfolioTest2()
        {
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Portfolio = new Portfolio()
                {
                    Id = Guid.NewGuid(),
                    AvailableMoney = 1000,
                    ActionsValue = 2000
                }
            };
            var playerId = player.Id;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.PlayerRepository.GetById(It.IsAny<Guid>())).Returns(player);

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            var portfolio = portfolioLogic.FetchPlayerPortfolio(playerId);

            Assert.Equal(portfolio.TotalMoney, (player.Portfolio.ActionsValue + player.Portfolio.AvailableMoney));
        }

        [Fact]
        public void UpdatePortfolioTest()
        {
            var portfolio = new Portfolio();
            var transaction = new Transaction()
            {
                MarketCapital = 500,
                NetVariation = 50,
                PercentageVariation = 10,
                Stock = new Stock(),
                StockQuantity = 20,
                TotalValue = 1000,
                Type = TransactionType.Buy,
                TransactionDate = DateTimeOffset.Now,
                Portfolio = portfolio
            };

            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PortfolioRepository
                .GetById(It.IsAny<Guid>()))
                .Returns(() => new Portfolio());
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Update(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            Assert.True(portfolioLogic.UpdatePortfolio(transaction));
        }
    }
}
