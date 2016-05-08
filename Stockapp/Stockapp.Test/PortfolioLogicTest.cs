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

namespace Stockapp.Test
{
    public class PortfolioLogicTest
    {
        [Fact]
        public void CreatePortfolioTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.GetById(It.IsAny<int>()));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Insert(It.IsAny<Portfolio>()));

            var player = new Player()
            {
                CI = 1111111,
                Email = "juan@gmail.com",
                Name = "Juan",
                Surname = "Heber",
                User = new User()
            };

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);
            portfolioLogic.CreatePortfolio(player);
            mockUnitOfWork.VerifyAll();
        }

        [Fact]
        public void FetchPortfolioTest()
        {
            var player = new Player()
            {
                Id = Guid.NewGuid()
            };
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PlayerRepository.GetById(It.IsAny<int>()));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Get(p => p.PlayerId == player.Id, null, ""));

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            var portfolio = portfolioLogic.FetchPlayerPortfolio(player.Id);
            mockUnitOfWork.VerifyAll();
        }

        [Fact]
        public void UpdatePortfolioTest()
        {
            var player = new Player()
            {
                Id = Guid.NewGuid()
            };
            var portfolio = new Portfolio()
            {
                Player = player
            };
            var transaction = new Transaction()
            {
                MarketCapital = 500,
                NetVariation = 50,
                PercentageVariation = 10,
                Stock = new Stock(),
                StockQuantity = 20,
                TotalValue = 1000,
                Type = TransactionType.Buy,
                TransactionDate = DateTimeOffset.Now
            };

            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Get(p => p.PlayerId == player.Id, null, ""));

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            portfolioLogic.UpdatePortfolio(portfolio, transaction);
            mockUnitOfWork.VerifyAll();
        }
    }
}
