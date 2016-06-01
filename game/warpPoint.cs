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
    class warpPoint:LevelObject
    {
        const int size = 40;

        public bool Warping=false;
        public bool isArrive = false;
        public bool disableWarp = false;

        public warpPoint(Game1 game, Screen screen, eventData ed,float sp, int rotate, float x, float y, float width, float height) : base(game, screen, ed, x, y, width, height)
        {
            this.rotate = rotate;
            specialData = sp;
            this.Texture = getChipTexture(mapChip.WARPPOINT, rotate);

            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (!disableWarp) Texture = getChipTexture(mapChip.WARPPOINT, rotate);
            else Texture=Assets.graphics.game.block;

        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            
            batch.Begin(transformMatrix: game.GetScaleMatrix(), blendState: BlendState.Additive);
            if (animatorLayor == 0)
            {
                foreach (GameObjectAnimator a in animator)
                {
                    a.Draw(batch, screenAlpha);
                }
            }
            batch.End();
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            int i = 0;
            int j = 0;
            for (i = 0; i < Width / size; i++)
            {
                for (j = 0; j < Height / size; j++)
                {
                    batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha);
                }

            }



            batch.End();
            batch.Begin(transformMatrix: game.GetScaleMatrix(), blendState: BlendState.Additive);
            if (animatorLayor == 1)
            {
                foreach (GameObjectAnimator a in animator)
                {
                    a.Draw(batch, screenAlpha);
                }
            }
            batch.End();
            smokeDraw(batch, screenAlpha, 40, true);
        }


        public override void flagEvent(object sender, EventArgs e)
        {
            if (eventData.type == 1)
            {


                if ((((eventData_1)eventData).isLoop))
                {


                    if (((eventData_1)eventData).mode == 0)
                    {
                        type1tmp = true;
                    }
                    if (((eventData_1)eventData).mode == 1)
                    {
                        type1tmp = false;
                    }



                }
                else
                {
                    if (((eventData_1)eventData).mode == 0)
                    {
                        disableWarp = true;
                    }
                    else if (((eventData_1)eventData).mode == 1)
                    {
                        disableWarp = false;
                    }
                }
            }
            else if (eventData.type == 2)
            {
                double distant = System.Math.Sqrt(System.Math.Pow((((eventData_2)eventData).X - X), 2) + System.Math.Pow((((eventData_2)eventData).Y - Y), 2)) / 40;
                //System.Windows.Forms.MessageBox.Show(distant.ToString());
                animator[0].setDelay(((eventData_2)eventData).interval);

                animator[0].start(GameObjectAnimator.SLIDE, new float[] { 0, ((eventData_2)eventData).X, ((eventData_2)eventData).Y, 1 / (((eventData_2)eventData).speed) * (float)distant, -1 });
                type2tmp = true;

                // DebugConsole.write("イベント作動！！！！！！！");


            }


        }

        public override void eventUpdate(float deltaTime)
        {
            if (eventData.type == 1)
            {
                if ((((eventData_1)eventData).isLoop))
                {

                    if (type1tmp)
                    {

                        tmpTime += deltaTime / 1000;
                        if (tmpTime >= ((eventData_1)eventData).interval)
                        {
                            disableWarp = false;
                            
                            type1tmp = false;
                            tmpTime = 0;
                        }


                    }
                    else
                    {
                        tmpTime += deltaTime / 1000;
                        if (tmpTime >= ((eventData_1)eventData).interval)
                        {
                            disableWarp = true;
                            type1tmp = true;
                            tmpTime = 0;
                        }
                    }
                }

            }
            else if (eventData.type == 2)
            {
                if ((((eventData_2)eventData).isLoop))
                {



                    if (!animator[0].isAnimate && !animator[0].isAnimatedelay)
                    {

                        if (type2tmp)
                        {
                            double distant = System.Math.Sqrt(System.Math.Pow(X - type2tmpX, 2) + System.Math.Pow((Y - type2tmpY), 2)) / 40;
                            animator[0].start(GameObjectAnimator.SLIDE, new float[] { 0, type2tmpX, type2tmpY, 1 / (((eventData_2)eventData).speed) * (float)distant, -1 });
                            type2tmp = false;
                            // DebugConsole.write(type2tmpX.ToString());
                        }
                        else
                        {
                            double distant = System.Math.Sqrt(System.Math.Pow((((eventData_2)eventData).X - X), 2) + System.Math.Pow((((eventData_2)eventData).Y - Y), 2)) / 40;
                            animator[0].start(GameObjectAnimator.SLIDE, new float[] { 0, ((eventData_2)eventData).X, ((eventData_2)eventData).Y, 1 / (((eventData_2)eventData).speed) * (float)distant, -1 });
                            type2tmp = true;
                            //   DebugConsole.write(type2tmp.ToString());
                        }





                    }

                }


            }

            

        }


    }
}
