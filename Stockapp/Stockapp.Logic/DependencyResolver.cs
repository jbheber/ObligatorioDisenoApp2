﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Resolver;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;

namespace Stockapp.Logic
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUserLogic, UserLogic>();
            registerComponent.RegisterType<IStockHistory, StockHistoryLogic>();
            registerComponent.RegisterType<IStockNewsLogic, StockNewsLogic>();

            //registerComponent.RegisterTypeWithControlledLifeTime<IUnitOfWork,UnitOfWork>();

        }
    }
}