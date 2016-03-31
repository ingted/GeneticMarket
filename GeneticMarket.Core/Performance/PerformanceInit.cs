using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Common.Trade;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Core.Performance
{
    /// <summary>
    /// initialization operations
    /// </summary>
    public partial class PerformanceLogic
    {

        private void positionOpened(Position position)
        {
            Strategy strategy = position.Owner as Strategy;
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;
            spi.openPositions.Add(position);

            PositionMetaInfo pmi = new PositionMetaInfo();

            pmi.openTime = commonContext.MarketMeter.GetMarketTime(position.Instrument);
            pmi.openTrend = commonContext.MarketMeter.GetMarketTrend(position.Instrument, strategy.DefaultPeriod);
            pmi.openVolatility = commonContext.MarketMeter.GetMarketVolatility(position.Instrument, strategy.DefaultPeriod);

            position.MetaInfo = pmi;
        }

        private void strategyCreated(Strategy strategy)
        {
            //if strategy was loaded from file, its perfInfo is not null
            if (strategy.PerformanceInfo == null)
            {
                PerformanceInfo pi = new PerformanceInfo();
                pi.archive = new PerformanceArchive();

                strategy.PerformanceInfo = pi;
            }
        }

        protected class PositionMetaInfo
        {
            //for each period we store its market TV
            public int openTrend = 0;
            public int openVolatility = 0;
            public int openTime = 0;
        }
    }
}
