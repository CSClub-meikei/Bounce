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
    class frame:GraphicalGameObject
    {
        public frame(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen,Texture, x, y, width, height)
        {
            setLocation(x, y);
            setSize(width, height);
        }
        public override void update(float deltaTime)
        {
            if (Input.IsKeyDown(Keys.Right)) { X += (int)(0.3f * deltaTime);  }
            if (Input.IsKeyDown(Keys.Left)) { X -= (int)(0.3f * deltaTime); }
            if (Input.IsKeyDown(Keys.Up)) { Y -= (int)(0.3f * deltaTime);}
            if (Input.IsKeyDown(Keys.Down)) { Y += (int)(0.3f * deltaTime);}
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            base.Draw(batch, screenAlpha);
        }
    }
}
