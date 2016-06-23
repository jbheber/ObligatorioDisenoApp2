using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface IPlayerLogic
    {
        bool RegisterPlayer(Player player);

        bool DeletePlayer(Player player);

        bool DeletePlayer(long player);

        bool UpdatePlayer(Player player);

        Player GetPlayer(long userId);

        void Dispose();
    }
}
