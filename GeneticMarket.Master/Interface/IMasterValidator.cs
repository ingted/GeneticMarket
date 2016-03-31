using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Trade;

namespace GeneticMarket.Master.Interface
{
    public interface IMasterValidator
    {
        PerformanceInfo GetActivePerformance();
    }
}
