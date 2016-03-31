using System.Collections.Generic;
using System.Text;
using System;
using GeneticMarket.BackEnd.DataProvider;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Config;
using GeneticMarket.BackEnd.Core;
using GeneticMarket.Logic;
using GeneticMarket.Common.Interface;
//using System.Threading;
using System.IO;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.BackEnd
{
    public class MarketWatch: IMarketWatch
    {
        public MarketWatch()
        {
        }

        private List<Instrument> instruments = new List<Instrument>();
        private IndicatorRepository indicatorRepository = null;
        private int currentTickIndex = 0;
        private bool onTickInProgress = false;
        private List<Tick> tickQueue = new List<Tick>();
        private Dictionary<Instrument, DateTime> startTimes = null;  //updated when loading a saved state
        public event MarketUpdateEventHandler MarketMeterProcess;
        public event MarketUpdateEventHandler AccountProcess;
        public event MarketUpdateEventHandler PositionOpenningLogicProcess;
        public event MarketUpdateEventHandler StrategyProcess;
        public event MarketUpdateEventHandler GeneticProcess;

        public void Register(List<string> instList,object indicatorRep)
        {
            indicatorRepository = indicatorRep as IndicatorRepository;

            //we do not empty instrument list because during load, some instruments are added
            foreach(string inst in instList)
            {
                if (null == FindInstrument(inst))
                {
                    instruments.Add(new Instrument(inst));
                }
            }
        }

        public void LoadInstruments(List<string> instList)
        {
            foreach (string inst in instList)
            {
                if (null == FindInstrument(inst))
                {
                    instruments.Add(new Instrument(inst));
                }
            }
        }

        /// <summary>
        /// as tick is outside borders of active appDomain we need to build tick here
        /// </summary>
        /// <param name="tick"></param>
        public void OnTick(string instrument,DateTime time, double bid, double ask)
        {
            Instrument tickInstrument = FindInstrument(instrument);
            currentTickIndex++;

            if (startTimes != null)
            {
                if (startTimes.ContainsKey(tickInstrument))
                {
                    DateTime dt = startTimes[tickInstrument];

                    //bypass if time is less than instrument time
                    if (time <= dt)
                    {
                        return;
                    }
                    else
                    {
                        startTimes.Remove(tickInstrument);

                        if (startTimes.Count == 0)
                        {
                            //prevent further check for startTimes to increase performance
                            startTimes = null;
                        }
                    }
                }
            }


            Tick tick = new Tick(time, bid, ask, tickInstrument);

            if (onTickInProgress)
            {
                lock (tickQueue)
                {
                    tickQueue.Add(tick);
                }

                return;
            }

            onTickInProgress = true;

            try
            {
                processTick(tick);
            }
            finally
            {
                while (true)
                {
                    Tick tickToProcess = null;

                    lock (tickQueue)
                    {
                        if (tickQueue.Count > 0)
                        {
                            tickToProcess = tickQueue[0];
                            tickQueue.RemoveAt(0);
                        }
                    }

                    if (tickToProcess != null)
                    {
                        try
                        {
                            processTick(tickToProcess);
                        }
                        catch
                        { 
                        }
                        finally
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                onTickInProgress = false;
            }
        }

        private void processTick(Tick tick)
        {
            //Logger.Log("Process tick #{0}", currentTickIndex);
            //Logger.Log("Tick instrument feed");
            tick = tick.Instrument.Tick(tick.Time, tick.Bid, tick.Ask);

            //Logger.Log("Indicator execution");
            indicatorRepository.ProcessTick(tick);

            //Logger.Log("Market Meter process");
            MarketMeterProcess(tick);

            //Logger.Log("strategy execution");
            StrategyProcess(tick);

            //Logger.Log("account process");

            //process SLTP hit of position and close them if so
            //also fire an event to external entities to check if a position needs to be closed
            //this cehcking required strategy signals so this event is fired after strategy updates
            AccountProcess(tick);

            //Logger.Log("position open process");
            PositionOpenningLogicProcess(tick);

            //Logger.Log("genetic process");
            GeneticProcess(tick);

            Logger.Log("tick #{0} execution done",currentTickIndex);
        }

        public int InstrumentCount
        {
            get
            {
                return instruments.Count;
            }
        }

        public Instrument GetInstrument(int index)
        {
            return instruments[index];
        }

        public Instrument FindInstrument(string instStr)
        {
            foreach (Instrument inst in instruments)
            {
                if (inst.ToString() == instStr)
                {
                    return inst;
                }
            }

            return null;
        }

        public int TickQueueSize
        {
            get
            {
                return tickQueue.Count;
            }
        }


        public void SetInstrumentStartTime(string instrument, DateTime startTime)
        {
            if (startTimes == null)
            {
                startTimes = new Dictionary<Instrument, DateTime>();
            }

            Instrument inst = FindInstrument(instrument);

            if (inst == null)
            {
                inst = new Instrument(instrument);

                instruments.Add(inst);
            }

            startTimes.Add(inst, startTime);
        }

        public int CurrentTickIndex
        {
            get
            {
                return currentTickIndex;
            }
        }
    }
}
