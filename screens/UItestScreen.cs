using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Bounce.socket;

namespace Bounce
{
    class UItestScreen:Screen
    {
        GraphicalGameObject title,sq1,sq2,back;
        TextObject userNameLabel;
        float sqangle;
        public bool enable = true;


        public UItestScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            title = new GraphicalGameObject(game,this,Assets.graphics.ui.titlelogo,200,25,880,350);
            sq1 = new GraphicalGameObject(game, this, Assets.graphics.ui.titleSquare,200,50,100,100);
            sq2 = new GraphicalGameObject(game, this, Assets.graphics.ui.titleSquare,1000,250,100,100);
            userNameLabel = new TextObject(game, this, Assets.graphics.ui.defultFont, "", Color.White, 0, 0);
            if (userData.userName != null)
            {
                userNameLabel.text = "ようこそ " + userData.userName + "さん";
            }
            //back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_title, 0, 0, 1280, 1280);
            //back.alpha = 0.3f;
            setUIcell(1,3);
            Controls[0,0]=(new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_play, 370, 380, 540, 120));
            Controls[0, 1] = (new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_settings, 370, 530, 540, 120));
            Controls[0, 2] = (new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_exit, 800, 650, 270, 70));
            Controls[0, 0].Enter += new EventHandler(this.ClickPlay);
            Controls[0, 1].Enter += new EventHandler(this.ClickSetting);
            Controls[0, 2].Enter += new EventHandler((sender, e) =>
              {
                  if (Client.tcp != null) { Client.tcp.disConnect(); Client.tcp = null; }
                  game.Exit();
              });
            selectedItem = new Point(0,0);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Assets.bgm.bgm_title);
            game.assist(1, true);
            game.assist(2, true);
        }
        public override void update(float deltaTime)
        {
            if (!enable) return;
            base.update(deltaTime);

            title.update(deltaTime);
            sqangle += 1;
            sq1.setAngle(sqangle);
            sq2.setAngle(-sqangle);
            sq1.update(deltaTime);
            sq2.update(deltaTime);
            userNameLabel.update(deltaTime);
            //back.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {

           // back.Draw(batch, screenAlpha);
            title.Draw(batch, screenAlpha);
            sq1.Draw(batch, screenAlpha);
            sq2.Draw(batch, screenAlpha);
            userNameLabel.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
        public void ClickPlay(object sender,EventArgs e)
        {
            Controls[0,0].addAnimator(2);
            Controls[0, 0].animator[0].setDelay(0.5f);
            Controls[0, 0].animator[1].setLimit(0.5f);
            Controls[0, 0].animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.02f, 0, 0.02f, 0, -1 });
           Controls[0, 0].animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] { 0.1f, 1,0,0,1,0,1 });
            animator.setDelay(1f);
            animator.start(ScreenAnimator.SLIDE, new float[] { 1, -1280, 0, 0, -1, 5f, 5f });
            Screen ns = new SelectGameScreen(game, 1280, 0);
            ns.animator.setDelay(1f);
            ns.animator.start(GameObjectAnimator.SLIDE, new float[] { 1, 0, 0, 0, -1, 5f, 5f });
            ns.animator.FinishAnimation += new EventHandler((se, ee) => { game.removeScreen(this); });
           game.AddScreen(ns);
            game.screens[0].animator.setDelay(1f);
            game.screens[0].animator.start(GameObjectAnimator.SLIDE, new float[] { 1, -100, 0, 0, 0,1f, 1f });
        }

        public void ClickSetting(object sender,EventArgs e)
        {
            screenAlpha = 0.2f;
            game.AddScreen(new SettingScreen(game,this));
            enable = false;
        }
    }
}
