using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bounce.socket
{
    public class packetData
    {
        public int msgID;
        public String userID;
        public int mode;
        public String data;

        public packetData(String str)
        {

            String[] spdata = str.Split(',');
            msgID = int.Parse(spdata[0]);
            userID = spdata[1];
            mode = int.Parse(spdata[2]);

            data = "";
            for (int i = 3; i < spdata.Length; i++)
            {
                data += spdata[i] + ",";
            }



        }

        public packetData(int _id, String _userId, int _mode, String _data)
        {
            msgID = _id;
            userID = _userId;
            mode = _mode;
            data = _data;

        }

        public override string ToString()
        {
            return msgID + "," + userID + "," + mode + "," + data;

        }
    }
}
