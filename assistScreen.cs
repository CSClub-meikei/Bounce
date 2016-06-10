using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce
{
    public class assistScreen:Screen
    {
        public GraphicalGameObject assistS, assistY;
        public assistScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            assistS = new GraphicalGameObject(game, this, Assets.graphics.ui.assist_S,1080,0,200,120);
            assistY = new GraphicalGameObject(game, this, Assets.graphics.ui.assist_Y, 1080, 120, 200, 120);
            assistS.addAnimator(1);
            assistY.addAnimator(1);
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            assistS.update(deltaTime);
            assistY.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            assistY.Draw(batch, screenAlpha);
            assistS.Draw(batch, screenAlpha);
        }
        public void setShow(int mode,bool show)
        {
            if (mode == 1)
            {

                if (show) assistS.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.5f });
                else assistS.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.5f });

            }
            else if (mode==2)
            {
                if (show) assistY.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.5f });
                else assistY.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.5f });
            }
        }
    }
}
