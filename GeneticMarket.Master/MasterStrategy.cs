using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Config;
using GeneticMarket.Common.Interface;
using GeneticMarket.BackEnd.DataProvider;
using GeneticMarket.Common.Market;
using GeneticMarket.Core;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Drawing;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using GeneticMarket.Base.Helper;
using GeneticMarket.Master.Interface;
using GeneticMarket.Master.Signal;
using GeneticMarket.Master.Helper;

namespace GeneticMarket.Master
{

    public class MasterStrategy: DataProviderManager, IMasterStrategy
    {
        #region private fields

        private List<ClientContextProvider> contextProviders = new List<ClientContextProvider>();

        private MasterSignal masterSignal = new MasterSignal();
        private MasterValidator validator = new MasterValidator();
        private MasterBestValidator bestValidator = new MasterBestValidator();
        private string activeInstrument = null;
        private ComboBox cboInstruments = null;
        private bool validatorRegistered = false;
        private bool bestValidatorRegistered = false;

        private AutoResetEvent tickProcessingFinished = null;
        private AutoResetEvent hasTickToProcess = null;

        private List<Tick> tickQueue = new List<Tick>();
        private Thread tickProcessorThread = null;
        //private object listenerChannel = null;
        private object clientHelperChannel = null;
        private ObjRef clientHelperRef = null;
        private ClientNodeHelper clientHelper = null;
        #endregion

        public MasterStrategy()
        {
            //listenerChannel = RemotingHelper.RegisterChannel("MasterStrategyChannel", 0);

            clientHelper = new ClientNodeHelper(this);
            clientHelper.ClientDisconnect += new OnClientDisconnectEventHandler(clientHelper_ClientDisconnect);

            clientHelperChannel = RemotingHelper.RegisterChannel("GeneticMarketClientHelperChannel", GeneralConfig.ClientNodeHelperPort);
            clientHelperRef = RemotingServices.Marshal(clientHelper, "GeneticMarket.ClientNodeHelper");
        }

        public bool RegisterNode(string url)
        {
            if (DataProviderCount == 0)
            {
                return false;
            }

            ClientContextProvider provider = (ClientContextProvider)Activator.GetObject(
                typeof(ClientContextProvider),
                url);

            if (provider != null)
            {
                try
                {
                    //maybe as soon as provider is added to list, tick thread starts sending ticks
                    //but we first need to init it
                    provider.Register(instruments);
                }
                catch
                {
                    return false;
                }

                contextProviders.Add(provider);

                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void RegisterValidator(Control validatorGroup)
        {
            validator.Register(this, validatorGroup);
            validatorRegistered = true;
        }

        public void ResetValidator()
        {
            validator.Reset();
        }

        public void ResetBestValidator()
        {
            bestValidator.Reset();
        }

        public void UnRegisterValidator()
        {
            validatorRegistered = false;
            validator.UnRegister();
        }

        public void RegisterBestValidator(Control validatorGroup)
        {
            bestValidator.Register(this, validatorGroup);
            bestValidatorRegistered = true;
        }

        public void UnRegisterBestValidator()
        {
            bestValidatorRegistered = false;
            bestValidator.UnRegister();
        }



        public void Init(Label lblBuySignal, Label lblSellSignal, Label lblBestSignal, ComboBox inst)
        {
            tickProcessingFinished = new AutoResetEvent(true);
            hasTickToProcess = new AutoResetEvent(false);

            inst.Items.Clear();

            foreach (string instr in instruments)
            {
                inst.Items.Add(instr);
            }

            inst.SelectedIndex = 0;
            cboInstruments = inst;
            inst.SelectedIndexChanged += new EventHandler(delegate(object sender, EventArgs args)
                {
                    activeInstrument = cboInstruments.SelectedItem.ToString();
                });

            activeInstrument = instruments[0];

            masterSignal.Register(this, lblBuySignal, lblSellSignal, lblBestSignal);
            tickProcessorThread = new Thread(new ThreadStart(tickProcessor));
            tickProcessorThread.Start();
        }

        public override bool Start(int tickLimit)
        {
            if (contextProviders.Count == 0)
            {
                return false;
            }

            return base.Start(tickLimit);
        }

        public override void Stop()
        {
            base.Stop();

            if (tickProcessorThread != null)
            {
                tickProcessorThread.Abort();
            }

            masterSignal.Unregister();

            if (validatorRegistered)
            {
                validator.UnRegister();
            }

            if (bestValidatorRegistered)
            {
                bestValidator.UnRegister();
            }

            //RemotingHelper.UnregisterChannel(listenerChannel);
            RemotingHelper.UnregisterChannel(clientHelperChannel);

            RemotingServices.Disconnect(clientHelper);

            foreach (ClientContextProvider provider in contextProviders)
            {
                provider.UnRegister();
            }
        }

        protected override void DataProviderAdded(IDataProvider idp)
        {
            idp.ProcessTick += new TickProcessDoneDelegate(idp_TickProcessDone);
        }

        private void clientHelper_ClientDisconnect(int index)
        {
            lock (contextProviders)
            {
                contextProviders.RemoveAt(index);

                //if there is no mode context providers pause data providers
                if (contextProviders.Count == 0)
                {
                    Pause();
                }
            }
        }
        
        private void idp_TickProcessDone(Tick tick)
        {
            lock (tickQueue)
            {
                //save tick in the queue to preserve ordering of ticks processed
                //in the case of tick flood
                tickQueue.Add(tick);
            }

            //Label ask = Application.OpenForms[0].Controls.Find("lblask",true)[0] as Label;
            //Label bid = Application.OpenForms[0].Controls.Find("lblbid", true)[0] as Label;

            //ask.Invoke(new MethodInvoker(delegate()
            //    {
            //        ask.Text = tick.Ask.ToString("F5");
            //        bid.Text = tick.Bid.ToString("F5");
            //    }));

            //wait for main thread to finish its work if its busy
            tickProcessingFinished.WaitOne();

            //inform main thread of a new tick to process
            hasTickToProcess.Set();
        }

        private void tickProcessor()
        {
            while (true)
            {
                //wait until you have some tick to process
                hasTickToProcess.WaitOne();

                Tick currentTick = null;

                lock (tickQueue)
                {
                    currentTick = tickQueue[0];
                    tickQueue.RemoveAt(0);
                }

                //maybe during this call, client is disconnected, so disconnect
                //code has to wait until this is done
                lock (contextProviders)
                {
                    //we ignore exceptions because if client is disconnected during OnTick
                    //remoting will throw exception
                    try
                    {
                        foreach (ClientContextProvider provider in contextProviders)
                        {
                            //we cannot send Tick itself cause instrument serialization will perhaps cause errors
                            //and conflicts
                            provider.OnTick(currentTick.Instrument.ToString(), currentTick.Time,
                                currentTick.Bid, currentTick.Ask);
                        }
                    }
                    catch
                    {
                    }
                }

                //position read instrument price for internal operations
                currentTick.Instrument.Tick(currentTick.Time, currentTick.Bid, currentTick.Ask);

                if (validatorRegistered)
                {
                    validator.OnTick(currentTick);
                }

                if (bestValidatorRegistered)
                {
                    bestValidator.OnTick(currentTick);
                }

                //when finished just signal the handle
                tickProcessingFinished.Set();
            }
        }

        #region IMasterStrategy Members

        public List<ClientContextProvider> ContextProviders
        {
            get
            {
                return contextProviders;
            }
        }

        public string ActiveInstrument
        {
            get
            {
                return activeInstrument;
            }
        }

        #endregion
    }
}
