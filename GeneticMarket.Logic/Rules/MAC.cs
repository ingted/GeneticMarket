using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;
using GeneticMarket.Common.Parameter;
using GeneticMarket.Logic.Indicators;
using GeneticMarket.Base.Helper;
using GeneticMarket.Base.Def;
using GeneticMarket.Common.Trade;
using GeneticMarket.Logic.Helper;

namespace GeneticMarket.Logic.Rules
{
    public class MAC : BaseRule
    {
        protected override void initParameters()
        {
            parameters.Add("MA1", new IntRuleParameter(4, 333));
            parameters.Add("MA2", new IntRuleParameter(4, 333));

            parameters.Add("MA1Method", new MultiValueRuleParameter(0, 1, 2, 3, 4, 5, 6, 7, 8));
            parameters.Add("MA1Apply", new MultiValueRuleParameter(0, 1, 2, 3, 4, 5, 6));

            parameters.Add("MA2Method", new MultiValueRuleParameter(0, 1, 2, 3, 4, 5, 6, 7, 8));
            parameters.Add("MA2Apply", new MultiValueRuleParameter(0, 1, 2, 3, 4, 5, 6));
        }

        protected override void addIndicators()
        {
            MAMethodType ma1Method = (MAMethodType)this["MA1Method"];
            MAMethodType ma2Method = (MAMethodType)this["MA2Method"];

            PriceType ma1apply = (PriceType)this["MA1Apply"];
            PriceType ma2apply = (PriceType)this["MA2Apply"];

            addIndicator("MA1", new MovingAverage(container.DefaultInstrument,container.DefaultPeriod, (int)this["MA1"], ma1Method, ma1apply));
            addIndicator("MA2", new MovingAverage(container.DefaultInstrument, container.DefaultPeriod, (int)this["MA2"], ma2Method, ma2apply));
        }

        public override int Execute(Tick tick)
        {
            double ma1 = getIndicatorValue("MA1");
            double ma2 = getIndicatorValue("MA2");

            double diff = Math.Round(ma1 - ma2, 4);

            if (diff == 0)
            {
                return SignalType.Neutral;
            }

            if (diff > 0d)
            {
                return SignalType.Long; 
            }

            return SignalType.Short;
        }

        protected override bool validateState()
        {
            //instrument and timeframe are not changed
            int ma1 = (int)this["MA1"];
            int ma2 = (int)this["MA2"];

            if (ma1 == ma2 || (ma2 - ma1) < 1)
            {
                return false;
            }

            return true;
        }
    }
}
