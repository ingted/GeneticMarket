using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;
using GeneticMarket.Common.Interface;
using GeneticMarket.Logic.Indicators;
//using System.Threading;
using System.Collections;

namespace GeneticMarket.Logic
{
    public class IndicatorRepository : IIndicatorRepository
    {
        //key is ID and value is indicator
        private Hashtable indicators = new Hashtable();

        public int IndicatorCount
        {
            get
            {
                return indicators.Count;
            }
        }

        public IIndicator AddIndicator(IIndicator indicator)
        {
            IIndicator result = indicators[indicator.GetIdentifier()] as IIndicator;

            if (result != null)
            {
                return result;
            }

            indicator.Initialize();
            indicators.Add(indicator.GetIdentifier(),indicator);

            return indicator;
        }

        //public IIndicator FindIndicator(int id)
        //{
        //    foreach (IIndicator ind in indicators)
        //    {
        //        if ( ind.GetIdentifier() == id )
        //        {
        //            return ind;
        //        }
        //    }

        //    return null;
        //}

        public void ProcessTick(Tick tick)
        {
            foreach(int key in indicators.Keys)
            {
                IIndicator indicator = indicators[key] as IIndicator;

                if (indicator.Instrument.Equals(tick.Instrument))
                {
                    indicator.Process(tick);
                }
            }
        }
    }
}
