using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce
{
    class SelectGameScreen:Screen
    {
        GraphicalGameObject title, titleback;


        public SelectGameScreen(Game1 game, int sx = 0, int sy = 0) : base(game,sx,sy)
        {
            title = new GraphicalGameObject(game, this, Assets.graphics.ui.label_gameselect,100,50,900,200);
            titleback = new GraphicalGameObject(game, this, Assets.graphics.ui.label_back, 200, 50, 900, 200);
            setUIcell(1, 2);
            Controls[0, 0] = new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_singleplay, 600, 400, 540, 120);
            Controls[0, 1] = new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_multiplay, 500, 550, 540, 120);
            titleback.addAnimator(2);
            titleback.animator[0].setDelay(1f);
            titleback.animator[1].setDelay(1f);
            titleback.animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] {0.3f, 0,0.8f, 1, 0, 1, 0 });
            titleback.animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
            title.addAnimator(2);
            title.animator[0].setDelay(1.5f);
            title.animator[1].setDelay(1.5f);
            //title.animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] { 0.3f, 1.5f, 1, 1, 0, 1, 0 });
            title.animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            title.update(deltaTime);
            titleback.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            title.Draw(batch, screenAlpha);
            titleback.Draw(batch, screenAlpha);

        }
    }
}
