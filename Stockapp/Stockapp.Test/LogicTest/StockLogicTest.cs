using Moq;
using Stockapp.Data;
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
            var result = stockLogic.CreateStock(new Stock() { Code = "aaaa" });

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
                Id = 1
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
                    
                    Code = "AAA"
                },
                new Stock()
                {
                    
                    Code = "BBBB"
                },
                new Stock()
                {
                    
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
                Id = 1
            };
            mockUnitOfWork.Setup(un => un.StockRepository.Get(It.IsAny<Expression<Func<Stock, bool>>>(), null, It.IsAny<string>())).Returns(new List<Stock>() { stock });

            IStockLogic stockLogic = new StockLogic(mockUnitOfWork.Object);
            var result = stockLogic.GetStock(stock.Id);

            Assert.Equal(result, stock);
        }
    }
}
