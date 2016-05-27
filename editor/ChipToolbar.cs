using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce
{
    class ChipToolbar : Screen
    {
        GraphicalGameObject back;

        public ChipToolbar(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            setUIcell(9, 1);
            int i = 0;

            for (i = 0; i <=8 ;i++)
                Controls[i, 0] = new ChipToolBarChip(game, this, i + 1, 395 + i * 70, 0, 60, 60);
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_ChipToolbar, 360, 0, 600, 100);

        }
        public override void update(float deltaTime)
        {
            back.update(deltaTime);
            base.update(deltaTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch,screenAlpha);
            base.Draw(batch);
        }
    }
}
