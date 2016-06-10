using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bounce.socket;
using System.Runtime.Serialization;
using System.Xml;

namespace Bounce.socket
{
    public static class Client
    {
        public static tcpConnector tcp;
        public static Game1 game;
        public static void connect(Game1 game)
        {
            tcp = new tcpConnector("csclub.meikei.ac.jp", 1121);
            tcp.received += new tcpConnector.tcpEventHandler(received);
            Client.game = game;
        }

        public static void received(object sender,packetData e)
        {
            //DebugConsole.write(e.data);
            String[] sp = e.data.Split(',');
           
            switch(sp[0])
            {
                case "getSaveData":
                    if(sp[1]=="" || sp[1] == "null")
                    {
                        game.settingData = new SettingData();
                        game.settingData.init(game);
                        game.settingData.BGM_volume = 1;
                        game.settingData.Effect_volume = 1;
                        game.settingData.Cleared = 0;
                        DebugConsole.write(game.settingData.BGM_volume.ToString());
                        game.settingData.save();
                    }
                    else
                    {
                        //Shift JISで書き込む
                        //書き込むファイルが既に存在している場合は、上書きする
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(
                            @"tmp.tmp",
                            false,
                            System.Text.Encoding.GetEncoding("shift_jis"));
                        //TextBox1.Textの内容を書き込む
                        sw.Write(sp[1]);
                        //閉じる
                        sw.Close();
                        //保存元のファイル名
                        string fileName = @"tmp.tmp";

                        //DataContractSerializerオブジェクトを作成
                        DataContractSerializer serializer =
                            new DataContractSerializer(typeof(SettingData));
                        //読み込むファイルを開く
                        XmlReader xr = XmlReader.Create(fileName);
                        //XMLファイルから読み込み、逆シリアル化する
                        game.settingData = (SettingData)serializer.ReadObject(xr);
                        game.settingData.init(game);
                        //ファイルを閉じる
                        xr.Close();
                       // System.Windows.Forms.MessageBox.Show(game.settingData.BGM_volume.ToString());
                       // DebugConsole.write(game.settingData.BGM_volume.ToString());
                    }
                   


                    break;
            }
        }


    }
}
