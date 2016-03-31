using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Config;

namespace GeneticMarket.Base.BaseObjects
{
    public class DoubleRange
    {
        public double Minimum = double.MinValue;
        public double Maximum = double.MaxValue;

        private int[] CellWeights = null;

        public DoubleRange()
        {
        }

        public DoubleRange(SimpleRange sr): this(sr.Minimum,sr.Maximum)
        {
        }

        public DoubleRange(double rangeMin,double rangeMax)
        {
            Minimum = rangeMin;
            Maximum = rangeMax;

            int cellCount = (int)((rangeMax - rangeMin) / GeneralConfig.RangeCellSize);

            CellWeights = new int[cellCount+1];

            //by default all range is supported
            for (int i = 0; i <= cellCount; i++)
            {
                CellWeights[i] = 1;
            }
        }

        public void CombineRange(SimpleRange sr,bool additive)
        {
            //TODO: TEMP
            return;
            
            double min = Math.Max(sr.Minimum, Minimum);
            double max = Math.Min(sr.Maximum, Maximum);

            int rangeSize = (int)Math.Round(((max - min) / GeneralConfig.RangeCellSize), GeneralConfig.RangeCellSizePrecision);
            int rangeStart = (int)((min - Minimum) / GeneralConfig.RangeCellSize);

            for (int i = rangeStart; i <= rangeStart + rangeSize; i++)
            {
                if (additive)
                {
                    CellWeights[i]++;
                }
                else
                {
                    CellWeights[i]--;
                }
            }
        }

        public double GetOverlap(SimpleRange range2)
        {
            //TODO: TEMP
            return 0d;

            double min = Math.Max(range2.Minimum, Minimum);
            double max = Math.Min(range2.Maximum, Maximum);
            int totalWeight = 0;

            int rangeSize = (int)((max - min) / GeneralConfig.RangeCellSize);
            int rangeStart = (int)((min - Minimum) / GeneralConfig.RangeCellSize);
            int rangeEnd = rangeStart + rangeSize;

            int result = 0;

            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                result += CellWeights[i];
                totalWeight += (int)Math.Abs(CellWeights[i]);
            }

            if (totalWeight == 0)
            {
                return 0d;
            }

            return (double)result / totalWeight;
        }

        public override string ToString()
        {
            //TODO: TEMP
            return "N/A";

            double val = Minimum;
            string result = "";

            for (int i = 0; i < CellWeights.Length; i++)
            {
                int counter = 0;
                int tempi = i;
                int startWeight = CellWeights[i];
                while (tempi < CellWeights.Length && CellWeights[tempi] == startWeight)
                {
                    tempi++;
                    counter++;
                }

                if (counter > 1)
                {

                    double endPeriodVal = val;
                    endPeriodVal += (counter - 1) * GeneralConfig.RangeCellSize;

                    result += "(" + val.ToString("F2") + "," + endPeriodVal.ToString("F2") +
                        ": " + startWeight.ToString() + ")";
                    i = tempi - 1;

                    val += counter * GeneralConfig.RangeCellSize;
                    
                }
                else
                {
                    result += "(" + val.ToString("F2") + ": "+CellWeights[i].ToString()+")";
                }

                val += GeneralConfig.RangeCellSize;
                result += "|";                
            }

            if (result.EndsWith("|")) result = result.Substring(0, result.Length - 1);


            return result;
        }
    }
}
