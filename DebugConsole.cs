using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce
{
    public static class DebugConsole
    {

        public static List<string> output = new List<string>();
        public static int lines;
        public static event EventHandler updated;
        
        


        public static void write(string str)
        {
            output.Add(str);
            updated(null, null);
            lines++;
        }
        public static string getOutput(int s, int e)
        {
            string res="";
            int i = 0;
            for (i = s; i <= e; i++)
            {
               if(i>=0 && i<output.Count) res += output[i] + "\n";
            }
            return res;
        }
    }
}
