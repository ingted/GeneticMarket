using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using GeneticMarket.Common.Trade;
using GeneticMarket.Common.Parameter;

namespace GeneticMarket.Logic.Helper
{
    public abstract class BaseRule: IRule
    {
        protected Dictionary<string, IRuleParameter> parameters = new Dictionary<string, IRuleParameter>();
        protected Dictionary<string, IIndicator> indicators = new Dictionary<string, IIndicator>();
        protected IndicatorRepository indicatorRepository = null;
        protected IRuleContainer container = null;

        public BaseRule()
        {
            initParameters();
        }

        #region Initialization

        /// <summary>
        /// this method can be called many times (when parent strategy is not unique)
        /// </summary>
        /// <param name="rc"></param>
        public virtual void Initialize(IRuleContainer rc)
        {
            idCache = -1;
            descCache = null;

            container = rc;

            //as a rule can be initialized more than once (for example when its strategy is a repeat)
            //we need to clear instruments and indicators list each time
            //instruments.Clear();
            
            indicators.Clear();
            addIndicators();
        }

        /// <summary>
        /// each time rule is initialized (strategy is being tested when adding to repository)
        /// this method is invoked
        /// </summary>
        protected virtual void addIndicators()
        {
        }

        protected void addIndicator(string key, IIndicator indicator)
        {
            IIndicator result = container.IndicatorRepository.AddIndicator(indicator);

            if (indicators.ContainsKey(key))
            {
                indicators[key] = result;
            }
            else
            {
                indicators.Add(key, result);
            }
        }

        protected virtual void initParameters()
        {
        }

        #endregion

        public virtual void Finalize()
        {
        }

        #region State Management

        /// <summary>
        /// Called upon randomization to validate if randomized state is valid6
        /// </summary>
        /// <returns></returns>
        protected virtual bool validateState()
        {
            //by default no logic is applied to state
            return true;
        }


        public virtual void RandomizeState()
        {
            do
            {
                foreach (string key in parameters.Keys)
                {
                    parameters[key].Randomize();
                }
            } while (!validateState());

            //periods and instruments are set when strategy is being add to rep
            //which is done by calling initialize

            idCache = -1;
            descCache = null;
        }

        public void CopyState(IRule target)
        {
            if (!(target is BaseRule))
            {
                throw new Exception("Cannot accept IRule operands");
            }

            BaseRule brTarget = target as BaseRule; 
            
            foreach (string key in parameters.Keys )
            {
                brTarget.parameters[key].Value = parameters[key].Value;
            }

            foreach (string key in indicators.Keys)
            {
                brTarget.indicators.Add(key, indicators[key]);
            }

            brTarget.container = container;
            brTarget.idCache = -1;
            brTarget.descCache = null;
        }

        #endregion

        public abstract int Execute(Tick tick);

        protected int idCache = -1;
        public int GetIdentifier()
        {
            if (idCache == -1)
            {
                idCache = GetDescription().GetHashCode();
            }

            return idCache;
        }

        protected string descCache = null;
        public string GetDescription()
        {
            if (descCache == null)
            {
                StringBuilder sb = new StringBuilder(GetType().Name);

                sb.Append("<PARAMS>");

                foreach (string key in parameters.Keys)
                {
                    string val = parameters[key].Value.ToString();

                    sb.Append(key);
                    sb.Append("=");
                    sb.Append(val);
                    sb.Append("|");
                }

                descCache = sb.ToString();
            }

            return descCache;
        }

        #region Child Helpers

        protected object this[string key]
        {
            get
            {
                return parameters[key].Value;
            }
            set
            {
                parameters[key].Value = value;
            }
        }

        protected double getIndicatorValue(string key)
        {
            return getIndicatorValue(key, 0);
        }

        protected double getIndicatorValue(string key,int index)
        {
            IIndicator ind = indicators[key];
            return ind.GetValue(index);
        }

        #endregion

        public static BaseRule LoadFromDescription(string desc)
        {
            int typeNameIndex = desc.IndexOf("<PARAMS>");
            string typeName = desc.Substring(0, typeNameIndex);

            //parameters are added upon instantiation
            BaseRule result = RuleFactory.GetInstance().CreateRuleInstance(Type.GetType("GeneticMarket.Logic.Rules." + typeName)) as BaseRule;

            //we only need to load parameter values 
            int endParamIndex = desc.Length-1;
            string paramsString = desc.Substring(typeNameIndex + 8, endParamIndex - typeNameIndex -8);

            string[] paramsKeyValues = paramsString.Split('|');

            for (int i = 0; i < paramsKeyValues.Length; i++)
            {
                if (paramsKeyValues[i].Length == 0)
                {
                    continue;
                }

                string[] parts = paramsKeyValues[i].Split('=');
                string key = parts[0];
                string value = parts[1];

                IRuleParameter argObject = result.parameters[key];
                bool isDouble = (argObject is DoubleRuleParameter);

                if (isDouble)
                {
                    result[key] = double.Parse(value);
                }
                else
                {
                    result[key] = int.Parse(value);
                }
            }

            return result;
        }
    }
}
