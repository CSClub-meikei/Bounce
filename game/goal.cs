using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Bounce.game
{
    class goal : LevelObject
    {
        public goal(Game1 game, Screen screen, eventData ed, int rotate, float x, float y, float width, float height) : base(game, screen, ed, x, y, width, height)
        {

            this.Texture = getChipTexture(mapChip.GOAL, rotate);

            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            if (!enable) return;

            batch.Begin(transformMatrix: game.GetScaleMatrix());

            batch.Draw(Texture, destinationRectangle: new Rectangle((int)actX-20 + (int)((Texture.Width / 2) * (Width / Texture.Width)), (int)actY-20 + (int)((Texture.Height / 2) * (Height / Texture.Height)), (int)Width+20, (int)Height+20), color: Color.White * alpha * screenAlpha, rotation: angle, origin: origin);

            batch.End();



        }
    }
}
