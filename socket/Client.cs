using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bounce.socket;
namespace Bounce.socket
{
    public static class Client
    {
        public static tcpConnector tcp;
        public static void connect()
        {
            tcp = new tcpConnector("csclub.meikei.ac.jp", 1121);
            tcp.received += new tcpConnector.tcpEventHandler(received);
        }

        public static void received(object sender,packetData e)
        {

        }
    }
}
