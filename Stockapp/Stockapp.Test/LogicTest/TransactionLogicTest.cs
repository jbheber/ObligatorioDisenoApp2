using Moq;
using Stockapp.Data;
using Stockapp.Data.Entities;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Stockapp.Test.LogicTest
{
    public class TransactionLogicTest
    {
        [Fact]
        public void RegisterTransactionTest()
        {
            var portfolio = new Portfolio()
            {
                Id = 1,
                TotalMoney = 1000000,
                AvailableMoney = 100000,
                AvailableActions = new List<Actions>() {
                    new Actions()
                    {
                        Id = 1,
                        QuantityOfActions = 1000,
                        StockId = 1,
                        PortfolioId = 1
                    }
                },
                Transactions = new List<Transaction>()
            };
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Get(It.IsAny<Expression<Func<Portfolio, bool>>>(), null, It.IsAny<string>()))
                .Returns(new List<Portfolio>() { portfolio });
            mockUnitOfWork.Setup(un => un.StockRepository.GetById(It.IsAny<long>())).Returns(new Stock
            {
                Id = 1,
                Code = "GOO",
                Description = "GooGoo",
                Name = "GOO",
                QuantiyOfActions = 10000,
                UnityValue = 1.67
            });
            mockUnitOfWork.Setup(un => un.TransactionRepository.Insert(It.IsAny<Transaction>()));
            mockUnitOfWork.Setup(un => un.PortfolioRepository.GetById(It.IsAny<long>())).Returns(portfolio);
            mockUnitOfWork.Setup(un => un.PortfolioRepository.Update(It.IsAny<Portfolio>()));
            mockUnitOfWork.Setup(un => un.Save());

            ITransactionLogic transactionLogic = new TransactionLogic(mockUnitOfWork.Object);
            var transaction = new Transaction()
            {
                Id = 1,
                Portfolio = portfolio,
                StockId = 1,
                StockQuantity = 20
            };
            var response = transactionLogic.RegisterTransaction(transaction);
            Assert.True(response);
        }

        [Fact]
        public void GetTransactionsBetweenDatesTest()
        {
            IList<Transaction> transactions = new List<Transaction>();
            for (int i = 0; i < 1; i++)
                transactions.Add(new Transaction()
                {
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
            Assert.Equal(response, transactions.Where(t => t.TransactionDate > thirtyDaysAgo));
            mockUnitOfWork.Verify(un => un.TransactionRepository.Get(null, null, It.IsAny<string>()));

            Assert.Equal(response.Count(), transactions.Where(t => t.TransactionDate > thirtyDaysAgo && t.TransactionDate < now).Count());
        }

        [Fact]
        public void GetTransactionsBetweenDatesAndTypeTest()
        {
            IList<Transaction> transactions = new List<Transaction>();
            for (int i = 0; i < 1; i++)
                transactions.Add(new Transaction()
                {
                    Portfolio = new Portfolio(),
                    Stock = new Stock() { Code = "aaaa" },
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

            Assert.Equal(response, transactions.Where(t => t.TransactionDate > thirtyDaysAgo && t.Type.ToString() == "Sell"));
            Assert.Equal(response, transactions.Where(t => t.TransactionDate > thirtyDaysAgo &&
            t.TransactionDate > thirtyDaysAgo && t.Type.ToString() == "Sell"));
        }

        [Fact]
        public void UpdateTransactionTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var transaction = new Transaction();

            mockUnitOfWork.Setup(un => un.TransactionRepository.Update(It.IsAny<Transaction>()));
            mockUnitOfWork.Setup(un => un.Save());

            ITransactionLogic transactionLogic = new TransactionLogic(mockUnitOfWork.Object);
            transaction.StockQuantity = 10000;
            var result = transactionLogic.UpdateTransaction(transaction);

            mockUnitOfWork.Verify(un => un.TransactionRepository.Update(It.IsAny<Transaction>()), Times.Once());
            mockUnitOfWork.Verify(un => un.Save(), Times.Once());
            Assert.True(result);
        }
    }
}



