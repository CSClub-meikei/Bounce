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
        public bool breaking = false;
        const float AnimationSpeed = 0.2f;
        int frame = 0;
        float AnimationControll = 0;
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
            if (breaking)
            {
                spread(delta);
                return;
            }
            int flag1 = 0;
            int flag2 = 0;
            if (parent.frame.X+5>X)
            {
                X = parent.frame.X+5;
                velocityX = Speed;
                flag1 = 1;
            }else if( parent.frame.X + parent.frame.Width-Width-5 < X)
            {
                X = parent.frame.X+ parent.frame.Width-Width-5;
                velocityX = -Speed;
                flag1 = 2;
            }

            if (parent.frame.Y+5 > Y)
            {
                Y = parent.frame.Y+5;
                velocityY = Speed;
                flag1 = 3;
            }
            else if (parent.frame.Y + parent.frame.Height - Height -5 < Y)
            {
               Y = parent.frame.Y + parent.frame.Height - Height-5;
                velocityY = -Speed;
                flag1 =4;
            }
            foreach (thorn b in parent.thorns) {
                if (overlapTester.overlapRectangles(new Rectangle((int)b.X, (int)b.Y, (int)b.Width, (int)b.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                { die();return; }
                        
                        }
                foreach (block b in parent.blocks)
                switch(overlapTester.overlapRectanglesEX(new Rectangle((int)b.X, (int)b.Y, (int)b.Width, (int)b.Height),new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                {
                    case 1:
                        velocityX = -Speed;
                        parent.time.text += ",1";
                        // X = b.X - Width;
                       // System.Windows.Forms.MessageBox.Show("1");
                        flag2 = 1;
                        break;
                    case 2:
                        velocityX = Speed;
                        // X = b.X+b.Width;
                        // System.Windows.Forms.MessageBox.Show("2");
                        parent.time.text += ",2";
                        flag2 = 2;
                        break;
                    case 3:
                        velocityY = -Speed;
                        // Y = b.Y - Height;
                        parent.time.text += ",3";
                        // System.Windows.Forms.MessageBox.Show("3");
                       
                        flag2 = 3;
                        break;
                    case 4:
                        velocityY = Speed;
                        //  Y = b.Y + b.Height;
                        //  System.Windows.Forms.MessageBox.Show("4");]
                        parent.time.text += ",4";
                        flag2 = 4;
                        break;

                }
            if (flag1 != 0 && flag2 != 0 && flag1 == flag2) { die(); return; }
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
            this.Texture = Assets.graphics.game.ball_animation[0];
            breaking =true;
            setLocation(X - 210, Y - 180);
            setSize(266, 266);
        }
        public void spread(float deltaTime)
        {
            AnimationControll += deltaTime;
            if (AnimationControll >= AnimationSpeed)
            {
                frame++;
                AnimationControll = 0;
            }
            if (frame == 45)
            {
                died();breaking = false;
            }
            this.Texture = Assets.graphics.game.ball_animation[frame];
        }
      public void died()
        {
            game.screens.Clear();
            game.screens.Add(new worldScreen(game));
            game.screens.Add(new GameScreen(game));
        }
    }
}
