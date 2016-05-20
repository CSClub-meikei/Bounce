using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
            Controls[0, 0] = new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_singleplay, 700, 350, 540, 120);
            Controls[0, 1] = new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_multiplay, 600, 500, 540, 120);
            titleback.addAnimator(2);
            titleback.animator[0].setDelay(1.5f);
            titleback.animator[1].setDelay(1.5f);
            titleback.animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] {0.2f, 0,0.8f, 1, 0, 1, 0 });
            titleback.animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.1f });
            title.addAnimator(2);
            title.animator[0].setDelay(1.5f);
            title.animator[1].setDelay(1.5f);
            //title.animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] { 0.3f, 1.5f, 1, 1, 0, 1, 0 });
            title.animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
            Controls[0, 0].addAnimator(2);
            Controls[0, 0].animator[0].setDelay(1.8f);
            Controls[0, 0].animator[1].setDelay(1.8f);
            Controls[0, 0].animator[0].FinishAnimation += new EventHandler((sender, e) => { selectedItem = new Point(0, 0); });
            Controls[0, 0].animator[0].start(GameObjectAnimator.ZOOMINOUT,new float []{0.1f,0,1,0,1,0,1});
            Controls[0, 0].animator[1].start(GameObjectAnimator.fadeInOut, new float[] {0,0.1f});
            Controls[0, 1].addAnimator(2);
            Controls[0, 1].animator[0].setDelay(2);
            Controls[0, 1].animator[1].setDelay(2);
            Controls[0, 1].animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] { 0.1f, 0, 1, 0, 1, 0, 1 });
            Controls[0, 1].animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.1f});
            Controls[0, 0].Enter += new EventHandler(this.Single);
            
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
        public void Single(object sender, EventArgs e)
        {
            //Controls[0, 0].addAnimator(2);
            Controls[0, 0].animator[0].setDelay(0.5f);
            Controls[0, 0].animator[1].setLimit(0.5f);
            Controls[0, 0].animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.02f, 0, 0.02f, 0, -1 });
            Controls[0, 0].animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] { 0.1f, 1, 0, 0, 1, 0, 1 });
            animator.setDelay(1f);
            animator.start(ScreenAnimator.SLIDE, new float[] { 1, -1280, 0, 0, -1, 5f, 5f });
            Screen ns = new SelectGameScreen(game, 1280, 0);
            
           
            game.screens[0].animator.FinishAnimation += new EventHandler((ss, ee) => {
                game.screens.Clear();
                //game.screens.Add(new worldScreen(game, "test.xml",false));
                game.screens.Add(new GameScreen(game));
            });
           game.screens[0].animator.setDelay(1f);
            game.screens[0].animator.start(GameObjectAnimator.fadeInOut, new float[] { 1,1 });
        }
    }
}
