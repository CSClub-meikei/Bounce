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
    class TextObject:GraphicalGameObject
    {
        public SpriteFont font;
        public String text;
        public Color color;

        public  bool centerX,centerY;
        public bool lockPlace;

        public TextObject(Game1 game, Screen screen,SpriteFont font, String text,Color color, float x, float y) :base(game,screen,null,x,y,0,0)
        {
            animator = new List<GameObjectAnimator>();
            this.setLocation(x, y);
            
            this.font = font;
            this.text = text;
            this.color = color;
        }
        public TextObject(Game1 game, Screen screen, SpriteFont font, String text, Color color, Rectangle rect) : base(game, screen, null, rect.X, rect.Y, 0, 0)
        {
            animator = new List<GameObjectAnimator>();
            this.setLocation(rect.X, rect.Y);
            this.setSize(rect.Width, rect.Height);
            this.font = font;
            this.text = text;
            this.color = color;
            if (Width != 0)centerX = true;
            if (Height != 0) centerY = true;
        }
        public override void update(float delta)
        {

            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            if (centerX)
            {
                Vector2 textsize = font.MeasureString(text);
                if (!lockPlace) actX = (Width / 2 - textsize.X / 2)+parent.X+X;
                else actX = (Width / 2 - textsize.X / 2) + X;
            }
            if (centerY)
            {
                Vector2 textsize = font.MeasureString(text);
                if (!lockPlace) actY = (Height / 2 - textsize.Y / 2) + parent.Y+Y;
                else actY = (Height / 2 - textsize.Y / 2) + Y;
            }
            
            
           
                if(!lockPlace || centerX || centerY) batch.DrawString(font, text, new Vector2((float)actX, (float)actY), color * screenAlpha * alpha);
                else batch.DrawString(font, text, new Vector2((float)X, (float)Y), color * screenAlpha * alpha);

            
          
            batch.End();

            
        }
    }
}
