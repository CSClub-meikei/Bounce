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
    class FrameChangeScreen:Screen
    {

         worldScreen parent;
        GraphicalGameObject gt, gb;
        TextObject label;

        int oldShape;

        public FrameChangeScreen(Game1 game, worldScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            parent = screen;

            gt = new GraphicalGameObject(game, this, Assets.graphics.ui.graTop, 0, -720, 1280, 720);
            gb = new GraphicalGameObject(game, this, Assets.graphics.ui.graBottom, 0, 720, 1280, 720);
            label = new TextObject(game, this, Assets.graphics.ui.defultFont, "", Color.Yellow, new Rectangle(0, 600, 1280, 0));
            gt.addAnimator(2);
            gb.addAnimator(2);
            gt.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
            gt.animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2,0,-360,-1,-1,2,2 });
            gb.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
            gb.animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, 0, 360, -1, -1,2, 2 });
            label.addAnimator(3);
            label.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
            label.animator[1].FinishAnimation += new EventHandler((sender, e) =>
              {
                  label.text = parent.frame.getFrameName(parent.frameShape);
                  label.animator[2].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.1f });
              });

            setUIcell(1, 1);
            label.text = parent.frame.getFrameName(parent.frameShape);
        }
        public override void update(float deltaTime)
        {

            if (oldShape != parent.frameShape)
            {
                label.animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.1f });
            }
        
            oldShape = parent.frameShape;

           
            gt.update(deltaTime);
            gb.update(deltaTime);
            label.update(deltaTime);
          


            base.update(deltaTime);
        }
        
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            gt.Draw(batch, screenAlpha);
            gb.Draw(batch, screenAlpha);
            label.Draw(batch, screenAlpha);
        }
        public void close()
        {
            gt.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.2f });
            gt.animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, 0, -720, -1, -1, 2, 2 });
            gb.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.2f });
            gb.animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, 0, 720, -1, -1, 2, 2 });
            label.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.2f });
        }
    }
}
