using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Common.Market;
using GeneticMarket.Core;
using GeneticMarket.Base.Def;
using GeneticMarket.Common.Trade;
using GeneticMarket.Master.Interface;
using GeneticMarket.Master.Signal;

namespace GeneticMarket.Master
{
    public class MasterValidator: IMasterValidator
    {
        protected IMasterStrategy masterStrategy = null;
        protected ValidatorSignal signal = new ValidatorSignal("");
        private Dictionary<string, PerformanceInfo> performanceInfo = new Dictionary<string, PerformanceInfo>();
        private int defaultStopLoss = 20;
        private int defaultTakeProfit = 25;

        public void Register(IMasterStrategy ms, Control group)
        {
            masterStrategy = ms;

            signal.Register(this, group);
        }
      
        public void UnRegister()
        {
            performanceInfo = new Dictionary<string, PerformanceInfo>();
            signal.UnRegister();
        }

        public void OnTick(Tick tick)
        {
            //check close position by SL/TP
            processSLTP(tick);

            int signal = getMasterSignal(tick);

            if (signal != SignalType.Neutral)
            {
                //check close position by signal
                closePosition(tick.Instrument, -signal);

                //check open position by signal
                openPosition(tick.Instrument, signal);
            }
        }

        private void processSLTP(Tick tick)
        {
            string inst = tick.Instrument.ToString();

            if (!performanceInfo.ContainsKey(inst))
            {
                return;
            }

            List<Position> closeList = new List<Position>();

            foreach (Position p in performanceInfo[inst].openPositions)
            {
                if (p.OnTick(tick))
                {
                    closeList.Add(p);
                }
            }

            foreach (Position p in closeList)
            {
                closePosition(p);
            }
        }

        private void openPosition(Instrument instrument, int type)
        {
            string inst = instrument.ToString();

            if (performanceInfo.ContainsKey(inst) == false)
            {
                performanceInfo.Add(inst, new PerformanceInfo());
            }

            //we cannot open more than one position per instrument at any time
            if (performanceInfo[inst].openPositions.Count > 0)
            {
                return;
            }

            Position p = new Position(instrument, type, defaultStopLoss, defaultTakeProfit, 0, defaultStopLoss);
            performanceInfo[inst].openPositions.Add(p);
        }

        private void closePosition(Instrument instrument, int type)
        {
            if ( !performanceInfo.ContainsKey(instrument.ToString()) )
            {
                return;
            }

            PerformanceInfo spi = performanceInfo[instrument.ToString()];
            List<Position> closeList = new List<Position>();

            foreach (Position p in spi.openPositions)
            {
                if (p.PositionType == type)
                {
                    closeList.Add(p);
                }
            }

            foreach (Position p in closeList)
            {
                closePosition(p);
            }

        }

        private void closePosition(Position p)
        {
            PerformanceInfo spi = performanceInfo[p.Instrument.ToString()];

            p.OnClose();
            spi.openPositions.Remove(p);
            spi.closedPositions.Add(p);

            int profit = p.GetProfitLoss();

            if (profit > 0)
            {
                spi.totalWin += profit;
                spi.winningPositionCount++;

                if (profit > spi.largestWin)
                {
                    spi.largestWin = profit;
                }
            }
            else if (profit < 0)
            {
                profit = Math.Abs(profit);
                spi.totalLoss += profit;
                spi.loosingPositionCount++;

                if (profit > spi.largestLoss)
                {
                    spi.largestLoss = profit;
                }
            }
        }

        protected virtual int getMasterSignal(Tick tick)
        {
            double longWeight = 0;
            double shortWeight = 0;

            foreach (ClientContextProvider provider in masterStrategy.ContextProviders)
            {
                double tempLong = 0d;
                double tempShort = 0d;

                provider.GetSignalWeight(tick.Instrument.ToString(), SignalType.Long, ref tempLong, ref tempShort);

                longWeight += tempLong;
                shortWeight += tempShort;
            }

            if (longWeight < 0) longWeight = 0;
            if (shortWeight < 0) shortWeight = 0;

            if (longWeight > 0 && longWeight > shortWeight)
            {
                return SignalType.Long;
            }
            else if (shortWeight > 0 && shortWeight > longWeight)
            {
                return SignalType.Short;
            }

            return SignalType.Neutral;
        }


        #region IMasterValidator Members

        public PerformanceInfo GetActivePerformance()        
        {
            if (!performanceInfo.ContainsKey(masterStrategy.ActiveInstrument))
            {
                return null;
            }

            return performanceInfo[masterStrategy.ActiveInstrument];
        }

        #endregion

        internal void Reset()
        {
            performanceInfo.Clear();
        }
    }
}
