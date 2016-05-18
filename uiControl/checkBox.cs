using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce.uiControl
{
    class checkBox:uiObject
    {
        public bool value;
        GraphicalGameObject cm;

        public checkBox(Game1 game, Screen screen, float x, float y, float width, float height, float fx = 0, float fy = 0, float fwidth = 0, float fheight = 0) : base(game, screen, Assets.graphics.ui.back_textbox, x, y, width, height)
        {
            cm = new GraphicalGameObject(game, parent, Assets.graphics.ui.check, (int)X, (int)Y, (int)Width, (int)Height);
            cm.addAnimator(1);
            Enter += new EventHandler(this.onClick);
        }
        public void onClick(object sender,EventArgs e)
        {
            value = !value;
           
        }
        public override void update(float delta)
        {
            base.update(delta);
            cm.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            base.Draw(batch, screenAlpha);
            if(value)cm.Draw(batch,screenAlpha);

        }
    }
}
