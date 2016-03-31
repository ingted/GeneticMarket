using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Core.Logic;
using GeneticMarket.Common.Trade;
using GeneticMarket.Common.Market;
using System.Threading;

namespace GeneticMarket.Core.Logic
{
    public class StrategyLogic
    {
        private LogicContext logicContext = null;

        //we set tick so worker thread reads and processes this tick
        private Tick currentTick = Tick.Empty;
        private Thread workerThread = null;

        public int TotalPositionCount
        {
            get
            {
                return logicContext.account.TotalPositionCount;
            }
        }

        public int ClosedPositionCount
        {
            get
            {
                return logicContext.account.ClosedPositionCount;
            }
        }

        public void GetSignalInfo(Instrument instrument, int signalType, ref double forWeight,ref double againstWeight)
        {
            logicContext.positionLogic.GetSignalInfo(instrument, signalType, ref forWeight, ref againstWeight);
        }

        public void Register(CommonContext commonContext,int logIndex)
        {
            logicContext = new LogicContext();
            logicContext.Register(commonContext, logIndex);

            workerThread = new Thread(new ThreadStart(logicProcessorThread));
            workerThread.Name = "StrategyLogicThread";
            workerThread.Start();
        }

        public void SetTick(Tick tick)
        {
            lock (currentTick)
            {
                currentTick = tick;
            }
        }

        private void logicProcessorThread()
        {
            while (true)
            {
                lock (currentTick)
                {
                    if (!currentTick.Equals(Tick.Empty))
                    {
                        //execute all strategies in this logic
                        logicContext.tsvrLogic.ProcessStrategies(currentTick);

                        logicContext.account.OnTick(currentTick);

                        logicContext.positionLogic.OnTick(currentTick);

                        logicContext.performanceLogic.CheckWeakStrategies();

                        currentTick = Tick.Empty;
                    }
                }

                Thread.Sleep(5);
            }
        }

        //void MarketWatch_StrategyLogicProcess(GeneticMarket.Common.Market.Tick tick)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
