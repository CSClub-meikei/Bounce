using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bounce
{
    class pauseScreen:Screen
    {
        GraphicalGameObject back;
        GameScreen screen;
        public pauseScreen(Game1 game, GameScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            back = new GraphicalGameObject(game, this, Assets.getColorTexture(game, Color.Black), 0, 0, 1280, 720);
            back.alpha = 0.5f;
            setUIcell(2, 1);

            Controls[0, 0] = new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_return, 140, 600, 500, 120);
            Controls[1, 0] = new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_toTitle, 640, 600, 500, 120);
            Controls[0, 0].Enter += new EventHandler((sender, e) =>
              {
                  game.removeScreen(this);
                  screen.world.Status = worldScreen.RUNNING;
              });
            Controls[1, 0].Enter += new EventHandler((sender, e) =>
            {
                game.clearScreen();
                game.AddScreen(new BackScreen(game));
                game.AddScreen(new UItestScreen(game));
            });

            this.screen = screen;
            selectedItem = new Point(0, 0);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            if (Input.onKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                game.removeScreen(this);
                screen.world.Status = worldScreen.RUNNING;
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch, screenAlpha);
            base.Draw(batch);
            
        }
    }
}
