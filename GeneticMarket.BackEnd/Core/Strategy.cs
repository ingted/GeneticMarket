using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using GeneticMarket.Base.Helper;
using GeneticMarket.Base.Config;
using GeneticMarket.Common.Trade;
using System.Collections;
using GeneticMarket.BackEnd;
using GeneticMarket.Logic;
using System.Runtime.Serialization;
using GeneticMarket.Logic.Helper;

namespace GeneticMarket.BackEnd.Core
{
    public class Strategy: IRuleContainer
    {
        private List<IRule> rules = new List<IRule>();

        //private int serialNumber = 0;
        private int smallPeriod = -1;
        private int defaultPeriod = -1;
        private int largePeriod = -1;

        private int currentSignal = SignalType.NA;
        private Instrument defaultInstrument = null;
        private IIndicatorRepository indicatorRep = null;

        public Strategy(IIndicatorRepository irep)
        {
            indicatorRep = irep;
        }

        //used in higher layer code
        //private object weightInfo = null;
        //public object WeightInfo
        //{
        //    get
        //    {
        //        return weightInfo;
        //    }
        //    set
        //    {
        //        weightInfo = value;
        //    }
        //}

        private object performanceInfo = null;
        public object PerformanceInfo
        {
            get
            {
                return performanceInfo;
            }
            set
            {
                performanceInfo = value;
            }
        }

        public int RuleCount
        {
            get
            {
                return rules.Count;
            }
        }

        public IRule CopyRule(int ruleIndex,Strategy target)
        {
            IRule rule = rules[ruleIndex];

            IRule newRule = RuleFactory.GetInstance().CreateRuleInstance(rule.GetType());
            //we do not initialize rule here coz when str is added to rep, its 
            //initialize method is called which calles init of its rules

            //newRule.Initialize(this);  
            rule.CopyState(newRule);
            target.AddRule(newRule);

            return newRule;
        }

        /// <summary>
        /// we do not check for rule duplication here
        /// because main factors that make a rule unique will be set in strategy
        /// randomization
        /// </summary>
        /// <param name="rule"></param>
        public void AddRule(IRule rule)
        {
            rules.Add(rule);
            //when strategy is randomized rule periods will change

            descCache = null;
            identifier = -1;

            //return true;
        }

        public void Process(Tick tick)
        {
            if (!tick.Instrument.Equals(defaultInstrument))
            {
                //we do not change strategy signal upon an out of range tick
                return;
            }

            currentSignal = SignalType.NA;

            //run all rules (with all instruments) and ask their positionData and update
            //current signals
            foreach (IRule rule in rules)
            {
                int ruleSignal = rule.Execute(tick);

                if (currentSignal == SignalType.NA)
                {
                    currentSignal = ruleSignal;
                }
                else
                {
                    if (currentSignal != ruleSignal)
                    {
                        currentSignal = SignalType.Neutral;
                        return;
                    }
                }
            }
        }

        public void Initialize(Instrument defInst)
        {
            defaultInstrument = defInst;

            foreach (IRule rule in rules)
            {
                rule.Initialize(this);
            }
        }

        public void Finalize()
        {
            foreach (IRule rule in rules)
            {
                rule.Finalize();
            }
        }

        //deep dup - dup rules instances too
        public Strategy Duplicate()
        {
            Strategy result = new Strategy(indicatorRep);

            foreach (IRule rule in rules)
            {
                //newRule contains parameters of rule but no value
                IRule newRule = RuleFactory.GetInstance().CreateRuleInstance(rule.GetType());

                //rule.init will be called upon strategy initialization
                //newRule.Initialize(this);
                rule.CopyState(newRule);

                result.AddRule(newRule);
            }

            CopyInfo(result);

            return result as Strategy;
        }

        public void Randomize(double ruleRatio)
        {
            int rcount = (int)(ruleRatio * rules.Count);

            if (rcount == 0) rcount = 1;

            List<int> indexList = Rand.GetRandomIndices(rules.Count, rcount);
            RandomizePeriods();

            foreach (int index in indexList)
            {
                IRule rule = rules[index];
                rule.RandomizeState();
            }

            //foreach (IRule rule in rules)
            //{
            //    rule.SetPeriods(smallPeriod, defaultPeriod, largePeriod);
            //}

            descCache = null;
            identifier = -1;
        }

        private void RandomizePeriods()
        {
            int defaultIndex = Rand.Random.Next(0, GeneralConfig.ValidTimeFrames.Length);
            int smallIndex = Rand.Random.Next(0, defaultIndex);
            int largeIndex = Rand.Random.Next(defaultIndex, GeneralConfig.ValidTimeFrames.Length);

            if (smallIndex == defaultIndex)
            {
                if (defaultIndex > 0)
                {
                    smallIndex--;
                }
                else
                {
                    defaultIndex++;
                }
            }

            if (largeIndex == defaultIndex)
            {
                if (defaultIndex < GeneralConfig.ValidTimeFrames.Length - 1)
                {
                    largeIndex++;
                }
                else
                {
                    defaultIndex--;
                }
            }

            defaultPeriod = GeneralConfig.ValidTimeFrames[defaultIndex];
            smallPeriod = GeneralConfig.ValidTimeFrames[smallIndex];
            largePeriod = GeneralConfig.ValidTimeFrames[largeIndex];
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Strategy)
            {
                Strategy s = obj as Strategy;
                return GetHashCode() == s.GetHashCode();
            }

            return base.Equals(obj);
        }

        private int identifier = -1;
        public override int GetHashCode()
        {
            if (identifier == -1)
            {
                identifier = GetDescription().GetHashCode();
            }

            return identifier;
        }

        private string descCache = null;
        public string GetDescription()
        {
            if (descCache == null)
            {
                string result = string.Format("TF:({0} {1} {2}) INST: {3} Rules:", smallPeriod, defaultPeriod, largePeriod,defaultInstrument.ToString());

                foreach (IRule rule in rules)
                {
                    result += "{" + rule.GetDescription() + "}";
                }

                descCache = result;
            }

            return descCache;
        }

        public int GetSignal()
        {
            return currentSignal;
        }
        
        public void CopyInfo(Strategy newStrategy)
        {
            Strategy str = newStrategy as Strategy;

            str.defaultPeriod = defaultPeriod;
            str.smallPeriod = smallPeriod;
            str.largePeriod = largePeriod;
            str.defaultInstrument = defaultInstrument;
            str.indicatorRep = indicatorRep;
        }

        public static Strategy LoadFromDescription(string desc,IndicatorRepository ir,IMarketWatch mw)
        {
            int rulesIndex = desc.IndexOf("Rules:");
            int instIndex = desc.IndexOf("INST:");
            string tfs = desc.Substring(3, instIndex - 3);
            string[] tfsList = tfs.Replace("(","").Replace(")","").Split(' ');

            Strategy result = new Strategy(ir);
            result.smallPeriod = int.Parse(tfsList[0]);
            result.defaultPeriod = int.Parse(tfsList[1]);
            result.largePeriod = int.Parse(tfsList[2]);

            string inst = desc.Substring(instIndex + 6, rulesIndex - instIndex - 7);
            result.defaultInstrument = mw.FindInstrument(inst);

            if (result.defaultInstrument == null)
            {
                throw new Exception("Cannot find strategy instrument");
            }

            rulesIndex = desc.IndexOf('{', rulesIndex);
            string[] rules = desc.Substring(rulesIndex).Split('{');

            foreach (string rule in rules)
            {
                if (rule.Length == 0)
                {
                    continue;
                }

                string trule = rule.Substring(0, rule.Length - 1);

                BaseRule ruleObject = BaseRule.LoadFromDescription(trule);
                //ruleObject.SetPeriods(result.smallPeriod, result.defaultPeriod, result.largePeriod);

                result.AddRule(ruleObject);
            }

            return result;
        }

        #region IRuleContainer Members

        public int SmallPeriod
        {
            get 
            {
                return smallPeriod;
            }
        }

        public int DefaultPeriod
        {
            get
            {
                return defaultPeriod;
            }
        }

        public int LargePeriod
        {
            get
            {
                return largePeriod;
            }
        }

        public Instrument DefaultInstrument
        {
            get
            {
                return defaultInstrument;
            }
        }

        public IIndicatorRepository IndicatorRepository
        {
            get
            {
                return indicatorRep;
            }
        }

        #endregion
    }
}
