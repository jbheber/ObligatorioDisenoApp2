using Moq;
using Stockapp.Data;
using Stockapp.Data.Extensions;
using Stockapp.Data.Repository;
using Stockapp.Logic;
using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stockapp.Test
{
    public class StockHistoryTest
    {
        [Fact]
        public void FetchStockHistoriesTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            IStockHistory stockHistoryLogic = new StockHistoryLogic(mockUnitOfWork.Object);

            var stock = new Stock()
            {
                StockHistory = new List<StockHistory>(),
                Description = "Test stock"
            };

            for(int i = 0; i < 40; i++)
            {
                stock.StockHistory
                    .ToList()
                    .Add(new StockHistory()
                    {
                        Id = Guid.NewGuid(),
                        DateOfChange = DateTimeOffset.Now.AddDays(-i),
                        RecordedValue = 1000 * i
                    });
            }
            var stockHistoriesResult = stockHistoryLogic.FetchStockHistories(stock);
            var expected = stock.StockHistory.OrderByDescending(x => x.DateOfChange).Take(20);

            Assert.Equal(expected, stockHistoriesResult);
        }

        public void UpdateStockHistoryTest()
        {
            //Arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.StockHistoryRepository.Update(It.IsAny<StockHistory>()));
            mockUnitOfWork.Setup(un => un.Save());

            IStockHistory stockHistoryLogic = new StockHistoryLogic(mockUnitOfWork.Object);

            //act
            bool updated = stockHistoryLogic.UpdateStockHistory(new StockHistory() { });

            //Assert
            mockUnitOfWork.Verify(un => un.StockHistoryRepository.Update(It.IsAny<StockHistory>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.True(updated);
        }
    }
}
