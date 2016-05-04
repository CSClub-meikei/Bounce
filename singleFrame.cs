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
    class singleFrame:GraphicalGameObject
    {
        public float dfX, dfY;
        public singleFrame(Game1 game, Screen screen, Texture2D Texture,double sx,double sy, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            dfX = (float)(X - sx);
            dfY = (float)(Y - sy);
        }
        }
}
