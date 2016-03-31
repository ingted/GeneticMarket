using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Market;
using GeneticMarket.Base.Helper;
using GeneticMarket.Common.Interface;
//using System.Threading;

namespace GeneticMarket.Common.Trade
{
    public delegate void OnPositionDelegate(Position position);

    public class Account
    {
        public Account()
        {
        }

        public event OnPositionDelegate OnOpenPosition;
        public event OnPositionDelegate OnClosePosition;
        public event OnPositionDelegate ProcessClosePosition;

        private List<Position> positions = new List<Position>();
        private int minStopLossPips = 25;
        private int minTakeProfitPips = 25;
        private int currentSerialNumber = 1;
        private int closedPositionCounter = 0;

        public int TotalPositionCount
        {
            get
            {
                return positions.Count;
            }
        }

        public int ClosedPositionCount
        {
            get
            {
                return closedPositionCounter;
            }
        }

        public void Register(IMarketWatch mw)
        {
            //SLTP has to check before market strategy and genetic env are executed
            mw.AccountProcess += new MarketUpdateEventHandler(OnTick);
        }

        public void UnRegister()
        {
        }

        public bool CanOpenPosition(int slPoints, int tpPoints)
        {
            if (slPoints < minStopLossPips)
            {
                return false;
            }

            if (tpPoints < minTakeProfitPips)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// we dont work with size of position as we are interested only in pip prfit/loss 
        /// not real money profit/loss
        /// </summary>
        /// <param name="instrument"></param>
        /// <param name="type"></param>
        /// <param name="slPoints"></param>
        /// <param name="tpPoints"></param>
        /// <returns></returns>
        public Position OpenPosition(Instrument instrument, int type, int slPoints, int tpPoints,int trailingStop,object owner)
        {
            if (false == CanOpenPosition(slPoints, tpPoints))
            {
                throw new Exception("Can not open position");
            }

            Position pos = new Position(instrument, type,slPoints,tpPoints,currentSerialNumber++,trailingStop);
            positions.Add(pos);
            pos.Owner = owner;

            if (OnOpenPosition != null)
            {
                OnOpenPosition(pos);
            }

            return pos;
        }

        public void ClosePosition(Position p)
        {
            p.OnClose();
            closedPositionCounter++;
            OnClosePosition(p);
        }

        /// <summary>
        /// process open positions for SL/TP hit
        /// 
        /// </summary>
        /// <param name="tick"></param>
        private void OnTick(Tick tick)
        {
            //openProfit = openLoss = 0;

            foreach (Position p in positions)
            {
                if (p.IsClosed) continue;

                //is SL/TP touched?
                if (p.OnTick(tick))
                {
                    //close by stoploss
                    ClosePosition(p);
                }
                else
                {
                    if (ProcessClosePosition != null)
                    {
                        ProcessClosePosition(p);
                    }
                }                
            }
        }

        public void Reset()
        {
            positions.Clear();
        }
    }
}
