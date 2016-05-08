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
    class moveScreenObject:GraphicalGameObject
    {
        bool onMoving = false;
        int fx, fy;
        int px, py;
        public moveScreenObject(Game1 game, Screen screen,Texture2D Texture ,float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {

        }

        public override void update(float delta)
        {

            if(Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && Input.OnMouseDown(Input.LeftButton))
            {
                onMoving = true;
                fx = (int)(Input.getPosition().X );
                fy = (int)(Input.getPosition().Y );
                px = parent.X;
                py = parent.Y;
            }
            if (onMoving && Input.IsMouseDown(Input.LeftButton))
            {
                parent.X = (int)(Input.getPosition().X - fx+px);
                parent.Y = (int)(Input.getPosition().Y - fy+py);
            }
            if (Input.OnMouseUp(Input.LeftButton))
            {
                onMoving = false;
            }
                base.update(delta);
        }
    }
}
