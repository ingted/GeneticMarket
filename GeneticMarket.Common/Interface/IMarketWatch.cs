using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;

namespace GeneticMarket.Common.Interface
{
    public delegate void MarketUpdateEventHandler(Tick tick);
    public delegate void NewInstrumentEventHandler(Instrument inst);

    public interface IMarketWatch
    {
        /// <summary>
        /// Account check for SLTP and close positions if required
        /// </summary>
        event MarketUpdateEventHandler AccountProcess;

        /// <summary>
        /// Update market meter info
        /// </summary>
        event MarketUpdateEventHandler MarketMeterProcess;

        /// <summary>
        /// call strategies
        /// </summary>
        event MarketUpdateEventHandler StrategyProcess;

        /// <summary>
        /// decide to open positions
        /// </summary>
        event MarketUpdateEventHandler PositionOpenningLogicProcess;

        event MarketUpdateEventHandler GeneticProcess;

        void Register(List<string> instList, object indicatorRepository);
        void OnTick(string instrument, DateTime time, double bid, double ask);
        Instrument GetInstrument(int index);
        Instrument FindInstrument(string instStr);
        void LoadInstruments(List<string> instruments);

        int InstrumentCount { get; }
        int CurrentTickIndex { get; }
        int TickQueueSize { get; }

        void SetInstrumentStartTime(string instrument, DateTime startTime);

    }
}
