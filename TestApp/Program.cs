using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Parameter;
using GeneticMarket.Base.Helper;
using GeneticMarket.Common.Market;
using System.IO;
using GeneticMarket.Base.BaseObjects;
using GeneticMarket.Base.Config;
using TicTacTec.TA.Library;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] input = new double[6] { 1, 2, 3, 4, 5, 6 };
            double[] output = new double[1];

            int i1,i2;

            Core.RetCode ret = TicTacTec.TA.Library.Core.MovingAverage(5, 5, input, 3, Core.MAType.Dema, out i1, out i2, output);

            //Instrument inst = new Instrument("EURUSD");

            //DateTime dt = DateTime.Now;

            //inst.Tick(dt, 1d, 2d);
            //inst.Tick(dt.AddSeconds(1d), 1.1d, 2.1d);
            //inst.Tick(dt.AddMinutes(2d), 1.2d, 2.2d);
            //inst.Tick(dt.AddSeconds(3d), 1.3d, 2.3d);
            //inst.Tick(dt.AddSeconds(4d), 1.4d, 2.4d);

            //inst[1].GetPriceArray(GeneticMarket.Base.Def.PriceType.Close, 4);

            //PerformanceArchive pa = new PerformanceArchive();

            //pa.SubmitPerformance(5, 0, 0);
            //pa.SubmitPerformance(-6, 5, 0);
            //pa.SubmitPerformance(-7, 6, 0);
            //pa.SubmitPerformance(-8, 7, 0);
            //pa.SubmitPerformance(-9, 8, 0);
            //pa.SubmitPerformance(3, 0, 1);
            //pa.SubmitPerformance(4, 0, 2);
            //pa.SubmitPerformance(5, 0, 3);
            //pa.SubmitPerformance(6, 0, 4);
            //pa.SubmitPerformance(7, 0, 5);
            //pa.SubmitPerformance(7, 0, 5);


            //pa.GetWeight(0,5);


            

        }

        
    }

 
}
