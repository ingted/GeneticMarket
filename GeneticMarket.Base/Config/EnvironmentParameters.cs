using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticMarket.Base.Config
{
    [Serializable]
    public class EnvironmentParameters
    {
        //how many strategues to randomize
        public double MutationProbability = 0.03;
        public int MutationMax = 50;

        //how many rules to randomize?
        public double MutationRuleRandomizationRatio = 0.3;

        public double CrossOverProbability = 0.03;
        public int CrossOverMax = 50;

        /// <summary>
        /// number of rules or each strategy being crossed over to swap
        /// </summary>
        public double CrossOverRuleCountRatio = 0.4;

        public int MaxAddStrategy = 100;
        public int MaxRulePerStrategy = 1;
        public int MinRulePerStrategy = 1;

        public int BadStrategyMinPosition = 15;
        public double BadStrategyMaxReputation = 1d;

        public int EvaluateTickPeriod = 50;

        public bool MutationEnabled = true;
        public bool CrossOverEnabled = true;
        public bool AddEnabled = true;
        public bool DeleteEnabled = true;

        //for positionLogic
        public bool OpenPositionEnabled = true;

        public int MaxStrategyInRepository = 100000;
        public int GCProcessPeriod = 2000;  //run GC every 2000 ticks
        public long MaxMemUsage = 1400000000; //1.4GB
    }
}
