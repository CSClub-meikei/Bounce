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
        float Speed = 0.3f;
        public bool breaking = false;
        const float AnimationSpeed = 0.3f;
        int frame = 0;
        float AnimationControll = 0;

        public bool warping = false;
        public ball(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            
            parent = (worldScreen)screen;
            setLocation(x, y);
            setSize(width, height);
           // velocityX = Speed;
           // velocityY = Speed;
        }
        public override void update(float delta)
        {
            if (breaking)
            {
                spread(delta);
                return;
            }
            int flag1 = 0;
            //int flag2 = 0;
            if (parent.Status == worldScreen.RUNNING)
            {
                foreach (GraphicalGameObject b in parent.frame.frames)
                {
                    switch (overlapTester.overlapRectanglesEX(new Rectangle((int)b.X, (int)b.Y, (int)b.Width, (int)b.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                    {
                        case 1:
                            velocityX = -Speed;
                            //DebugConsole.write(",1");
                            X = b.X - Width;
                            // System.Windows.Forms.MessageBox.Show("1");
                            flag1 = 2;
                            break;
                        case 2:
                            velocityX = Speed;
                            X = b.X + b.Width;
                            // X = b.X+b.Width;
                            // System.Windows.Forms.MessageBox.Show("2");
                            //DebugConsole.write(",2");
                            flag1 = 1;
                            break;
                        case 3:
                            velocityY = -Speed;
                            Y = b.Y - Height;
                            // Y = b.Y - Height;
                            // DebugConsole.write(",3");
                            // System.Windows.Forms.MessageBox.Show("3");

                            flag1 = 4;
                            break;
                        case 4:
                            velocityY = Speed;
                            Y = b.Y + b.Height;
                            //  Y = b.Y + b.Height;
                            //  System.Windows.Forms.MessageBox.Show("4");]
                            // DebugConsole.write(",4");
                            flag1 = 3;
                            break;
                    }
                }

            }
            int flag21 = 0;
            int flag22 = 0;


                foreach (List<LevelObject> l in parent.Layor) foreach (LevelObject o in l)
                {
                    if (!o.enable) continue;

                    if (o is warpPoint)
                    {

                       if (((warpPoint)o).disableWarp)//ワープが無効なときにブロックと同じ振る舞いをする
                        {
                            switch (overlapTester.overlapRectanglesEX(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                            {
                                case 1:
                                    velocityX = -Speed;
                                    // DebugConsole.write(",1");
                                    // X = b.X - Width;
                                    // System.Windows.Forms.MessageBox.Show("1");
                                    if (flag21 == 0)
                                    {
                                        flag21 = 1;
                                    }
                                    else
                                    {
                                        flag22 = 2;
                                    }

                                    break;
                                case 2:
                                    velocityX = Speed;
                                    // X = b.X+b.Width;
                                    // System.Windows.Forms.MessageBox.Show("2");
                                    //  DebugConsole.write(",2");
                                    if (flag21 == 0)
                                    {
                                        flag21 = 2;
                                    }
                                    else
                                    {
                                        flag22 = 1;
                                    }
                                    break;
                                case 3:
                                    velocityY = -Speed;
                                    // Y = b.Y - Height;
                                    //  DebugConsole.write(",3");
                                    // System.Windows.Forms.MessageBox.Show("3");

                                    if (flag21 == 0)
                                    {
                                        flag21 = 3;
                                    }
                                    else
                                    {
                                        flag22 = 4;
                                    }
                                    break;
                                case 4:
                                    velocityY = Speed;
                                    //  Y = b.Y + b.Height;
                                    //  System.Windows.Forms.MessageBox.Show("4");]
                                    //   DebugConsole.write(",4");
                                    if (flag21 == 0)
                                    {
                                        flag21 = 4;
                                    }
                                    else
                                    {
                                        flag22 = 3;
                                    }
                                    break;

                            }
                            continue;
                        }


                            if (((warpPoint)o).isArrive)
                        {
                         if(!overlapTester.overlapRectangles(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                            {
                                ((warpPoint)o).isArrive = false;
                                warping = false;
                            }
                            
                            continue;
                        }

                            if (!((warpPoint)o).Warping)
                        {



                            switch (overlapTester.overlapRectanglesEX(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                            {
                                case 1:
                                    if (o.rotate == 3) ((warpPoint)o).Warping = true;
                                    else velocityX = -Speed;
                                    break;
                                case 2:
                                    if (o.rotate == 1) ((warpPoint)o).Warping = true;
                                    else velocityX = Speed;
                                    break;

                                case 3:
                                    if (o.rotate == 0) ((warpPoint)o).Warping = true;
                                    else velocityY = -Speed;
                                    break;

                                case 4:
                                    if (o.rotate == 2) ((warpPoint)o).Warping = true;
                                    else velocityY = Speed;
                                    break;

                            }

                        }else
                        {
                            warping = true;
                            Rectangle warpBox = new Rectangle(0, 0, 0, 0);

                            switch (o.rotate)
                            {
                                case 0:

                                    warpBox = new Rectangle((int)o.X, (int)o.Y+35, (int)o.Width, (int)o.Height);

                                    break;
                                case 1:

                                    warpBox = new Rectangle((int)o.X-35, (int)o.Y, (int)o.Width, (int)o.Height);

                                    break;
                                case 2:

                                    warpBox = new Rectangle((int)o.X, (int)o.Y - 35, (int)o.Width, (int)o.Height);

                                    break;
                                case 3:

                                    warpBox = new Rectangle((int)o.X+35, (int)o.Y, (int)o.Width, (int)o.Height);

                                    break;


                            }

                            if(overlapTester.overlapRectangles(warpBox, new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                            {
                                warpPoint next = (warpPoint)o;
                                foreach (List<LevelObject> l2 in parent.Layor) foreach (LevelObject o2 in l2)
                                    {
                                        if(o2 is warpPoint)
                                        {
                                            if (o2.specialData == o.specialData && o!=o2) next = (warpPoint)o2;
                                        }
                                    }

                                double dis=0;
                                switch (o.rotate)
                                {
                                    case 0:
                                        dis = X - o.X;
                                        break;
                                    case 1:
                                        dis = Y - o.Y;
                                        break;
                                    case 2:
                                        dis = X - o.X;
                                        break;
                                    case 3:
                                        dis = Y - o.Y;
                                        break;

                                }
                             ((warpPoint)o).Warping = false;
                                    next.isArrive = true;
                                switch (next.rotate)
                                {
                                    case 0:
                                        X = next.X+dis;
                                        Y = next.Y;
                                        velocityY = -Speed;
                                        break;
                                    case 1:
                                        X = next.X;
                                        Y = next.Y+dis;
                                        velocityX = Speed;
                                        break;
                                    case 2:
                                        X = next.X+dis;
                                        Y = next.Y;
                                        velocityY = Speed;
                                        break;
                                    case 3:
                                        X = next.X;
                                        Y = next.Y+dis;
                                        velocityX = -Speed;
                                        break;

                                }

                               
                                
                                DebugConsole.write("warped");
                            }
                            

                        }

                    }
                    else if (o is block)
                    {
                        
                        switch (overlapTester.overlapRectanglesEX(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            case 1:
                                velocityX = -Speed;
                               // DebugConsole.write(",1");
                                // X = b.X - Width;
                                // System.Windows.Forms.MessageBox.Show("1");
                                if (flag21 == 0)
                                {
                                    flag21 = 1;
                                }else
                                {
                                    flag22 = 2;
                                }
                                
                                break;
                            case 2:
                                velocityX = Speed;
                                // X = b.X+b.Width;
                                // System.Windows.Forms.MessageBox.Show("2");
                              //  DebugConsole.write(",2");
                                if (flag21 == 0)
                                {
                                    flag21 = 2;
                                }
                                else
                                {
                                    flag22 = 1;
                                }
                                break;
                            case 3:
                                velocityY = -Speed;
                                // Y = b.Y - Height;
                              //  DebugConsole.write(",3");
                                // System.Windows.Forms.MessageBox.Show("3");

                                if (flag21 == 0)
                                {
                                    flag21 = 3;
                                }
                                else
                                {
                                    flag22 = 4;
                                }
                                break;
                            case 4:
                                velocityY = Speed;
                                //  Y = b.Y + b.Height;
                                //  System.Windows.Forms.MessageBox.Show("4");]
                             //   DebugConsole.write(",4");
                                if (flag21 == 0)
                                {
                                    flag21 = 4;
                                }
                                else
                                {
                                    flag22 = 3;
                                }
                                break;

                        }
                    }
                    else if(o is thorn)
                    {
                        if (overlapTester.overlapRectangles(new Rectangle((int)o.X+10, (int)o.Y+10, (int)o.Width-20, (int)o.Height-20), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        { die(); return; }
                    }
                    else if(o is Switch)
                    {
                        Rectangle box = new Rectangle(0, 0, 0, 0);
                        switch (o.rotate)
                        {
                            case 0:
                                box = new Rectangle((int)o.X, (int)o.Y + 31, (int)o.Width, 9);
                                break;
                            case 1:
                                box = new Rectangle((int)o.X, (int)o.Y, 9, (int)o.Height);
                                break;
                            case 2:
                                box = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, 9);
                                break;
                            case 3:
                                box = new Rectangle((int)o.X + 31, (int)o.Y, 9, (int)o.Height);
                                break;
                            case 4:
                                box = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                                break;
                        }
                        if (overlapTester.overlapRectangles(box, new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            parent.flags[o.eventData.num] = true;
                            ((Switch)o).IsPush = true;
                            //DebugConsole.write("イベント: " + o.eventData.num.ToString());
                        }
                    }
                    else if (o is shapeChangePoint)
                    {
                      
                        if (overlapTester.overlapRectangles(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            if (Input.onKeyDown(Keys.Space) && parent.Status == worldScreen.RUNNING) parent.Status = worldScreen.CHANGING;
                            else if (Input.onKeyDown(Keys.Space) && parent.Status == worldScreen.CHANGING) parent.Status = worldScreen.RUNNING;
                        }
                    }
                    else if (o is accel)
                    {
                        if (overlapTester.overlapRectangles(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            if (o.rotate == 0)
                            {
                                Speed = 0.5f;
                                if (velocityX > 0)
                                {
                                    velocityX = 0.5f;
                                }else
                                {
                                    velocityX = -0.5f;
                                }
                                if (velocityY > 0)
                                {
                                    velocityY = 0.5f;
                                }else
                                {
                                    velocityY = -0.5f;
                                }
                            }
                            else
                            {
                                Speed = 0.1f;
                                if (velocityX > 0)
                                {
                                    velocityX = 0.1f;
                                }
                                else
                                {
                                    velocityX = -0.1f;
                                }
                                if (velocityY > 0)
                                {
                                    velocityY = 0.1f;
                                }
                                else
                                {
                                    velocityY = -0.1f;
                                }
                            }
                           
                        }else
                        {
                            Speed = 0.3f;
                        }

                    }
                    else if (o is savePoint)
                    {
                        if (overlapTester.overlapRectangles(new Rectangle((int)o.X + 10, (int)o.Y + 10, (int)o.Width - 20, (int)o.Height - 20), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            if (!((savePoint)o).saved)
                            {
                                ((savePoint)o).saved = true;
                                parent.savePoint = new Point((int)o.X, (int)o.Y);
                                parent.savedScreen = new savedScreen(game, parent);
                            }
                           
                        }
                    }
                    else if(o is goal)
                    {
                        if (overlapTester.overlapRectangles(new Rectangle((int)o.X+20, (int)o.Y+20, (int)o.Width-40, (int)o.Height-40), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            parent.clear();
                            X = o.X;
                            Y = o.Y;
                            
                            
                        }


                        }
                }

                
            if (flag1 != 0 && flag21 != 0 && flag1 == flag21) { die(); return; }
            if (flag21 == flag22 && flag21!=0 && flag22!=0) { die();return; }
            //Console.WriteLine("vx:" + velocityX.ToString());
            if (parent.Status == worldScreen.RUNNING)
            {
                X += velocityX * delta;
                Y += velocityY * delta;
            }
            


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
            Assets.soundEffects.glass.Play();
        }
        public void spread(float deltaTime)
        {
            AnimationControll += deltaTime;
            if (AnimationControll >= AnimationSpeed)
            {
                frame++;
                AnimationControll = 0;
            }
            if (frame == 44)
            {
                died();breaking = false;
            }
            this.Texture = Assets.graphics.game.ball_animation[frame];
        }
      public void died()
        {
            if (parent.testPlay)
            {
                parent.stopTest();
                return;
            }
            // game.screens.Clear();
            parent.Status = worldScreen.DIED;
           // game.screens.Add(new GameScreen(game));
        }
        public void clearAnimation(float deltaTime)
        {

        }
    }
}
