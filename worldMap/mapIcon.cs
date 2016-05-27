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
    class mapIcon : uiObject
    {
        new worldMapScreen parent;

        bool isHover;
        public Point dif;
        public mapIcon(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            parent = (worldMapScreen)screen;

            GotFocus += new EventHandler(hover);
            LostFocus += new EventHandler(leave);
        }
        public override void update(float delta)
        {
            if (parent.isAnimate && isHover)
            {
                Input.setPotition(new Point((int)actX+25, (int)actY+25));
            }
            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            base.Draw(batch, screenAlpha);
        }
        public void hover(object sender, EventArgs e)
        {
            isHover = true;
            if (parent.isAnimate) return;
            parent.isAnimate = true;
           
            parent.animator[0].start(ScreenAnimator.SLIDE, new float[] { 1, (float)-X + 615+dif.X, (float)-Y + 335+dif.Y, 0, -1, 0.5f, 0.5f });
            parent.animator[1].start(ScreenAnimator.SLIDE, new float[] { 2, (float)-X + 615+dif.X, (float)-Y + 335+dif.Y, 0, -1, 0.5f, 0.5f });
        }
        public void leave(object sender, EventArgs e)
        {
            isHover = false;
        }
    }
}
