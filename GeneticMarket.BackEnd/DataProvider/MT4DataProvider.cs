using System;
using System.Collections.Generic;
using System.Text;
using NDde.Client;
using GeneticMarket.Common.Market;
using GeneticMarket.BackEnd.DataProvider;
using GeneticMarket.Common.Interface;
using System.Runtime.Remoting.Messaging;
using GeneticMarket.Base.Def;

namespace GeneticMarket.BackEnd.DataProvider
{
    public class MT4DataProvider: IDataProvider
    {
        #region IDataProvider Members
        private DdeClient ddeClient = null;
        private Instrument instrument = null;
        private int tickLimit = -1;
        private bool isPaused = false;
        private int tag = 0;
        private DataProviderStatus status = DataProviderStatus.NotStarted;

        public event EventHandler Stopped;
        public event TickProcessDoneDelegate ProcessTick;

        public MT4DataProvider(Instrument inst)
        {
            instrument = inst;
        }

        public void Initialize()
        {
            ddeClient = new DdeClient("MT4", "QUOTE");
            ddeClient.Connect();
            ddeClient.Advise += new EventHandler<DdeAdviseEventArgs>(ddeClient_Advise);
        }

        public void Process(int ticks)
        {
            tickLimit = ticks;
            status = DataProviderStatus.Running;

            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                ddeClient.StartAdvise(instrument.ToString(), 1, true, 6000);
            }
        }

        void ddeClient_Advise(object sender, DdeAdviseEventArgs e)
        {
            if (isPaused) return;

            //item is instrument name
            //text is  2010/07/15 21:27 1.29012 1.29016
            Tick t = new Tick();
            t.Instrument = instrument;
            string[] parts = e.Text.Split(' ');
            t.Time = DateTime.Parse(parts[0] + " " + parts[1]);
            t.Bid = double.Parse(parts[2]);
            t.Ask = double.Parse(parts[3]);

            if (ProcessTick != null)
            {
                ProcessTick(t);
            }
            tickLimit--;

            if (tickLimit == 0)
            {
                ddeClient.StopAdvise(instrument.ToString(), 1000);
                status = DataProviderStatus.Stopped;
            }
        }

        public void Pause()
        {
            isPaused = true;
            status = DataProviderStatus.Paused;
        }

        public void Stop()
        {
            if (ddeClient != null && ddeClient.IsConnected)
            {
                ddeClient.Disconnect();
            }

            if (Stopped != null)
            {
                Stopped(this, EventArgs.Empty);
            }

            status = DataProviderStatus.Stopped;
        }


        public Instrument Instrument
        {
            get
            {
                return instrument;
            }
        }

        public int TotalTickCount
        {
            get
            {
                return -1;
            }
        }
        #endregion

        #region IDataProvider Members

        public IDataProvider NextProvider
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IDataProvider Members


        public int Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

        public DataProviderStatus Status
        {
            get
            {
                return status;
            }
        }

        #endregion
    }
}
