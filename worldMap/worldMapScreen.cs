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
        public levelinfoScreen levelinfoScreen;
        mapIcon labo,w1,w2,w3,w4,w5,w6,w7,w8;
        public bool isAnimate;
        public int selectedIconIndex;
        public bool enable = true;
        public  List<mapIcon> icons = new List<mapIcon>();
        


        public worldMapScreen(Game1 game, int index = 0,bool isClear=false, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            map = new GraphicalGameObject(game, this, Assets.graphics.worldMap.map,0,0,2688,2688);
            levelNamePlate = new GraphicalGameObject(game, this, Assets.graphics.worldMap.levelNamePlate, -20,-20, 500, 100);
            levelNamePlateLabel = new TextObject(game, this, Assets.graphics.ui.defultFont, "", Color.Black,20,20);
            levelNamePlateLabel.lockPlace = true;
            animator.Add(new ScreenAnimator(this, game));
            animator.Add(new ScreenAnimator(this, game));
            animator.Add(new ScreenAnimator(this, game));
            DebugConsole.write("mapworld :" + isClear.ToString());
            animator[0].FinishAnimation += new EventHandler(fa1);
            animator[1].FinishAnimation += new EventHandler(fa2);
            //animator[0].setLimit(1);
            //animator[1].setLimit(1);
            
            Y = -1400;
           setUIcell(4, 3);

            // Controls[0, 0] = w2; Controls[1, 0] = w3; Controls[2, 0] = w4; Controls[3, 0] = w5;
            //  Controls[0, 1] = w1; Controls[1, 1] = w8; Controls[2, 1] = w7; Controls[3, 1] = w6;
            //  Controls[0, 2] = labo;

            DebugConsole.write(game.settingData.Cleared.ToString());
            selectedIconIndex = index;
            mapIconBuild(isClear);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Assets.bgm.story);
            //  selectedItem = new Point(0,2);

            game.assist(1, true);
            game.assist(2, true);
        }

        public void  mapIconBuild(bool isClear)
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
            w1.disctiprion = "さあ、冒険の始まりだ。\nそこまで難しくはないから焦らず行こう！";
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
            w3.disctiprion = "このダンジョンは水に浸かっているようだ。\n水の中ではボールの動きが遅くなるから注意だ";
            w3.path = "level/w3.bmd";
            w3.next = 1;
            w3.nextid = 4;
            w3.back = 2;
            w3.backid = 2;

            w4.num = "world4";
            w4.title = "ジャングル宮殿";
            w4.level = "レベル : ４  ☆☆☆☆";
            w4.disctiprion = "だんだん仕掛けが増えてきてコースも\n複雑になってきた。\nここではL字のフレームが活躍しそうだ";
            w4.path = "level/w4.bmd";
            w4.next = 1;
            w4.nextid = 5;
            w4.back = 2;
            w4.backid = 3;

            w5.num = "world5";
            w5.title = "からくり島";
            w5.level = "レベル : ３  ☆☆☆";
            w5.disctiprion = "ココのコースでは初めてボールをフレームの外に\nだすぞ。仕掛けの力を借りてゴールを目指せ！";
            w5.path = "level/w5.bmd";
            w5.next = 3;
            w5.nextid = 6;
            w5.back = 4;
            w5.backid = 4;

            w6.num = "world6";
            w6.title = "波よせる海";
            w6.level = "レベル : ４  ☆☆☆☆";
            w6.disctiprion = "水が満ちたり引いたりする。\nタイミングを見極めよう。";
            w6.path = "level/w6.bmd";
            w6.next = 2;
            w6.nextid = 7;
            w6.back = 4;
            w6.backid = 5;

            w7.num = "world7";
            w7.title = "天まで届く山";
            w7.level = "レベル : ４  ☆☆☆☆";
            w7.disctiprion = "ブロックが複雑に動いている。\n挟まれないように気をつけるんだ。";
            w7.path = "level/w7.bmd";
            w7.next = 4;
            w7.nextid = 8;
            w7.back = 1;
            w7.backid = 6;

            w8.num = "world8";
            w8.title = "神秘の神殿";
            w8.level = "レベル : ５  ☆☆☆☆";
            w8.disctiprion = "最終コースだ。とても複雑な構造をしている。\nがんばってくれ！";
            w8.path = "level/w8.bmd";
           
            w8.back = 3;
            w8.backid = 7;


            mapIcon[] tmp = new mapIcon[] { w1, w2, w3, w4, w5, w6, w7, w8 };

            int i = 0;
            icons.Add(labo);
            for (i = 0; i < game.settingData.Cleared;i++) icons.Add(tmp[i]);

            if (isClear && game.settingData.Cleared==selectedIconIndex)
            {
                selectedIconIndex++;
                game.settingData.Cleared = selectedIconIndex;
                game.settingData.save();
                tmp[game.settingData.Cleared-1].isShow = false;
                icons.Add(tmp[game.settingData.Cleared-1]);
                icons[game.settingData.Cleared].isShow = false;
                DebugConsole.write(game.settingData.Cleared.ToString());
                animator[0].FinishAnimation += new EventHandler((sender, e) =>
                {
                   
                    
                    icons[game.settingData.Cleared].Show();
                    //System.Windows.Forms.MessageBox.Show(game.settingData.Cleared.ToString());

                });
               
                icons[selectedIconIndex].hover(null, null);
                DebugConsole.write("ha");
            }
            else
            {
                DebugConsole.write("sine");

               
               icons[selectedIconIndex].hover(null, null);
            }
           



          
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
                        if(icons.Count != selectedIconIndex + 1)
                        {
                            icons[selectedIconIndex].leave(null, null);
                            selectedIconIndex = icons[selectedIconIndex].nextid;
                        }

                      
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
                        if (icons.Count != selectedIconIndex + 1)
                        {
                            icons[selectedIconIndex].leave(null, null);
                            selectedIconIndex = icons[selectedIconIndex].nextid;
                        }
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
                        if (icons.Count != selectedIconIndex + 1)
                        {
                            icons[selectedIconIndex].leave(null, null);
                            selectedIconIndex = icons[selectedIconIndex].nextid;
                        }
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
                        if (icons.Count != selectedIconIndex + 1)
                        {
                            icons[selectedIconIndex].leave(null, null);
                            selectedIconIndex = icons[selectedIconIndex].nextid;
                        }
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
                if (Input.onKeyDown(Keys.Escape))
                {
                    game.clearScreen();
                    game.AddScreen(new BackScreen(game));
                    game.AddScreen(new UItestScreen(game));
                }
            }

            map.update(deltaTime);

            levelNamePlateLabel.text = icons[selectedIconIndex].title;
            levelNamePlateLabel.update(deltaTime);
            levelNamePlate.update(deltaTime);
            if (levelNamePlateLabel.text == null) levelNamePlateLabel.text = "";
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
