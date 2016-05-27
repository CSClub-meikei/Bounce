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

        mapIcon labo,w1,w2,w3,w4,w5,w6,w7,w8;
        public bool isAnimate;

        public worldMapScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            map = new GraphicalGameObject(game, this, Assets.graphics.worldMap.map,0,0,2688,2688);

            labo = new mapIcon(game, this, Assets.graphics.worldMap.laboIcon, 600, 2000, 50, 50);
            labo.dif = new Point(-200, 200);
            w1 = new mapIcon(game, this, Assets.graphics.game.block, 730, 1530, 50, 50);
            w2 = new mapIcon(game, this, Assets.graphics.game.block, 900, 1100, 50, 50);
            w3 = new mapIcon(game, this, Assets.graphics.game.block, 1100, 1100, 50, 50);
            w4 = new mapIcon(game, this, Assets.graphics.game.block, 1570, 950, 50, 50);
            w5 = new mapIcon(game, this, Assets.graphics.game.block, 2188, 1540, 50, 50);

            w5.dif = new Point(200, 0);
            w6 = new mapIcon(game, this, Assets.graphics.game.block, 2188, 1860, 50, 50);
            w6.dif = new Point(200, 0);
            w7 = new mapIcon(game, this, Assets.graphics.game.block, 1500, 2000, 50, 50);
            w8 = new mapIcon(game, this, Assets.graphics.game.block, 1550, 1340, 50, 50);

            animator.Add(new ScreenAnimator(this, game));
            animator.Add(new ScreenAnimator(this, game));

            animator[0].FinishAnimation += new EventHandler(fa1);
            animator[1].FinishAnimation += new EventHandler(fa2);
            //animator[0].setLimit(1);
            //animator[1].setLimit(1);
            
            Y = -1400;
            setUIcell(4, 3);

            Controls[0, 0] = w2; Controls[1, 0] = w3; Controls[2, 0] = w4; Controls[3, 0] = w5;
            Controls[0, 1] = w1; Controls[1, 1] = w8; Controls[2, 1] = w7; Controls[3, 1] = w6;
            Controls[0, 2] = labo;




            selectedItem = new Point(0,2);
        }
        public override void update(float deltaTime)
        {
            if(!animator[0].isAnimate && !animator[1].isAnimate)
            {
                if (Input.onKeyDown(Keys.Up)) selectedItem = new Point(selectedItem.X, selectedItem.Y - 1);
                if (Input.onKeyDown(Keys.Down)) selectedItem = new Point(selectedItem.X, selectedItem.Y + 1);
                if (Input.onKeyDown(Keys.Right)) selectedItem = new Point(selectedItem.X + 1, selectedItem.Y);
                if (Input.onKeyDown(Keys.Left)) selectedItem = new Point(selectedItem.X - 1, selectedItem.Y);
            }
            

            foreach (uiObject o in Controls) if (o != null) o.update(deltaTime);
           
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
           // labo.update(deltaTime);
          //  w1.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
           
            map.Draw(batch, screenAlpha);
            base.Draw(batch);
            //  labo.Draw(batch, screenAlpha);
            // w1.Draw(batch, screenAlpha);
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
