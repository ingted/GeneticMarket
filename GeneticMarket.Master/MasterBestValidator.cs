using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Common.Market;
using GeneticMarket.Core;
using GeneticMarket.Base.Def;
using GeneticMarket.Common.Trade;
using GeneticMarket.Master.Interface;
using GeneticMarket.Master.Signal;

namespace GeneticMarket.Master
{
    public class MasterBestValidator : MasterValidator
    {
        public MasterBestValidator()
        {
            signal = new ValidatorSignal("Best");
        }

        protected override int getMasterSignal(Tick tick)
        {
            int bestSignal = SignalType.Neutral;
            int providerCount = 0;

            foreach (ClientContextProvider provider in masterStrategy.ContextProviders)
            {
                bestSignal += provider.GetBestSignal(masterStrategy.ActiveInstrument);
                providerCount++;
            }

            if (bestSignal == providerCount)
            {
                return SignalType.Long;
            }
            else if (bestSignal == -providerCount)
            {
                return SignalType.Short;
            }

            return SignalType.Neutral;
        }
    }
}
