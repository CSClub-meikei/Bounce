using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Bounce
{
    class worldMapScreen:Screen
    {
        new public List<ScreenAnimator> animator = new List<ScreenAnimator>();

        GraphicalGameObject map,levelNamePlate;
        TextObject levelNamePlateLabel;
        levelinfoScreen levelinfoScreen;
        mapIcon labo,w1,w2,w3,w4,w5,w6,w7,w8;
        public bool isAnimate;
        public int selectedIconIndex;
        public bool enable = true;
        public  List<mapIcon> icons = new List<mapIcon>();

        public worldMapScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            map = new GraphicalGameObject(game, this, Assets.graphics.worldMap.map,0,0,2688,2688);
            levelNamePlate = new GraphicalGameObject(game, this, Assets.graphics.worldMap.levelNamePlate, -20,-20, 400, 100);
            levelNamePlateLabel = new TextObject(game, this, Assets.graphics.ui.defultFont, "", Color.Black,20,20);
            levelNamePlateLabel.lockPlace = true;
            animator.Add(new ScreenAnimator(this, game));
            animator.Add(new ScreenAnimator(this, game));
            animator.Add(new ScreenAnimator(this, game));

            animator[0].FinishAnimation += new EventHandler(fa1);
            animator[1].FinishAnimation += new EventHandler(fa2);
            //animator[0].setLimit(1);
            //animator[1].setLimit(1);
            
            Y = -1400;
           setUIcell(4, 3);

            // Controls[0, 0] = w2; Controls[1, 0] = w3; Controls[2, 0] = w4; Controls[3, 0] = w5;
            //  Controls[0, 1] = w1; Controls[1, 1] = w8; Controls[2, 1] = w7; Controls[3, 1] = w6;
            //  Controls[0, 2] = labo;



            mapIconBuild();

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Assets.bgm.story);
            //  selectedItem = new Point(0,2);
        }

        public void  mapIconBuild()
        {
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


            labo.num = "Tutorial";
            labo.title = "チュートリアル";
            labo.level = "レベル : 1  ☆";
            labo.disctiprion = "冒険に行く前に練習してみよう。";
            labo.path = "level/Tutorial.bmd";
            labo.next = 4;
            labo.nextid = 1;


            w1.num = "world1";
            w1.title = "はじまりの草原";
            w1.level = "レベル : 1  ☆";
            w1.disctiprion = "さあ、冒険の始まりだ。\nそこまで難しくはないから\n焦らず行こう！";
            w1.path = "level/w1.bmd";
            w1.next = 4;
            w1.nextid = 2;
            w1.back = 3;
            w1.backid = 0;

            w2.num = "world2";
            w2.title = "湖畔のダンジョン";
            w2.level = "レベル : ２  ☆☆";
            w2.disctiprion = "湖畔にあるダンジョンだ。\n動く仕掛けが登場するぞ！慎重にいこう。";
            w2.path = "level/w2.bmd";
            w2.next = 1;
            w2.nextid = 3;
            w2.back = 3;
            w2.backid = 1;

            w3.num = "world3";
            w3.title = "湖ダンジョン";
            w3.level = "レベル : ３  ☆☆☆";
            w3.disctiprion = "このダンジョンは一部水に浸かっているようだ。\n水の中ではボールの動きが遅くなるから注意だ";
            w3.path = "level/w3.bmd";
            w3.next = 1;
            w3.nextid = 4;
            w3.back = 2;
            w3.backid = 2;

            icons.AddRange(new mapIcon[] { labo, w1, w2, w3, w4, w5, w6, w7, w8 });

            icons[selectedIconIndex].hover(null, null);
        }





        public override void update(float deltaTime)
        {
            if (levelinfoScreen != null) levelinfoScreen.update(deltaTime);
            if (!enable) return;
            if(!animator[0].isAnimate && !animator[1].isAnimate)
            {
                if (Input.onKeyDown(Keys.Up))
                {
                    if (icons[selectedIconIndex].next == 4)
                    {
                        icons[selectedIconIndex].leave(null, null);
                        selectedIconIndex = icons[selectedIconIndex].nextid;
                    }
                    else if (icons[selectedIconIndex].back == 4)
                    {
                        icons[selectedIconIndex].leave(null, null);
                        selectedIconIndex = icons[selectedIconIndex].backid;
                    }
                   

                    icons[selectedIconIndex].hover(null,null);
                }
                if (Input.onKeyDown(Keys.Down))
                {
                    if (icons[selectedIconIndex].next == 3)
                    {
                        icons[selectedIconIndex].leave(null, null);
                        selectedIconIndex = icons[selectedIconIndex].nextid;
                    }
                    else if (icons[selectedIconIndex].back == 3)
                    {
                        icons[selectedIconIndex].leave(null, null);
                        selectedIconIndex = icons[selectedIconIndex].backid;
                    }


                    icons[selectedIconIndex].hover(null, null);
                }
                if (Input.onKeyDown(Keys.Right))
                {
                    if (icons[selectedIconIndex].next == 1)
                    {
                        icons[selectedIconIndex].leave(null, null);
                        selectedIconIndex = icons[selectedIconIndex].nextid;
                    }
                    else if (icons[selectedIconIndex].back == 1)
                    {
                        icons[selectedIconIndex].leave(null, null);
                        selectedIconIndex = icons[selectedIconIndex].backid;
                    }


                    icons[selectedIconIndex].hover(null, null);
                }
                if (Input.onKeyDown(Keys.Left))
                {
                    if (icons[selectedIconIndex].next == 2)
                    {
                        icons[selectedIconIndex].leave(null, null);
                        selectedIconIndex = icons[selectedIconIndex].nextid;
                    }
                    else if (icons[selectedIconIndex].back == 2)
                    {
                        icons[selectedIconIndex].leave(null, null);
                        selectedIconIndex = icons[selectedIconIndex].backid;
                    }


                    icons[selectedIconIndex].hover(null, null);
                }
                if (Input.onKeyDown(Keys.Space))
                {
                    levelinfoScreen = new levelinfoScreen(game, this);
                    levelinfoScreen.animator.FinishAnimation += new EventHandler((sender, e) => { levelinfoScreen.selectedItem = new Point(0, 0); });
                    levelinfoScreen.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.2f });
                    enable = false;
                }

            }


            foreach (mapIcon icon in icons) icon.update(deltaTime);
           
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

            levelNamePlateLabel.text = icons[selectedIconIndex].title;
            levelNamePlateLabel.update(deltaTime);
            levelNamePlate.update(deltaTime);
           // labo.update(deltaTime);
          //  w1.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            map.Draw(batch, screenAlpha);
            levelNamePlate.actX = -20;
            levelNamePlate.actY =-20;
            levelNamePlate.Draw(batch, screenAlpha);
            levelNamePlateLabel.Draw(batch, screenAlpha);
            foreach (mapIcon icon in icons) icon.Draw(batch,screenAlpha);
            if (levelinfoScreen != null) levelinfoScreen.Draw(batch);
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
