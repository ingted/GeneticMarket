using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Base.Helper;
using GeneticMarket.BackEnd;
using GeneticMarket.Common.Interface;
using GeneticMarket.Common.Market;
using System.Runtime.Serialization;

namespace GeneticMarket.BackEnd.Core
{
    public delegate void OnStrategyDelegate(Strategy strategy);

    public class StrategyRepository
    {
        public event OnStrategyDelegate OnNewStrategy;
        public event OnStrategyDelegate OnDeleteStrategy;

        #region private fields

        //private int currentSerialNumber = 1;
        private List<Strategy> strategies = new List<Strategy>();
        private int strategyCount = 0;
        private int deleteCounter = 0;

        private IMarketWatch marketWatch = null;

        private IStrategyRepositoryHelper helper = null;

        //private List<Instrument> availableInstruments = null;
        
        //used to increase find strategy performance
        private List<int> strategyCodes = new List<int>();

        #endregion

        #region public properties and methods 
        public void Register(IMarketWatch mw,IStrategyRepositoryHelper repHelper)
        {
            if (mw != null)
            {
                marketWatch = mw;
            }

            if (repHelper != null)
            {
                helper = repHelper;
            }
        }

        public int StrategyCount
        {
            get
            {
                //for performance
                return strategyCount;
            }
        }

        public int DeleteCounter
        {
            get
            {
                return deleteCounter;
            }
        }

        public Strategy this[int index]
        {
            get
            {
                //as the list is dynamically being changed, maybe a code (e.g. signalInfo calculation)
                //refers to a deleted strategy
                //but we try to cover this using try/catch
                try
                {
                    return strategies[index];
                }
                catch
                {
                    return null;
                }
            }
        }

        public Strategy FindStrategy(int code)
        {
            int index = strategyCodes.IndexOf(code);

            if (index == -1)
            {
                return null;
            }

            return strategies[index];
        }

        public bool AddStrategy(Strategy s)
        {
            Logger.Log("srep adding");

            int instIndex = Rand.Random.Next(marketWatch.InstrumentCount);
            //we need to initialize s first because this can change its hashcode
            s.Initialize(marketWatch.GetInstrument(instIndex));

            Logger.Log("srep init done");

            //we need to use clientNodeHelper and check if other repositories in other servers
            //have this strategy
            if (helper == null || helper.CheckStrategyExistence(s.GetHashCode()) == false)
            {
                Logger.Log("srep check ok");

                strategies.Add(s);
                strategyCount++;
                strategyCodes.Add(s.GetHashCode());

                Logger.Log("srep added");

                if (OnNewStrategy != null)
                {
                    OnNewStrategy(s);
                }

                Logger.Log("srep finished");

                return true;
            }

            return false;
        }

        public void DeleteStrategy(Strategy strategy)
        {
            if (OnDeleteStrategy != null)
            {
                OnDeleteStrategy(strategy);
            } 
            
            strategies.Remove(strategy);
            strategyCount--;
            deleteCounter++;
            strategyCodes.Remove(strategy.GetHashCode());

            strategy.Finalize();
        }

        #endregion

        /// <summary>
        /// this method is only called by external code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool HasStrategy(int code)
        {
            return strategyCodes.Contains(code);
        }
    }
}
