using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GeneticMarket.Common.Trade;
using GeneticMarket.Base.Helper;
using GeneticMarket.BackEnd.Core;

namespace GeneticMarket.Core.Performance
{
    public partial class PerformanceLogic
    {
        public void UpdatePerformanceUI(Strategy strategy, Label winCount, Label looseCount,
            Label totalWin, Label totalLoss, Label largestWin, Label largestLoss, Label openCount,
            Label closedCount, Label lblOpenPL,Label repLbl)
        {
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;

            winCount.Text = NumStr.ColonifyNumber(spi.winningPositionCount);
            looseCount.Text = NumStr.ColonifyNumber(spi.loosingPositionCount);
            totalWin.Text = NumStr.ColonifyNumber(spi.totalWin);
            totalLoss.Text = NumStr.ColonifyNumber(spi.totalLoss);
            largestWin.Text = NumStr.ColonifyNumber(spi.largestWin);
            largestLoss.Text = NumStr.ColonifyNumber(spi.largestLoss);
            openCount.Text = NumStr.ColonifyNumber(spi.openPositions.Count);
            closedCount.Text = NumStr.ColonifyNumber(spi.closedPositions.Count);
            repLbl.Text = NumStr.ColonifyWeight(spi.reputation);

            int openpl = 0;
            foreach (Position p in spi.openPositions)
            {
                openpl += p.GetProfitLoss();
            }

            lblOpenPL.Text = NumStr.ColonifyNumber(openpl);
        }

        public string SavePerformance(Strategy strategy)
        {
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;
            StringBuilder sb = new StringBuilder();

            sb.Append(spi.winningPositionCount);
            sb.Append(",");
            sb.Append(spi.totalWin);
            sb.Append(",");
            sb.Append(spi.loosingPositionCount);
            sb.Append(",");
            sb.Append(spi.totalLoss);
            sb.Append(",");
            sb.Append(spi.largestWin);
            sb.Append(",");
            sb.Append(spi.largestLoss);
            sb.Append(",");
            sb.Append(spi.openPositions.Count);
            sb.Append(",");
            sb.Append(spi.closedPositions.Count);
            sb.Append(",");
            sb.Append(spi.archive.SaveArchive());


            return sb.ToString();
        }

        public void LoadPerformance(string line, Strategy strategy)
        {
            PerformanceInfo spi = strategy.PerformanceInfo as PerformanceInfo;

            if (spi == null)
            {
                spi = new PerformanceInfo();
                strategy.PerformanceInfo = spi;
            }

            string[] parts = line.Split(new char[1]{','},9);

            spi.winningPositionCount = int.Parse(parts[0]);
            spi.totalWin = int.Parse(parts[1]);
            spi.loosingPositionCount = int.Parse(parts[2]);
            spi.totalLoss = int.Parse(parts[3]);
            spi.largestWin = int.Parse(parts[4]);
            spi.largestLoss = int.Parse(parts[5]);

            //item #6 and 7 are not loaded

            spi.archive = new PerformanceArchive();
            spi.archive.LoadArchive(parts[8]);

            updateReputation(strategy);
        }
    }
}
