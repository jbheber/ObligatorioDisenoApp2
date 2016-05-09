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
    public class StockNewsRepositoryTest
    {
        [Fact]
        public void GetAllStockNewsTest()
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<StockNews> result = unitOfWork.StockNewsRepository.GetAll();

            Assert.Equal(result.SafeCount(), stockNewsData.Count);
        }

        [Fact]
        public void GetFilterStockNewsTest()
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<StockNews> result = unitOfWork.StockNewsRepository.Get(p => p.Title == "News1", null);

            Assert.Equal(
                result.SafeCount(),
                stockNewsData.Where(d => d.Title == "News1" && !d.IsDeleted).SafeCount());
        }

        [Fact]
        public void GetNonDeletedStockNewsTest()
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            IEnumerable<StockNews> result = unitOfWork.StockNewsRepository.Get();

            Assert.Equal(result.SafeCount(), stockNewsData.Where(d => d.IsDeleted == false).SafeCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void GetByIdTest(int index)
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);

            var allStockNews = unitOfWork.StockNewsRepository.Get();
            var entity = allStockNews.ElementAt(index);

            Assert.Equal(entity, unitOfWork.StockNewsRepository.GetById(entity.Id));
        }

        [Fact]
        public void InsertStockNewsTest()
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var stockNews = new StockNews()
            {
                ReferencedStocks = new List<Stock>(),
                PublicationDate = DateTimeOffset.Now,
                Title = "News8",
                Content = "This is the news number 8",
                IsDeleted = false,
                Id = Guid.NewGuid()
            };

            unitOfWork.StockNewsRepository.Insert(stockNews);

            var result = unitOfWork.StockNewsRepository.GetAll();

            Assert.Equal(stockNews, unitOfWork.StockNewsRepository.GetById(stockNews.Id));
        }

        [Fact]
        public void InsertSingleStockNewsTest()
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var stockNews = new StockNews()
            {
                ReferencedStocks = new List<Stock>(),
                PublicationDate = DateTimeOffset.Now,
                Title = "News10",
                Content = "This is the news number 10",
                IsDeleted = false,
                Id = Guid.NewGuid()
            };

            unitOfWork.StockNewsRepository.Insert(stockNews);

            var result = unitOfWork.StockNewsRepository.GetAll();

            Assert.True(result.isNotEmpty());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteStockNewsByIdTest(int index)
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var elementId = stockNewsData.ElementAt(index).Id;

            unitOfWork.StockNewsRepository.Delete(elementId);
            unitOfWork.Save();

            var allStockNews = unitOfWork.StockNewsRepository.GetAll();

            Assert.True(allStockNews.First(u => u.Id == elementId).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void DeleteStockNewsTest(int index)
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = stockNewsData.ElementAt(index);

            unitOfWork.StockNewsRepository.Delete(element);
            unitOfWork.Save();

            var allStockNews = unitOfWork.StockNewsRepository.GetAll();

            Assert.True(allStockNews.First(p => p.Id == element.Id).IsDeleted);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void UpdateStockNewsTest(int index)
        {
            var stockNewsData = GetStockNewsList();
            var stockNewsSet = new Mock<DbSet<StockNews>>().SetupData(stockNewsData);

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<StockNews>()).Returns(stockNewsSet.Object);

            var unitOfWork = new UnitOfWork(context.Object);
            var element = stockNewsData.ElementAt(index);

            var originalTitle = element.Title;

            element.Title = "News30";

            unitOfWork.StockNewsRepository.Update(element);
            unitOfWork.Save();

            var allStockNews = unitOfWork.StockNewsRepository.GetAll();

            Assert.NotEqual(allStockNews.First(u => u.Id == element.Id).Title, originalTitle);
        }

        private List<StockNews> GetStockNewsList()
        {
            return new List<StockNews>()
            {
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News1",
                    Content = "This is the news number 1",
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News2",
                    Content = "This is the news number 2",
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News3",
                    Content = "This is the news number 3",
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News4",
                    Content = "This is the news number 4",
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
                 new StockNews()
                {
                    ReferencedStocks = new List<Stock>(),
                    PublicationDate = DateTimeOffset.Now,
                    Title = "News5",
                    Content = "This is the news number 5",
                    IsDeleted = false,
                    Id = Guid.NewGuid()
                },
            };
        }
    }
}

