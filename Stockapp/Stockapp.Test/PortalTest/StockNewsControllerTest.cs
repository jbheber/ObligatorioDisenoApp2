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
    public class StockNewsControllerTest
    {
        [Fact]
        public void RegisterStockNewsReturnsCreatedAtRouteTest()
        {
            var mockStockNewsLogic = new Mock<IStockNewsLogic>();

            mockStockNewsLogic.Setup(x => x.RegisterStockNews(It.IsAny<StockNews>()))
                .Returns(true);
            var controller = new StockNewsController(mockStockNewsLogic.Object);

            var stockNewsId = Guid.NewGuid();
            var stockNews = new StockNews()
            {
                ReferencedStocks = new List<Stock>(),
                PublicationDate = DateTimeOffset.Now,
                Title = "News5",
                Content = "This is the news number 5",
                IsDeleted = false,
                Id = Guid.NewGuid()
            };
            IHttpActionResult actionResult = controller.PostStockNews(stockNews);

            var contentResult = Assert.IsType<CreatedAtRouteNegotiatedContentResult<StockNews>>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Content, stockNews);
        }

        [Fact]
        public void RegisterStockNewsReturnsBadRequestTest()
        {
            var mockStockNewsLogic = new Mock<IStockNewsLogic>();

            mockStockNewsLogic.Setup(x => x.RegisterStockNews(It.IsAny<StockNews>()))
                .Throws(new UserExceptions("StockNews exception"));

            var controller = new StockNewsController(mockStockNewsLogic.Object);

            IHttpActionResult actionResult = controller.PostStockNews(new StockNews());

            var contentResult = Assert.IsType<BadRequestErrorMessageResult>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Message, "StockNews exception");
        }

        [Fact]
        public void DeleteStockNewsReturnsNoContentTest()
        {
            var mockStockNewsLogic = new Mock<IStockNewsLogic>();

            mockStockNewsLogic.Setup(x => x.DeleteStockNews(It.IsAny<StockNews>())).Returns(true);
            var controller = new StockNewsController(mockStockNewsLogic.Object);

            var stockNews = new StockNews();
            IHttpActionResult actionResult = controller.DeleteStockNews(stockNews);

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);
        }
    }
}



