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
    public class StockHistoryRepositoryTest
    {
        [Fact]
        public void GetAllStockHistoryTest()
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<StockHistory> result = unitOfWork.StockHistoryRepository.GetAll();

            Assert.Equal(result.SafeCount(), stockHistoryData.Count);
        }

        [Fact]
        public void GetFilterStockHistoryTest()
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<StockHistory> result = unitOfWork.StockHistoryRepository.Get(p => p.RecordedValue == 1, null);

            Assert.Equal(
                result.SafeCount(),
                stockHistoryData.Where(d => d.RecordedValue == 1 && !d.IsDeleted).SafeCount());
        }

        [Fact]
        public void GetNonDeletedStockHistoryTest()
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<StockHistory> result = unitOfWork.StockHistoryRepository.Get();

            Assert.Equal(result.SafeCount(), stockHistoryData.Where(d => d.IsDeleted == false).SafeCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void GetByIdTest(int index)
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allStockHistories = unitOfWork.StockHistoryRepository.Get();
            var entity = allStockHistories.ElementAt(index);

            Assert.Equal(entity, unitOfWork.StockHistoryRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertStockHistoryTest()
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var stockHisyory = new StockHistory()
            {
                DateOfChange = DateTimeOffset.Now,
                RecordedValue = 100,
                IsDeleted = false,

            };

            unitOfWork.StockHistoryRepository.Insert(stockHisyory);

            var result = unitOfWork.StockHistoryRepository.GetAll();

            Assert.Equal(stockHisyory, unitOfWork.StockHistoryRepository.GetById(stockHisyory.Id));
        }

        [Fact]
        public void InsertSingleStockHistoryTest()
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var stockHisyory = new StockHistory()
            {
                DateOfChange = DateTimeOffset.Now,
                RecordedValue = 1000,
                IsDeleted = false,

            };

            unitOfWork.StockHistoryRepository.Insert(stockHisyory);

            var result = unitOfWork.StockHistoryRepository.GetAll();

            Assert.True(result.IsNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteStockHistoryByIdTest(int index)
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = stockHistoryData.ElementAt(index).Id;

            unitOfWork.StockHistoryRepository.Delete(elementId);
            unitOfWork.Save();

            var allStockHistories = unitOfWork.StockHistoryRepository.GetAll();

            Assert.True(allStockHistories.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteStockHistoryTest(int index)
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = stockHistoryData.ElementAt(index);

            unitOfWork.StockHistoryRepository.Delete(element);
            unitOfWork.Save();

            var allStockHistories = unitOfWork.StockHistoryRepository.GetAll();

            Assert.True(allStockHistories.First(p => p.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdateStockHistoryTest(int index)
        {
            var stockHistoryData = GetStockHistoryList();
            var stockHistorySet = new Mock<DbSet<StockHistory>>().SetupData(stockHistoryData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockHistory>()).Returns(stockHistorySet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = stockHistoryData.ElementAt(index);

            var originalRecordedValue = element.RecordedValue;

            element.RecordedValue = 10000;

            unitOfWork.StockHistoryRepository.Update(element);
            unitOfWork.Save();

            var allStockHistories = unitOfWork.StockHistoryRepository.GetAll();

            Assert.NotEqual(allStockHistories.First(u => u.Id == element.Id).RecordedValue, originalRecordedValue);
        }

        private List<StockHistory> GetStockHistoryList()
        {
            return new List<StockHistory>()
            {
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 0,
                    IsDeleted = false,
                   Id = 1
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 1,
                    IsDeleted = false,
                   Id = 2
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 2,
                    IsDeleted = false,
                   Id = 3
                },
                new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 3,
                    IsDeleted = false,
                   Id = 4
                },
                 new StockHistory()
                {
                    DateOfChange = DateTimeOffset.Now,
                    RecordedValue = 4,
                    IsDeleted = false,
                   Id = 5
                },
            };
        }
    }
}
