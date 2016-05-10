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
    public class StockLogicTest
    {
        [Fact]
        public void CreateStockTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.StockRepository.Get(null, null, ""));
            mockUnitOfWork.Setup(un => un.StockRepository.Insert(It.IsAny<Stock>()));
            mockUnitOfWork.Setup(un => un.Save());


            IStockLogic stockLogic = new StockLogic(mockUnitOfWork.Object);
            var result = stockLogic.CreateStock(new Stock());

            mockUnitOfWork.Verify(un => un.StockRepository.Get(null, null, ""), Times.Once());
            mockUnitOfWork.Verify(un => un.StockRepository.Insert(It.IsAny<Stock>()), Times.Once());
            mockUnitOfWork.Verify(un => un.Save(), Times.Once());
            Assert.True(result);
        }

        [Fact]
        public void UpdateStockTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var stock = new Stock()
            {
                Id = Guid.NewGuid()
            };

            mockUnitOfWork.Setup(un => un.StockRepository.Update(It.IsAny<Stock>()));
            mockUnitOfWork.Setup(un => un.Save());


            IStockLogic stockLogic = new StockLogic(mockUnitOfWork.Object);
            stock.Code = "AAAA";
            var result = stockLogic.UpdateStock(stock);

            mockUnitOfWork.Verify(un => un.StockRepository.Update(It.IsAny<Stock>()), Times.Once());
            mockUnitOfWork.Verify(un => un.Save(), Times.Once());
            Assert.True(result);
        }

        [Fact]
        public void GetAllStocksTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            IEnumerable<Stock> stocks = new List<Stock>()
            {
                new Stock()
                {
                    Id = Guid.NewGuid(),
                    Code = "AAA"
                },
                new Stock()
                {
                    Id = Guid.NewGuid(),
                    Code = "BBBB"
                },
                new Stock()
                {
                    Id = Guid.NewGuid(),
                    Code = "CCCC"
                },
            };

            mockUnitOfWork.Setup(un => un.StockRepository.Get(null, null, "")).Returns(stocks);

            IStockLogic stockLogic = new StockLogic(mockUnitOfWork.Object);
            var result = stockLogic.GetAllStocks();

            mockUnitOfWork.Verify(un => un.StockRepository.Get(null, null, ""), Times.Once());
            Assert.Equal(result, stocks);
        }

        [Fact]
        public void GetStockTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var stock = new Stock()
            {
                Id = Guid.NewGuid()
            };

            mockUnitOfWork.Setup(un => un.StockRepository.GetById(It.IsAny<Guid>())).Returns(stock);

            IStockLogic stockLogic = new StockLogic(mockUnitOfWork.Object);
            var result = stockLogic.GetStock(stock.Id);

            mockUnitOfWork.Verify(un => un.StockRepository.GetById(It.IsAny<Guid>()), Times.Once());
            Assert.Equal(result, stock);
        }
    }
}
