using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Def;
using GeneticMarket.Base.Helper;
using GeneticMarket.Common.Interface;

namespace GeneticMarket.Common.Trade
{
    public class Position
    {
        private Instrument instrument = null;
        private object owner = null;
        private int positionType = SignalType.Neutral;
        private int serialNumber = 0;
        private DateTime openTime = DateTime.MinValue;
        private double openPrice = 0;

        //dyamic data
        private int profitPips = 0;

        //MM data
        private int stopLoss = 0;
        private int takeProfit = 0;
        private int trailingStop = 0;

        //close data
        private bool isClosed = false;
        private double closePrice = 0;
        private DateTime closeTime = DateTime.MinValue;
        private object metaInfo = null;

        public Position(Instrument ins, int type, int sl, int tp, int serial,int trailingStop)
        {
            positionType = type;
            instrument = ins;

            openTime = instrument.CurrentTime;

            if (type == SignalType.Long)
            {
                openPrice = ins.TickAsk;
            }
            else if (type == SignalType.Short)
            {
                openPrice = ins.TickBid;
            }

            stopLoss = sl;
            takeProfit = tp;
            serialNumber = serial;
            this.trailingStop = trailingStop;
        }

        public int GetProfitLoss()
        {
            return profitPips;
        }

        public object Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        public object MetaInfo
        {
            get
            {
                return metaInfo;
            }
            set
            {
                metaInfo = value;
            }
        }

        public Instrument Instrument
        {
            get
            {
                return instrument;
            }
        }

        public int Age
        {
            get
            {
                return (int)instrument.CurrentTime.Subtract(openTime).TotalSeconds;
            }
        }

        public int PositionType
        {
            get
            {
                return positionType;
            }
        }

        public bool IsClosed
        {
            get
            {
                return isClosed;
            }
        }

        /// <summary>
        /// returns true of SL/TP is hit
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public bool OnTick(Tick tick)
        {
             if (!tick.Instrument.Equals(instrument)) return false;

            if (positionType == SignalType.Long)
            {
                profitPips = instrument.ToPips(openPrice, instrument.TickBid);
            }
            else
            {
                profitPips = instrument.ToPips(instrument.TickAsk, openPrice); 
            }

            if (trailingStop != -1)
            {
                if (profitPips > trailingStop)
                {
                    stopLoss = profitPips - trailingStop;
                }
            }

            return (profitPips <= -stopLoss) || (profitPips >= takeProfit);
        }

        public void OnClose()
        {
            isClosed = true;
            closeTime = instrument.CurrentTime;

            if (positionType == SignalType.Long)
            {
                closePrice = instrument.TickBid;
            }
            else
            {
                closePrice = instrument.TickAsk;
            }
        }
    }
}
