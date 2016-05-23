using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce.story
{
    class Character:GraphicalGameObject
    {
        public int face;

        public bool active;



        public Character(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            
        }
        public override void update(float delta)
        {
            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            base.Draw(batch, screenAlpha);
        }
    }
}
