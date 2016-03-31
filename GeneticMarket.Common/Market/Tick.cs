using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticMarket.Common.Market
{
    public class Tick
    {
        public DateTime Time = DateTime.MinValue;
        public double Ask = 0.0;
        public double Bid = 0.0;
        public Instrument Instrument = null;

        public Tick()
        {
        }

        public Tick(DateTime t, double bid, double ask, Instrument inst)
        {
            Time = t;
            Bid = bid;
            Ask = ask;
            Instrument = inst;
        }
    }
}
