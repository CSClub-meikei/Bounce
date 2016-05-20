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
    class tileObject:GraphicalGameObject
    {
        int loopX,loopY;
        public tileObject(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height,int loopX, int loopY) : base(game, screen, Texture, x, y, width, height)
        {
            this.loopX = loopX;
            this.loopY = loopY;
        }
        public override void update(float delta)
        {
            base.update(delta);
            actX = actX * 0.2f;
            actY = actY * 0.2f;
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            int i = 0;
            int j = 0;
            for (i = 0; i < loopX; i++)
            {
                for (j = 0; j < loopY; j++)
                {
                    batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX+(i*Width)), (int)(actY+(j*Height)), (int)Width, (int)Height ), color: Color.White * alpha * screenAlpha);
                }
            }
           

            batch.End();
        }
    }
}
