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
    class SerifTextObjectOld:GraphicalGameObject
    {
        SpriteFont font;
        float speed;
        public string[] text;
        Color color;

        public int scroll;
        public const int maxLines=4;
        public const int pt=35;
        int index;
        int line;
        float tmpTime;

        public bool play = false;

        public event EventHandler finish;
        public event EventHandler finishAll;



        public SerifTextObjectOld(Game1 game, Screen screen, SpriteFont font, String text, Color color,float speed, float x, float y) :base(game,screen,null,x,y,0,0)
        {
            this.font = font;
            this.color = color;
            this.speed = speed;
            this.text = text.Split(new string[] { "||" },StringSplitOptions.None);
            addAnimator(1);

        }
        public override void update(float delta)
        {

            if (!play) return;
            if (index == text[line].Length ) return;

                tmpTime += delta / 1000;
            if(tmpTime >= speed)
            {
                tmpTime = 0;
                index++;
                if (index == text[line].Length)
                {
                    if (finishAll != null && text.Length == line - 1) finishAll(this, EventArgs.Empty);
                    if (finish != null) finish(this, EventArgs.Empty);
                }

                
            }




            string str = "";
            int i = 0;
            for (i = scroll; i < line; i++) str += text[i];

            if (maxLines <= CountChar(str + text[line].Substring(0, index), "\n"))
            {
                scroll++;
                animator[0].start(GameObjectAnimator.SLIDE, new float[] { 2, (int)X, (int)Y - pt, 0.2f, -1, 1, 1 });
                // System.Windows.Forms.MessageBox.Show("scroll");
            }


            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            if (line == 0)
            {
                 batch.DrawString(font, text[line].Substring(0, index), new Vector2((float)actX, (float)actY), color);
            }else
            {
                string str="";
                int i = 0;
                for (i = scroll; i < line; i++) str += text[i];


                batch.DrawString(font, str + text[line].Substring(0, index), new Vector2((float)actX, (float)actY), color);
            }
           
            batch.End();

        }
        public void next()
        {

            line++;
            index = 0;
           
        }

        public void init()
        {
            index = 0;
            tmpTime = 0;
            scroll = 0;

        }

        // 文字の出現回数をカウント
        public int CountChar(string s, string c)
        {
            return s.Length - s.Replace(c, "").Length;
        }
    }
}
