using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;
using System.Xml;

namespace Bounce
{
    class worldScreen:Screen
    {
        public mapData map;

       public TextObject time;
        tileObject back;
        public ball ball;
        public frame frame;
        public List<bool> flags = new List<bool>();
        
        public List<List<LevelObject>> Layor;

        

        public const int READY=0;
        public const int RUNNING = 1;
        public const int PAUSE = 3;
        public const int CHANGING = 2;
        public const int CLEARED = 4;

        public int Status = READY;
        public int frameShape = 1;

        public bool testPlay = false;
        public event EventHandler stop;
        public event EventHandler onClear;
        public worldScreen(Game1 game,string file,bool IsTest, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            testPlay = IsTest;
            load(file);
            init();
            
        }
        public worldScreen(Game1 game, mapData data, bool IsTest, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            testPlay = IsTest;
            map = data;

            GenerateMapFromData();
            init();
           
        }
        public void init()
        {
            ball = new ball(game, this, Assets.graphics.game.ball, map.start.X, map.start.Y, 40, 40);
            frame = new frame(game, this, Assets.graphics.game.frameW, map.start.X, map.start.Y, 200, 200);
            X = (int)-frame.X + 640;
            Y = (int)-frame.Y + 360;
            //  ball=new GraphicalGameObject(game,this,Assets.graphics.game.ball,)
            setUIcell(1, 1);
            time = new TextObject(game, this, Assets.graphics.ui.font, "time: 0", Color.White, 0, 0);
            int i = 0;
            for (i = 0; i <= 100; i++) flags.Add(false);
            back = new tileObject(game, this, Assets.graphics.game.mapBack, -10000, -10000, 1000, 1000, 20, 20);
            back.alpha = 0.5f;
            flags[0] = true;
           
            
            if (testPlay) Status = RUNNING;
        }
        public override void update(float deltaTime)
        {
            if (Status != PAUSE && Status !=READY && Status!= CLEARED )
            {
               
                
                frame.update(deltaTime);
                time.update(deltaTime);
                X = (int)-frame.X + 640;
                Y = (int)-frame.Y + 360;

               
              
                foreach (List<LevelObject> l in Layor) foreach (LevelObject o in l) o.update(deltaTime);

                ball.update(deltaTime);
            }

            if (Status == READY)
            {
              
            }
            
            if (Status == CHANGING)
            {
                if (Input.onKeyDown(Keys.Right)) { frameShape++; frame.LoadFrame(frameShape); }
                if (Input.onKeyDown(Keys.Left)) { frameShape--; frame.LoadFrame(frameShape); }
            }
            base.update(deltaTime);
            if (Input.onKeyDown(Keys.P) && testPlay)
            {
                // game.screens.Remove(this);
                stopTest();
            }
            back.update(deltaTime);
            back.X = back.X * 0.5;
            back.Y = back.Y * 0.5;
        }
        public void stopTest()
        {
            if (stop != null) stop(this, EventArgs.Empty);
            
        }
        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch, screenAlpha);
           if(Layor!=null) foreach (List<LevelObject> l in Layor) foreach (LevelObject o in l) o.Draw(batch,screenAlpha);
           
            ball.Draw(batch, screenAlpha);
            frame.Draw(batch, screenAlpha);
            time.Draw(batch, screenAlpha);

            if (Status == READY)
            {
              
            }

            base.Draw(batch);
        }
        public void load(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                //DataContractSerializerオブジェクトを作成
                DataContractSerializer serializer =
                    new DataContractSerializer(typeof(mapData));
                //読み込むファイルを開く
                XmlReader xr = XmlReader.Create(fileName);
                //XMLファイルから読み込み、逆シリアル化する
                map = (mapData)serializer.ReadObject(xr);
                //ファイルを閉じる
                xr.Close();
                

            }
            else
            {
                map = new mapData();

            }
            GenerateMapFromData();
           

        }
        public void GenerateMapFromData()
        {
            Layor = new List<List<LevelObject>>();
            Layor.Add(new List<LevelObject>());
            foreach (List<mapChip> layor in map.Layor) foreach (mapChip chip in layor)
                {
                    switch (chip.type)
                    {
                        case mapChip.BLOCK:
                            this.Layor[0].Add(new block(game, this, chip.eventData, chip.rotate, (float)chip.X, (float)chip.Y, (float)chip.Width, (float)chip.Height));
                            break;
                        case mapChip.THORN:
                            this.Layor[0].Add(new thorn(game, this, chip.eventData, chip.rotate, (float)chip.X, (float)chip.Y, (float)chip.Width, (float)chip.Height));
                            break;
                        case mapChip.SWITCH:
                            this.Layor[0].Add(new Switch(game, this, chip.eventData, chip.rotate, (float)chip.X, (float)chip.Y, (float)chip.Width, (float)chip.Height));
                            break;
                        case mapChip.SHPOINT:
                            this.Layor[0].Add(new shapeChangePoint(game, this, chip.eventData, chip.rotate, (float)chip.X, (float)chip.Y, (float)chip.Width, (float)chip.Height));
                            break;
                        case mapChip.GOAL:
                            this.Layor[0].Add(new goal(game, this, chip.eventData, chip.rotate, (float)chip.X, (float)chip.Y, (float)chip.Width, (float)chip.Height));
                            break;
                    }

                }
        }
        public void clear()
        {
            Status = CLEARED;
            if (testPlay) stopTest();
            if (onClear != null) onClear(this, EventArgs.Empty);
        }
        public void load_old(String fileName)
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
                  //  block b = new block(game, this, Assets.graphics.game.block, int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4]));
                  //  b.eventData.type = int.Parse(tmp[6]);
                  //  b.eventData.num= int.Parse(tmp[7]);
                   // b.moveLocation.X = int.Parse(tmp[8]);
                   // b.moveLocation.Y = int.Parse(tmp[9]);
                   // b.EventVisible = int.Parse(tmp[10]);
                  //  blocks.Add(b);
                }else if (tmp[0] == "2")
                {
                  //  thorn t = new thorn(game, this, Assets.graphics.game.thorn[int.Parse(tmp[5])], int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4]));
                  //  t.eventData.type = int.Parse(tmp[6]);
                  //  t.eventData.num = int.Parse(tmp[7]);
                   // t.moveLocation.X = int.Parse(tmp[8]);
                  //  t.moveLocation.Y = int.Parse(tmp[9]);
                  //  t.EventVisible = int.Parse(tmp[10]);
                   // thorns.Add(t);
                }
                else if (tmp[0] == "3")
                {
                  //  Switch t = new Switch(game, this, Assets.graphics.game.Switch[int.Parse(tmp[5])], int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4]));
                  //  t.eventData.type = int.Parse(tmp[6]);
                  //  t.eventData.num = int.Parse(tmp[7]);
                  //  t.moveLocation.X = int.Parse(tmp[8]);
                  // t.moveLocation.Y = int.Parse(tmp[9]);
                  //  t.EventVisible = int.Parse(tmp[10]);
                  //  switchs.Add(t);
                }
                else if (tmp[0] == "4")
                {
                   // shapeChangePoint t = new shapeChangePoint(game, this, Assets.graphics.game.changePoint, int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4]));
                   // t.eventData.type = int.Parse(tmp[6]);
                  //  t.eventData.num = int.Parse(tmp[7]);
                   // t.moveLocation.X = int.Parse(tmp[8]);
                  //  t.moveLocation.Y = int.Parse(tmp[9]);
                  //  t.EventVisible = int.Parse(tmp[10]);
                  //  changePoints.Add(t);
                }
            }
        }
    }
}
