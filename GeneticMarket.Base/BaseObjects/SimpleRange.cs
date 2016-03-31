using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticMarket.Base.BaseObjects
{
    public class SimpleRange
    {
        public double Minimum = 0d;
        public double Maximum = 0d;

        public SimpleRange()
        {
        }

        public SimpleRange(double min, double max)
        {
            Set(min, max);
        }

        public void Set(double min, double max)
        {
            Maximum = max;
            Minimum = min;
        }

        public void Set(SimpleRange sr)
        {
            Maximum = sr.Maximum;
            Minimum = sr.Minimum;
        }

        public override string ToString()
        {
            return Minimum.ToString("F2")+ ":"+ Maximum.ToString("F2");
        }


        public SimpleRange Duplicate()
        {
            return new SimpleRange(Minimum, Maximum);
        }
    }
}
