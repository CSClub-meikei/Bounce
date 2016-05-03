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
    class ball:GraphicalGameObject
    {
       new worldScreen parent;
        const float Speed = 0.3f;
        public ball(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            parent = (worldScreen)screen;
            setLocation(x, y);
            setSize(width, height);
            velocityX = Speed;
            velocityY = Speed;
        }
        public override void update(float delta)
        {
            bool flag1 = false;
            bool flag2 = false;
            if (parent.frame.X+5>X)
            {
                X = parent.frame.X+5;
                velocityX = Speed;
                flag1 = true;
            }else if( parent.frame.X + parent.frame.Width-Width-5 < X)
            {
                X = parent.frame.X+ parent.frame.Width-Width-5;
                velocityX = -Speed;
                flag1 = true;
            }

            if (parent.frame.Y+5 > Y)
            {
                Y = parent.frame.Y+5;
                velocityY = Speed;
                flag1 = true;
            }
            else if (parent.frame.Y + parent.frame.Height - Height -5 < Y)
            {
               Y = parent.frame.Y + parent.frame.Height - Height-5;
                velocityY = -Speed;
                flag1 = true;
            }
            foreach (thorn b in parent.thorns) {
                if (overlapTester.overlapRectangles(new Rectangle((int)b.X, (int)b.Y, (int)b.Width, (int)b.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                    die();
                        
                        }
                foreach (block b in parent.blocks)
                switch(overlapTester.overlapRectanglesEX(new Rectangle((int)b.X, (int)b.Y, (int)b.Width, (int)b.Height),new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                {
                    case 1:
                        velocityX = -Speed;
                        parent.time.text += ",1";
                        // X = b.X - Width;
                       // System.Windows.Forms.MessageBox.Show("1");
                        flag2 = true;
                        break;
                    case 2:
                        velocityX = Speed;
                        // X = b.X+b.Width;
                        // System.Windows.Forms.MessageBox.Show("2");
                        parent.time.text += ",2";
                        flag2 = true;
                        break;
                    case 3:
                        velocityY = -Speed;
                        // Y = b.Y - Height;
                        parent.time.text += ",3";
                        // System.Windows.Forms.MessageBox.Show("3");
                       
                        flag2 = true;
                        break;
                    case 4:
                        velocityY = Speed;
                        //  Y = b.Y + b.Height;
                        //  System.Windows.Forms.MessageBox.Show("4");]
                        parent.time.text += ",4";
                        flag2 = true;
                        break;

                }
            if (flag1 && flag2) die();
            //Console.WriteLine("vx:" + velocityX.ToString());
            X += velocityX * delta;
            Y += velocityY * delta;


            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {

            base.Draw(batch, screenAlpha);
        }
        public void die()
        {
            game.screens.Clear();
            game.screens.Add(new worldScreen(game));
            game.screens.Add(new GameScreen(game));
        }
    }
}
