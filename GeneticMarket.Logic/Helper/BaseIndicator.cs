using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Interface;
using GeneticMarket.Common.Market;

namespace GeneticMarket.Logic.Helper
{
    [Serializable]
    public abstract class BaseIndicator: IIndicator
    {
        #region ISignal Members
        protected int timeFrame = -1;

        [NonSerialized]
        protected Instrument instrument = null;

        public BaseIndicator(Instrument instrument,int timeFrame)
        {
            this.instrument = instrument;
            this.timeFrame = timeFrame;
        }

        public abstract int CalculateIdentifier();
        public abstract void Process(Tick tick);
        
        public Instrument Instrument
        {
            get
            {
                return instrument;
            }
        }

        public virtual void Initialize()
        {
        }

        public virtual void Finalize()
        {
        }

        protected double currentValue = 0d;
        public virtual double GetValue(int index)
        {
            if ( index == 0 )
            {
                return currentValue;
            }

            return double.NaN;
        }

        private int idCache = -1;
        public int GetIdentifier()
        {
            if (idCache == -1)
            {
                idCache = CalculateIdentifier();
            }

            return idCache;
        }

        private TimeFrame tfCache = null;
        protected TimeFrame TimeFrame
        {
            get
            {
                if (tfCache == null)
                {
                    tfCache = instrument[timeFrame];
                }

                return tfCache;
            }
        }

        #endregion
    }
}
