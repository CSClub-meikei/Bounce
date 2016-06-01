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
    class savePoint:LevelObject
    {
        public bool _saved;
        public bool saved
        {
            get { return _saved; }
            set {
                if(_saved != value)
                {
                    _saved = value;

                    if (saved)
                    {
                        Texture = Assets.graphics.game.savePoint[1];
                        animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.8F, 1, 0F, 0.4F, 0.0F, 1F });
                        animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
                    }
                    else Texture = Assets.graphics.game.savePoint[0];

                }
                
               
            }
        }


        public savePoint(Game1 game, Screen screen, eventData ed, int rotate, float x, float y, float width, float height) : base(game, screen, ed, x, y, width, height)
        {

            this.Texture = getChipTexture(mapChip.SAVEPOINT, rotate);

            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
            addAnimator(2);
            animator[0].setLimit(1);
            animator[1].setLimit(1);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            if (!enable) return;

            batch.Begin(transformMatrix: game.GetScaleMatrix());

            batch.Draw(Texture, destinationRectangle: new Rectangle((int)actX + (int)((Texture.Width / 2) * (Width / Texture.Width)), (int)actY + (int)((Texture.Height / 2) * (Height / Texture.Height)), (int)Width + 20, (int)Height + 20), color: Color.White * alpha * screenAlpha, rotation: angle, origin: origin);

            batch.End();

            batch.Begin(transformMatrix: game.GetScaleMatrix(), blendState: BlendState.Additive);
          
                foreach (GameObjectAnimator a in animator)
                {
                    a.Draw(batch, screenAlpha);
                }
            
            batch.End();
            smokeDraw(batch, screenAlpha, 60, false);
        }
    }
}
