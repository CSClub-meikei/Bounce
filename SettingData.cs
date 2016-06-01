using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bounce.socket;
using System.Runtime.Serialization;
using System.Xml;

namespace Bounce
{
    
    public class SettingData
    {
        public float BGM_volume;
        public  float Effect_volume;

        public int  userid;

        public int Cleared;


      
        public void save()
        {
            //保存先のファイル名
            string fileName = @"tmp.tmp";

            //保存するクラス(TestClass)のインスタンスを作成
            SettingData obj = this;

            //DataContractSerializerオブジェクトを作成
            //オブジェクトの型を指定する
            DataContractSerializer serializer =
                new DataContractSerializer(typeof(SettingData));
            //BOMが付かないUTF-8で、書き込むファイルを開く
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new System.Text.UTF8Encoding(false);
            XmlWriter xw = XmlWriter.Create(fileName, settings);
            //シリアル化し、XMLファイルに保存する
            serializer.WriteObject(xw, obj);
            //ファイルを閉じる
            xw.Close();

            //"C:\test\1.txt"をShift-JISコードとして開く
            System.IO.StreamReader sr = new System.IO.StreamReader(
                @"tmp.tmp",
                System.Text.Encoding.GetEncoding("shift_jis"));
            //内容をすべて読み込む
            string s = sr.ReadToEnd();
            //閉じる
            sr.Close();

            Client.tcp.send(new packetData(Client.tcp.id, userData.userid.ToString(), @const.request, "bounce,setSaveData," + s));

        }

    }
}
