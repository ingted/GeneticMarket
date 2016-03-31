using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Common.Parameter
{
    public class IntRuleParameter: IRuleParameter
    {
        private int currentValue = 0;
        private int minValue = 0;
        private int maxValue = 0;
        private int step = 1;

        public IntRuleParameter(int min, int max):this(min,max,1)
        {
        }

        public IntRuleParameter(int min, int max,int step)
        {
            minValue = min;
            maxValue = max;
            this.step = step;
        }

        public void Randomize()
        {
            currentValue = Rand.Random.Next(minValue, maxValue+1);

            if (step != 1)
            {
                int reminder = currentValue % step;

                currentValue -= reminder;
            }
        }

        public object Value
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = (int)value;
            }
        }
    }
}
