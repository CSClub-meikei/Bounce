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
    class Switch: LevelObject
    {
        public Switch(Game1 game, Screen screen,eventData ed,int rotate, float x, float y, float width, float height) : base(game, screen, ed, x, y, width, height)
        {
            this.Texture = getChipTexture(mapChip.SWITCH, rotate);

            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
        }

        

    }
}
