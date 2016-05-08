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
            var player = new Player()
            {
                CI = 1111111,
                Email = "juan@gmail.com",
                Name = "Juan",
                Surname = "Heber",
                User = new User(),
                Id = Guid.NewGuid()
            };

            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Get(null, null, ""));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Insert(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);
            Assert.True(portfolioLogic.CreatePortfolio(player));
        }

        [Fact]
        public void CreatePortfolioTestFalse()
        {
            var player = new Player()
            {
                CI = 1111111,
                Email = "juan@gmail.com",
                Name = "Juan",
                Surname = "Heber",
                User = new User(),
                Id = Guid.NewGuid()
            };

            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Get(null, null, ""));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Insert(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.Save());

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            portfolioLogic.CreatePortfolio(player);
            Assert.True(portfolioLogic.CreatePortfolio(player));
        }

        [Fact]
        public void FetchPortfolioTest()
        {
            var player = new Player()
            {
                Id = Guid.NewGuid(),
            };
            var playerId = player.Id;
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Get(x => x.Player.Id == playerId, null, "Player,Transactions"))
                .Returns(new List<Portfolio>() { new Portfolio { PlayerId = player.Id} });
            mockUnitOfWork.Setup(un => un.Save());

            mockUnitOfWork.Object.PortfolioRepository.Insert(new Portfolio { PlayerId = player.Id });

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            var portfolio = portfolioLogic.FetchPlayerPortfolio(player.Id);
            mockUnitOfWork.VerifyAll();
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
                .GetById(It.IsAny<Portfolio>()))
                .Returns(() => new Portfolio());
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Update(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Insert(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.Save());

            mockUnitOfWork.Object.PortfolioRepository.Insert(portfolio);

            IPortfolioLogic portfolioLogic = new PortfolioLogic(mockUnitOfWork.Object);

            Assert.True(portfolioLogic.UpdatePortfolio(transaction));
        }
    }
}
