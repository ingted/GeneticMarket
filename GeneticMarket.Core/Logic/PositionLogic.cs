using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Trade;
using GeneticMarket.Common.Interface;
using GeneticMarket.Common.Market;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Base.Config;
using GeneticMarket.Base.Def;
using GeneticMarket.Base.Helper;
using System.IO;

namespace GeneticMarket.Core.Logic
{
    public class PositionLogic
    {
        private ClientContext commonContext = null;

        public void Register(ClientContext context)
        {
            commonContext = context;

            commonContext.MarketWatch.PositionOpenningLogicProcess += new MarketUpdateEventHandler(OnMarketTick);
            commonContext.DefaultAccount.ProcessClosePosition += new OnPositionDelegate(account_ProcessPositionClose);
        }

        void account_ProcessPositionClose(Position position)
        {
            if (canPositionBeClosed(position) == false) return;

            if ((position.Owner as Strategy).GetSignal() == -position.PositionType)
            {
                commonContext.DefaultAccount.ClosePosition(position);
            }                
        }

        private bool canPositionBeClosed(Position position)
        {
            //anti-scalp
            int profitLoss = position.GetProfitLoss();

            if ( profitLoss  > 0 && profitLoss < commonContext.CommonConfig.PositionMinProft)
            {
                return false;
            }

            int openSeconds = position.Age;

            if (openSeconds < commonContext.CommonConfig.PositionMinAge)
            {
                return false;
            }

            return true;
        }

        void OnMarketTick(Tick tick)
        {
            if (commonContext.EnvParams.OpenPositionEnabled == false)
            {
                return;
            }

            //process strategy signals and decide if a position needs to be opened
            for (int i = 0; i < commonContext.StrategyRepository.StrategyCount; i++)
            {
                Strategy s = commonContext.StrategyRepository[i];

                if (commonContext.PerformanceLogic.GetStrategyOpenPositionCount(s) >= commonContext.CommonConfig.MaxOpenPositionPerStrategy) continue;

                int signal = s.GetSignal();
                if (signal == SignalType.Neutral || signal == SignalType.NA) continue;

                commonContext.PortfolioManager.OpenPosition(s.DefaultInstrument, signal,s);
            }
        }
    }
}
