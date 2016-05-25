using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce.story
{
    class storyData_2:storyData
    {
        public bool isShow;
        public bool active;
        public int ch;
        public int mode;
        public float delay;

       public storyData_2(bool isShow,bool active,int ch,int mode,int delay)
        {
            this.isShow = isShow;
            this.active = active;
            this.ch = ch;
            this.mode = mode;
            this.delay = delay;

        }

    }
}
