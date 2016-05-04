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
        public ball ball;
        public frame frame;
        public List<block> blocks = new List<block>();
        public List<thorn> thorns = new List<thorn>();
        
        public const int RUNNING = 1;
        public const int PAUSE = 0;
        public const int CHANGING = 2;
        public int Status = RUNNING;
        public int frameShape = 1;

        public worldScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {


            load("test.txt");
           // blocks.Add(new block(game, this, Assets.graphics.game.block, 480, 200, 360, 40));
           // blocks.Add(new block(game, this, Assets.graphics.game.block, 480, 240, 40,640));
          //  blocks.Add(new block(game, this, Assets.graphics.game.block, 800, 240, 40, 400));
          //  blocks.Add(new block(game, this, Assets.graphics.game.block, 800, 600, 800, 40));
          //  thorns.Add(new thorn(game, this, Assets.graphics.game.thorn, 520, 840, 600, 40));
          //  blocks.Add(new block(game, this, Assets.graphics.game.block, 480, 880, 1200, 40));
        //    thorns.Add(new thorn(game, this, Assets.graphics.game.thorn, 1050, 760, 600, 40));
         //   blocks.Add(new block(game, this, Assets.graphics.game.block, 1050, 800, 600, 80));
            // thorns.Add(new thorn(game, this, Assets.graphics.game.thorn, 500, 460, 120, 40));

            ball = new ball(game, this, Assets.graphics.game.ball, 640, 360, 40, 40);
            frame = new frame(game, this, Assets.graphics.game.frameW, 540, 260, 200, 200);
            //  ball=new GraphicalGameObject(game,this,Assets.graphics.game.ball,)
            setUIcell(1, 1);
            time = new TextObject(game, this, Assets.graphics.ui.font, "time: 0", Color.White, 0, 0);
        }
        public override void update(float deltaTime)
        {
            
            if (Status == RUNNING)
            {
                if (Input.onKeyDown(Keys.C)) { frame.LoadFrame(2); }
                if (Input.IsKeyDown(Keys.Right)) { X -= (int)(0.3f * deltaTime); }
                if (Input.IsKeyDown(Keys.Left)) { X += (int)(0.3f * deltaTime); }
                if (Input.IsKeyDown(Keys.Up)) { Y += (int)(0.3f * deltaTime); }
                if (Input.IsKeyDown(Keys.Down)) { Y -= (int)(0.3f * deltaTime); }
                foreach (block b in blocks) b.update(deltaTime);
                foreach (thorn b in thorns) b.update(deltaTime);
                ball.update(deltaTime);
            }
            if (Status == CHANGING)
            {
                if (Input.onKeyDown(Keys.Right)) { frameShape++; frame.LoadFrame(frameShape); }
                if (Input.onKeyDown(Keys.Left)) { frameShape--; frame.LoadFrame(frameShape); }
            }
            if (Status != PAUSE)
            {
                if (Input.onKeyDown(Keys.Space) && Status == RUNNING) Status = CHANGING;
                else if (Input.onKeyDown(Keys.Space) && Status == CHANGING) Status = RUNNING;

                frame.update(deltaTime);
                time.update(deltaTime);
                base.update(deltaTime);
            }
          
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
        public void load(String fileName)
        {
            //"C:\test\1.txt"をShift-JISコードとして開く
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName,
                System.Text.Encoding.GetEncoding("shift_jis"));
            //内容をすべて読み込む
            string s = sr.ReadToEnd();
            //閉じる
            sr.Close();
            String[] data;
           data= s.Split('/');
            foreach(String str in data)
            {
                String[] tmp = str.Split(',');
                if (tmp[0] == "1")
                {
                    blocks.Add(new block(game, this, Assets.graphics.game.block, int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4])));
                }else if (tmp[0] == "2")
                {
                    thorns.Add(new thorn(game, this, Assets.graphics.game.thorn, int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4])));
                }
            }
        }
    }
}
