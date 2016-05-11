using Moq;
using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Logic.API;
using Stockapp.Portal.Controllers;
using Stockapp.Portal.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace Stockapp.Test.PortalTest
{
    public class StockHistoryControllerTest
    {
        [Fact]
        public void UpdateStockHistoriesReturnsNoContentTest()
        {
            var mockStockHistoryLogic = new Mock<IStockHistoryLogic>();

            mockStockHistoryLogic.Setup(x => x.UpdateStockHistory(It.IsAny<StockHistory>())).Returns(true);
            var controller = new StockHistoryController(mockStockHistoryLogic.Object);

            var stockHistoryId = Guid.NewGuid();
            IHttpActionResult actionResult = controller.PutStockHistory(stockHistoryId, new StockHistory() { Id = stockHistoryId });

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);

        }

        [Fact]
        public void UpdateStockHistoriesReturnsNotFoundTest()
        {
            var mockStockHistoryLogic = new Mock<IStockHistoryLogic>();

            mockStockHistoryLogic.Setup(x => x.UpdateStockHistory(It.IsAny<StockHistory>())).Returns(false);
            var controller = new StockHistoryController(mockStockHistoryLogic.Object);

            var stockHistoryId = Guid.NewGuid();
            IHttpActionResult actionResult = controller.PutStockHistory(stockHistoryId, new StockHistory() { Id = stockHistoryId });

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }
    }
}




