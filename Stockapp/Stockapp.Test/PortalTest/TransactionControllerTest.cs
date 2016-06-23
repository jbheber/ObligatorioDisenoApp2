using Moq;
using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Logic.API;
using Stockapp.Portal.Controllers;
using Stockapp.Portal.Models;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace Stockapp.Test.PortalTest
{
    public class TransactionControllerTest
    {
        [Fact]
        public void UpdateTransactionsReturnsNoContentTest()
        {
            var mockTransactionLogic = new Mock<ITransactionLogic>();

            mockTransactionLogic.Setup(x => x.UpdateTransaction(It.IsAny<Transaction>())).Returns(true);
            var controller = new TransactionController(mockTransactionLogic.Object);

            var transactionId = 1;
            IHttpActionResult actionResult = controller.PutTransaction(transactionId, new Transaction() { Id = transactionId });

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);

        }

        [Fact]
        public void UpdateTransactionsReturnsNotFoundTest()
        {
            var mockTransactionLogic = new Mock<ITransactionLogic>();

            mockTransactionLogic.Setup(x => x.UpdateTransaction(It.IsAny<Transaction>())).Returns(false);
            var controller = new TransactionController(mockTransactionLogic.Object);

            var transactionId = 1;
            IHttpActionResult actionResult = controller.PutTransaction(transactionId, new Transaction() { Id = transactionId });

            var contentResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.NotNull(contentResult);
        }

        [Fact]
        public void RegisterTransactionReturnsCreatedAtRouteTest()
        {
            var mockTransactionLogic = new Mock<ITransactionLogic>();

            mockTransactionLogic.Setup(x => x.RegisterTransaction(It.IsAny<Transaction>()))
                .Returns(true);
            var controller = new TransactionController(mockTransactionLogic.Object);
            
            var transaction = new Transaction()
            {
                Stock = new Stock(),
                StockQuantity = 4,
                TotalValue = 4,
                TransactionDate = DateTimeOffset.Now,
                Type = new TransactionType(),
                Portfolio = new Portfolio(),
                IsDeleted = false,
                Id = 1
            };
            IHttpActionResult actionResult = controller.PostTransaction(transaction);

            var contentResult = Assert.IsType<CreatedAtRouteNegotiatedContentResult<Transaction>>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Content, transaction);
        }

        [Fact]
        public void RegisterTransactionReturnsBadRequestTest()
        {
            var mockTransactionLogic = new Mock<ITransactionLogic>();

            mockTransactionLogic.Setup(x => x.RegisterTransaction(It.IsAny<Transaction>()))
                .Throws(new UserException("Stock exception"));

            var controller = new TransactionController(mockTransactionLogic.Object);

            IHttpActionResult actionResult = controller.PostTransaction(new Transaction());

            var contentResult = Assert.IsType<BadRequestErrorMessageResult>(actionResult);
            Assert.NotNull(contentResult);

            Assert.Equal(contentResult.Message, "Stock exception");
        }
    }
}


