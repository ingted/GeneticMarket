using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;

namespace GeneticMarket.Common.Interface
{
    public delegate void TickProcessDoneDelegate(Tick tick);

    public interface IDataProvider
    {
        //used to link providers on the same instrument
        IDataProvider NextProvider { get; set; }
        Instrument Instrument { get; }
        void Initialize();

        int TotalTickCount { get; }

        //tag is used to update DP listbox when dp is activated
        int Tag { get; set; }
        DataProviderStatus Status { get; }

        //tick is number of ticks to feed into marketWatch
        //if it is 0 data is read forever
        void Process(int ticks);
        void Pause();
        void Stop();

        event TickProcessDoneDelegate ProcessTick;
        event EventHandler Stopped;
    }
}
