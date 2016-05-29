using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Bounce.socket
{
    public class tcpConnector
    {
        TcpClient tcp;
        byte[] ReceiveBuffer;
        System.IO.MemoryStream ReceivedData;
        //Delegate を宣言しておく

        System.Net.Sockets.NetworkStream ns;
        System.Text.Encoding enc = System.Text.Encoding.UTF8;

        public delegate void tcpEventHandler(object sender, packetData e);

        public int id;

        public event tcpEventHandler received;

        System.ComponentModel.BackgroundWorker bw;


        public string host;
        public int port;

        public tcpConnector(string host,int port)
        {
            this.host = host;
            this.port = port;

            //TcpClientを作成し、サーバーと接続する
            tcp =
                new System.Net.Sockets.TcpClient(host, port);
            Console.WriteLine("サーバー({0}:{1})と接続しました({2}:{3})。",
                ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Address,
                ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Port,
                ((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Address,
                ((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Port);

            //NetworkStreamを取得する
            ns = tcp.GetStream();

            bw = new System.ComponentModel.BackgroundWorker();
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.run);
           
        }

        public void startReceive()
        {
            bw.RunWorkerAsync();
        }

        public void send(packetData pd)
        {
            if (pd.mode == @const.request)
            {
                id++;
            }
            //サーバーにデータを送信する
            //文字列をByte型配列に変換
            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            byte[] sendBytes = enc.GetBytes(pd.ToString() + '\n');
            //データを送信する
            ns.Write(sendBytes, 0, sendBytes.Length);
            //Console.WriteLine(sendMsg);
        }

        public  void run(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (tcp.Connected)
            {
                //サーバーから送られたデータを受信する
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                byte[] resBytes = new byte[256];
                int resSize = 0;
                do
                {
                    //データの一部を受信する
                    resSize = ns.Read(resBytes, 0, resBytes.Length);
                    //Readが0を返した時はサーバーが切断したと判断
                    if (resSize == 0)
                    {
                        Console.WriteLine("サーバーが切断しました。");
                        break;
                    }
                    //受信したデータを蓄積する
                    ms.Write(resBytes, 0, resSize);
                    //まだ読み取れるデータがあるか、データの最後が\nでない時は、
                    // 受信を続ける
                } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');
                //受信したデータを文字列に変換
                string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
                ms.Close();
                //末尾の\nを削除
                resMsg = resMsg.TrimEnd('\n');

                packetData pd = new packetData(resMsg);
                if (pd.mode == @const.request)
                {
                    id = pd.msgID;

                }
                if (received != null) received(this, new packetData(resMsg));


            }
        }
        public void disConnect()
        {
            tcp.Close();
        }
    }
}
