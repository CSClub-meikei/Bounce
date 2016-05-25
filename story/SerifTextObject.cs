using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Bounce.story
{
    class SerifTextObject:GraphicalGameObject
    {
        SpriteFont font;
        float speed;
        public List<storyData_1> text = new List<storyData_1>();
        Color color;


        public int scroll;
        public const int maxLines = 4;
        public const int pt = 50;
        int index;
        int line;
        float tmpTime;

        public bool play = false;

        public event EventHandler finish;
        public event EventHandler finishAll;

        public static int oy;

        public SerifTextObject(Game1 game, Screen screen, SpriteFont font, String text, Color color, float speed, float x, float y) :base(game,screen,null,x,y,0,0)
        {
            this.font = font;
            this.color = color;
            this.speed = speed;
            oy = (int)Y;
            addAnimator(1);
           
        }
        public override void update(float delta)
        {
            base.update(delta);
            if (text.Count == 0) return;
            if (line > text.Count - 1 ) return;
            if (index == text[line].serif.Length) return;


            tmpTime += delta / 1000;
            if (tmpTime >= text[line].spped)
            {
                tmpTime = 0;
                index++;
                if (index == text[line].serif.Length)
                {
                    //if (finishAll != null && text.Count == line - 1) finishAll(this, EventArgs.Empty);
                    if (finish != null) finish(this, EventArgs.Empty);



                    
                }
            }
            string str = "";
            int i=0;
            if (line == 0)
            {
                str = text[line].serif.Substring(0, index);
            }
            else
            {
                for (i = scroll; i < line-1; i++)
                {
                    str += text[i].serif;
                }
                str += text[line].serif.Substring(0, index);

            }


            if (maxLines == CountChar(str, "\n")+1)
            {
                int tmp = (int)Y;



                scroll++;
                //oy = Y;
                animator[0] = new GameObjectAnimator(this,game);
                animator[0].FinishAnimation += new EventHandler((sender, e) => { Y = oy - pt * scroll;});
                animator[0].start(GameObjectAnimator.SLIDE, new float[] { 2, (int)X, (int)Y - pt, 0.2f, -1, 2, 2 });
                
                // System.Windows.Forms.MessageBox.Show("scroll");
            }





        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            if (text.Count == 0) return;
            if (text[line].ch == 1)
            {
                color = Color.Blue;
            }else
            {
                color = Color.Red;
            }
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            if (line == 0)
            {
                batch.DrawString(font, text[line].serif.Substring(0, index), new Vector2((float)actX, (float)actY), color);
            }
            else
            {
                string str = "";
                int i = 0;
                for (i = scroll; i < line; i++) str += text[i].serif;


                batch.DrawString(font, new string('\n',scroll) + str + text[line].serif.Substring(0, index), new Vector2((float)actX, (float)actY), color);
            }

            batch.End();
        }
        public void setData(storyData_1 str)
        {

            if (str.ch == 1)
            {
                str.serif = str.serif.Insert(0, "CH1 : ");
            }else
            {
                str.serif = str.serif.Insert(0, "unknown : ");
            }

            text.Add(str);
            
        }
        public void next()
        {
            if(text.Count-1 > line)
            {
                line++;
                index = 0;
            }else
            {

            }
            
        }
        public void clear()
        {
            text.Clear();
            line = 0;
            index = 0;
            speed = 0;
            tmpTime = 0;
            scroll = 0;
            Y = oy;
        }
        // 文字の出現回数をカウント
        public int CountChar(string s, string c)
        {
            return s.Length - s.Replace(c, "").Length;
        }
    }
}
