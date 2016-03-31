using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Logic;
using GeneticMarket.BackEnd.Core;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GeneticMarket.Base.Config;
using System.IO.Compression;
using GeneticMarket.Common.Interface;
using GeneticMarket.Common.Market;

namespace GeneticMarket.Core.Logic
{
    public class PersistenceLogic
    {
        private ClientContext commonContext = null;

        public void Register(ClientContext context)
        {
            commonContext = context;
        }

        public void SaveReport(string fileName,string comment)
        {
            StreamWriter sw = null;
            bool compress = (Path.GetExtension(fileName) == ".zip");

            if (compress)
            {
                GZipStream zipStream = new GZipStream(new FileStream(fileName,FileMode.CreateNew), CompressionMode.Compress);
                sw = new StreamWriter(zipStream);
            }
            else
            {
                sw = new StreamWriter(fileName);
            }

            comment = comment.Trim();
            sw.Write("ID("+comment+")");

            sw.Write(",Signal");
            sw.Write(",X, ");
            sw.Write("Reputation@");

            for (int i = 0; i < commonContext.MarketWatch.InstrumentCount; i++)
            {
                Instrument inst = commonContext.MarketWatch.GetInstrument(i);

                sw.Write(inst.ToString()+"="+inst.CurrentTime.ToString());

                if (i < commonContext.MarketWatch.InstrumentCount - 1)
                {
                    sw.Write(", ");
                }
                else
                {
                    sw.Write(",X,");
                }
            }

            sw.WriteLine("Win Count, Total Win Points, Loose Count, Total Loose Points, Largest Win, Largest Loss, Open Position Count, Closed Position Count,X, Description");

            for (int i = 0; i < commonContext.StrategyRepository.StrategyCount; i++)
            {
                Strategy strategy = commonContext.StrategyRepository[i];

                sw.Write(strategy.GetHashCode());
                sw.Write("," + strategy.GetSignal().ToString());
                sw.Write(",X,");

                string performance = commonContext.PerformanceLogic.SavePerformance(strategy);
                sw.Write(performance);

                sw.Write(",X,");

                sw.Write(strategy.GetDescription());
                sw.WriteLine();
            }

            sw.Close();
        }

        /// <summary>
        /// when loading a report we suppose that node is connected to GM so we have a list of
        /// available instruments
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int LoadReport(string fileName)
        {
            bool isCompressed = (Path.GetExtension(fileName) == ".zip");
            StreamReader sr = null;

            if (isCompressed)
            {
                GZipStream zipStream = new GZipStream(new FileStream(fileName,FileMode.Open), CompressionMode.Decompress);
                sr = new StreamReader(zipStream);
            }
            else
            {
                sr = new StreamReader(fileName);
            }

            string firstLine = sr.ReadLine();

            //# means comment line
            while (firstLine.StartsWith("#"))
            {
                firstLine = sr.ReadLine();
            }

            string[] headerParts = firstLine.Split(new string[1] { ",X," }, StringSplitOptions.None);
            string instruments = headerParts[1].Replace("Reputation@", "").Trim();

            string[] instrumentsArray = instruments.Split(',');
            List<string> refindList = new List<string>();

            foreach (string inst in instrumentsArray)
            {
                string temp = inst;
                int timeIndex = inst.IndexOf("=");

                if (timeIndex != -1)
                {
                    string time = temp.Substring(timeIndex + 1).Trim();
                    temp = temp.Replace("=" + time, "").Trim();

                    commonContext.MarketWatch.SetInstrumentStartTime(temp, DateTime.Parse(time));
                }

                refindList.Add(temp);
            }

            commonContext.MarketWatch.LoadInstruments(refindList);
            
            while ( !sr.EndOfStream )
            {
                string line = sr.ReadLine();
                string[] parts = line.Split(new string[1]{",X,"},StringSplitOptions.None);
                string idPart = parts[0];
                //string reputationPart = parts[1];
                string performancePart = parts[1];
                string descPart = parts[2];

                idPart = idPart.Substring(0, idPart.IndexOf(","));
                int id = int.Parse(idPart);

                Strategy strategy = Strategy.LoadFromDescription(descPart,commonContext.IndicatorRepository,commonContext.MarketWatch);

                commonContext.PerformanceLogic.LoadPerformance(performancePart, strategy);
                commonContext.StrategyRepository.AddStrategy(strategy);

                int newId = strategy.GetHashCode();
            }

            sr.Close();

            return commonContext.StrategyRepository.StrategyCount;
        }
    }
}
