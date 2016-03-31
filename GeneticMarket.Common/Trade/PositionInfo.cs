using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;

namespace GeneticMarket.Common.Trade
{
    public class PositionInfo
    {
        public Instrument Instrument = null;
        public int Signal = SignalType.NA;

        public PositionInfo()
        {
        }

        public PositionInfo(Instrument inst, int signal)
        {
            this.Instrument = inst;
            this.Signal = signal;
        }

    }
}
