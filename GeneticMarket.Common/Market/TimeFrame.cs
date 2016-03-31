using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Def;

namespace GeneticMarket.Common.Market
{
    public class TimeFrame
    {
        private List<Candle> candles = new List<Candle>();
        private int period = 1;  //number of minutes of each candle

        //last time we have received a tick
        private DateTime latestTime = DateTime.MinValue;

        //latest slot number of the received tick (in whole time)
        //for example for M5 10:01 and 10:02 have the same quote and 10:05 has slot of 10:02 plus one
        private int latestSlotNumber = -1;

        public TimeFrame(int period)
        {
            this.period = period; 
        }

        //public UI.PriceChart PriceDiagram = null;

        /// <summary>
        /// 0 means current active candle, 1 mens previous candle and ...
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Candle this[int index]
        {
            get
            {
                return candles[candles.Count - 1 - index];
            }
        }

        public int CandleCount
        {
            get
            {
                return candles.Count;
            }
        }

        //slot means timeframes of length = period
        //ticks in the same slot are aggregated in a single quote
        private int GetTimeSlotNumber(DateTime dt)
        {
            return (int)(new TimeSpan(dt.Ticks).TotalMinutes / period);
        }

        private DateTime SlotNumberToCandleStartTime(int slotNumber)
        {
            return new DateTime(new TimeSpan(0, slotNumber * period, 0).Ticks);
        }

        internal void FeedTick(Tick tick)
        {
            cache.Clear();

            if (latestTime != DateTime.MinValue)
            {
                int minuteDifference = (int)tick.Time.Subtract(latestTime).TotalMinutes;

                if (minuteDifference > period)
                {
                    //some time has passed and we have not received a tick related to our timeframe
                    //add some dummy candles here
                    int missedPeriods = minuteDifference / period;

                    for (int i = 1; i <= missedPeriods; i++)
                    {
                        Candle dummyCandle = new Candle();

                        dummyCandle.Time = latestTime.AddMinutes(period * i);

                        candles.Add(dummyCandle);
                    }
                }
            }

            int tickSlotNumber = GetTimeSlotNumber(tick.Time);

            //do we have to add a new candle?
            if (tickSlotNumber == latestSlotNumber)
            {
                Candle q = candles[candles.Count - 1];

                q.Close = tick.Bid;
                q.High = Math.Max(q.Close, q.High);
                q.Low = Math.Min(q.Close, q.Low);
            }
            else
            {
                latestSlotNumber = tickSlotNumber;

                //by default we store bid prices in candles
                Candle q = new Candle(tick.Bid);
                q.Time = SlotNumberToCandleStartTime(tickSlotNumber);
                candles.Add(q);
            }

            latestTime = tick.Time;
        }

        /// <summary>
        /// returns high prices for last 'size' candles or low, open,close
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private double[] calculatePriceArray(PriceType type,int size)
        {
            double[] result = new double[size];

            for (int i = 0; i < size; i++)
            {
                Candle c = this[size-i-1];

                switch (type)
                {
                    case PriceType.Open:
                    {
                        result[i] = c.Open;
                        break;
                    }
                    case PriceType.Close:
                    {
                        result[i] = c.Close;
                        break;
                    }
                    case PriceType.Low:
                    {
                        result[i] = c.Low;
                        break;
                    }
                    case PriceType.High:
                    {
                        result[i] = c.High;
                        break;
                    }
                    case PriceType.HL2:
                    {
                        result[i] = (c.High + c.Low) / 2;
                        break;
                    }
                    case PriceType.HLC3:
                    {
                        result[i] = (c.Low + c.High + c.Close) / 3;
                        break;
                    }
                    case PriceType.HLCC4:
                    {
                        result[i] = (c.Close + c.Close + c.High + c.Low) / 4;
                        break;
                    }
                }
            }

            return result;
        }

        private Dictionary<string, double[]> cache = new Dictionary<string, double[]>();
        public double[] GetPriceArray(PriceType type, int size)
        {
            string key = ((int)type).ToString() + size.ToString();

            if (!cache.ContainsKey(key))
            {
                cache.Add(key, calculatePriceArray(type, size));
            }

            return cache[key];
        }
    }
}
