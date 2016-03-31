using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Common.Parameter
{
    public class DoubleRuleParameter: IRuleParameter
    {
        #region IRuleParameter Members
        private double currentValue = 0.0;
        private int precision = 2;  //number of decimal points to round
        private double minValue = 0;
        private double maxValue = 0;

        public DoubleRuleParameter(double min, double max, int valuePrecision)
        {
            precision = valuePrecision;
            minValue = min;
            maxValue = max;
        }

        public void Randomize()
        {
            double size = maxValue - minValue;

            double val = minValue + size * Rand.Random.NextDouble();

            val = Math.Round(val, precision);

            currentValue = val;
        }

        public object Value
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = (double)value;
            }
        }

        #endregion
    }
}
