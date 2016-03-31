using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.BackEnd.Core;

namespace GeneticMarket.Core.Genetic
{
    public partial class GeneticEnvironment
    {
        private void deleteBadStrategies()
        {
            bool highPressure = false;
            int strCount = commonContext.StrategyRepository.StrategyCount;
            if (strCount > commonContext.EnvParams.MaxStrategyInRepository)
            {
                highPressure = true;
            }

            double reputation = 0d;
            int balance = 0;
            int positionCount = 0;

            for (int i = 0; i < commonContext.StrategyRepository.StrategyCount; i++)
            {
                Strategy strategy = commonContext.StrategyRepository[i];

                commonContext.PerformanceLogic.GetStrategyPerformanceInfo(strategy,
                    ref positionCount, ref balance, ref reputation);

                if (positionCount >= commonContext.EnvParams.BadStrategyMinPosition)
                {
                    if (reputation <= commonContext.EnvParams.BadStrategyMaxReputation)
                    {
                        commonContext.StrategyRepository.DeleteStrategy(strategy);
                        i--;
                    }
                }
            }

            GC.Collect();
        }
    }
}
