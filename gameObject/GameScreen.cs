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
    class GameScreen:Screen
    {

        TextObject time;

        public GameScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            time = new TextObject(game, this, Assets.graphics.ui.font, "time: 0", Color.White,0,0);
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {

            time.update(deltaTime);
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            time.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
    }
}
