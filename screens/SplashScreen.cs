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
    class SplashScreen:Screen
    {
        GraphicalGameObject sp1, sp2;
        public SplashScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            setUIcell(1, 1);
            sp1 = new GraphicalGameObject(game, this, Assets.graphics.ui.sp1, 0, 0, 1280, 720);
            sp2 = new GraphicalGameObject(game, this, Assets.graphics.ui.sp2, 0, 0, 1280, 720);
            sp1.alpha = 0;
            sp2.alpha = 0;
            sp1.addAnimator(2);
            sp2.addAnimator(2);
            sp1.animator[0].FinishAnimation += new EventHandler((sender, e) => {  sp1.animator[1].setDelay(3); sp1.animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.5f }); });
            sp1.animator[1].FinishAnimation += new EventHandler((sender, e) => { sp2.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.5f }); });
            sp2.animator[0].FinishAnimation+=new EventHandler((sender,e) => { sp2.animator[1].setDelay(5); sp2.animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.5f }); });
            sp2.animator[1].FinishAnimation += new EventHandler((sender, e) => {game.AddScreen(new BackScreen(game));game.AddScreen(new UItestScreen(game)); game.removeScreen(this); });
            sp1.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.5f });
            game.assist(1, false);
            game.assist(2, false);
        }
        public override void update(float deltaTime)
        {
            sp1.update(deltaTime);
            sp2.update(deltaTime);
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            sp1.Draw(batch, screenAlpha);
            sp2.Draw(batch, screenAlpha);


            base.Draw(batch);
        }
    }
}
