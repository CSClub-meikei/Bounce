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
    class mapChip:GraphicalGameObject
    {
        const int size = 40;
        public int fx, fy;
        public int rx, ry;
        public bool moving=false;
        public bool selected = false;
        public String ssize;
        new EditorScreen parent;
        public int mode = 0;


        public event EventHandler Onselect;
        public mapChip(Game1 game, Screen screen, Texture2D Texture,int mode, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            this.mode = mode;
            parent = (EditorScreen)screen;
        }
        public override void update(float delta)
        {

            if (Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && Input.OnMouseDown(Input.LeftButton))
            {
                fx = (int)(Input.getPosition().X - parent.X - actX);
                fy = (int)(Input.getPosition().Y - parent.Y - actY);
                moving = true;
                selected = true;
                if (Onselect != null) Onselect(this, EventArgs.Empty);
            }
            if(moving && Input.IsMouseDown(Input.LeftButton))
            {
                X = (int)((Input.getPosition().X-fx-parent.X*2)/40)*40;
                Y = (int)((Input.getPosition().Y-fy - parent.Y*2) / 40) * 40;
              //  if ((Input.getPosition().X < 80)) parent.X +=40;
              //  if ((Input.getPosition().X > 1200)) parent.X -= 40;
              //  if ((Input.getPosition().Y < 80)) parent.Y +=40;
              //  if ((Input.getPosition().X > 680)) parent.Y -=40;
            }
            if (Input.OnMouseUp(Input.LeftButton))
            {
                moving = false;
                
            }

          if(selected)num();

            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            int i = 0;
            int j = 0;
            for (i = 0; i < Width / size; i++)
            {
                for (j = 0; j < Height / size; j++)
                {
                    batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha);
                }

            }
            batch.End();
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            if (selected) batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)actX + (int)((Texture.Width / 2) * (Width / Texture.Width)), (int)actY + (int)((Texture.Height / 2) * (Height / Texture.Height)), (int)Width, (int)Height), color: Color.White * 0.5f * screenAlpha, rotation: angle, origin: origin);
            batch.End();
        }
        public void num()
        {
            if (Input.onKeyDown(Keys.Delete)) parent.Rchips.Add(this);
            if (Input.onKeyDown(Keys.Right)) Width += 40;
            if (Input.onKeyDown(Keys.Down)) Height += 40;
            if (Input.onKeyDown(Keys.Left) && Width!=40) Width -= 40;
            if (Input.onKeyDown(Keys.Up) && Height!=40) Height-= 40;
          //  Console.WriteLine("w:" + Width.ToString() + "h:" + Height.ToString());
        }
    }
}
