using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticMarket.Base.Def
{
    public class SignalType
    {
        public const int Neutral = 0;
        public const int Long = 1;
        public const int Short = -1;

        //used for example when target instrument does not match
        //tick instrument so nothing can be said about signal
        public const int NA = -2;
    }
}
