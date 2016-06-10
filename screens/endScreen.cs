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
    class endScreen:Screen
    {
        endContentScreen content;
        GraphicalGameObject back;
        public endScreen(Game1 game,int x = 0,int y=0):base(game,x,y)
        {
            content = new endContentScreen(game,0,900);
            back = new GraphicalGameObject(game,this, Assets.graphics.ui.back_title, 0, 0, 1280, 720);
            back.alpha = 0.2f;
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            back.update(deltaTime);
            content.update(deltaTime);
            content.Y -= 1;
            if (content.Y == -2100)
            {
                animator.FinishAnimation += new EventHandler((sender, e) =>
                  {
                      game.clearScreen();
                      game.AddScreen(new BackScreen(game));
                      game.AddScreen(new UItestScreen(game));

                  });
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 1 });
                
            }
            DebugConsole.write(content.Y.ToString());
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            back.Draw(batch,screenAlpha);
            content.Draw(batch);
        }
    }
}
