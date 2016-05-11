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
        public shapeChangePoint(Game1 game, Screen screen, int rotate,eventData ed, float x, float y, float width, float height) : base(game, screen, ed, x, y, width, height)
        {
            this.Texture = getChipTexture(mapChip.SHPOINT, rotate);

            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
            addAnimator(3);
            animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.5F, 0.5F, 0F, 0.4F, 0.0F, 1F });
            animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
            flagChanged += new EventHandler(this.FlagEvent);
        }
        public void FlagEvent(object sender, EventArgs e)
        {
            if (eventData.type == 2)
            {

                animator[0].start(GameObjectAnimator.SLIDE, new float[] { 0, ((eventData_2)eventData).X, ((eventData_2)eventData).Y, ((eventData_2)eventData).speed, -1 });

                DebugConsole.write("イベント作動！！！！！！！");


            }
        }
    }
}
