using Moq;
using Stockapp.Data;
using Stockapp.Data.Access;
using Stockapp.Data.Entities;
using Stockapp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stockapp.Test.RepositoryTest
{
    public class GameSettingsRepositoryTest
    {
        [Fact]
        public void GetGameSettingsRepository()
        {
            var gameSet = new Mock<DbSet<GameSettings>>().SetupData(new List<GameSettings>() { new GameSettings() });

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<GameSettings>()).Returns(gameSet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            var result = unitOfWork.GameSettingsRepository.Get();

            Assert.NotNull(result);
        }

        [Fact]
        public void InsertGameSettingsRepository()
        {
            var gameSet = new Mock<DbSet<GameSettings>>().SetupData();

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<GameSettings>()).Returns(gameSet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            unitOfWork.GameSettingsRepository.Insert(new GameSettings());
            unitOfWork.Save();

            var result = unitOfWork.GameSettingsRepository.Get();

            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteGameSettingsRepository()
        {
            var gameSettings = new GameSettings()
            {
               
            };
            var gameSet = new Mock<DbSet<GameSettings>>().SetupData(new List<GameSettings>() { gameSettings });

            var context = new Mock<Context>();
            context.Setup(ctx => ctx.Set<GameSettings>()).Returns(gameSet.Object);


            var unitOfWork = new UnitOfWork(context.Object);

            unitOfWork.GameSettingsRepository.Delete(gameSettings.Id);
            unitOfWork.Save();
            var settings = unitOfWork.GameSettingsRepository.GetById(gameSettings.Id);
            Assert.Null(settings);
        }
    }
}
