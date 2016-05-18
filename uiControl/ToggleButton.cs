using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce.uiControl
{
    public class ToggleButton:uiObject
    {
        Texture2D def, tog;
        public bool value;

        public ToggleButton(Game1 game, Screen screen, Texture2D def, Texture2D tog, float x, float y, float width, float height, float fx = 0, float fy = 0, float fwidth = 0, float fheight = 0) : base(game, screen, def, x, y, width, height)
        {
            this.def = def;
            this.tog = tog;
            GotFocus += new EventHandler(this.onHover);
            LostFocus += new EventHandler(this.onLeave);
            Enter += new EventHandler(this.onClick);
            
        }
        public override void update(float delta)
        {
            base.update(delta);
            if (!Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && Input.OnMouseUp(Input.LeftButton))
            {
                value = false;
                Texture = def;
            }
        }
        public void onHover(object sender, EventArgs e)
        {
            alpha = 1;
        }
        public void onLeave(object sender, EventArgs e)
        {
      //      alpha = 0.8f;
        }
        public void onClick(object sender , EventArgs e)
        {
            value = true;
            Texture = tog;
        }
    }
}
