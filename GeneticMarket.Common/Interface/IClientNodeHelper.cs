using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;

namespace GeneticMarket.Core
{
    public interface IClientNodeHelper: IStrategyRepositoryHelper
    {
        void Disconnect(int id);
    }
}
