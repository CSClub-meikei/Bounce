using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bounce.story;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
namespace Bounce
{
    class levelinfoScreen:Screen
    {
        GraphicalGameObject back;
        Character ch;
        TextObject num, title,level, dis;
        worldMapScreen screen;
        public levelinfoScreen(Game1 game,worldMapScreen screen ,int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            back = new GraphicalGameObject(game, this, Assets.graphics.worldMap.levelinfo, 0, 0, 1280, 720);
            num = new TextObject(game, this, Assets.graphics.ui.font,screen.icons[screen.selectedIconIndex].num,Color.White,new Rectangle(0,83,1280,0));
            title = new TextObject(game, this, Assets.graphics.ui.defultFont, screen.icons[screen.selectedIconIndex].title, Color.White, new Rectangle(0, 180, 1280, 0));
            level = new TextObject(game, this, Assets.graphics.ui.defultFont, screen.icons[screen.selectedIconIndex].level, Color.White, new Rectangle(0, 280, 1280, 0));
            dis = new TextObject(game, this, Assets.graphics.ui.defultFont, screen.icons[screen.selectedIconIndex].disctiprion, Color.White, new Rectangle(0, 400, 1280, 0));
            
            setUIcell(1, 1);
            Controls[0, 0] = (new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_play, 370, 600, 540, 120));
            Controls[0, 0].Enter += play;
            
            this.screen = screen;
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            back.update(deltaTime);
            num.update(deltaTime);
            title.update(deltaTime);
            level.update(deltaTime);
            dis.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            
            back.Draw(batch, screenAlpha);
            num.Draw(batch, screenAlpha);
            title.Draw(batch, screenAlpha);
            level.Draw(batch, screenAlpha);
            dis.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
        public void play(object sender,EventArgs e)
        {
            Controls[0, 0].addAnimator(2);
            Controls[0, 0].animator[0].setDelay(0.5f);
            Controls[0, 0].animator[1].setLimit(0.5f);
            Controls[0, 0].animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.02f, 0, 0.02f, 0, -1 });
            Controls[0, 0].animator[0].start(GameObjectAnimator.ZOOMINOUT, new float[] { 0.1f, 1, 0, 0, 1, 0, 1 });

            animator.setDelay(1);
            animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 1 });

            screen.enable = true;

            screen.animator[2].setDelay(2);
            screen.animator[2].FinishAnimation += new EventHandler((sender2, e2) =>
              {
                  game.screens.Remove(screen);
              });
            screen.animator[2].start(ScreenAnimator.fadeInOut, new float[] { 1, 1 });


            GameScreen ns = new GameScreen(game, screen.icons[screen.selectedIconIndex].path);
            ns.animator.setDelay(2);
            ns.screenAlpha = 0;
            ns.animator.FinishAnimation += new EventHandler((sender2, e2) => { ns.animating = false; });
           ns.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.5f });
            game.screens.Add(ns);
           
        }
    }
}
