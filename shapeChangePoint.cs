using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce
{
    class shapeChangePoint:LevelObject
    {
        public shapeChangePoint(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            addAnimator(3);
            animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.5F, 0.5F, 0F, 0.4F, 0.0F, 1F });
            animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
            flagChanged += new EventHandler(this.FlagEvent);
        }
        public void FlagEvent(object sender, EventArgs e)
        {
            if (flagType == 2)
            {

                animator[2].start(GameObjectAnimator.SLIDE, new float[] { 0, moveLocation.X, moveLocation.Y, 1, 0 });

                DebugConsole.write("イベント作動！！！！！！！");


            }
        }
    }
}
