using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using GeneticMarket.Common.Interface;
using GeneticMarket.Base.Config;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Logic
{
    public class RuleFactory
    {
        private static RuleFactory instance = new RuleFactory();
        public static RuleFactory GetInstance()
        {
            return instance;
        }

        private List<Type> ruleTypes = new List<Type>();

        protected RuleFactory()
        {
            Type[] types = typeof(GeneticMarket.Logic.Helper.BaseRule).Assembly.GetTypes();

            foreach (Type type in types)
            {
                if (typeof(IRule).IsAssignableFrom(type) && type.FullName.StartsWith("GeneticMarket.Logic.Rules"))
                {
                    ruleTypes.Add(type);
                }
            }
        }

        public int RuleCount
        {
            get
            {
                return ruleTypes.Count;
            }
        }

        public IRule CreateRuleInstance(Type ruleType)
        {
            foreach (Type t in ruleTypes)
            {
                if (t.Equals(ruleType))
                {
                    IRule result = Activator.CreateInstance(t) as IRule;

                    return result;
                }
            }

            return null;
        }

        public IRule CreateRandomInstance()
        {
            int index = Rand.Random.Next(0, RuleCount - 1);

            IRule result = Activator.CreateInstance(ruleTypes[index]) as IRule;
            result.RandomizeState();

            return result;
        }
    }
}
