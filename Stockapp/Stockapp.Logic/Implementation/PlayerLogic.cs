using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Data.Extensions;

namespace Stockapp.Logic.Implementation
{
    public class PlayerLogic : IPlayerLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public PlayerLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public bool DeletePlayer(Player player)
        {
            UnitOfWork.PlayerRepository.Delete(player);
            UnitOfWork.Save();
            return true;
        }

        public bool DeletePlayer(long player)
        {
            UnitOfWork.PlayerRepository.Delete(player);
            UnitOfWork.Save();
            return true;
        }

        public Player GetPlayer(long userId)
        {
            var players = UnitOfWork.PlayerRepository.Get(p => p.UserId == userId, null, "User,Portfolio");
            return players.IsNotEmpty() ? players.SingleOrDefault() : null;
        }

        public bool RegisterPlayer(Player player)
        {
            UnitOfWork.PortfolioRepository.Insert(player.Portfolio);
            UnitOfWork.PlayerRepository.Insert(player);
            UnitOfWork.Save();
            return true;
        }

        public bool UpdatePlayer(Player player)
        {
            UnitOfWork.UserRepository.Update(player.User);
            UnitOfWork.PlayerRepository.Update(player);
            UnitOfWork.Save();
            return true;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
