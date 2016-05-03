using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Box2DX.Collision;

namespace Bounce
{
    public static class overlapTester
    {


        public static void test()
        {
            Box2DX.Collision.Collision.TestOverlap(new AABB(), new AABB());
        }

        //               ↓
        //1 ->□ 2 □<- 3□ 4□
        //                   ↑
        //r2をぶつける側r1はぶつかられる法
        public static bool overlapRectangles(Rectangle r1, Rectangle r2)
        {
            if (r1.X < r2.X + r2.Width && r1.X + r1.Width > r2.X && r1.Y + r1.Height > r2.Y && r1.Y < r2.Y + r2.Height) return true;
            return false;
        }
        public static int overlapRectanglesEX(Rectangle r1,Rectangle r2)
        {
            if (r1.X < r2.X + r2.Width && r1.X + r1.Width > r2.X && r1.Y + r1.Height > r2.Y && r1.Y < r2.Y + r2.Height) {
                int a1 = r2.X + r2.Width - r1.X;
                int a2 = r1.X + r1.Width - r2.X;
                int a3 = r2.Y + r2.Height - r1.Y;
                int a4 = r1.Y + r1.Height - r2.Y;

                int flag = 0;
                if (a1 < a2) { flag = 1; }
                else { flag = 2; }

                Console.WriteLine("a1:" + a1 + "a2:" + a2 + "a3:" + a3 + "a4:" + a4);
                if (a3 < a4)
                {
                    if (flag == 1 && a1 < a3) { return 1; }
                    else if (flag == 2 && a2 < a3) { return 2; }
                    else { return 3; }


                } else

                    if (flag == 2 && a2 < a4) { return 2; }
                else if (flag == 1 && a1 < a4) { return 1; }
                else { return 4; }



           



       }
            else { return 0; }
       

        }
    }
}
