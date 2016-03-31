using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Trade;
using GeneticMarket.Core.Logic;
using GeneticMarket.Core.Trade;

namespace GeneticMarket.Core
{
    public class LogicContext
    {
        public TSVRLogic tsvrLogic = null;
        public PositionLogic positionLogic = null;
        public PerformanceLogic performanceLogic = null;
        public PortfolioManager portfolioManager = null;
        public Account account = null;

        public int logicIndex = 0;


        public void Register(CommonContext commonContext, int index)
        {
            tsvrLogic = new TSVRLogic();
            positionLogic = new PositionLogic();
            performanceLogic = new PerformanceLogic();
            account = new Account();
            portfolioManager = new PortfolioManager();

            logicIndex = index;

            tsvrLogic.Register(commonContext, this);
            positionLogic.Register(commonContext, this);
            performanceLogic.Register(commonContext, this);
            portfolioManager.Register(commonContext,this);
        }
    }
}
