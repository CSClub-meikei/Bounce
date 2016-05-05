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
    class EditorScreen:Screen
    {
        public List<mapChip> chips=new List<mapChip>();
        public List<mapChip> Rchips = new List<mapChip>();
        public TextObject label;
        public bool selecting;
        public EditorScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            label = new TextObject(game, this, Assets.graphics.ui.font, "", Color.White, 0, 0);
            setUIcell(1, 1);
            load("test.txt");
        }
        public override void update(float deltaTime)
        {
            if (Input.onKeyDown(Keys.A)) chips.Add(new mapChip(game, this, Assets.graphics.game.block,1, Input.getPosition().X - X, Input.getPosition().Y - Y, 40, 40));
            if (Input.onKeyDown(Keys.Z)) chips.Add(new mapChip(game, this, Assets.graphics.game.thorn[0], 2, Input.getPosition().X - X, Input.getPosition().Y - Y, 40, 40));
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
                foreach (mapChip m in chips) m.selected = false;
            }
            foreach (mapChip m in chips) m.update(deltaTime);

            foreach (mapChip m in Rchips) chips.Remove(m);

            foreach (mapChip m in chips)
            {
                if (m.selected)
                {
                    selecting = true;
                    break;
                }
                selecting = false;
            }
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            foreach (mapChip m in chips) m.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
        public void save(String fileName)
        {

            String s ="";
            foreach(mapChip c in chips)
            {
                s += c.mode.ToString() + "," + c.X.ToString() + "," + c.Y.ToString() + "," + c.Width.ToString() + "," + c.Height.ToString() + "," + c.rotate.ToString() + "/";
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
                    chips.Add(new mapChip(game, this, Assets.graphics.game.block, 1, int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4])));
    
                }
                else if (tmp[0] == "2")
                {
                    chips.Add(new mapChip(game, this, Assets.graphics.game.thorn[int.Parse(tmp[5])], 2, int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]), int.Parse(tmp[4])));
                }
            }
        }
    }
}
