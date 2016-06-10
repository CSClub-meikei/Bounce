using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bounce
{
    class Slider:uiObject
    {

        //public float value;
        public float MaxValue=10;
        public float step;
        float CircleX=0;
        float lastCircleX;
        Rectangle area;
        Point firstMousePosition;
        bool move = false;
        public bool toInt;

        public event EventHandler change;
        public Slider(Game1 game, Screen screen, Point point, float x, float y, float width,float height) : base(game, screen, Assets.graphics.ui.sliderBack,x,y,width,height)
        {
            setLocation(point.X, point.Y);
            setSize(300, 15);
            area = new Rectangle((int)(actX + CircleX - 12), (int)(actY - 45), 100, 100);

        }
        public override void update(float delta)
        {
           
            if(Input.IsHover(area) &&Input.OnMouseDown(Input.LeftButton))
            {
                lastCircleX = CircleX;
                firstMousePosition = Input.getPosition();
                move = true;
            }
            if (move && Input.IsMouseDown(Input.LeftButton))
            {
                CircleX = lastCircleX + (Input.getPosition().X - firstMousePosition.X );
                if (change != null) change(this, EventArgs.Empty);
            }
            if (Input.OnMouseUp(Input.LeftButton)){
                move = false;
            }

            if (CircleX >= Width) CircleX = (float)Width ;
            if (CircleX <= 0) CircleX = 0;
            area = new Rectangle((int)(actX + CircleX - 63), (int)(actY - 40), 100, 100);


            if(Selected && Input.onKeyDown(Keys.Right))
            {
                if(getValue() + step<MaxValue)
                setValue(getValue() + step);
            }
            if (Selected && Input.onKeyDown(Keys.Left))
            {
                if (getValue() - step > 0)
                    setValue(getValue() - step);
            }
            if (Selected) alpha = 1f;
            else alpha = 0.2f;
            base.update(delta);

        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            
            base.Draw(batch, screenAlpha);
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            
            batch.Draw(Assets.graphics.ui.sliderCircle, area,Color.White);

            batch.End();
        }
        public float getValue()
        {
           return (float)((CircleX/Width)*MaxValue);
        }
        public void setValue(float value)
        {
            CircleX   = (float)(value/MaxValue*Width);
            if (change != null) change(this, EventArgs.Empty);
        }
    }
}
