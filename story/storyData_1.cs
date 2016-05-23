using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce.story
{
    class storyData_1:storyData
    {
        public storyData_1(int c,string s,float sp)
        {
            this.ch = c;
            this.serif = s;
            this.spped = sp;
        }
        public int ch;
        public string serif;
        public float spped;
    }
}
