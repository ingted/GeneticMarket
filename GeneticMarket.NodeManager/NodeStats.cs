using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Core;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.BaseObjects;
using System.Collections;
using System.Threading;
using GeneticMarket.Base.Config;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.NodeManager
{
    public class NodeStats
    {
        #region private fields

        private Panel infoPanel = null;
        private Panel marketPanel = null;

        private ClientContext commonContext = null;
        private Instrument activeInstrument = null;
        private int activeTimeFrame = 0;
        private bool isActive = true;

        private const string lblStrategyCount = "lblStrategyCount";
        private const string lblOpenPositionCount = "lblOpenPositionCount";
        private const string lblClosedPositionCount = "lblClosedPositionCount";
        private const string lblSignalCount = "lblSignalCount";
        private const string lblCurrentTick = "lblCurrentTick";
        private const string lblStrategyDeleteCount = "lblStrategyDeleteCount";

        private const string lblMarketTrend = "lblMarketTrend";
        private const string lblMarketVolatility = "lblMarketVolatility";
        //private const string lblMarketBaseSentiment = "lblMarketBaseSentiment";
        //private const string lblMarketQuoteSentiment = "lblMarketQuoteSentiment";
        private const string lblTickQueueSize = "lblTickQueueSize";


        private ComboBox cboInstruments = null;
        private ComboBox cboTimeFrame = null;

        //key is label name valud is label object
        private Hashtable labelCache = new Hashtable();

        #endregion

        #region public methods (Register/UnRegister)
        /// <summary>
        /// this method is run in another thread context - we need to switch to UI
        /// </summary>
        /// <param name="context"></param>
        /// <param name="marketPnl"></param>
        /// <param name="infoPnl"></param>
        public void Register(ClientContext context, Panel marketPnl, Panel infoPnl)
        {
            commonContext = context;
            marketPanel = marketPnl;

            //display info of a single strategy
            infoPanel = infoPnl;

            ComboBox cboInst = marketPanel.Controls.Find("cboInstruments", true)[0] as ComboBox;
            ComboBox cboTf = marketPanel.Controls.Find("cboTimeFrames", true)[0] as ComboBox;

            setupUI(cboInst, cboTf);

            commonContext = context;
            ThreadPool.QueueUserWorkItem(new WaitCallback(statUpdaterThread));
            isActive = true;
        }

        public void UnRegister()
        {
            isActive = false;
        }
        #endregion

        #region private methods
        private void updateStats()
        {
            int trend = commonContext.MarketMeter.GetMarketTrend(activeInstrument, activeTimeFrame);
            int vol = commonContext.MarketMeter.GetMarketVolatility(activeInstrument, activeTimeFrame);
            //SimpleRange bsentiment = commonContext.MarketMeter.GetMarketSentiment(activeInstrument.BaseCurrency);
            //SimpleRange qsentiment = commonContext.MarketMeter.GetMarketSentiment(activeInstrument.QuoteCurrency);

            setLabel(lblMarketTrend, trend);
            setLabel(lblMarketVolatility, vol);
            //setLabel(lblMarketBaseSentiment, bsentiment);
            //setLabel(lblMarketQuoteSentiment, qsentiment);
            setLabel(lblCurrentTick, NumStr.ColonifyNumber(commonContext.MarketWatch.CurrentTickIndex));
            setLabel(lblOpenPositionCount, NumStr.ColonifyNumber(commonContext.DefaultAccount.TotalPositionCount - 
                commonContext.DefaultAccount.ClosedPositionCount));
            setLabel(lblClosedPositionCount, NumStr.ColonifyNumber(commonContext.DefaultAccount.ClosedPositionCount));
            setLabel(lblStrategyCount, NumStr.ColonifyNumber(commonContext.StrategyRepository.StrategyCount));
            setLabel(lblSignalCount, NumStr.ColonifyNumber(commonContext.IndicatorRepository.IndicatorCount));
            setLabel(lblStrategyDeleteCount, NumStr.ColonifyNumber(commonContext.StrategyRepository.DeleteCounter));
            setLabel(lblTickQueueSize, NumStr.ColonifyNumber(commonContext.MarketWatch.TickQueueSize));
        }
        private void statUpdaterThread(object state)
        {
            while (isActive)
            {
                if (activeInstrument != null && infoPanel != null)
                {
                    infoPanel.Invoke(new MethodInvoker(updateStats));
                }

                //update stats every .25 second
                Thread.Sleep(250);
            }
        }

        private void setLabel(string lbl, object text)
        {
            if (!labelCache.ContainsKey(lbl))
            {
                Control[] ctls = infoPanel.Controls.Find(lbl, true);

                if (ctls.Length > 0)
                {
                    labelCache.Add(lbl, ctls[0]);
                }
                else
                {
                    ctls = marketPanel.Controls.Find(lbl, true);

                    if (ctls.Length > 0)
                    {
                        labelCache.Add(lbl, ctls[0]);
                    }
                    else
                    {
                        throw new Exception("Invalida label name:" + lbl);
                    }
                }
            }

            (labelCache[lbl] as Label).Text = text.ToString();
        }

        private void setupUI(ComboBox inst, ComboBox tf)
        {
            inst.Invoke(new MethodInvoker(delegate()
                {
                    inst.Items.Clear();
                    tf.Items.Clear();

                    for (int i = 0; i < commonContext.MarketWatch.InstrumentCount; i++)
                    {
                        inst.Items.Add(commonContext.MarketWatch.GetInstrument(i));
                    }

                    for (int i = 0; i < GeneralConfig.ValidTimeFrames.Length; i++)
                    {
                        tf.Items.Add(GeneralConfig.ValidTimeFrames[i]);
                    }

                    cboInstruments = inst;
                    cboTimeFrame = tf;

                    //init both items
                    inst.SelectedIndex = 0;
                    tf.SelectedIndex = 0;

                    inst.SelectedIndexChanged += new EventHandler(updateSetup);
                    tf.SelectedIndexChanged += new EventHandler(updateSetup);

                    //fire update event so interface and stats are updated
                    updateSetup(null, null);
                }));
        }

        private void updateSetup(object sender, EventArgs e)
        {
            string instrument = cboInstruments.SelectedItem.ToString();
            int tf = int.Parse(cboTimeFrame.SelectedItem.ToString());

            Instrument inst = commonContext.MarketWatch.FindInstrument(instrument);

            activeInstrument = inst;
            activeTimeFrame = tf;
        }

        #endregion
    }
}
