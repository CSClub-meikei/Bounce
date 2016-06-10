using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce
{
    class toast:Screen
    {
        string msg;
        float time;
        float tmpTime;
        TextObject text;
        GraphicalGameObject back;
        int pt;
        bool animating;
        public toast(Game1 game,string msg,float time, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            pt = 60;
           // X = 1280 / 2 - (pt * msg.Length / 2);
            Y = 600;
            this.time = time;
            text = new TextObject(game, this, Assets.graphics.ui.defultFont, msg, Color.White, 1280 / 2 - ((pt-11) * msg.Length / 2), pt / 2);
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.toast, 1280 / 2 - ((pt+2) * msg.Length / 2), 0, (pt+2)*msg.Length,pt*2);
            
            animator.start(ScreenAnimator.fadeInOut, new float[] {0, 0.5f });
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            text.update(deltaTime);
            back.update(deltaTime);
            tmpTime += deltaTime / 1000;
            if (tmpTime >= time && !animating)
            {
                animator.FinishAnimation += new EventHandler((sender, e) =>
                  {
                      game.removeScreen(this);
                  });
                animator = new ScreenAnimator(this, game);
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });
                animating = true;
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            back.Draw(batch, screenAlpha);
            text.Draw(batch, screenAlpha);
        }
    }
}
