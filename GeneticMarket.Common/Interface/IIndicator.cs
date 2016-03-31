using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;

namespace GeneticMarket.Common.Interface
{
    //must have gethashcode
    public interface IIndicator
    {
        Instrument Instrument { get; }

        /// <summary>
        /// Initialize is called as soon as signal is added to the repository
        /// </summary>
        void Initialize();
        void Finalize();

        double GetValue(int index);
        int GetIdentifier();

        void Process(Tick tick);
    }
}
