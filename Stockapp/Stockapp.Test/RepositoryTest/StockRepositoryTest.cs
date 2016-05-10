using Moq;
using Stockapp.Data;
using Stockapp.Data.Access;
using Stockapp.Data.Extensions;
using Stockapp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stockapp.Test
{
    public class StockRepositoryTest
    {
        [Fact]
        public void GetAllStockTest()
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Stock> result = unitOfWork.StockRepository.GetAll();

            Assert.Equal(result.SafeCount(), stockData.Count);
        }

        [Fact]
        public void GetFilterStockTest()
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Stock> result = unitOfWork.StockRepository.Get(p => p.UnityValue == 1, null);

            Assert.Equal(
                result.SafeCount(),
                stockData.Where(d => d.UnityValue == 1 && !d.IsDeleted).SafeCount());
        }

        [Fact]
        public void GetNonDeletedStockTest()
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<Stock> result = unitOfWork.StockRepository.Get();

            Assert.Equal(result.SafeCount(), stockData.Where(d => d.IsDeleted == false).SafeCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void GetByIdTest(int index)
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allStocks = unitOfWork.StockRepository.Get();
            var entity = allStocks.ElementAt(index);

            Assert.Equal(entity, unitOfWork.StockRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertStockTest()
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var stock = new Stock()
            {
                Code = "NEW",
                Name = "StockN",
                Description = "Este es el stockN",
                UnityValue = 100,
                StockNews = new List<StockNews>(),
                StockHistory = new List<StockHistory>(),
                IsDeleted = false,
                Id = Guid.NewGuid()
            };

            unitOfWork.StockRepository.Insert(stock);

            var result = unitOfWork.StockRepository.GetAll();

            Assert.Equal(stock, unitOfWork.StockRepository.GetById(stock.Id));
        }

        [Fact]
        public void InsertSingleStockTest()
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var stock = new Stock()
            {
                Code = "NEW",
                Name = "StockN",
                Description = "Este es el stockN",
                UnityValue = 100,
                StockNews = new List<StockNews>(),
                StockHistory = new List<StockHistory>(),
                IsDeleted = false,
                Id = Guid.NewGuid()
            };

            unitOfWork.StockRepository.Insert(stock);

            var result = unitOfWork.StockRepository.GetAll();

            Assert.True(result.isNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteStockByIdTest(int index)
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = stockData.ElementAt(index).Id;

            unitOfWork.StockRepository.Delete(elementId);
            unitOfWork.Save();

            var allStocks = unitOfWork.StockRepository.GetAll();

            Assert.True(allStocks.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteStockTest(int index)
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = stockData.ElementAt(index);

            unitOfWork.StockRepository.Delete(element);
            unitOfWork.Save();

            var allStocks = unitOfWork.StockRepository.GetAll();

            Assert.True(allStocks.First(p => p.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdateStockTest(int index)
        {
            var stockData = GetStockList();
            var stockSet = new Mock<DbSet<Stock>>().SetupData(stockData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<Stock>()).Returns(stockSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = stockData.ElementAt(index);

            var originalUnityValue = element.UnityValue;

            element.UnityValue = 1000;

            unitOfWork.StockRepository.Update(element);
            unitOfWork.Save();

            var allStocks = unitOfWork.StockRepository.GetAll();

            Assert.NotEqual(allStocks.First(u => u.Id == element.Id).UnityValue, originalUnityValue);
        }

        private List<Stock> GetStockList()
        {
            return new List<Stock>()
            {
                new Stock()
                {
                    Code = "MSFT",
                    Name = "Stock1",
                    Description = "Este es el stock1",
                    UnityValue = 1,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                new Stock()
                {
                    Code = "SET",
                    Name = "Stock2",
                    Description = "Este es el stock2",
                    UnityValue = 2,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                new Stock()
                {
                    Code = "GET",
                    Name = "Stock3",
                    Description = "Este es el stock3",
                    UnityValue = 3,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                new Stock()
                {
                    Code = "SAP",
                    Name = "Stock4",
                    Description = "Este es el stock4",
                    UnityValue = 4,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                 new Stock()
                {
                    Code = "SAT",
                    Name = "Stock5",
                    Description = "Este es el stock5",
                    UnityValue = 5,
                    StockNews = new List<StockNews>(),
                    StockHistory = new List<StockHistory>(),
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
            };
        }
    }
}
