using System;
using System.Collections.Generic;
using System.Text;
using GeneticMarket.Core;

namespace GeneticMarket.Master.Interface
{
    public interface IMasterStrategy
    {
        //services for masterSignal
        List<ClientContextProvider> ContextProviders {get; }
        string ActiveInstrument {get;}


        //services for clientNodeHelper
        //bool CheckStrategyExistence(int code);
        //void ClientDisconnect(int id);        
    }
}
