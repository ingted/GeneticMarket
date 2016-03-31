using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticMarket.Common.Interface
{
    public interface IRuleParameter
    {
        void Randomize();
        object Value { get; set; }
    }
}
