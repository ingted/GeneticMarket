using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Helper;
using GeneticMarket.BackEnd.Core;

namespace GeneticMarket.Core.Genetic
{
    public partial class GeneticEnvironment
    {
        private void doMutation(int mutationCount)
        {
            int counter = 0;
            while (true)
            {
                if (counter > mutationCount) break;

                Logger.Log("gpm counter={0}", counter);

                int idx = Rand.Random.Next(0, commonContext.StrategyRepository.StrategyCount);
                Strategy newStrategy = commonContext.StrategyRepository[idx].Duplicate();

                int tryCounter = 0;

                do
                {
                    Logger.Log("gpm randomize");
                    newStrategy.Randomize(commonContext.EnvParams.MutationRuleRandomizationRatio);
                    tryCounter++;

                    Logger.Log("gpm trycounter={0}", tryCounter);

                } while (commonContext.StrategyRepository.AddStrategy(newStrategy) == false && tryCounter < commonContext.CommonConfig.MaxTryPerAddStrategy);

                if (tryCounter == commonContext.CommonConfig.MaxTryPerAddStrategy)
                {
                    //all strategy choice space is filled
                    break;
                }
                counter++;
            }
        }

    }
}
