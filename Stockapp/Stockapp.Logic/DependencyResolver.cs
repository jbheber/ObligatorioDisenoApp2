using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Resolver;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using Stockapp.Data.Repository;

namespace Stockapp.Logic
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUserLogic, UserLogic>();
            registerComponent.RegisterType<IPlayerLogic, PlayerLogic>();
            registerComponent.RegisterType<IAdminLogic, AdminLogic>();
            registerComponent.RegisterType<IStockLogic, StockLogic>();
            registerComponent.RegisterType<IStockHistoryLogic, StockHistoryLogic>();
            registerComponent.RegisterType<IStockNewsLogic, StockNewsLogic>();
            registerComponent.RegisterType<ITransactionLogic, TransactionLogic>();
            registerComponent.RegisterType<IInvitationCodeLogic, InvitationCodeLogic>();
            registerComponent.RegisterType<IPortfolioLogic, PortfolioLogic>();
            registerComponent.RegisterType<IGameSettingsLogic, GameSettingsLogic>();

            registerComponent.RegisterTypeWithControlledLifeTime<IUnitOfWork,UnitOfWork>();

        }
    }
}
