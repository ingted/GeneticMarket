using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Def;
using GeneticMarket.Base.Config;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Common.Market
{
    public class Instrument
    {
        private string baseCurrency = null;
        private string quoteCurrency = null;
        private List<Tick> ticks = new List<Tick>();
        private double defaultSpread = 0.00006;
        private double point = 0.00001;
        private Dictionary<int, TimeFrame> timeFrames = new Dictionary<int, TimeFrame>();
        private string toStringCache = null;

        //stored for speedup
        public DateTime CurrentTime = DateTime.MinValue;
        //public int CurrentTickIndex = 0;

        //stored for speedup
        public double TickAsk = 0.0;

        //stored for speedup
        public double TickBid = 0.0;

        public Instrument(string baseCur, string quoteCur, double defPoint, double defSpread,int[] tfPeriods)
        {
            internalInit(baseCur, quoteCur, defPoint, defSpread, tfPeriods);
        }

        private void internalInit(string baseCur, string quoteCur, double defPoint, double defSpread, int[] tfPeriods)
        {
            baseCurrency = baseCur;
            quoteCurrency = quoteCur;

            defaultSpread = defSpread;
            point = defPoint;

            if (tfPeriods != null)
            {
                foreach (int tf in tfPeriods)
                {
                    timeFrames.Add(tf, new TimeFrame(tf));
                }
            }
        }

        public Instrument(string baseCur, string quoteCur, int[] tfPeriods)
            : this(baseCur, quoteCur, 0.00001, 0.00015, tfPeriods)
        {
        }

        public Instrument(string currncyPair)
        {
            internalInit(currncyPair.Substring(0, 3), currncyPair.Substring(3, 3), 0.00001, 0.00015, GeneralConfig.ValidTimeFrames);
        }

        public Instrument(string currncyPair,int[] tfPeriods)
        {
            internalInit(currncyPair.Substring(0, 3), currncyPair.Substring(3, 3), 0.00001, 0.00015, tfPeriods);
        }

        public int ToPips(double firstPrice, double secondPrice)
        {
            return (int)(Math.Round((secondPrice - firstPrice) / point,5));
        }

        public double ToPrice(int pips)
        {
            return pips * point;
        }

        public TimeFrame this[int period]
        {
            get
            {
                return timeFrames[period];
            }
        }

        public Tick GetTick(int relativeIndex)
        {
            return ticks[ticks.Count - 1 - relativeIndex];
        }

        //method is called by market watch
        //returns the newly added tick (this is also avilable via this[0]
        public Tick Tick(DateTime tickTime, double tickBid, double tickAsk)
        {
            if (tickAsk == 0d)
            {
                tickAsk = Math.Round(tickBid + defaultSpread,5);
            }

            //if tickAsk is not available add default spread
            Tick t = new Tick(tickTime, tickBid, tickAsk, this);
            ticks.Add(t);

            //store data in local vars too for speedup
            TickAsk = tickAsk;
            TickBid = tickBid;
            CurrentTime = tickTime;

            foreach (TimeFrame tf in timeFrames.Values)
            {
                tf.FeedTick(t);
            }

            //CurrentTickIndex++;
            return t;
        }

        public override string ToString()
        {
            if (toStringCache == null)
            {
                toStringCache = baseCurrency + quoteCurrency;
            }

            return toStringCache;
        }
    }
}
