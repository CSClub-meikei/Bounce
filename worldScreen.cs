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
    class worldScreen:Screen
    {
       public TextObject time;
        GraphicalGameObject b;
        ball ball;
        public frame frame;
        public List<block> blocks = new List<block>();
        public List<thorn> thorns = new List<thorn>();

        public worldScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            blocks.Add(new block(game, this, Assets.graphics.game.block, 480, 200, 360, 40));
            blocks.Add(new block(game, this, Assets.graphics.game.block, 480, 240, 40,640));
            blocks.Add(new block(game, this, Assets.graphics.game.block, 800, 240, 40, 400));
            blocks.Add(new block(game, this, Assets.graphics.game.block, 800, 600, 800, 40));
            thorns.Add(new thorn(game, this, Assets.graphics.game.thorn, 520, 840, 600, 40));
            blocks.Add(new block(game, this, Assets.graphics.game.block, 480, 880, 1200, 40));
            thorns.Add(new thorn(game, this, Assets.graphics.game.thorn, 1050, 760, 600, 40));
            blocks.Add(new block(game, this, Assets.graphics.game.block, 1050, 800, 600, 80));
            // thorns.Add(new thorn(game, this, Assets.graphics.game.thorn, 500, 460, 120, 40));

            ball = new ball(game, this, Assets.graphics.game.ball, 300, 200, 40, 40);
            frame = new frame(game, this, Assets.graphics.game.frame, 540, 260, 200, 200);
            //  ball=new GraphicalGameObject(game,this,Assets.graphics.game.ball,)
            setUIcell(1, 1);
            time = new TextObject(game, this, Assets.graphics.ui.font, "time: 0", Color.White, 0, 0);
        }
        public override void update(float deltaTime)
        {

              if (Input.IsKeyDown(Keys.Right)) { X -= (int)(0.3f * deltaTime);}
              if (Input.IsKeyDown(Keys.Left)) { X += (int)(0.3f * deltaTime);}
              if (Input.IsKeyDown(Keys.Up)) { Y += (int)(0.3f * deltaTime); }
              if (Input.IsKeyDown(Keys.Down)) { Y -= (int)(0.3f * deltaTime); }
            foreach (block b in blocks) b.update(deltaTime);
            foreach (thorn b in thorns) b.update(deltaTime);
            ball.update(deltaTime);
            frame.update(deltaTime);
            time.update(deltaTime);
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            foreach (block b in blocks) b.Draw(batch,screenAlpha);
            foreach (thorn b in thorns) b.Draw(batch, screenAlpha);
            ball.Draw(batch, screenAlpha);
            frame.Draw(batch, screenAlpha);
            time.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
    }
}
