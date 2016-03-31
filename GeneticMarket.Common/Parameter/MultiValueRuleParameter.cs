using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;
using GeneticMarket.Base.Helper;

namespace GeneticMarket.Common.Parameter
{
    public class MultiValueRuleParameter: IRuleParameter
    {
        private List<object> values = new List<object>();
        private object activeVal = null;

        public MultiValueRuleParameter(params object[] values)
        {
            foreach (object val in values)
            {
                if (val.GetType().IsArray)
                {
                    foreach (object innerVal in (Array)val)
                    {
                        this.values.Add(innerVal);
                    }
                }
                else
                {
                    this.values.Add(val);
                }
            }
        }

        #region IRuleParameter Members

        public void  Randomize()
        {
            int index = Rand.Random.Next(0, values.Count);

            activeVal = values[index];
        }

        public object  Value
        {
	        get 
	        {
                return activeVal;
	        }
	        set 
	        {
                activeVal = value;
	        }
        }

        #endregion
    }
}
