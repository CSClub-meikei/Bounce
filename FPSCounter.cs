using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bounce
{
    class FPSCounter
    {
       
        private List<double> tmp = new List<double>();
        public int bufferSize=30;
        private int fpsResult=0;
        private int count=0;
       
      //  System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        public float getDeltaTime(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.Milliseconds;
           
           // Console.WriteLine(n);
           // sw.Stop();
           // double res = sw.ElapsedMilliseconds ;
           // sw.Reset();
           // sw.Start();
            return delta;
        }
        public int fpsCounter(double delta)
        {
            tmp.Add(1/delta*1000);
           if (count == bufferSize)
            {
                int total=0;
                foreach (int i in tmp)
                {
                    total += i;
                }
                fpsResult = total / bufferSize;
                if (fpsResult < 0) fpsResult = 0;
                tmp.Clear();
                count = 0;
            }
            count++;
            return fpsResult;
        }
    }
}
