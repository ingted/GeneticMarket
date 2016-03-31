using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Helper;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Common.Trade;

namespace GeneticMarket.Core.Performance
{
    public partial class PerformanceLogic
    {
        public int DoRefine(IStrategyRefiner refiner)
        {
            int result = 0;

            try
            {
                for (int i = 0; i < commonContext.StrategyRepository.StrategyCount; i++)
                {
                    Strategy s = commonContext.StrategyRepository[i];

                    if (CallRefineMethod(refiner, s))
                    {
                        commonContext.StrategyRepository.DeleteStrategy(s);
                        result++;
                    }
                }
            }
            catch
            {
                return -1;
            }

            return result;
        }

        public int TestRefine(IStrategyRefiner refiner)
        {
            int result = 0;

            try
            {
                for (int i = 0; i < commonContext.StrategyRepository.StrategyCount; i++)
                {
                    Strategy s = commonContext.StrategyRepository[i];

                    if (CallRefineMethod(refiner, s))
                    {
                        result++;
                    }
                }
            }
            catch
            {
                return -1;
            }

            return result;
        }

        private bool CallRefineMethod(IStrategyRefiner obj, Strategy strategy)
        {
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;

            return obj.CheckRefine(spi.winningPositionCount, spi.loosingPositionCount, spi.totalLoss, spi.totalWin,
                spi.largestLoss, spi.largestWin, spi.closedPositions.Count, spi.openPositions.Count, spi.reputation);

        }
    }
}
