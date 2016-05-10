using Moq;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Stockapp.Test.LogicTest
{
    public class TransactionLogicTest
    {
        [Fact]
        public void RegisterTransactionTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.TransactionRepository.Insert(It.IsAny<Transaction>()));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.GetById(It.IsAny<Guid>())).Returns(new Portfolio());
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Update(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.Save());

            ITransactionLogic transactionLogic = new TransactionLogic(mockUnitOfWork.Object);
            var transaction = new Transaction()
            {
                Portfolio = new Portfolio()
            };
            var response = transactionLogic.RegisterTransaction(transaction);
            Assert.True(response);
        }

        [Fact]
        public void GetTransactionsBetweenDatesTest()
        {
            IList<Transaction> transactions = new List<Transaction>();
            for (int i = 0; i < 100; i++)
                transactions.Add(new Transaction()
                {
                    Id = Guid.NewGuid(),
                    MarketCapital = 55 * i,
                    NetVariation = 120 * i,
                    PercentageVariation = 20 + i,
                    Portfolio = new Portfolio(),
                    Stock = new Stock(),
                    StockQuantity = 100,
                    TotalValue = 500,
                    TransactionDate = DateTimeOffset.Now.AddDays(-i)
                });

            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.TransactionRepository.Get(null, null, It.IsAny<string>())).Returns(transactions);

            ITransactionLogic transactionLogic = new TransactionLogic(mockUnitOfWork.Object);
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset thirtyDaysAgo = DateTimeOffset.Now.AddDays(-30);
            var response = transactionLogic.GetTransacions(thirtyDaysAgo, now);
            mockUnitOfWork.Verify(un => un.TransactionRepository.Get(null, null, It.IsAny<string>()));

            Assert.Equal(response.Count(), transactions.Where(t => t.TransactionDate > thirtyDaysAgo && t.TransactionDate < now).Count());
        }

        [Fact]
        public void GetTransactionsBetweenDatesAndTypeTest()
        {
            IList<Transaction> transactions = new List<Transaction>();
            for (int i = 0; i < 100; i++)
                transactions.Add(new Transaction()
                {
                    Id = Guid.NewGuid(),
                    MarketCapital = 55 * i,
                    NetVariation = 120 * i,
                    PercentageVariation = 20 + i,
                    Portfolio = new Portfolio(),
                    Stock = new Stock(),
                    StockQuantity = 100,
                    TotalValue = 500,
                    TransactionDate = DateTimeOffset.Now.AddDays(-i)
                });

            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.TransactionRepository.Get(null, null, It.IsAny<string>())).Returns(transactions);

            ITransactionLogic transactionLogic = new TransactionLogic(mockUnitOfWork.Object);
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset thirtyDaysAgo = DateTimeOffset.Now.AddDays(-30);
            var response = transactionLogic.GetTransacions(thirtyDaysAgo, now);

            Assert.Equal(response, transactions.Where(t => t.TransactionDate > thirtyDaysAgo &&
            t.TransactionDate > thirtyDaysAgo && t.Type.ToString() == "Sell"));
        }
    }
}
