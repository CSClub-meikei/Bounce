using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Bounce
{
    class WideButton:uiObject
    {
        GraphicalGameObject back, front;
        public Rectangle frontPosition;

        public WideButton(Game1 game, Screen screen, Texture2D back,Texture2D front, float x, float y, float width, float height,  float fx=0, float fy=0, float fwidth=0, float fheight=0) : base(game, screen, back, x, y, width, height)
        {
            if(fx==0 && fy==0 && fwidth==0 && fheight == 0)
            {
                fx = x;fy = y;fwidth = width;fheight = height;
            }
            this.back = new GraphicalGameObject(game, screen, back, x, y, width, height);
            this.back.alpha = 0f;
            this.back.addAnimator(1);
            this.front = new GraphicalGameObject(game, screen, front, fx, fy, fwidth, fheight);
            this.front.addAnimator(2);
            frontPosition = new Rectangle((int)fx, (int)fy, (int)fwidth, (int)fheight);
            GotFocus += new EventHandler(this.got);
            LostFocus += new EventHandler(this.lost);
            Enter+= new EventHandler(this.accepted);
        }

        public override void update(float delta)
        {
            base.update(delta);
            back.update(delta);
            front.update(delta);
            
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            back.Draw(batch, screenAlpha);
            front.Draw(batch, screenAlpha);
        }
        public void got(object sender,EventArgs e)
        {
            Assets.soundEffects.s.Play();
            back.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.5f });
            front.animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.5F, 0.5F, 0F, 0.4F, 0.0F, 1F });
            front.animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
            // System.Windows.Forms.MessageBox.Show("got");
        }
        public void lost(object sender, EventArgs e)
        {
            back.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.5f });
            front.animator[0].stop();
            front.animator[1].stop();
            //System.Windows.Forms.MessageBox.Show("lost");
        }
        public void accepted(object sender, EventArgs e)
        {
            Assets.soundEffects.d.Play();
        }
    }
}
