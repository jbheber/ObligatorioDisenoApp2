using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data.Entities;
using Stockapp.Data.Repository;
using Stockapp.Data.Extensions;

namespace Stockapp.Logic.Implementation
{
    public class GameSettingsLogic : IGameSettingsLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public GameSettingsLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public GameSettings GetOrCreateGameSettings()
        {
            var gameSettings = UnitOfWork.GameSettingsRepository.Get();
            if (gameSettings.isEmpty())
            {
                var newSettings = new GameSettings();
                UnitOfWork.GameSettingsRepository.Insert(newSettings);
                UnitOfWork.Save();
                return newSettings;
            }
            return gameSettings.SingleOrDefault();
        }

        public bool UpdateOrCreateGameSettings(GameSettings settings)
        {
            if (UnitOfWork.GameSettingsRepository.GetById(settings.Id) != null)
            {
                var newSettings = new GameSettings();
                UnitOfWork.GameSettingsRepository.Insert(newSettings);
                UnitOfWork.Save();
                return true;
            }
            UnitOfWork.GameSettingsRepository.Update(settings);
            return true;
        }
    }
}
