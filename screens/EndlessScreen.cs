using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce
{
    class EndlessScreen:Screen
    {
        WideButton back;
        GraphicalGameObject b;
        string[] files;
        public EndlessScreen(Game1 game,int x=0,int y=0):base(game,x,y)
        {
            files = System.IO.Directory.GetFiles(
              @"Endless\", "*.bmd", System.IO.SearchOption.AllDirectories);
            setUIcell(1, files.Length+1);

            int i = 0;
            for (i=0;i<files.Length;i++)
            {
                Controls[0, i] = new levelSelectList(game, this, System.IO.Path.GetFileNameWithoutExtension(files[i]), files[i], 50, 0 + i * 60, 1000, 50);
            }
            selectedItem = new Microsoft.Xna.Framework.Point(0, 0);
            Controls[0, files.Length] = new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_return, 720, 500, 500, 120);
            b = new GraphicalGameObject(game, this, Assets.graphics.ui.back_title, 0, 0, 1280, 720);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            if(Input.onKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && selectedItem.Y!=files.Length)
            {
                game.removeScreen(this);
                GameScreen ns = new GameScreen(game, -1, ((levelSelectList)Controls[selectedItem.X, selectedItem.Y]).path);
                ns.animator.setDelay(2);
                ns.screenAlpha = 0;
                ns.animator.FinishAnimation += new EventHandler((sender2, e2) => { ns.animating = false; });
                ns.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.5f });
                game.AddScreen(ns);
                b.update(deltaTime);
              
            }

        }
        public override void Draw(SpriteBatch batch)
        {
            b.Draw(batch, screenAlpha);
            base.Draw(batch);

        }
    }
}
