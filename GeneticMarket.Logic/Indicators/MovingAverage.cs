using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Logic.Helper;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using TicTacTec.TA;

namespace GeneticMarket.Logic.Indicators
{
    public class MovingAverage: BaseIndicator
    {
        public int period = -1;
        private MAMethodType method = MAMethodType.Sma;
        private PriceType applyTo = PriceType.Close;
        private double[] outReal = new double[1];
        private int begIdx;
        private int nbElement;

        public MovingAverage(Instrument inst, int tf, int period, MAMethodType method, PriceType applyTo)
            : base(inst, tf)
        {
            this.period = period;
            this.method = method;
            this.applyTo = applyTo;
        }

        private double GetPrice(int index)
        {
            Candle cdl = TimeFrame[index];

            double high = cdl.High;
            double low = cdl.Low;
            double close = cdl.Close;
            double open = cdl.Open;

            switch (applyTo)
            {
                case PriceType.Close:
                    {
                        return close;
                    }
                case PriceType.High:
                    {
                        return high;
                    }
                case PriceType.Low:
                    {
                        return low;
                    }
                case PriceType.Open:
                    {
                        return open;
                    }
                case PriceType.HL2:
                    {
                        return (high+low)/2;
                    }
                case PriceType.HLC3:
                    {
                        return (high + low + close ) /3; 
                    }
                case PriceType.HLCC4:
                    {
                        return (high + low + close + close) / 4;
                    }
            }

            throw new Exception("Invalid ApplyTo");
        }
        
        public override void Process(Tick tick)
        {
            TimeFrame tf = TimeFrame; 
            int candleCount = Math.Min(tf.CandleCount, period);

            double[] priceArray = tf.GetPriceArray(applyTo, candleCount);

            TicTacTec.TA.Library.Core.RetCode retCode = TicTacTec.TA.Library.Core.MovingAverage(
                priceArray.Length-1,priceArray.Length-1,priceArray,
                period,(TicTacTec.TA.Library.Core.MAType)method,
                out begIdx,out nbElement,outReal);
            

            if (retCode != TicTacTec.TA.Library.Core.RetCode.Success)
            {
                throw new Exception("Error calculating indicator value");
            }

            currentValue = outReal[0];
            //switch (method)
            //{
            //    case MAMethodType.Simple:
            //        {
            //            sma(candleCount);
            //            break;
            //        }
            //    case MAMethodType.Exponential:
            //        {
            //            ema(candleCount);
            //            break;
            //        }
            //    case MAMethodType.Smoothed:
            //        {
            //            smma(candleCount);
            //            break;
            //        }
            //    case MAMethodType.LinearWeighted:
            //        {
            //            lwma(candleCount);
            //            break;
            //        }
            //    default:
            //        {
            //            throw new Exception("Invalid method");
            //        }
            //}

        }

        //private void lwma(int candleCount)
        //{
        //    double sum = 0;
        //    double wsum = 0;

        //    for (int i = 0; i < candleCount; i++)
        //    {
        //        double price = GetPrice(i);

        //        sum += price;
        //        wsum += price * (period - i);
        //    }

        //    currentValue = wsum / sum;
        //}

        //private void smma(int candleCount)
        //{
        //    if (candleCount < period)
        //    {
        //        sma(candleCount);
        //        return;
        //    }

        //    double temp = period * currentValue;
        //    temp -= currentValue;
        //    temp += GetPrice(0);

        //    currentValue = temp / period;
        //}

        //private void ema(int candleCount)
        //{
        //    if (candleCount < period)
        //    {
        //        sma(candleCount);
        //        return;
        //    }

        //    double multiplier = (double)2 / (1 + period);
        //    double result = GetPrice(0) - currentValue;
        //    result *= multiplier;

        //    currentValue += result;
        //}

        //private void sma(int candleCount)
        //{
        //    currentValue = 0;

        //    for (int i = 0; i < candleCount; i++)
        //    {
        //        currentValue += GetPrice(i);
        //    }

        //    currentValue /= candleCount;
        //}

        public override int CalculateIdentifier()
        {
            return ("MA" + instrument.ToString() + timeFrame.ToString() + applyTo.ToString()+method.ToString()+ period.ToString()).GetHashCode();
        }
    }
}
