using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Common.Interface;
using GeneticMarket.Core;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using GeneticMarket.BackEnd.Core;

namespace GeneticMarket.NodeManager
{
    public partial class frmStrategyView : Form
    {
        public int strategyId = -1;
        public ClientContext context = null;
        public ClientContextProvider contextProvider = null;

        private int tickCounter = 0;

        public frmStrategyView()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmStrategyView_Load(object sender, EventArgs e)
        {
            Text += " : "+strategyId.ToString();

            //force update on first tick
            updateUI();

            if (contextProvider != null)
            {
                contextProvider.BeforeTickProcess += new EventHandler(contextProvider_BeforeTickProcess);
            }
        }

        private void contextProvider_BeforeTickProcess(object sender, EventArgs e)
        {
            tickCounter++;

            int remainingTicks = tickCounter % context.CommonConfig.StrategyViewUpdatePeriod;
            int total = context.CommonConfig.StrategyViewUpdatePeriod;

            lblUpdateRemaining.Invoke(new MethodInvoker(delegate()
                {
                    if (remainingTicks == 0)
                    {
                        lblUpdateRemaining.Text = "0";
                    }
                    else
                    {
                        lblUpdateRemaining.Text = (total - remainingTicks).ToString();
                    }
                }));

            if (remainingTicks == 0)
            {
                tickCounter = 0;

                updateUI();
            }
        }

        private void updateUI()
        {
            //lookup strategy everytime because maybe its deleted
            Strategy strategy = context.StrategyRepository.FindStrategy(strategyId);

            if (strategy == null)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    Text = "Strategy View : [NOT FOUND]";
                    lblCurrentSignal.Text = "N/A";
                }));

                return;
            }

            this.Invoke(new MethodInvoker(delegate()
            {
                //update UI
                //context.WeightLogic.UpdateReputationUI(strategy, lblReputation);
                context.PerformanceLogic.UpdatePerformanceUI(strategy,
                    lblWinCount, lblLossCount, lblTotalWin, lblTotalLoss,
                    lblLargestWin, lblLargestLoss, lblOpenPositionCount,
                    lblClosedPositionCount, lblOpenPL,lblReputation);

                string strSignal = strategy.DefaultInstrument.ToString() + " : ";

                int signal = strategy.GetSignal();

                if (signal == SignalType.Neutral)
                {
                    strSignal +="NEUTRAL";
                }
                else if (signal == SignalType.Long)
                {
                    strSignal += "BUY";
                }
                else if (signal == SignalType.Short)
                {
                    strSignal += "SELL";
                }
                else if (signal == SignalType.NA)
                {
                    strSignal += "N/A";
                }

                lblCurrentSignal.Text = strSignal;
            }));
        }

        private void frmStrategyView_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void frmStrategyView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (contextProvider != null)
            {
                contextProvider.BeforeTickProcess -= new EventHandler(contextProvider_BeforeTickProcess);
            }

        }
    }
}
