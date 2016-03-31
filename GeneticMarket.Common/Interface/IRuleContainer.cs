using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;

namespace GeneticMarket.Common.Interface
{
    public interface IRuleContainer
    {
        int SmallPeriod { get; }
        int DefaultPeriod { get; }
        int LargePeriod { get; }

        Instrument DefaultInstrument { get; }
        IIndicatorRepository IndicatorRepository { get; }
    }
}
