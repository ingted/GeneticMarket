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
    public class GainCapitalDataProvider: IDataProvider
    {
        private string fileName = "";
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

        public GainCapitalDataProvider(Instrument inst, string fName)
        {
            instrument = inst;
            fileName = fName;
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

            //bypass first line
            streamReader.ReadLine();

            while (true)
            {
                if (streamReader.EndOfStream)
                {
                    break;
                }

                //read tick data
                string line = streamReader.ReadLine();

                string[] parts = line.Split(',');

                t.Time = DateTime.ParseExact(parts[3], "yyyy-MM-dd HH:mm:ss", null);
                t.Bid = double.Parse(parts[4]);
                t.Ask = double.Parse(parts[5]);

                if (ProcessTick != null)
                {
                    ProcessTick(t);
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
