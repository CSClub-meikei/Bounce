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
        public const int HABA = 8;
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
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW,X,Y, (int)X, (int)Y, (int)Width,HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X+Width-HABA), (int)Y, HABA, (int)Height));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)X, (int)(Y+Height-HABA), (int)Width,HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X), (int)Y,HABA, (int)Height));
                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X - (Width / 2)+ parent.ball.Width/2), (float)(parent.ball.Y - (Height / 2) + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X - (Width / 2) + parent.ball.Width / 2), (float)(parent.ball.Y - (Height / 2)+ parent.ball.Height/2), 0, 0, 2f, 2f });
                   
                    break;
                case 2:
                   
                    setSize(fw*1.6, fh/2);
                   // setLocation(parent.ball.X-(Width/2),parent.ball.Y-(Height/2));
                    
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)X, (int)Y, (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X + Width - HABA), (int)Y, HABA, (int)Height));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)X, (int)(Y + Height - HABA), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X), (int)Y, HABA, (int)Height));

                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X - (Width / 2) + parent.ball.Width / 2), (float)(parent.ball.Y - (Height / 2) + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X - (Width / 2) + parent.ball.Width / 2), (float)(parent.ball.Y - (Height / 2) + parent.ball.Height / 2), 0, 0, 2f, 2f });

                    break;
                case 3:
                    setSize(fw /2, fh *1.6);
                    // setLocation(parent.ball.X-(Width/2),parent.ball.Y-(Height/2));

                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)X, (int)Y, (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X + Width - HABA), (int)Y, HABA, (int)Height));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameW, X, Y, (int)X, (int)(Y + Height - HABA), (int)Width, HABA));
                    frames.Add(new singleFrame(game, parent, Assets.graphics.game.frameH, X, Y, (int)(X), (int)Y, HABA, (int)Height));

                    animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, (float)(parent.ball.X - (Width / 2)  ), (float)(parent.ball.Y - (Height / 2) + parent.ball.Height / 2), 0, 0, 2f, 2f });
                    animator[1].start(GameObjectAnimator.SLIDE, new float[] { 2, (float)(parent.ball.X - (Width / 2) ), (float)(parent.ball.Y - (Height / 2) + parent.ball.Height / 2), 0, 0, 2f, 2f });

                    break;
            }
        }
        public override void update(float deltaTime)
        {
            if (Input.IsKeyDown(Keys.Right)) { X += (int)(0.3f * deltaTime);  }
            if (Input.IsKeyDown(Keys.Left)) { X -= (int)(0.3f * deltaTime); }
            if (Input.IsKeyDown(Keys.Up)) { Y -= (int)(0.3f * deltaTime);}
            if (Input.IsKeyDown(Keys.Down)) { Y += (int)(0.3f * deltaTime);}
            foreach (singleFrame g in frames)
            {

                g.X = X + g.dfX;
                g.Y = Y + g.dfY;
                g.update(deltaTime);
            }
                base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            foreach (GraphicalGameObject g in frames) g.Draw(batch, screenAlpha);
        }
    }
}
