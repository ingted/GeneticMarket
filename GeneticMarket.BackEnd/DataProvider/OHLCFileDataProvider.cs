using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GeneticMarket.BackEnd.DataProvider;
using GeneticMarket.Common.Market;
using GeneticMarket.Common.Interface;
using System.Runtime.Remoting.Messaging;
using GeneticMarket.Base.Def;

namespace GeneticMarket.BackEnd.DataProvider
{
    public class OHLCFileDataProvider: IDataProvider
    {
        private string fileName = "";
        private char separator = ',';
        private StreamReader streamReader = null;
        private Instrument instrument = null;
        private int ThreadTickLimit = 0;
        private Thread runnerThread = null;
        private IDataProvider nextProvider = null;
        private int tag = 0;
        private DataProviderStatus status = DataProviderStatus.NotStarted;

        public event TickProcessDoneDelegate ProcessTick;
        public event EventHandler Stopped;

        public Instrument Instrument
        {
            get
            {
                return instrument;
            }
        }

        public OHLCFileDataProvider(Instrument inst,string fName,char separatorCharacter)
        {
            instrument = inst;
            fileName = fName;
            separator = separatorCharacter;
        }

        #region IDataProvider Members

        public void Initialize()
        {
            streamReader = new StreamReader(fileName);
        }

        public void Process(int ticks)
        {
            ThreadTickLimit = ticks;

            status = DataProviderStatus.Running;
            if (runnerThread != null && runnerThread.IsAlive)
            {
                runnerThread.Resume();
                return;
            }

            runnerThread = new Thread(new ThreadStart(tickThread));
            runnerThread.Start();
        }

        public int TotalTickCount
        {
            get
            {
                return File.ReadAllLines(fileName).Length*4;
            }
        }

        private void tickThread()
        {
            Tick t = new Tick();
            t.Instrument = instrument;

            while (true)
            {
                if (streamReader.EndOfStream)
                {
                    break;
                }

                //read tick data
                string line = streamReader.ReadLine();

                string[] parts = line.Split(separator);
                
                //OHLC
                for (int i = 1; i <= 4; i++)
                {
                    t.Time = DateTime.ParseExact(parts[0],"dd.MM.yyyy HH:mm",null);
                    t.Bid = double.Parse(parts[i]);
                    
                    if (ProcessTick != null)
                    {
                        ProcessTick(t);
                    }
                    //tickIndex++;
                }
                if (ThreadTickLimit != 0)
                {
                    ThreadTickLimit--;

                    if (ThreadTickLimit == 0)
                    {
                        break;
                    }
                }
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(Stop));
        }

        /// <summary>
        /// just a wrapper so I can call Stop by QueueUserWorkItem
        /// </summary>
        /// <param name="dummy"></param>
        private void Stop(object dummy)
        {
            Stop();
        }

        public void Stop()
        {
            if (runnerThread != null && runnerThread.IsAlive)
            {
                if (runnerThread.ThreadState == ThreadState.Suspended)
                {
                    runnerThread.Resume();
                }

                runnerThread.Abort();
            }
            runnerThread = null;

            if (streamReader != null)
            {
                streamReader.Close();
            }

            status = DataProviderStatus.Stopped;

            if (Stopped != null)
            {
                Stopped(this, EventArgs.Empty);
            }
        }

        public void Pause()
        {
            if (runnerThread != null && runnerThread.IsAlive)
            {
                runnerThread.Suspend();
                status = DataProviderStatus.Paused;
            }
        }

        public IDataProvider NextProvider
        {
            get
            {
                return nextProvider;
            }
            set
            {
                nextProvider = value;
            }
        }

        #endregion

        #region IDataProvider Members


        public int Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

        public DataProviderStatus Status
        {
            get
            {
                return status;
            }
        }

        #endregion
    }
}
