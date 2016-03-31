using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Common.Trade;
using GeneticMarket.Master.Interface;
using System.Threading;

namespace GeneticMarket.Master.Signal
{
    public class ValidatorSignal
    {
        private IMasterValidator parent = null;
        private bool isActive = true;

        private Label lblOpenPL = null;
        private Label lblClosedPos = null;
        private Label lbltotalProfit = null;
        private Label lbltotalLoss = null;
        private Label lblbiggestProfit = null;
        private Label lblbiggestLoss = null;
        private Label lblProfitablePos = null;
        private Label lblLoosingPos = null;
        private string idSuffix = "";

        public ValidatorSignal(string suffix)
        {
            idSuffix = suffix;
        }

        public void Register(IMasterValidator mv, Control group)
        {
            parent = mv;

            isActive = true;

            //TODO: find and store reference to labels
            lblOpenPL = group.Controls.Find("lblOpenPL"+idSuffix, true)[0] as Label;
            lblClosedPos = group.Controls.Find("lblClosed"+idSuffix, true)[0] as Label;
            lbltotalProfit = group.Controls.Find("lblTotalProfit"+idSuffix, true)[0] as Label;
            lbltotalLoss = group.Controls.Find("lblTotalLoss"+idSuffix, true)[0] as Label;
            lblbiggestProfit = group.Controls.Find("lblBiggestProfit"+idSuffix, true)[0] as Label;
            lblbiggestLoss = group.Controls.Find("lblBiggestLoss"+idSuffix, true)[0] as Label;
            lblProfitablePos = group.Controls.Find("lblProfitCount"+idSuffix, true)[0] as Label;
            lblLoosingPos = group.Controls.Find("lblLossCount"+idSuffix, true)[0] as Label;

            ThreadPool.QueueUserWorkItem(new WaitCallback(infoUdaterThread));
        }

        private void infoUdaterThread(object state)
        {
            while (isActive)
            {
                if (lblOpenPL != null)
                {
                    lblOpenPL.Invoke(new MethodInvoker(refreshInfo));
                }

                Thread.Sleep(1000);
            }
        }

        private void refreshInfo()
        {
            PerformanceInfo spi = parent.GetActivePerformance();

            if (spi == null)
            {
                return;
            }

            int openPL = 0;

            foreach (Position p in spi.openPositions)
            {
                openPL += p.GetProfitLoss();
            }

            lblOpenPL.Text = openPL.ToString();
            lblClosedPos.Text = spi.closedPositions.Count.ToString();
            lblbiggestLoss.Text = spi.largestLoss.ToString();
            lblbiggestProfit.Text = spi.largestWin.ToString();
            lbltotalLoss.Text = spi.totalLoss.ToString();
            lbltotalProfit.Text = spi.totalWin.ToString();
            lblProfitablePos.Text = spi.winningPositionCount.ToString();
            lblLoosingPos.Text = spi.loosingPositionCount.ToString();
        }

        public void UnRegister()
        {
            isActive = false;
        }
    }
}
