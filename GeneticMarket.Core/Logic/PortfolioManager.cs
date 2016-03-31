using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Common.Trade;
using GeneticMarket.Common.Market;
using GeneticMarket.Common.Interface;

namespace GeneticMarket.Core.Logic
{
    public class PortfolioManager
    {
        private Account account = null;

        public void Register(ClientContext context)
        {
            account = context.DefaultAccount;
        }

        public Position OpenPosition(Instrument instrument,int signalType,object owner)
        {
            //SET SL/TP for position
            //Size is not set becasue we only deal with points
            return account.OpenPosition(instrument, signalType, 25, 30, 25, owner);
        }
    }
}
