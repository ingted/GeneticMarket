using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticMarket.Base.Helper
{
    public class Rand
    {
        public static Random Random = new Random();

        public static List<int> GetRandomIndices(int listSize, int resultSize)
        {
            if ( resultSize > listSize ) 
            {
                resultSize = listSize;
            }

            List<int> indexList = new List<int>();
            List<int> result = new List<int>();

            for (int i = 0; i < listSize; i++)
            {
                indexList.Add(i);
            }

            int counter = 0;
            while (true)
            {
                int idx = Random.Next(0, indexList.Count);
                result.Add(indexList[idx]);
                indexList.Remove(idx);
                
                counter++;

                if (counter >= resultSize) break;
            }

            return result;
        }
    }
}
