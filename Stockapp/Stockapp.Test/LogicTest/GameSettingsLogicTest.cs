using Moq;
using Stockapp.Data.Entities;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stockapp.Test.LogicTest
{
    public class GameSettingsLogicTest
    {
        [Fact]
        public void GetGameSettingTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.GameSettingsRepository.Get(null, null, "")).Returns(new List<GameSettings>() { new GameSettings()});

            IGameSettingsLogic gameSettings = new GameSettingsLogic(mockUnitOfWork.Object);
            var result = gameSettings.GetOrCreateGameSettings();

            mockUnitOfWork.Verify(un => un.GameSettingsRepository.Get(null, null, ""), Times.Once());
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAndCreateGameSettingTest()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.GameSettingsRepository.Get(null, null, ""));
            mockUnitOfWork.Setup(un => un.GameSettingsRepository.Insert(It.IsAny<GameSettings>()));
            mockUnitOfWork.Setup(un => un.Save());

            IGameSettingsLogic gameSettings = new GameSettingsLogic(mockUnitOfWork.Object);
            var result = gameSettings.GetOrCreateGameSettings();

            mockUnitOfWork.Verify(un => un.GameSettingsRepository.Get(null, null, ""), Times.Once());
            mockUnitOfWork.Verify(un => un.GameSettingsRepository.Insert(It.IsAny<GameSettings>()), Times.Once());
            mockUnitOfWork.Verify(un => un.Save(), Times.Once());

            Assert.NotNull(result);
        }
    }
}
