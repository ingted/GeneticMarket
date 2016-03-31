using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Common.Trade;
using GeneticMarket.Base.Config;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using GeneticMarket.Common.Interface;
using System.Windows.Forms;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Core.Performance
{
    public partial class PerformanceLogic
    {
        protected ClientContext commonContext = null;
        
        protected void updateReputation(Strategy strategy)
        {
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;

            int balance = spi.totalWin - spi.totalLoss;
            int tradeCount = spi.loosingPositionCount + spi.winningPositionCount;

            double newRep = (double)balance / tradeCount;

            if (tradeCount >= commonContext.CommonConfig.ExperienceTradeCount)
            {
                spi.reputation = newRep;
            }
            else
            {
                double c = (double)tradeCount / commonContext.CommonConfig.ExperienceTradeCount;
                spi.reputation = newRep * c;
            }
        }

        public void Register(ClientContext context)
        {
            commonContext = context;

            commonContext.MarketWatch.StrategyProcess += new MarketUpdateEventHandler(processStrategies);

            commonContext.DefaultAccount.OnClosePosition += new OnPositionDelegate(positionClosed);
            commonContext.DefaultAccount.OnOpenPosition += new OnPositionDelegate(positionOpened);
            
            commonContext.StrategyRepository.OnDeleteStrategy += new OnStrategyDelegate(strategyDeleted);
            commonContext.StrategyRepository.OnNewStrategy += new OnStrategyDelegate(strategyCreated);
        }

        private void processStrategies(Tick tick)
        {
            for (int i = 0; i < commonContext.StrategyRepository.StrategyCount; i++)
            {
                Strategy strategy = commonContext.StrategyRepository[i];

                strategy.Process(tick);
            }
        }

        private void strategyDeleted(Strategy strategy)
        {
            //delete positions that are linked to this strategy
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;

            //we cannot iterate on spi.OpenPositions because in tsvrManager_StrategyPositionClosed method
            //the collection is modified(position is removed)
            ArrayList arr = new ArrayList();

            foreach (Position position in spi.openPositions)
            {
                arr.Add(position);
            }

            foreach (Position position in arr)
            {
                commonContext.DefaultAccount.ClosePosition(position);
            }
        }

        private void positionClosed(Position position)
        {
            Strategy strategy = position.Owner as Strategy;
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;

            spi.openPositions.Remove(position);
            spi.closedPositions.Add(position);

            int profit = position.GetProfitLoss();

            if (profit > 0)
            {
                spi.totalWin += profit;
                spi.winningPositionCount++;

                if (profit > spi.largestWin)
                {
                    spi.largestWin = profit;
                }
            }
            else if ( profit < 0 )
            {
                profit = -profit;

                spi.totalLoss += profit;
                spi.loosingPositionCount++;

                if (profit > spi.largestLoss)
                {
                    spi.largestLoss = profit;
                }
            }

            if (profit != 0)
            {
                //a position is decided to be closed (closers list and closeWeight is updated elsewhere)
                //update TSVR of closers
                PositionMetaInfo pmi = position.MetaInfo as PositionMetaInfo;

                updateReputation(strategy);

                spi.archive.SubmitPerformance(profit,
                    position.PositionType,
                    pmi.openTime,
                    pmi.openTrend,
                    pmi.openVolatility);
            }      
        }
    }
}
