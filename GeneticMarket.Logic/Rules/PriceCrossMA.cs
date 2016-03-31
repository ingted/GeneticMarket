using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Logic.Helper;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using GeneticMarket.Common.Parameter;
using GeneticMarket.Logic.Indicators;

namespace GeneticMarket.Logic.Rules
{
    public class PriceCrossMA: BaseRule
    {
        protected override void initParameters()
        {
            parameters.Add("MA1", new IntRuleParameter(4, 333));
            parameters.Add("MA1Method", new MultiValueRuleParameter(0, 1, 2, 3, 4, 5, 6, 7, 8));
            parameters.Add("MA1Apply", new MultiValueRuleParameter(0, 1, 2, 3, 4, 5, 6));
        }

        protected override void addIndicators()
        {
            MAMethodType ma1Method = (MAMethodType)this["MA1Method"];
            PriceType ma1apply = (PriceType)this["MA1Apply"];

            addIndicator("MA1", new MovingAverage(container.DefaultInstrument, container.DefaultPeriod, 
                (int)this["MA1"], ma1Method, ma1apply));
        }

        public override int Execute(Tick tick)
        {
            double ma1 = getIndicatorValue("MA1");

            double bidDiff = Math.Round(tick.Bid - ma1, 4);
            double minGap = tick.Instrument.ToPrice(5);

            if (bidDiff > minGap)
            {
                return SignalType.Long;
            }
            if (bidDiff < -minGap)
            {
                return SignalType.Short;
            }

            return SignalType.Neutral;
        }
    }
}
