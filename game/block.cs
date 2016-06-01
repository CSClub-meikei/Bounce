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
    class block: LevelObject
    {
        public block(Game1 game, Screen screen, eventData ed, int rotate,float x, float y, float width, float height) : base(game, screen,ed, x, y, width, height)
        {
            

            this.Texture = getChipTexture(mapChip.BLOCK, rotate);
            
            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
            addAnimator(2);
        }
        const int size = 40;
        public override void update(float delta)
        {
            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            if (!enable) return;
            batch.Begin(transformMatrix: game.GetScaleMatrix(), blendState: BlendState.Additive);
            if (animatorLayor == 0)
            {
                foreach (GameObjectAnimator a in animator)
                {
                    a.Draw(batch, screenAlpha);
                }
            }
            batch.End();
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            int i = 0;
            int j = 0;
            for (i = 0; i < Width / size;i++)
            {
                for (j = 0; j < Height / size; j++)
                {
                    batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX + i * size) , (int)(actY + j * size) , (int)size, (int)size), color: Color.White * alpha * screenAlpha);
                }
                  
            }
          

            batch.End();
            batch.Begin(transformMatrix: game.GetScaleMatrix(), blendState: BlendState.Additive);
            if (animatorLayor == 1)
            {
                foreach (GameObjectAnimator a in animator)
                {
                    a.Draw(batch, screenAlpha);
                }
            }
            batch.End();
            smokeDraw(batch, screenAlpha,40,true);
        }
       
    }
}
