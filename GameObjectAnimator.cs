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
    public class GameObjectAnimator
    {
        GraphicalGameObject o;
        Game1 game;
        int type;
        float[] option;
        int step = 0;
        public float time;
        public float time2;
        int frame = 0;
        float[] tmp;//定義 x,y,w,h, アニメーターレイヤ x,y,w,h,alpha
        public bool isAnimate = false;
        bool isAnimatedelay = false;
        float vx = 0;
        float vy = -2;
        int c = 0;
        int c2 = 0;
        float delay = 0;
        float limit = 0;

        public const int FLASH = 1;//Option 1 表示長さ 2フェードアウト 3非表示長さ 4フェードイン長さ　 5適用レイヤー -1本体 0~アニメーター
        public const int GLOW = 2;//Option 1 動きあり1  2 拡大率 3透明度 4標準長さ  5広がり長さ 6広がり保持時間 7縮まる長さ 
        public const int SLIDE = 3;//Option 1 登場:0 移動:1  (2,3移動先X,Y)  4移動時間(目標) 5アニメーターバッファ適用レイヤー 5加速v 6減速v
        public const int fadeInOut = 4;//Option  1 0:in 1:out  2長さ 3limit
        public const int EXPLOSION = 5;

        public event EventHandler FinishAnimation;

        public GameObjectAnimator(GraphicalGameObject o, Game1 game)
        {
            this.o = o;
            this.game = game;
        }
        public void start(int type, float[] option)
        {
            this.type = type;
            this.option = option;
            Init();
            if (delay == 0) this.isAnimate = true;
            if (delay != 0) this.isAnimatedelay = true;

        }
        public void stop()
        {
            time = 0;
            time2 = 0;
            isAnimate = false;
            step = 0;
            c = 0;
            vx = 0;
            vy = 0;
            if (FinishAnimation != null) FinishAnimation(this, EventArgs.Empty);
        }
        public void Init()
        {
            switch (type)
            {
                case FLASH:



                    break;



                case GLOW:

                    tmp = new float[] { (float)o.X, (float)o.Y, (float)o.Width, (float)o.Height, (float)o.X, (float)o.Y, (float)o.Width, (float)o.Height, 1F };

                    break;




                case SLIDE:
                    tmp = new float[] { (float)o.X, (float)o.Y, (float)o.Width, (float)o.Height };
                    if (option[0] == 1)
                    {
                        float s = 0;
                        float t = 0;

                        while (s < Math.Abs(option[1] - tmp[0]))
                        {
                            t += option[5];
                            s += t;
                            c++;
                        }

                        if (option[1] - tmp[0] > 0) vx = t;
                        if (option[1] - tmp[0] < 0) vx = -t;
                    }
                    else if (option[0] == 2)
                    {
                        float s = 0;
                        float t = 0;

                        while (s < Math.Abs(option[2] - tmp[1]))
                        {
                            t += option[5];
                            s += t;
                            c++;
                        }

                        if (option[2] - tmp[1] > 0) vy = t;
                        if (option[2] - tmp[1] < 0) vy = -t;

                    }
                    break;
                case fadeInOut:
                    if (option[0] == 0) o.alpha = 0;

                    if (option[0] == 1) o.alpha = 1;
                    break;
            }
        }
        public void setDelay(float d)
        {
            delay = d;
        }
        public void setLimit(float d)
        {
            limit = d;
        }
        public void update(float deltaTime)
        {
            if (isAnimatedelay)
            {
                time += (float)deltaTime / 1000;
                if (delay < time)
                {
                    time = 0;
                    isAnimatedelay = false;
                    isAnimate = true;
                }
            }
            if (!isAnimate) return;
            time2 += deltaTime / 1000;
            if (limit!=0 && time2 > limit) this.stop();
            switch (type)
            {
                case FLASH:
                    Flash(deltaTime);
                    break;
                case GLOW:
                    Glow(deltaTime);
                    break;
                case SLIDE:
                    Slide(deltaTime);
                    break;
                case fadeInOut:
                    fade(deltaTime);
                    break;
                case EXPLOSION:
                    explosion(deltaTime);
                    break;
            }
        }
        public void Draw(SpriteBatch batch, float screenAlpha)
        {

            if (!isAnimate) return;

            switch (type)
            {
                case FLASH:



                    break;
                case GLOW:

                    batch.Draw(o.Texture, new Rectangle((int)tmp[4]+o.parent.X, (int)tmp[5]+o.parent.Y, (int)tmp[6], (int)tmp[7]), Color.White * option[2] * screenAlpha * tmp[8]);

                    break;
                case EXPLOSION:


                  //  batch.Draw(texture: game.assets.exAnimaton, destinationRectangle: new Rectangle((int)o.actX, (int)o.actY, (int)o.Width, (int)o.Height), sourceRectangle: new Rectangle(frame * 96, 0, 96, 96));

                    Console.WriteLine("frame=" + frame.ToString());



                    break;
            }
        }
        private void Flash(double deltaTime)
        {

            switch (step)
            {
                case 0:

                    time += (float)deltaTime / 1000;
                    if (option[4] == -1) { o.alpha = 1F; }
                    else { o.animator[(int)option[4]].tmp[8] = 1F; }
                    if (time >= option[0]) { step = 1; time = 0; }

                    break;
                case 1:
                    if (option[1] == 0) { step = 2; }
                    if (option[4] == -1) { o.alpha -= ((float)deltaTime / 1000) * (1 / option[1]); }
                    else { o.animator[(int)option[4]].tmp[8] -= ((float)deltaTime / 1000) * (1 / option[1]); }
                    time += (float)deltaTime / 1000;
                    if (time >= option[1]) { step = 2; time = 0; }

                    break;
                case 2:
                    time += (float)deltaTime / 1000;
                    if (option[4] == -1) { o.alpha = 0F; }
                    else { o.animator[(int)option[4]].tmp[8] = 0F; }
                    if (time >= option[2]) { step = 3; time = 0; }

                    break;
                case 3:
                    if (option[1] == 0) { step = 2; }
                    if (option[4] == -1) { o.alpha += ((float)deltaTime / 1000) * (1 / option[3]); }
                    else { o.animator[(int)option[4]].tmp[8] += ((float)deltaTime / 1000) * (1 / option[3]); }
                    time += (float)deltaTime / 1000;
                    if (time >= option[3]) { step = 0; time = 0; }

                    break;
            }
        }

        private void Glow(double deltaTime)
        {
            if (option[0] == 0) return;
            switch (step)
            {
                case 0:
                    // tmp = new float[] { (float)o.X, (float)o.Y, (float)o.Width, (float)o.Height, (float)o.X, (float)o.Y, (float)o.Width, (float)o.Height,1.0F };

                    time += (float)deltaTime / 1000;
                    tmp[4] = tmp[0];
                    tmp[5] = tmp[1];
                    if (time >= option[3]) { step = 1; time = 0; }

                    break;
                case 1:
                    time += (float)deltaTime / 1000;
                    tmp[4] = tmp[0] - ((time * (1 / option[4]) * (tmp[2] * option[1])) / 2);
                    tmp[5] = tmp[1] - ((time * (1 / option[4]) * (tmp[3] * option[1])) / 2);
                    tmp[6] = tmp[2] + (time * (1 / option[4]) * (tmp[2] * option[1]));
                    tmp[7] = tmp[3] + (time * (1 / option[4]) * (tmp[3] * option[1]));

                    if (time >= option[4]) { step = 2; time = 0; }

                    break;
                case 2:
                    time += (float)deltaTime / 1000;

                    if (time >= option[5]) { step = 3; time = 0; }
                    tmp[4] = tmp[0] - (option[1] * tmp[2] / 2);
                    tmp[5] = tmp[1] - (option[1] * tmp[3] / 2);
                    tmp[6] = tmp[2] + (option[1] * tmp[2]);
                    tmp[7] = tmp[3] + (option[1] * tmp[3]);
                    break;
                case 3:
                    time += (float)deltaTime / 1000;
                    //tmp[4] = (tmp[0] - (option[1] * tmp[2]/2)) +  ((time * (1 / option[4]) * (tmp[2] * option[1])) / 2);
                    //tmp[5] = (tmp[1] - (option[1] * tmp[3]/2)) + ((time * (1 / option[4]) * (tmp[3] * option[1])) / 2);
                    tmp[4] = tmp[0] - (option[1] * tmp[2] / 2) + time * (1 / option[6]) * (tmp[2] * option[1] / 2);
                    tmp[5] = tmp[1] - (option[1] * tmp[3] / 2) + time * (1 / option[6]) * (tmp[3] * option[1] / 2);
                    tmp[6] = (tmp[2] * (option[1] + 1F)) - time * (1 / option[6]) * (tmp[2] * option[1]);
                    tmp[7] = (tmp[3] * (option[1] + 1F)) - time * (1 / option[6]) * (tmp[3] * option[1]);


                    if (time >= option[6]) { step = 0; time = 0; }
                    break;


            }





        }

        public void Slide(double deltaTime)
        {
            if (option[0] == 0)
            {


                time += (float)(deltaTime / 1000);
                o.X = tmp[0] + (option[1] - tmp[0]) / option[3] * time;
                o.Y = tmp[1] + (option[2] - tmp[1]) / option[3] * time;
                if (option[4] != -1) o.animator[(int)option[4]].tmp[0] = tmp[0] + (option[1] - tmp[0]) / option[3] * time;
                if (option[4] != -1) o.animator[(int)option[4]].tmp[1] = tmp[1] + (option[2] - tmp[1]) / option[3] * time;
                if (time > option[3]) { this.stop(); }




            }
            else if (option[0] == 1)
            {


                time += (float)(deltaTime / 1000);
                o.X += (vx);
                if (vx > 0) vx -= option[5];
                if (vx < 0) vx += option[5];
                c2++;
                if (c == c2) this.stop();
                if (option[4] != -1) o.animator[(int)option[4]].tmp[0] = (float)o.X;

            }
            else if (option[0] == 2)
            {
                time += (float)(deltaTime / 1000);
                o.Y += (vy);
                if (vy > 0) vy -= option[5];
                if (vy < 0) vy += option[5];
                c2++;
                if (c == c2) this.stop();
                if (option[4] != -1) o.animator[(int)option[4]].tmp[1] = (float)o.Y;
                Console.WriteLine("XXXXX:" + o.Y.ToString() + "VX" + vy.ToString());
            }


        }
        public void fade(double deltaTime)
        {
            if (option[0] == 0)
            {
                o.alpha += ((float)deltaTime / 1000) * (1 / option[1]);
                if(option.Length == 2)
                {
                    if (o.alpha >= 1) this.stop();
                }
                else if (option.Length == 3)
                {
                    if (o.alpha >= option[2]) this.stop();
                }
               
            }
            else if (option[0] == 1)
            {
                o.alpha -= ((float)deltaTime / 1000) * (1 / option[1]);
                if (option.Length == 2)
                {
                    if (o.alpha <= 0) this.stop();
                }
                else if (option.Length == 3)
                {
                    if (o.alpha <= option[2]) this.stop();
                }
                   
            }
        }

        public void explosion(float deltaTime)
        {
            //System.Windows.Forms.MessageBox.Show("update");
            o.alpha = 0f;
            time += deltaTime / 1000;
            if (time > 0.05f)
            {
                frame++;
                time = 0;
                
            }
            if (frame == 18) { o.alpha = 1f;frame = 0; this.stop(); }
        }
    }
}
