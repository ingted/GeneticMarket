using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.BaseObjects;

namespace GeneticMarket.Base.Config
{
    public class GeneralConfig
    {
        public static SimpleRange TrendMaxRange = new SimpleRange(-10d, 10d);
        public static SimpleRange SentimentMaxRange = new SimpleRange(-1d, 1d);
        public static SimpleRange VolatilityMaxRange = new SimpleRange(0d, 2d);

        public static int[] ValidTimeFrames =new int[3] { 1 , 5, 15};
        public static double RangeCellSize=0.1;
        public static int RangeCellSizePrecision=1;

        public static int TimeSlotMinutes = 15;

        public const int ClientNodeHelperPort = 39939;
        //public const string ClientNodeHelperUrl = "tcp://localhost:8000/GeneticMarket.Master.ClientNodeHelper";

        //we need at list a history of 10 positions to state a weight for a candidate position
        public const int MinPositionRecordForPerformanceWeightCalculation = 10;

        //how many neighbours do we need to find to calculate weight for a candidate position
        public const int PerformanceWeightCalculationNeighbourCount = 3;
    }
}
