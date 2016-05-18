using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce
{
    class BackScreen:Screen
    {
        GraphicalGameObject back;
        public BackScreen(Game1 game) : base(game)
        {
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_title, 0, 0, 1920, 1920);
            back.alpha = 0.3f;
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            back.update(deltaTime);
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
    }
}
