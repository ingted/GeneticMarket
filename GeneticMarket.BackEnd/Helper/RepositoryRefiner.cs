using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Helper;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.BackEnd.Logic;
using GeneticMarket.Common.Interface;
using GeneticMarket.Common.Market;

namespace GeneticMarket.BackEnd.Helper
{
    public class RepositoryRefiner
    {
        //public static int TestRefine(StrategyRepository repository, IStrategyRefiner refiner)
        //{
        //    int result = 0;

        //    try
        //    {
        //        for (int i = 0; i < repository.StrategyCount; i++)
        //        {
        //            Strategy s = repository[i];

        //            if (CallRefineMethod(refiner, s))
        //            {
        //                result++;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return -1;
        //    }

        //    return result;
        //}

        //public static int DoRefine(StrategyRepository repository,IStrategyRefiner refiner)
        //{
        //    List<Strategy> deleteList = new List<Strategy>();

        //    try
        //    {
        //        for (int i = 0; i < repository.StrategyCount; i++)
        //        {
        //            Strategy s = repository[i];

        //            if (CallRefineMethod(refiner,s))
        //            {
        //                deleteList.Add(s);
        //            }
        //        }

        //        foreach (Strategy s in deleteList)
        //        {
        //            repository.DeleteStrategy(s);
        //        }
        //    }
        //    catch
        //    {
        //        return -1;
        //    }

        //    return deleteList.Count;
        //}

        //private static bool CallRefineMethod(IStrategyRefiner obj, Strategy strategy)
        //{
        //    StrategyPerformanceInfo spi = strategy.PerformanceInfo as StrategyPerformanceInfo;
        //    StrategyWeightInfo swi = strategy.WeightInfo as StrategyWeightInfo;
        //    double rep = swi.reputation;

        //    return obj.CheckRefine(spi.winningPositionCount, spi.loosingPositionCount, spi.totalLoss, spi.totalWin,
        //        spi.largestLoss, spi.largestWin, spi.closedPositions.Count, spi.openPositions.Count, rep);
            
        //}
    }
}
