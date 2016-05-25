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
    class worldMapScreen:Screen
    {
        new public List<ScreenAnimator> animator = new List<ScreenAnimator>();

        GraphicalGameObject map;

        mapIcon labo,w1;
        public bool isAnimate;

        public worldMapScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            map = new GraphicalGameObject(game, this, Assets.graphics.worldMap.map,0,0,2048,2048);

            labo = new mapIcon(game, this, Assets.graphics.game.block, 1200, 600, 50, 50);
            w1 = new mapIcon(game, this, Assets.graphics.game.block, 700, 900, 50, 50);
            animator.Add(new ScreenAnimator(this, game));
            animator.Add(new ScreenAnimator(this, game));

            animator[0].FinishAnimation += new EventHandler(fa1);
            animator[1].FinishAnimation += new EventHandler(fa2);
            //animator[0].setLimit(1);
            //animator[1].setLimit(1);
            setUIcell(1, 1);


        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            try
            {
                foreach (ScreenAnimator a in animator)
                {
                    a.update(deltaTime);
                }
            }
            catch (Exception)
            {

                //throw;
            }
           
            map.update(deltaTime);
            labo.update(deltaTime);
            w1.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            map.Draw(batch, screenAlpha);
            labo.Draw(batch, screenAlpha);
            w1.Draw(batch, screenAlpha);
        }
        public void fa1(object sender,EventArgs e)
        {
           // System.Windows.Forms.MessageBox.Show("f1");
            if (!animator[1].isAnimate) isAnimate = false;
            animator[0] = new ScreenAnimator(this, game);
            animator[0].setLimit(1);
            animator[0].FinishAnimation += new EventHandler(fa1);
        }
        public void fa2(object sender,EventArgs e)
        {
           // System.Windows.Forms.MessageBox.Show("f2");
            if (!animator[0].isAnimate) isAnimate = false;
            animator[1] = new ScreenAnimator(this, game);
            animator[1].setLimit(1);
            animator[1].FinishAnimation += new EventHandler(fa2);

        }
    }
}
