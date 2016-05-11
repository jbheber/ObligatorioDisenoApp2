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
    public class StockControllerTest
    {
        [Fact]
        public void UpdateStocksReturnsNoContentTest()
        {
            var mockStockLogic = new Mock<IStockLogic>();

            mockStockLogic.Setup(x => x.UpdateStock(It.IsAny<Stock>())).Returns(true);
            var controller = new StockController(mockStockLogic.Object);

            var stockId = Guid.NewGuid();
            IHttpActionResult actionResult = controller.PutStock(stockId, new Stock() { Id = stockId });

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);

        }

        [Fact]
        public void UpdateStocksReturnsNotFoundTest()
        {
            var mockStockLogic = new Mock<IStockLogic>();

            mockStockLogic.Setup(x => x.UpdateStock(It.IsAny<Stock>())).Returns(false);
            var controller = new StockController(mockStockLogic.Object);

            var stockId = Guid.NewGuid();
            IHttpActionResult actionResult = controller.PutStock(stockId, new Stock() { Id = stockId });

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }

        [Fact]
        public void RegisterStockReturnsCreatedAtRouteTest()
        {
            var mockStockLogic = new Mock<IStockLogic>();

            mockStockLogic.Setup(x => x.CreateStock(It.IsAny<Stock>()))
                .Returns(true);
            var controller = new StockController(mockStockLogic.Object);

            var stockId = Guid.NewGuid();
            var stock = new Stock()
            {
                Code = "SAT",
                Name = "Stock5",
                Description = "Este es el stock5",
                UnityValue = 5,
                StockNews = new List<StockNews>(),
                StockHistory = new List<StockHistory>(),
                IsDeleted = false,
                Id = Guid.NewGuid()
            };
            IHttpActionResult actionResult = controller.PostStock(stock);

            var contentResult = Assert.IsType<CreatedAtRouteNegotiatedContentResult<Stock>>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Content, stock);
        }

        [Fact]
        public void RegisterStockReturnsBadRequestTest()
        {
            var mockStockLogic = new Mock<IStockLogic>();

            mockStockLogic.Setup(x => x.CreateStock(It.IsAny<Stock>()))
                .Throws(new UserExceptions("Stock exception"));

            var controller = new StockController(mockStockLogic.Object);

            IHttpActionResult actionResult = controller.PostStock(new Stock());

            var contentResult = Assert.IsType<BadRequestErrorMessageResult>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Message, "Stock exception");
        }
    }
}


