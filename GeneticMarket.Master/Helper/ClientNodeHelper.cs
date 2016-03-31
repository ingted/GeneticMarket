using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using GeneticMarket.Core;
using GeneticMarket.Base.Config;
using GeneticMarket.BackEnd.Core;
using System.Runtime.Remoting;
using GeneticMarket.Master.Interface;

namespace GeneticMarket.Master.Helper
{
    public delegate void OnClientDisconnectEventHandler(int index);

    public class ClientNodeHelper : MarshalByRefObject, IClientNodeHelper
    {
        private IMasterStrategy masterStrategy = null;

        public event OnClientDisconnectEventHandler ClientDisconnect;

        public ClientNodeHelper(IMasterStrategy ms)
        {
            masterStrategy = ms;
        }

        public bool CheckStrategyExistence(int code)
        {
            foreach (ClientContextProvider provider in masterStrategy.ContextProviders)
            {
                if (provider.CheckStrategyExistence(code))
                {
                    return true;
                }
            }

            return false;
        }

        public void Disconnect(int id)
        {
            int resultIndex = -1;

            for (int i = 0; i < masterStrategy.ContextProviders.Count; i++)
            {
                ClientContextProvider provider = masterStrategy.ContextProviders[i];

                if (provider.Id == id)
                {
                    resultIndex = i;
                }
            }

            if (resultIndex != -1 && ClientDisconnect != null)
            {
                ClientDisconnect(resultIndex);
            }
        }
    }
}
