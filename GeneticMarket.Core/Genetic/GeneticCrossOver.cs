using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Base.Helper;
using GeneticMarket.Common.Interface;

namespace GeneticMarket.Core.Genetic
{
    public partial class GeneticEnvironment
    {
        private void doCrossOver(int crossOverCount)
        {
            int strCount = commonContext.StrategyRepository.StrategyCount;

            List<int> firstIndices = Rand.GetRandomIndices(strCount, crossOverCount);
            List<int> secondIndices = Rand.GetRandomIndices(strCount, crossOverCount);
            Strategy newStrategy = null;

            for (int i = 0; i < firstIndices.Count; i++)
            {
                int idx1 = firstIndices[i];
                int idx2 = secondIndices[i];
                bool randomizeWeights = false;
                int counter = 0;

                Strategy strategy1 = commonContext.StrategyRepository[idx1];
                Strategy strategy2 = commonContext.StrategyRepository[idx2];

                int tryCounter = 0;

                do
                {
                    newStrategy = crossOverStrategies(strategy1, strategy2, randomizeWeights);
                    counter++;

                    if (counter > (strategy1.RuleCount * strategy2.RuleCount))
                    {
                        randomizeWeights = true;
                    }

                    tryCounter++;

                    if (tryCounter == commonContext.CommonConfig.MaxTryPerAddStrategy)
                    {
                        //all strategy choice space is filled
                        break;
                    }

                } while (commonContext.StrategyRepository.AddStrategy(newStrategy) == false && 
                    tryCounter < commonContext.CommonConfig.MaxTryPerAddStrategy);
            }
        }

        private Strategy crossOverStrategies(Strategy Strategy1, Strategy Strategy2, bool randomizeWeights)
        {
            Strategy newStrategy = new Strategy(commonContext.IndicatorRepository);

            //calculate number of rules to mix from strategy 1 and 2
            int ruleCount = getCount(Strategy1.RuleCount + Strategy2.RuleCount,
                commonContext.EnvParams.CrossOverRuleCountRatio, commonContext.EnvParams.MinRulePerStrategy * 2, 
                Strategy1.RuleCount + Strategy2.RuleCount);

            ruleCount /= 2;

            List<int> crossOverRuleIndexList1 = Rand.GetRandomIndices(Strategy1.RuleCount, ruleCount);
            List<int> crossOverRuleIndexList2 = Rand.GetRandomIndices(Strategy2.RuleCount, ruleCount);

            foreach (int index1 in crossOverRuleIndexList1)
            {
                IRule rule = Strategy1.CopyRule(index1, newStrategy);

                if (randomizeWeights) rule.RandomizeState();
            }

            foreach (int index2 in crossOverRuleIndexList2)
            {
                IRule rule = Strategy2.CopyRule(index2, newStrategy);

                if (randomizeWeights) rule.RandomizeState();
            }

            Strategy1.CopyInfo(newStrategy);

            return newStrategy;
        }

    }
}
