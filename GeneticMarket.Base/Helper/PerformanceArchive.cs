using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.BaseObjects;
using GeneticMarket.Base.Config;

namespace GeneticMarket.Base.Helper
{
    public class PerformanceArchive
    {
        public const int PerformanceCoordinateCount = 4;

        private List<PerformanceRecord> records = new List<PerformanceRecord>();
        private List<int> recordHashCodes = new List<int>();
        private int recordCount = 0;  //2 records in the same point are counted 2 here

        public void SubmitPerformance(int profit, params int[] coordinates)
        {
            recordCount++;

            int hash = PerformanceRecord.CalculateHashCode(coordinates);
            int index = recordHashCodes.IndexOf(hash);

            if (index != -1)
            {
                records[index].Submit(profit);
                    
                return;
            }

            PerformanceRecord pr = new PerformanceRecord(coordinates);
            records.Add(pr);
            recordHashCodes.Add(pr.GetHashCode());

            pr.Submit(profit);
        }

        /// <summary>
        /// if a position is going to be placed with the below specs,
        /// what is reutation for that?
        /// first we need probability of hitting each of neighbours of the given point in the space
        /// d(i) is distance from ith neighbour to the given point
        /// we try to find 'n' nearest neighbours of the given point
        /// this is given by: p(i) = sigma(d(j),j<>i)/(n-1)sigma(d(t))
        /// for each point, actual weight is: p(i)w(i)i(i) (probabilityxweightximpact)
        /// next we normalize sum of actual weights of n neighbours
        /// this is done by dividing by: sigmal(i(i)) to have a final value measured in profit/loss points
        /// above formula simplified gives: sigma(w(i)i(i)sigma(d(j),j<>i))/(n-1)sigma(i(i))sigma(d(i))
        /// </summary>
        /// <param name="time"></param>
        /// <param name="signalType"></param>
        /// <param name="trend"></param>
        /// <param name="volatility"></param>
        /// <returns></returns>
        public double GetWeight(params int[] coordinates)
        {
            if (recordCount < GeneralConfig.MinPositionRecordForPerformanceWeightCalculation)
            {
                return 0d;
            }
            double sumDistance = 0d;

            Dictionary<PerformanceRecord, double> neighbours = findNearestNeighbours(coordinates);

            foreach (double distance in neighbours.Values)
            {
                sumDistance += distance;
            }

            double numerator = 0d;  //top part
            double denominator = 0d; //bottom part - sigma(i(i)) 

            foreach (PerformanceRecord pr in neighbours.Keys)
            {
                double sigmaD = sumDistance - neighbours[pr];  //sum of all d(i) except one being examined

                if (sumDistance == 0)
                {
                    numerator += (pr.Weight * pr.Impact);
                }
                else
                {
                    numerator += (pr.Weight * pr.Impact * sigmaD);
                }

                denominator += pr.Impact;
            }

            if (sumDistance != 0)
            {
                denominator *= (GeneralConfig.PerformanceWeightCalculationNeighbourCount - 1);
                denominator *= sumDistance;
            }

            double result = numerator / denominator;

            return result;
        }

        private Dictionary<PerformanceRecord, double> findNearestNeighbours(params int[] coordinates)
        {
            Dictionary<PerformanceRecord, double> neighbours = new Dictionary<PerformanceRecord, double>();
            double maxWeight = -1d;
            PerformanceRecord maxNeighbour = null;

            //find some nearest neighbours
            foreach (PerformanceRecord pr in records)
            {
                double distance = pr.GetDistance(coordinates);

                if (neighbours.Count < GeneralConfig.PerformanceWeightCalculationNeighbourCount)
                {
                    neighbours.Add(pr,distance);

                    if (distance > maxWeight)
                    {
                        maxWeight = distance;
                        maxNeighbour = pr;
                    }
                }
                else
                {
                    //we need some filtering here, if current PR is better than curret items in neighbours
                    //replace it
                    if (distance < maxWeight)
                    {
                        neighbours.Remove(maxNeighbour);
                        neighbours.Add(pr,distance);

                        maxNeighbour = pr;
                        maxWeight = distance;

                        //check if currentMaxNeightbour is a correct value
                        foreach (PerformanceRecord p in neighbours.Keys)
                        {
                            if (neighbours[p] > maxWeight)
                            {
                                maxWeight = neighbours[p];
                                maxNeighbour = p;
                            }
                        }
                    }
                }
            }

            return neighbours;
        }

        public string SaveArchive()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(recordCount.ToString());
            sb.Append(",");

            foreach (PerformanceRecord pr in records)
            {
                sb.Append(pr.SaveRecord());
                sb.Append(",");
            }

            return sb.ToString();
        }

        public void LoadArchive(string data)
        {
            string[] parts = data.Split(',');

            recordCount = int.Parse(parts[0]);

            for (int i = 1; i < parts.Length; i++)
            {
                if (parts[i].Length == 0) continue;
                PerformanceRecord pr = new PerformanceRecord();
                pr.LoadRecord(parts[i]);

                records.Add(pr);
                recordHashCodes.Add(pr.GetHashCode());
            }
        }
    }
}
