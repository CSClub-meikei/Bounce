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
    class AssetsLoadScreen:Screen
    {
        progressBar bar;
        TextObject label;

        public AssetsLoadScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            bar = new progressBar(game, this, new Rectangle(100, 600, 1080, 100),Color.White,Color.Gray,Color.White,Color.LightGray);
            bar.MaxValue = 100;
            bar.animationSpeed = 5f;
            bar.setValue(0);
            label = new TextObject(game, this, Assets.graphics.ui.font, "NOW LOADING", Color.White, 500, 400);
            setUIcell(1, 1);
            Assets.progress += new Assets.AssetsLoadEventHandler(this.progress);
            Assets.Loaded += new Assets.AssetsLoadEventHandler(this.finish);
            Task.Run(() => {
                Assets.Load(game);
            });
            
        }
        public override void update(float deltaTime)
        {
            bar.update(deltaTime);
            label.update(deltaTime);
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            bar.Draw(batch, screenAlpha);
            label.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
        public void progress(object sender,Assets.AssetsLoadEventArgs e)
        {
            label.text = e.message;
            bar.setValue(e.progress);
        }
        public void finish(object sender,Assets.AssetsLoadEventArgs e)
        {
            animator.FinishAnimation += new EventHandler((ss, ee) =>
            {
                game.assistScreen = new assistScreen(game);
                string[] cmds = System.Environment.GetCommandLineArgs();
                Screen ns;
                ns = new SplashScreen(game);
                if (cmds.Length >= 2)
                {
                    if (cmds[1] == "enableNetWork")
                    {
                        ns = new waitScreen(game);
                    }
                }

                
                // ns.animator.setDelay(2f);
                ns.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 1 });
               game.AddScreen(ns);
                game.removeScreen(this);
            });
            animator.setDelay(0.6f);
            animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f });
          
        }

    }
}
