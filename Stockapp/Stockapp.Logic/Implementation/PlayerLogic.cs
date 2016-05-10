using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;

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

        public bool DeletePlayer(Guid player)
        {
            UnitOfWork.PlayerRepository.Delete(player);
            UnitOfWork.Save();
            return true;
        }

        public Player GetPlayer(Guid playerId)
        {
            return UnitOfWork.PlayerRepository.GetById(playerId);
        }

        public bool RegisterPlayer(Player player)
        {
            UnitOfWork.PlayerRepository.Insert(player);
            UnitOfWork.Save();
            return true;
        }

        public bool UpdatePlayer(Player player)
        {
            UnitOfWork.PlayerRepository.Update(player);
            UnitOfWork.Save();
            return true;
        }
    }
}
