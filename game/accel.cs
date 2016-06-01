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
    class accel:LevelObject
    {
        int frame;
        float t;
        float animationSpeed=0.05f;

        Texture2D water;

        public accel(Game1 game, Screen screen, eventData ed, int rotate, float x, float y, float width, float height) : base(game, screen,ed, x, y, width, height)
        {

            this.rotate = rotate;

            this.Texture = getChipTexture(mapChip.ACCEL, rotate);
            if (rotate == 2)
            {
                Texture = Assets.getColorTexture(game, new Color(0, 145, 227));
                water = Assets.graphics.game.water[0];
                alpha = 0.7f;
            }
            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
            addAnimator(2);
        }
        const int size = 120;
        public override void update(float delta)
        {
            base.update(delta);
            t += delta / 1000;
            if(t>= animationSpeed)
            {
                frame++;
                if (rotate == 0)
                {
                    Texture = Assets.graphics.game.accel[frame];
                }
                else if(rotate==1)
                {
                    Texture = Assets.graphics.game.brake[frame];
                }else if(rotate==2)
                {
                    water = Assets.graphics.game.water[frame];
                }
               
                if (frame == 43 && rotate!=2)
                {
                    frame = 0;
                }else if(frame==30 && rotate == 2)
                {
                    frame = 0;
                }
                t = 0;
            }
            
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
            for (i = 0; i < Width / size; i++)
            {
                for (j = 0; j < Height / size; j++)
                {
                    if(rotate==2 && j== 0)
                    {
                        batch.Draw(water, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha);
                    }
                    else
                    {
                        batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha);
                    }
                  
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
            smokeDraw(batch, screenAlpha,300,true);
        }
    }
}
