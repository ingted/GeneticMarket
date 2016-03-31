using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Logic;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Core.Genetic
{
    public partial class GeneticEnvironment
    {
        private void generateInitialPopulation()
        {
            addStrategy(commonContext.CommonConfig.InitialPopulationSize);
        }

        private void addStrategy(int newCount)
        {
            int counter = 0;
            while (true)
            {
                if (counter >= newCount) break;

                Strategy s = new Strategy(commonContext.IndicatorRepository);

                //create a unique strategy
                int ruleCount = Rand.Random.Next(commonContext.EnvParams.MinRulePerStrategy,
                    commonContext.EnvParams.MaxRulePerStrategy);

                for (int i = 0; i < ruleCount; i++)
                {
                    IRule rule = RuleFactory.GetInstance().CreateRandomInstance();
                    s.AddRule(rule);
                }

                int tryCounter = 0;

                do
                {
                    //randomize all rules' states
                    s.Randomize(1);
                    tryCounter++;

                } while (commonContext.StrategyRepository.AddStrategy(s) == false && tryCounter < commonContext.CommonConfig.MaxTryPerAddStrategy);

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
