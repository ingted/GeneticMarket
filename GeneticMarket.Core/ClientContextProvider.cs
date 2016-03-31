using System;
using System.Collections;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using GeneticMarket.BackEnd.Core;
using System.Runtime.Remoting.Channels;
using GeneticMarket.Base.Config;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Collections.Generic;
using GeneticMarket.Base.Helper;
using System.Runtime.Remoting;
using System.IO;

namespace GeneticMarket.Core
{
    public class ClientContextProvider: MarshalByRefObject
    {
        /// <summary>
        /// we use singleton for cotext provider so both client (NodeManager) and server (GeneticMarkets)
        /// have access to the SAME instance of CommonContext so client can provide correct stats information
        /// in the UI which reflects server activity
        /// </summary>
        private ClientContext context = null;
        private object channel = null;
        //private ObjRef marshalRef = null;

        //this is set via server code
        private IClientNodeHelper nodeHelper = null;
        private int id = -1;
        private string serverIp = null;
        private bool doAutoSave = false;  //set from outside to instruct context to auto-save upon next tick processing

        //this is an event which is used by strategy view forms so that their 
        //UI update does not conflict with tick processing logic
        public event EventHandler BeforeTickProcess;
        public event EventHandler ConsoleDisconnect;
        public event EventHandler ConsoleConnect;

        public ClientContextProvider(ClientContext ctx,int clientPort,string serverIPNum)
        {
            context = ctx;
            serverIp = serverIPNum;

            channel = RemotingHelper.RegisterChannel("GeneticNodeManagerChannel", clientPort);
            RemotingServices.Marshal(this, "GeneticMarket.NodeManager.CommonContextProvider");
        }

        public void Register(List<string> instruments)
        {
            //read GM clientNodeHelper too
            nodeHelper = (IClientNodeHelper)Activator.GetObject(
                typeof(IClientNodeHelper),
                "tcp://" + serverIp + ":" + GeneralConfig.ClientNodeHelperPort.ToString() + "/GeneticMarket.ClientNodeHelper");

            context.RegisterAll(instruments,nodeHelper);
            context.GeneticEnvironment.Start();

            if (ConsoleConnect != null)
            {
                ConsoleConnect(this, EventArgs.Empty);
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public bool EnableAutoSave
        {
            get
            {
                return doAutoSave;
            }
            set
            {
                doAutoSave = value;
            }
        }

        #region genetic business public methods

        //public void SetMarketSentiment(string currency, double min, double max)
        //{
        //    context.MarketMeter.SetMarketSentiment(currency, min, max);
        //}

        /// <summary>
        /// this method is instroduces so server does not need to marshal marketWatch
        /// </summary>
        public void OnTick(string instrument, DateTime time, double bid, double ask)
        {
            if (BeforeTickProcess != null)
            {
                BeforeTickProcess(this, EventArgs.Empty);
            }

            if (doAutoSave)
            {
                if (DateTime.Now.Minute % context.CommonConfig.AutoSaveMinutes == 0)
                {
                    string saveName = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss");
                    saveName = string.Format(context.CommonConfig.AutoSaveFileName, saveName);

                    int counter = 0;
                    while ( File.Exists(saveName) )
                    {
                        saveName = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss") + "." + counter.ToString();
                        saveName = string.Format(context.CommonConfig.AutoSaveFileName, saveName);

                        counter++;
                    }

                    context.PersistenceLogic.SaveReport(saveName,"Auto-Save");
                }
            }

            context.MarketWatch.OnTick(instrument, time, bid, ask);
        }

        public void GetSignalWeight(string instrument, int signalType, ref double forWeight, ref double againstWeight)
        {
            context.PerformanceLogic.GetSignalWeight(context.MarketWatch.FindInstrument(instrument),
                signalType, ref forWeight, ref againstWeight);
        }

        public int GetBestSignal(string instrument)
        {
            return context.PerformanceLogic.GetBestSignal(instrument);
        }

        public bool CheckStrategyExistence(int code)
        {
            return context.StrategyRepository.HasStrategy(code);
        }

        #endregion

        public void UnRegister()
        {
            if (ConsoleDisconnect != null)
            {
                ConsoleDisconnect(this, EventArgs.Empty);
            }

            nodeHelper = null;
        }
      
        public void Stop()
        {
            if (nodeHelper != null)
            {
                //bypass exceptions in the case that console is stopped unexpectedly
                try
                {
                    //we do this to ensure that while node manager is being disconnected
                    //it is not doing logic
                    nodeHelper.Disconnect(id);
                }
                catch
                {
                }
            }

            if (channel != null)
            {
                RemotingHelper.UnregisterChannel(channel);
                RemotingServices.Disconnect(this);
            }

            context = null;
            nodeHelper = null;
        }
    }
}
