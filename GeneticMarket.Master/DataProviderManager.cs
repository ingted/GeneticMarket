using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;
using GeneticMarket.BackEnd.DataProvider;
using GeneticMarket.Common.Market;
using System.Windows.Forms;
using GeneticMarket.Base.Def;

namespace GeneticMarket.Master
{
    public delegate void DataProviderEventHandler(int tag);

    public class DataProviderManager
    {
        //for each instrument we have exactly one idp in this list, other dps of that instrunebt
        //will be linked to the root one
        private List<IDataProvider> dataProviders = new List<IDataProvider>();
        private int runningDataProviderCount = 0;
        protected List<string> instruments = new List<string>();

        public event DataProviderEventHandler DataProviderStopped;
        public event DataProviderEventHandler DataProviderStarted;
        public event DataProviderEventHandler DataProviderPaused;

        protected virtual void DataProviderAdded(IDataProvider idp)
        {
        }

        protected int DataProviderCount
        {
            get
            {
                return dataProviders.Count;
            }
        }

        public int AddDataProvider(string type, string instrument, int tag, object[] arguments)
        {
            Instrument dpInstrument = null;

            if (!instruments.Contains(instrument))
            {
                instruments.Add(instrument);
                dpInstrument = new Instrument(instrument, null); //we need to TF information here in console
            }
            else
            {
                //do not create a new instance of instrument, use an existing one
                dpInstrument = findDataProvider(instrument).Instrument;
            }

            IDataProvider idp = null;
            switch (type.ToLower())
            {
                case "ohlc":
                {
                    OHLCFileDataProvider provider = new OHLCFileDataProvider(new Instrument(instrument),
                        arguments[0].ToString(), (char)arguments[1]);

                    idp = provider;
                    break;
                }
                case "gain":
                {
                    GainCapitalDataProvider provider = new GainCapitalDataProvider(new Instrument(instrument),
                        arguments[0].ToString());

                    idp = provider;
                    break;
                }
                case "mt4":
                {
                    MT4DataProvider mt4 = new MT4DataProvider(new Instrument(instrument));

                    idp = mt4;
                    break;
                }
            }

            idp.Tag = tag;
            idp.Stopped += new EventHandler(onDataProviderStopped);
            idp.Initialize();

            IDataProvider currentDp = findDataProvider(instrument);

            if (currentDp == null)
            {
                dataProviders.Add(idp);
            }
            else
            {
                while (currentDp.NextProvider != null) currentDp = currentDp.NextProvider;

                currentDp.NextProvider = idp;
            }

            DataProviderAdded(idp);

            return idp.TotalTickCount;
        }

        public bool Start()
        {
            return Start(0);
        }

        public virtual bool Start(int tickLimit)
        {
            foreach (IDataProvider idp in dataProviders)
            {
                if (idp.Status == GeneticMarket.Base.Def.DataProviderStatus.NotStarted || 
                    idp.Status == GeneticMarket.Base.Def.DataProviderStatus.Paused)
                {
                    runningDataProviderCount++;
                    idp.Process(tickLimit);
                    DataProviderStarted(idp.Tag);
                }
                else if (idp.Status == DataProviderStatus.Stopped)
                {
                    IDataProvider dp = idp;
                    
                    //maybe next DP is paused or not started
                    while (dp.NextProvider != null &&
                        (dp.NextProvider.Status != DataProviderStatus.NotStarted &&
                            dp.NextProvider.Status != DataProviderStatus.Paused)) dp = dp.NextProvider;

                    if (dp.NextProvider != null && 
                        (dp.NextProvider.Status == DataProviderStatus.NotStarted ||
                         dp.NextProvider.Status == DataProviderStatus.Paused))
                    {
                        runningDataProviderCount++;
                        dp.NextProvider.Process(tickLimit);
                        DataProviderStarted(dp.NextProvider.Tag);
                    }
                }

            }

            return true;
        }

        public virtual void Pause()
        {
            foreach (IDataProvider idp in dataProviders)
            {
                idp.Pause();
                DataProviderPaused(idp.Tag);
            }
        }

        public virtual void Stop()
        {
            foreach (IDataProvider idp in dataProviders)
            {
                if (idp.Status != DataProviderStatus.Stopped &&
                    idp.Status != DataProviderStatus.NotStarted)
                {
                    //stopping a DP will start its next DP in the instrument queue!
                    idp.Stop();
                }
            }
        }

        private IDataProvider findDataProvider(string instrument)
        {
            foreach (IDataProvider idp in dataProviders)
            {
                if (idp.Instrument.ToString() == instrument)
                {
                    return idp;
                }
            }

            return null;
        }

        private void onDataProviderStopped(object sender, EventArgs e)
        {
            IDataProvider dp = sender as IDataProvider;

            runningDataProviderCount--;
            DataProviderStopped(dp.Tag);

            if (dp.NextProvider != null)
            {
                dp.NextProvider.Process(0);
                runningDataProviderCount++;
                DataProviderStarted(dp.NextProvider.Tag);
            }
        }
    }
}
