using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticMarket.Base.Config
{
    [Serializable]
    //higher level configurations 
    public class CommonConfig
    {
        //when generating random strategies and adding to repository
        //max try is below
        public int MaxTryPerAddStrategy = 500;

        //a strategy cannot signal long position every tick! only a limited number of orders
        //can be opened per strategy signals
        public int MaxOpenPositionPerStrategy = 1;
        public int InitialPopulationSize = 25;

        //close conditions to prevent scalping
        public int PositionMinProft = 20;
        public int PositionMinAge = 120;  //in seconds

        public int StrategyViewUpdatePeriod = 20; //every 20 ticks update UI
        public int AutoSaveMinutes = 10;
        public string AutoSaveFileName = "autosave.{0}.zip";

        public int ExperienceTradeCount = 10; //minimum number of reqired trades to trust this strategy

        

    }
}
