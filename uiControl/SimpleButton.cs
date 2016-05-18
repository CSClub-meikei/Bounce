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
    class SimpleButton:uiObject
    {
        public SimpleButton(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height, float fx = 0, float fy = 0, float fwidth = 0, float fheight = 0) : base(game, screen, Texture, x, y, width, height)
        {
            GotFocus += new EventHandler(this.onHover);
            LostFocus += new EventHandler(this.onLeave);
            alpha = 0.8f;
        }
        public void onHover(object sender, EventArgs e)
        {
            alpha = 1;
        }
        public void onLeave(object sender, EventArgs e)
        {
            alpha = 0.8f;
        }
    }
}
