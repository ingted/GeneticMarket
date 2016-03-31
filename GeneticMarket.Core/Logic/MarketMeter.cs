using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.BaseObjects;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Config;
using GeneticMarket.Base.Helper;
using GeneticMarket.BackEnd;
using GeneticMarket.Common.Interface;

namespace GeneticMarket.Core.Logic
{
    public class MarketMeter
    {
        #region private fields
        //key is instrument concatenated with timeframe
        private Dictionary<string, double> currentVolatility = new Dictionary<string, double>();
        private Dictionary<string, double> currentTrend = new Dictionary<string, double>();
        #endregion

        public void Register(ClientContext context)
        {
            IMarketWatch mw = context.MarketWatch;

            mw.MarketMeterProcess += new MarketUpdateEventHandler(updateMarketInfo);

            for(int i=0;i<mw.InstrumentCount;i++)
            {
                Instrument inst = mw.GetInstrument(i);

                foreach (int tf in GeneralConfig.ValidTimeFrames)
                {
                    currentVolatility.Add(inst.ToString() + tf.ToString(), 0);
                    currentTrend.Add(inst.ToString() + tf.ToString(), 0);
                }
            }
        }

        public int GetMarketTime(Instrument inst)
        {
            DateTime time = inst.CurrentTime;

            return ConvertMarketTime(time);
        }

        public int ConvertMarketTime(DateTime time)
        {
            int day = (int)time.DayOfWeek;
            int hour = time.Hour;

            return (day * 24) + hour;
        }

        public int GetMarketTrend(Instrument inst, int tf)
        {
            return (int)currentTrend[inst.ToString() + tf.ToString()];
        }

        public int GetMarketVolatility  (Instrument inst, int tf)
        {
            return (int)currentVolatility[inst.ToString() + tf.ToString()];
        }

        #region update market TV (private)

        private void setMarketTrend(Instrument inst, int tf, double value)
        {
            currentTrend[inst.ToString() + tf.ToString()] = value;
        }

        private void setMarketVolatility(Instrument inst, int tf,double value)
        {
            currentVolatility[inst.ToString() + tf.ToString()] = value;
        }

        private void updateMarketInfo(Tick tick)
        {
            //Update market TSVR
            foreach (int tf in GeneralConfig.ValidTimeFrames)
            {
                updateMarketVolatility(tick.Instrument, tf);
                updateMarketTrend(tick.Instrument, tf);
            }
        }
              
        private void updateMarketVolatility(Instrument inst, int tf)
        {
            TimeFrame timeFrame = inst[tf];
            double result = 0;

            int candles = inst[tf].CandleCount;
            Candle currentCandle = timeFrame[0];

            if (candles == 1)
            {
                result = currentCandle.High - currentCandle.Low;
            }
            else
            {
                double value1 = currentCandle.High - currentCandle.Low;
                double value2 = Math.Abs(currentCandle.High - timeFrame[1].Close);
                double value3 = Math.Abs(currentCandle.Low - timeFrame[1].Close);
                double currentAtr = Math.Max(Math.Max(value1, value2), value3);
                double prevAtr = currentVolatility[inst.ToString() + tf.ToString()];

                if (candles > 14)
                {
                    result = 13 * prevAtr;
                    result += currentAtr;
                    result /= 14;
                }

                else
                {
                    //average of counted candles ATRs
                    result = prevAtr;
                    result *= (candles - 1);
                    result += currentAtr;
                    result /= candles;
                }
            }

            result = Math.Round(result, GeneralConfig.RangeCellSizePrecision);

            setMarketVolatility(inst, tf, result);
        }

        private void updateMarketTrend(Instrument inst, int tf)
        {
            TimeFrame timeFrame = inst[tf];

            double result = 0;
            double prevTrendValue = currentTrend[inst.ToString() + tf.ToString()];

            result = timeFrame[0].High + timeFrame[0].Low + timeFrame[0].Open + timeFrame[0].Close;
            result /= 4;

            int candles = inst[tf].CandleCount;

            if (candles == 1)
            {
                result = 0;
            }
            else
            {
                int barCount = (int)Math.Min(candles - 1, 13);

                for (int i = 1; i <= barCount; i++)
                {
                    double temp = timeFrame[i].High + timeFrame[i].Low + timeFrame[i].Open + timeFrame[i].Close;
                    temp /= 4;

                    result += temp;
                }

                result /= (barCount+1);
            }

            result = Math.Round(result, GeneralConfig.RangeCellSizePrecision);
            setMarketTrend(inst, tf, result);
        }

        #endregion
    }
}
