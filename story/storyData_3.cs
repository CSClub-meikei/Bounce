using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bounce.story
{
    class storyData_3:storyData
    {
        public const int EXIT = 0;
        public const int CLEARSERIF = 1;

        public storyData_3(int c)
        {
            command = c;
        }
        public int command=0;

    }
}
