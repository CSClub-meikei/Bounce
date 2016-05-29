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
    class frame:GraphicalGameObject
    {
        new worldScreen parent;
        public   int HABA = 8;
        public float fw, fh;
        
        public List<singleFrame> frames = new List<singleFrame>();
        public frame(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen,Texture, x, y, width, height)
        {
            parent = (worldScreen)screen;
            setLocation(x, y);
            fw = width;
            fh = height;
           setSize(width, height);
            addAnimator(2);
            LoadFrame(1);
           

        }
        public void LoadFrame(int shape)
        {
            frames.Clear();
            switch (shape)
            {
                case 1:
                    setSize(fw,fh);
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW,X,Y, (int)(X-(Width/2)), (int)(Y-(Height/2)), (int)Width,HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)((X - (Width / 2)) + Width-HABA), (int)(Y - (Height / 2)), HABA, (int)Height));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)((Y - (Height / 2)) + Height-HABA), (int)Width,HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), HABA, (int)Height));
                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X +parent.ball.Width/2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height/2), 0, 0, 2f, 2f });
                   
                    break;
                case 2:
                   
                    setSize(fw*1.6, fh*0.6);
                    // setLocation(parent.ball.X-(Width/2),parent.ball.Y-(Height/2));

                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)((X - (Width / 2)) + Width - HABA), (int)(Y - (Height / 2)), HABA, (int)Height));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)((Y - (Height / 2)) + Height - HABA), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), HABA, (int)Height));
                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });

                    break;
                case 3:
                    setSize(fw *0.6, fh *1.6);
                    // setLocation(parent.ball.X-(Width/2),parent.ball.Y-(Height/2));

                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)((X - (Width / 2)) + Width - HABA), (int)(Y - (Height / 2)), HABA, (int)Height));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)((Y - (Height / 2)) + Height - HABA), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), HABA, (int)Height));
                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });

                    break;
                case 4:
                    setSize(fw, fh);

                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X-60), (int)(Y-200), (int)120, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X - 60), (int)(Y - 200), (int)HABA, 360));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X + 60-HABA), (int)(Y - 200), (int)HABA, 240));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - 60), (int)(Y + 160 -HABA), (int)360, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X + 60), (int)(Y + 40 - HABA), (int)240, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X + 300-HABA), (int)(Y + 40 - HABA), (int)HABA, 120));

                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });



                    break;
                case 5:

                    setSize(fw, fh);
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)((X - (Width / 2)) + Width - HABA), (int)(Y - (Height / 2)), HABA, (int)Height/2-30));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)((X - (Width / 2)) + Width - HABA), (int)(Y +30), HABA, (int)Height / 2 - 30));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)((Y - (Height / 2)) + Height - HABA), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), HABA, (int)Height));
                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });



                    break;
                    
                case 6:
                    setSize(fw*0.6, fh*0.6);
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)((X - (Width / 2)) + Width - HABA), (int)(Y - (Height / 2)), HABA, (int)Height));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)(X - (Width / 2)), (int)((Y - (Height / 2)) + Height - HABA), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X - (Width / 2)), (int)(Y - (Height / 2)), HABA, (int)Height));
                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X + parent.ball.Width / 2), (float)(parent.ball.Y + parent.ball.Height / 2), 0, 0, 2f, 2f });

                    break;

            }
        }
        public override void update(float deltaTime)
        {
            if (parent.Status == worldScreen.RUNNING)
            {
                if (Input.IsKeyDown(Keys.Right)) { X += (int)(0.3f * deltaTime); }
                if (Input.IsKeyDown(Keys.Left)) { X -= (int)(0.3f * deltaTime); }
                if (Input.IsKeyDown(Keys.Up)) { Y -= (int)(0.3f * deltaTime); }
                if (Input.IsKeyDown(Keys.Down)) { Y += (int)(0.3f * deltaTime); }
            }
              foreach (singleFrame g in frames)
              {
                g.update(deltaTime);
                g.X = X + g.dfX;
                g.Y = Y + g.dfY;
                
               }

           
            
                base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            foreach (GraphicalGameObject g in frames) g.Draw(batch, screenAlpha);
        }
    }
}
