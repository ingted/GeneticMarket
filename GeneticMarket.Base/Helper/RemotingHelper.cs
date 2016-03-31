using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Runtime.Remoting.Channels.Tcp;

namespace GeneticMarket.Base.Helper
{
    public class RemotingHelper
    {
        public static object RegisterChannel(string name,int port)
        {
            BinaryServerFormatterSinkProvider serverFormatter =
               new BinaryServerFormatterSinkProvider();

            serverFormatter.TypeFilterLevel = TypeFilterLevel.Full;

            BinaryClientFormatterSinkProvider clientProv =
                new BinaryClientFormatterSinkProvider();

            Hashtable props = new Hashtable();
            props["name"] = name;
            props["port"] = port;

            TcpChannel tcp = new TcpChannel(props, clientProv, serverFormatter);
            ChannelServices.RegisterChannel(tcp, false);

            return tcp;
        }

        public static void UnregisterChannel(object channel)
        {
            ChannelServices.UnregisterChannel(channel as IChannel);
        }
    }
}
