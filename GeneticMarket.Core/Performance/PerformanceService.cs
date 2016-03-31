using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Trade;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Core.Performance
{
    /// <summary>
    /// services that are publicly provided to external classes
    /// </summary>
    public partial class PerformanceLogic
    {
        /// <summary>
        /// used to determine if strategy should be deleted
        /// </summary>
        public void GetStrategyPerformanceInfo(Strategy strategy, ref int positionCount, ref int balance, ref double reputation)
        {
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;

            positionCount = spi.winningPositionCount + spi.loosingPositionCount;
            balance = spi.totalWin - spi.totalLoss;
            reputation = spi.reputation;
        }

        /// <summary>
        /// positionLogic uses this number to decide if the strategy can open a position or not
        /// </summary>
        public int GetStrategyOpenPositionCount(Strategy strategy)
        {
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;
            return spi.openPositions.Count;
        }

        public int GetBestSignal(string inst)
        {
            Instrument instrument = commonContext.MarketWatch.FindInstrument(inst);

            int buySignal = getBestSignal(instrument, SignalType.Long);
            int sellSignal = getBestSignal(instrument, SignalType.Short);

            if (buySignal == SignalType.Long && sellSignal != SignalType.Short)
            {
                return SignalType.Long;
            }
            else if (buySignal != SignalType.Long && sellSignal == SignalType.Short)
            {
                return SignalType.Short;
            }

            return SignalType.Neutral;
        }

        private int getBestSignal(Instrument inst,int signalType)
        {
            double maxWeight = -1;
            int bestStrategySignal = SignalType.Neutral;

            for (int i = 0; i < commonContext.StrategyRepository.StrategyCount; i++)
            {
                try
                {
                    Strategy s = commonContext.StrategyRepository[i];

                    if (s != null && s.DefaultInstrument.Equals(inst))
                    {
                        double weight = getStrategyWeight(s, inst, signalType);

                        if (weight > maxWeight)
                        {
                            maxWeight = weight;
                            bestStrategySignal = s.GetSignal();
                        }
                    }

                }
                catch (Exception exc)
                {
                    //in the case of exception we suppose that its becasue of thread concurrency
                    //in order to increase performance of main thread, we do not add concurrency detection code
                    //there just bypass errors here
                    Logger.Log(exc);
                }
            }

            return bestStrategySignal;
        }

        public void GetSignalWeight(Instrument inst, int signalType, ref double forWeight, ref double againstWeight)
        {
            forWeight = 0d;
            againstWeight = 0d;
            //as this method is called from server while client code is being run in another thread, 
            //maybe number of strategies gets changed during execution
            for (int i = 0; i < commonContext.StrategyRepository.StrategyCount; i++)
            {
                try
                {
                    Strategy s = commonContext.StrategyRepository[i];

                    if (s != null && s.DefaultInstrument.Equals(inst))
                    {
                        int signal = s.GetSignal();

                        if (signal != SignalType.Neutral)
                        {
                            double sWeight = getStrategyWeight(s, inst, signalType);

                            if (sWeight > 0)
                            {
                                if (signal == signalType)
                                {
                                    forWeight += sWeight;
                                }
                                else if (signal == -1 * signalType)
                                {
                                    againstWeight += sWeight;
                                }
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    //in the case of exception we suppose that its becasue of thread concurrency
                    //in order to increase performance of main thread, we do not add concurrency detection code
                    //there just bypass errors here
                    Logger.Log(exc);
                }
            }
        }

        private double getStrategyWeight(Strategy s, Instrument inst, int signalType)
        {
            //in rare cases concurrency of strategy executions and signal info stat thread 
            //causes this
            if (s == null) return 0;

            PerformanceInfo smi = s.PerformanceInfo as PerformanceInfo;

            return smi.archive.GetWeight(signalType,
                commonContext.MarketMeter.GetMarketTime(inst),
                commonContext.MarketMeter.GetMarketTrend(inst, s.DefaultPeriod),
                commonContext.MarketMeter.GetMarketVolatility(inst, s.DefaultPeriod));
        }        
    }
}
