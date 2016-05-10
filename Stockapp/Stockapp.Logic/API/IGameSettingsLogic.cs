using Stockapp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface IGameSettingsLogic
    {
        GameSettings GetOrCreateGameSettings();
        bool UpdateOrCreateGameSettings(GameSettings settings);
    }
}
