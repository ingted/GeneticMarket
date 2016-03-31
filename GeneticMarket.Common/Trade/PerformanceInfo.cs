using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Common.Trade
{
    /// <summary>
    /// we need this class in masterValidato too for other purposes so make it public here
    /// </summary>
    public class PerformanceInfo
    {
        public List<Position> openPositions = new List<Position>();
        public List<Position> closedPositions = new List<Position>();

        public int winningPositionCount = 0;
        public int loosingPositionCount = 0;

        public int totalLoss = 0;
        public int totalWin = 0;
        public int largestLoss = 0;
        public int largestWin = 0;

        public double reputation = 0d;   //overall reputation

        public PerformanceArchive archive = null;
    }
}
