using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Core;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using GeneticMarket.BackEnd.Core;
using System.Drawing;
using System.Threading;
using GeneticMarket.Common.Interface;
using GeneticMarket.Base.Helper;
using GeneticMarket.Master.Interface;

namespace GeneticMarket.Master.Signal
{
    public class MasterSignal
    {
        private IMasterStrategy masterStrategy = null;
        private bool isActive = true;

        private Label lblBuySignal = null;
        private Label lblSellSignal = null;
        private Label lblBestSignal = null;

        public void Register(IMasterStrategy ms, Label buyLabel, Label sellLabel,Label bestSignalLabel)
        {
            lblBuySignal = buyLabel;
            lblSellSignal = sellLabel;
            lblBestSignal = bestSignalLabel;

            masterStrategy = ms;
            isActive = true;

            ThreadPool.QueueUserWorkItem(new WaitCallback(signalUpdaterThread));
        }

        public void Unregister()
        {
            isActive = false;
        }

        private void signalUpdaterThread(object state)
        {
            while (isActive)
            {
                if (lblBuySignal != null && masterStrategy.ActiveInstrument != null)
                {
                    lblBuySignal.Invoke(new MethodInvoker(refreshSignal));
                }

                Thread.Sleep(1000);
            }
        }

        private void boldLabel(Label lbl, bool isBold)
        {
            if (isBold)
            {
                lbl.Font = new Font(lbl.Font, FontStyle.Bold);
            }
            else
            {
                lbl.Font = new Font(lbl.Font, FontStyle.Regular);
            }
        }

        private void refreshSignal()
        {
            double longWeight = 0d;
            double shortWeight = 0d;
            int bestSignal = SignalType.Neutral;
            int providerCount = 0;

            foreach (ClientContextProvider provider in masterStrategy.ContextProviders)
            {
                double tempLong = 0d;
                double tempShort = 0d;

                provider.GetSignalWeight(masterStrategy.ActiveInstrument, SignalType.Long, ref tempLong, ref tempShort);

                longWeight += tempLong;
                shortWeight += tempShort;

                bestSignal += provider.GetBestSignal(masterStrategy.ActiveInstrument);
                providerCount++;                
            }

            string longString = " (" + NumStr.ColonifyWeight(longWeight)  + ")";
            string shortString = " (" + NumStr.ColonifyWeight(shortWeight) + ")";

            double totalWeight = Math.Abs(longWeight) + Math.Abs(shortWeight);

            if (totalWeight == 0)
            {
                lblBuySignal.Text = "N/A";
                lblSellSignal.Text = "N/A";
                return;
            }

            int longRatio = (int)((longWeight / totalWeight) * 100);
            int shortRatio = (int)((shortWeight / totalWeight) * 100);

            lblBuySignal.Text = longRatio.ToString() + "%" + longString;
            lblSellSignal.Text = shortRatio.ToString() + "%" + shortString;

            boldLabel(lblBuySignal, longRatio > shortRatio);
            boldLabel(lblSellSignal, shortRatio > longRatio);

            //all nodes say LONG
            if (bestSignal == providerCount)
            {
                lblBestSignal.Text = "BUY";
            }
            else if (bestSignal == -providerCount)
            {
                lblBestSignal.Text = "SELL";
            }
            else
            {
                lblBestSignal.Text = "NEUTRAL";
            }
        }
    }
}