using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce
{
    class readyScreen:Screen
    {
        GraphicalGameObject title, bar1, bar2;
        public readyScreen(Game1 game,GameScreen screen ,int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            title = new GraphicalGameObject(game, this, Assets.graphics.game.ready,426,310,426,100);
            bar1 = new GraphicalGameObject(game, this, Assets.graphics.game.readyBar, 0, 350, 426, 20);
            bar2 = new GraphicalGameObject(game, this, Assets.graphics.game.readyBar, 852, 350, 426, 20);
            title.addAnimator(3);
            title.animator[2].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
            title.animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] { 0.2f, 0, 1, 0, 1, 0, 1 });
            title.animator[1].setDelay(0.5f);
            title.animator[1].setLimit(1);
            title.animator[1].FinishAnimation += new EventHandler((sender, e) =>
              {
                  
                  title.animator[0].setLimit(0.3f);
                 title.animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] { 0.2f, 1, 0, 0, 1,0, 1 });
                  title.animator[2].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.2f });
                  animator.FinishAnimation += new EventHandler((sender3, e3) => { screen.world.Status = worldScreen.RUNNING; screen.world.flags[0] = true; });
                  animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });
              });

            title.animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.1f, 0.1f, 0.1f, 0.1f, -1 });
            setUIcell(1, 1);
            
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            title.update(deltaTime);
            bar1.update(deltaTime);
            bar2.update(deltaTime);

        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            title.Draw(batch, screenAlpha);
            bar1.Draw(batch, screenAlpha);
            bar2.Draw(batch, screenAlpha);

        }
    }
}
