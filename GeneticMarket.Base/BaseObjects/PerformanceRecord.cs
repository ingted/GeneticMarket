using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Base.BaseObjects
{
    public class PerformanceRecord
    {
        private int[] recordPosition = new int[PerformanceArchive.PerformanceCoordinateCount];

        private int totalProfit = 0;
        private int totalLoss = 0;
        private int profitablePositions = 0;
        private int loosingPositions = 0;

        public PerformanceRecord(params int[] coordinates)
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                recordPosition[i] = coordinates[i];
            }

            this.GetHashCode();
        }

        public void Submit(int profit)
        {
            if (profit > 0)
            {
                totalProfit += profit;
                profitablePositions++;
            }
            else if (profit < 0)
            {
                totalLoss += (-profit);
                loosingPositions++;
            }
        }

        public double GetDistance(params int[] coordinates)
        {
            double result = 0;

            for (int i = 0; i < coordinates.Length; i++)
            {
                result += Math.Pow(recordPosition[i] - coordinates[i], 2);
            }

            return Math.Sqrt(result);
        }

        public double Weight
        {
            get
            {
                if (profitablePositions + loosingPositions == 0) return 0;

                return (totalProfit - totalLoss) / (profitablePositions + loosingPositions);
            }
        }

        /// <summary>
        /// how much credit can we place on the weight of this record
        /// in other words, how much is it probable that this record's weight is true
        /// </summary>
        public double Impact
        {
            get
            {
                return (profitablePositions + loosingPositions);
            }
        }

        private int hashCache = -1;
        public override int GetHashCode()
        {
            if (hashCache == -1)
            {
                hashCache = CalculateHashCode(recordPosition);
            }

            return hashCache;
        }

        /// <summary>
        /// hashcode of 0,1 and 1,0 must differ
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public static int CalculateHashCode(params int[] coordinates)
        {
            string result = "";

            for (int i = 0; i < coordinates.Length; i++)
            {
                result += coordinates[i].GetHashCode().ToString();
            }

            return result.GetHashCode();
        }

        public string SaveRecord()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(totalProfit);
            sb.Append(",");
            sb.Append(totalLoss);
            sb.Append(",");
            sb.Append(profitablePositions);
            sb.Append(",");
            sb.Append(loosingPositions);
            sb.Append(",");
            foreach (int pos in recordPosition)
            {
                sb.Append(pos);
                sb.Append(",");
            }

            return StrConvert.EncodeTo64(sb.ToString());
        }

        public void LoadRecord(string data)
        {
            string dataDecoded = StrConvert.DecodeFrom64(data);

            string[] parts = dataDecoded.Split(',');

            totalProfit = int.Parse(parts[0]);
            totalLoss = int.Parse(parts[1]);
            profitablePositions = int.Parse(parts[2]);
            loosingPositions = int.Parse(parts[3]);

            for (int i = 4; i < parts.Length; i++)
            {
                if (parts[i].Length == 0) continue;

                recordPosition[i - 4] = int.Parse(parts[i]);
            }
        }
    }
}
