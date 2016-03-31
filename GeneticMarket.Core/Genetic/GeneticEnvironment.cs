using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Config;
using GeneticMarket.Common.Market;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Base.Helper;
using GeneticMarket.Common.Interface;
using GeneticMarket.Core.Logic;
using GeneticMarket.Logic;
using System.Diagnostics;
using System.Windows.Forms;

namespace GeneticMarket.Core.Genetic
{
    public partial class GeneticEnvironment
    {
        private ClientContext commonContext = null;

        public void Register(ClientContext context)
        {
            commonContext = context;

            commonContext.MarketWatch.GeneticProcess += new MarketUpdateEventHandler(OnTick);
        }

        public void Start()
        {
            //maybe repository is loaded from a file. in this case no initial pop geenration is needed
            if (commonContext.StrategyRepository.StrategyCount == 0)
            {
                generateInitialPopulation();
            }
        }

        private void OnTick(Tick tick)
        {
            Logger.Log("gp start");

            EnvironmentParameters envParams = commonContext.EnvParams;

            

            if (commonContext.MarketWatch.CurrentTickIndex % envParams.GCProcessPeriod == 0)
            {
                Logger.Log("gp gc");
                GC.Collect();
                Logger.Log("gp gc done");
            }

            //change population every 5 minutes
            if (commonContext.MarketWatch.CurrentTickIndex % envParams.EvaluateTickPeriod == 0)
            {
                Logger.Log("gp logic");

                if (envParams.DeleteEnabled)
                {
                    Logger.Log("gp delete");
                    //delete bad ones from strategy repository (if they have positions close them)
                    deleteBadStrategies();
                    Logger.Log("gp delete done");
                }

                long memUsage = Process.GetCurrentProcess().WorkingSet64;

                if (memUsage < envParams.MaxMemUsage)
                {
                    Logger.Log("gp mem ok");

                    int strCount = commonContext.StrategyRepository.StrategyCount;

                    if (strCount < commonContext.EnvParams.MaxStrategyInRepository)
                    {                        
                        if (envParams.MutationEnabled)
                        {
                            Logger.Log("gp mutation");
                            //create news offspring and add to strategy repository
                            int mutationCount = getCount(strCount, envParams.MutationProbability, 0, envParams.MutationMax);
                            Logger.Log("gpm count={0}",mutationCount);
                            doMutation(mutationCount);
                            Logger.Log("gp mutation done");
                        }

                        if (envParams.CrossOverEnabled)
                        {
                            Logger.Log("gp co");
                            int crossOverCount = getCount(strCount, envParams.CrossOverProbability, 0, envParams.CrossOverMax);
                            doCrossOver(crossOverCount);
                            Logger.Log("gp co done");
                        }

                        if (envParams.AddEnabled)
                        {
                            Logger.Log("gp add");
                            int newCount = Rand.Random.Next(1, envParams.MaxAddStrategy);
                            addStrategy(newCount);
                            Logger.Log("gp add done");
                        }
                    }
                }

                Logger.Log("gp finish");
            }
        }

        private int getCount(int total, double ratio, int min, int max)
        {
            int result = (int)(total * ratio);

            result = Math.Min(max, result);
            result = Math.Max(min, result);

            return result;
        }
    }
}
