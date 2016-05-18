using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce
{
    class EditorScreen_p:Screen
    {
        public List<mapChip_p> chips=new List<mapChip_p>();
        public List<mapChip_p> Rchips = new List<mapChip_p>();
        public TextObject label;
        public bool selecting;
        public EditorScreen_p(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            label = new TextObject(game, this, Assets.graphics.ui.font, "", Color.White, 0, 0);
            setUIcell(1, 1);
            load("test.txt");
        }
        public override void update(float deltaTime)
        {
            if (Input.onKeyDown(Keys.A)) chips.Add(new mapChip_p(game, this, Assets.graphics.game.block,1, 1,1,Input.getPosition().X - X, Input.getPosition().Y - Y, 40, 40));
            if (Input.onKeyDown(Keys.Z)) chips.Add(new mapChip_p(game, this, Assets.graphics.game.thorn[0], 2,1,1, Input.getPosition().X - X, Input.getPosition().Y - Y, 40, 40));
            if (Input.onKeyDown(Keys.W)) chips.Add(new mapChip_p(game, this, Assets.graphics.game.Switch[0], 3, 1,1, Input.getPosition().X - X, Input.getPosition().Y - Y, 40, 40));
            if (Input.onKeyDown(Keys.C)) chips.Add(new mapChip_p(game, this, Assets.graphics.game.changePoint,4, 1, 2,Input.getPosition().X - X, Input.getPosition().Y - Y, 40, 40));
            if (Input.IsKeyDown(Keys.S)) save("test.txt");
                if (!selecting)
            {
                if (Input.IsKeyDown(Keys.Right)) { X -= (int)(0.8f * deltaTime); }
                if (Input.IsKeyDown(Keys.Left)) { X += (int)(0.8f * deltaTime); }
                if (Input.IsKeyDown(Keys.Up)) { Y += (int)(0.8f * deltaTime); }
                if (Input.IsKeyDown(Keys.Down)) { Y -= (int)(0.8f * deltaTime); }
            }
           
            if (Input.OnMouseDown(Input.LeftButton))
            {
                foreach (mapChip_p m in chips) m.selected = false;
            }
            foreach (mapChip_p m in chips) m.update(deltaTime);

            foreach (mapChip_p m in Rchips) chips.Remove(m);

            foreach (mapChip_p m in chips)
            {
                if (m.selected)
                {
                    selecting = true;
                    break;
                }
                selecting = false;
            }
            label.setLocation(-X, -Y);
            label.update(deltaTime);
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            foreach (mapChip_p m in chips) m.Draw(batch, screenAlpha);
            label.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
        public void save(String fileName)
        {

            String s ="";
            foreach(mapChip_p c in chips)
            {
                s += c.mode.ToString() + "," + c.X.ToString() + "," + c.Y.ToString() + "," + c.Width.ToString() + "," + c.Height.ToString() + "," + c.rotate.ToString() + "," + c.flagType.ToString() + "," + c.flagNum.ToString() + "," + c.mx.ToString() + "," + c.my.ToString() + "," + c.EventVisible.ToString() + "/";
            }

            //Shift JISで書き込む
            //書き込むファイルが既に存在している場合は、上書きする
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fileName,
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
            //TextBox1.Textの内容を書き込む
            sw.Write(s);
            //閉じる
            sw.Close();
        }
        public void load(String fileName)
        {
            //コース再読み込み
            //"C:\test\1.txt"をShift-JISコードとして開く
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName,
                System.Text.Encoding.GetEncoding("shift_jis"));
            //内容をすべて読み込む
            string s = sr.ReadToEnd();
            //閉じる
            sr.Close();
            String[] data;
            data = s.Split('/');
            foreach (String str in data)
            {
                String[] tmp = str.Split(',');
                if (tmp[0] == "1")
                {
                    mapChip_p chip = new mapChip_p(game, this, Assets.graphics.game.block, 1, 1, 1,int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4]));
                    chip.flagType = int.Parse(tmp[6]);
                    chip.flagNum = int.Parse(tmp[7]);
                    chip.mx = int.Parse(tmp[8]);
                    chip.my = int.Parse(tmp[9]);
                    chip.EventVisible = int.Parse(tmp[10]);
                    chips.Add(chip);
    
                }
                else if (tmp[0] == "2")
                {

                    mapChip_p chip = new mapChip_p(game, this, Assets.graphics.game.thorn[int.Parse(tmp[5])], 2, int.Parse(tmp[5]),1, int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4]));

                    chip.flagType = int.Parse(tmp[6]);
                    chip.flagNum = int.Parse(tmp[7]);
                    chip.mx = int.Parse(tmp[8]);
                    chip.my = int.Parse(tmp[9]);
                    chip.EventVisible = int.Parse(tmp[10]);
                    chips.Add(chip);

                }
                else if (tmp[0] == "3")
                {

                    mapChip_p chip = new mapChip_p(game, this, Assets.graphics.game.Switch[int.Parse(tmp[5])], 3, int.Parse(tmp[5]), 1,int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4]));

                    chip.flagType = int.Parse(tmp[6]);
                    chip.flagNum = int.Parse(tmp[7]);
                    chip.mx = int.Parse(tmp[8]);
                    chip.my = int.Parse(tmp[9]);
                    chip.EventVisible = int.Parse(tmp[10]);
                    chips.Add(chip);

                }
                else if (tmp[0] == "4")
                {

                    mapChip_p chip = new mapChip_p(game, this, Assets.graphics.game.changePoint, 4, int.Parse(tmp[5]),2, int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4]));

                    chip.flagType = int.Parse(tmp[6]);
                    chip.flagNum = int.Parse(tmp[7]);
                    chip.mx = int.Parse(tmp[8]);
                    chip.my = int.Parse(tmp[9]);
                    chip.EventVisible = int.Parse(tmp[10]);
                    chips.Add(chip);

                }
            }
        }
    }
}
