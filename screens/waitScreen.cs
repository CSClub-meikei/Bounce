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
    class waitScreen:Screen
    {
        GraphicalGameObject back;
        public waitScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.waitPlay, 0, 0, 1280, 720);
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            back.update(deltaTime);
            if (Input.onKeyDown(Keys.Space))
            {
                animator.FinishAnimation += new EventHandler((sender, e) => { game.screens.Remove(this); });

                animator.start(ScreenAnimator.SLIDE, new float[] { 2, 0, -720, -1, -1,3,3 });
                game.screens.Insert(0, new LoginScreen(game));

            }
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            back.Draw(batch, screenAlpha);

        }
    }
}
