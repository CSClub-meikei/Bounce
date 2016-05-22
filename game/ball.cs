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
            int flag2 = 0;

            foreach (GraphicalGameObject b in parent.frame.frames)
            {
                switch (overlapTester.overlapRectanglesEX(new Rectangle((int)b.X, (int)b.Y, (int)b.Width, (int)b.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                {
                    case 1:
                        velocityX = -Speed;
                        DebugConsole.write(",1");
                        // X = b.X - Width;
                        // System.Windows.Forms.MessageBox.Show("1");
                        flag1 = 2;
                        break;
                    case 2:
                        velocityX = Speed;
                        // X = b.X+b.Width;
                        // System.Windows.Forms.MessageBox.Show("2");
                        DebugConsole.write(",2");
                        flag1 = 1;
                        break;
                    case 3:
                        velocityY = -Speed;
                        // Y = b.Y - Height;
                        DebugConsole.write(",3");
                        // System.Windows.Forms.MessageBox.Show("3");

                        flag1 = 4;
                        break;
                    case 4:
                        velocityY = Speed;
                        //  Y = b.Y + b.Height;
                        //  System.Windows.Forms.MessageBox.Show("4");]
                        DebugConsole.write(",4");
                        flag1 = 3;
                        break;
                }
            }


            int flag21 = 0;
            int flag22 = 0;


                foreach (List<LevelObject> l in parent.Layor) foreach (LevelObject o in l)
                {
                    if (!o.enable) continue;

                    if (o is warpPoint)
                    {

                        Rectangle box = new Rectangle(0, 0, 0, 0);
                        Rectangle box2 = new Rectangle(0, 0, 0, 0);
                        double dis = 0;

                        switch (o.rotate)
                        {
                            case 0:
                                box = new Rectangle((int)o.X, (int)o.Y + 35, (int)o.Width, (int)o.Height+40);
                                box2 = new Rectangle((int)o.X, (int)o.Y + 70, (int)o.Width, (int)o.Height);
                                dis = X - o.X;
                                break;
                            case 1:
                                box = new Rectangle((int)o.X - 35, (int)o.Y, (int)o.Width+40, (int)o.Height);
                                box2 = new Rectangle((int)o.X-70, (int)o.Y, (int)o.Width, (int)o.Height);
                                dis = Y - o.Y;
                                break;
                            case 2:
                                box = new Rectangle((int)o.X, (int)o.Y - 35, (int)o.Width, (int)o.Height+40);
                                box2 = new Rectangle((int)o.X, (int)o.Y - 70, (int)o.Width, (int)o.Height);
                                dis = X - o.X;
                                break;
                            case 3:
                                box = new Rectangle((int)o.X + 35, (int)o.Y, (int)o.Width+35, (int)o.Height);
                                box2 = new Rectangle((int)o.X+70, (int)o.Y, (int)o.Width, (int)o.Height);
                                dis = Y - o.Y;
                                break;
                            case 4:
                                box = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                                break;
                        }
                        
                        if (overlapTester.overlapRectangles(box2, new Rectangle((int)X, (int)Y, (int)Width, (int)Height)) && !((warpPoint)o).isArrive)
                        {
                            DebugConsole.write("box2");
                            warping = true;
                           // System.Windows.Forms.MessageBox.Show(o.rotate.ToString());
                            foreach (List<LevelObject> l2 in parent.Layor) foreach (LevelObject o2 in l2)
                                {
                                    if (o2 is warpPoint && o.specialData == o2.specialData )
                                    {

                                        if(o.X != o2.X || o.Y != o2.Y)
                                        {
                                            ((warpPoint)o2).isArrive = true;
                                            switch (o2.rotate)
                                            {
                                                case 0:
                                                    X = o2.X + dis;
                                                    Y = o2.Y + 35;
                                                    velocityY = -Speed;

                                                    break;
                                                case 1:
                                                    X = o2.X - 35;
                                                    Y = o2.Y + dis;
                                                    velocityX = Speed;
                                                    break;
                                                case 2:

                                                    X = o2.X + dis;
                                                    Y = o2.Y - 35;
                                                    velocityY = Speed;
                                                    break;
                                                case 3:

                                                    X = o2.X + 35;
                                                    Y = o2.Y + dis;
                                                    velocityX = -Speed;
                                                    break;
                                                case 4:

                                                    X = o2.X;
                                                    Y = o2.Y;

                                                    break;
                                            }

                                            break;
                                           
                                        }
                                       
                                    }

                                }



                        }else if(overlapTester.overlapRectangles(box, new Rectangle((int)X, (int)Y, (int)Width, (int)Height))){
                            DebugConsole.write("box1");
                            warping = true;
                            break;
                            
                        }
                    

                        else
                        {
                            warping = false;
                            ((warpPoint)o).isArrive = false;
                        }


                    }
                    else if (o is block && !warping)
                    {
                        switch (overlapTester.overlapRectanglesEX(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            case 1:
                                velocityX = -Speed;
                                DebugConsole.write(",1");
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
                                DebugConsole.write(",2");
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
                                DebugConsole.write(",3");
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
                                DebugConsole.write(",4");
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
                    else if(o is thorn && !warping)
                    {
                        if (overlapTester.overlapRectangles(new Rectangle((int)o.X+10, (int)o.Y+10, (int)o.Width-20, (int)o.Height-20), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        { die(); return; }
                    }
                    else if(o is Switch && !warping)
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
                            DebugConsole.write("イベント: " + o.eventData.num.ToString());
                        }
                    }
                    else if (o is shapeChangePoint && !warping)
                    {
                      
                        if (overlapTester.overlapRectangles(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            if (Input.onKeyDown(Keys.Space) && parent.Status == worldScreen.RUNNING) parent.Status = worldScreen.CHANGING;
                            else if (Input.onKeyDown(Keys.Space) && parent.Status == worldScreen.CHANGING) parent.Status = worldScreen.RUNNING;
                        }
                    }
                    
                    else if(o is goal && !warping)
                    {
                        if (overlapTester.overlapRectangles(new Rectangle((int)o.X+20, (int)o.Y+20, (int)o.Width-40, (int)o.Height-40), new Rectangle((int)X, (int)Y, (int)Width, (int)Height)))
                        {
                            parent.clear();
                            X = o.X;
                            Y = o.Y;
                            
                            
                        }


                        }
                }

                n:
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
            game.screens.Clear();
           
            game.screens.Add(new GameScreen(game));
        }
        public void clearAnimation(float deltaTime)
        {

        }
    }
}
