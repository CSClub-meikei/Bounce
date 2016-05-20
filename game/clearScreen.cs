using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce
{
    class clearScreen:Screen
    {
        GraphicalGameObject title;
        public clearScreen(Game1 game, GameScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            title = new GraphicalGameObject(game, this, Assets.graphics.game.clear, 320, 260, 640, 200);
            title.addAnimator(2);
            title.animator[0].setLimit(0.5f);
            title.animator[1].setLimit(0.5f);
            title.animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.5F, 0.5F, 0F, 0.4F, 0.0F, 1F });
            title.animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
            animator.setDelay(2f);
            animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            title.update(deltaTime);
           

        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            title.Draw(batch, screenAlpha);
            

        }
    }
}
