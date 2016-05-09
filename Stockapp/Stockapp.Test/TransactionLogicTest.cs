using Moq;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System.Collections.Generic;
using Xunit;

namespace Stockapp.Test
{
    public class TransactionLogicTest
    {
        [Fact]
        public void RegisterTransaction()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.TransactionRepository.Insert(It.IsAny<Transaction>()));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Update(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.Save());

            ITransactionLogic transactionLogic = new TransactionLogic(mockUnitOfWork.Object);
            var transaction = new Transaction()
            {
                Portfolio = new Portfolio()
            };
            var response = transactionLogic.RegisterTransaction(transaction);
            mockUnitOfWork.VerifyAll();
            Assert.True(response);
        }
    }
}
