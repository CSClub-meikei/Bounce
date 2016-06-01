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
    class LevelObject : GraphicalGameObject
    {

        public bool _flag;
        public bool _startFlagDelay;
        public bool endFlagDelay;
        public bool flagtmp;
        public float FlagDelayTime;
        public eventData eventData;
        public float specialData;
        public event EventHandler flagChanged;

        public int rotate;

        public float tmpTime=0f;
        public bool type1tmp;
        public bool type2tmp;
        public int type2tmpX, type2tmpY;


        public bool enable = true;
        new worldScreen parent;

        float effectSpeed = 0.01f;
        int frame=0;
        bool effecting;
        float effecttmp;

        public bool flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                if (eventData.delay == 0)
                {
                    endFlagDelay = true;
                     flagChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    endFlagDelay = false;
                     _startFlagDelay = true;
                }
            }
        }
        public LevelObject(Game1 game, Screen screen, eventData ed, float x, float y, float width, float height) : base(game, screen, null, x, y, width, height)
        {
            parent = (worldScreen)screen;
            eventData = ed;
            flagChanged += new EventHandler(this.flagEvent);
            if (eventData.type == 1)
            {
                if (((eventData_1)eventData).mode == 1) enable = false;
            }
            type2tmpX = (int)X;
            type2tmpY = (int)Y;
            addAnimator(1);
        }
        public override void update(float delta)
        {
            if (parent.flags[eventData.num] && !flag) flag = true;

            if (_startFlagDelay)
            {
                FlagDelayTime += delta / 1000;
                if (FlagDelayTime >= eventData.delay)
                {
                    endFlagDelay = true;
                    flagChanged?.Invoke(this, EventArgs.Empty);
                    _startFlagDelay = false;
                }
            }
            if(flag&&endFlagDelay)eventUpdate(delta);
            if (effecting) smokeEffect(delta);
            base.update(delta);
        }
        public void smokeEffect(float deltaTime)
        {
            DebugConsole.write("update" + frame.ToString());
            effecttmp += deltaTime / 1000;
            if (effecttmp >= effectSpeed)
            {
                effecttmp = 0;
                frame++;
                if (frame == 40) effecting = false;
            }
        }
        public void smokeDraw(SpriteBatch batch,float screenAlpha,int size,bool drawLoop)
        {
            if (effecting)
            {
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                if (drawLoop)
                {
                    int i = 0;
                    int j = 0;
                    for (i = 0; i < Width / size; i++)
                    {
                        for (j = 0; j < Height / size; j++)
                        {
                            batch.Draw(Assets.graphics.game.smoke[frame], new Rectangle((int)actX - 30+i*size, (int)actY - 30+j*size, 100, 100), Color.White * alpha * screenAlpha);
                        }

                    }


                }
                else
                {
                    batch.Draw(Assets.graphics.game.smoke[frame], new Rectangle((int)actX - 30, (int)actY - 30, 100, 100), Color.White * alpha * screenAlpha);
                }
                
                batch.End();
                DebugConsole.write("animating");
            }
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
          
            
            if (!enable) return;
            base.Draw(batch, screenAlpha);
           
        }
        public Texture2D getChipTexture(int num, int rotate)
        {
            Texture2D res = null;
            switch (num)
            {
                case mapChip.BLOCK:
                    res = Assets.graphics.game.block;
                    break;
                case mapChip.THORN:
                    res = Assets.graphics.game.thorn[rotate];
                    break;
                case mapChip.SWITCH:
                    res = Assets.graphics.game.Switch[rotate];
                    break;
                case mapChip.SHPOINT:
                    res = Assets.graphics.game.changePoint;
                    break;
                case mapChip.WARPPOINT:
                    res = Assets.graphics.game.warpPoint[rotate];
                    break;
                case mapChip.GUMPOINT:
                    res = Assets.graphics.game.block;
                    break;
                case mapChip.GOAL:
                    res = Assets.graphics.game.goal;
                    break;
                case mapChip.ACCEL:
                    res = Assets.graphics.game.accel[0];
                    break;
                case mapChip.SAVEPOINT:
                    res = Assets.graphics.game.savePoint[0];
                    break;
            }


            return res;

        }

        public virtual void flagEvent(object sender, EventArgs e)
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
                        enable = false;
                    }
                    else if (((eventData_1)eventData).mode == 1)
                    {
                        enable = true;
                        effecting = true;
                        animator[0] = new GameObjectAnimator(this, game);
                        animator[0].setDelay(0.1f);
                        animator[0].start(GameObjectAnimator.fadeInOut,new float[] { 0, 0.1f });
                        //DebugConsole.write("イベント発生");
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
            else if (eventData.type == 4)
            {
                parent.animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f, 0.5f });


                eventData_4 ed = (eventData_4)eventData;
                adviceScreen ns = new adviceScreen(game, ed.character, ed.face, ed.msg);
                ns.screenAlpha = 0;
                ns.animator.setDelay(0.5f);
                ns.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.2f,0.8f });
                ns.closed += new EventHandler((sender2, e2) => {
                    parent.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.1f });
                    parent.Status = worldScreen.RUNNING;
                });
                parent.Status = worldScreen.PAUSE;
                game.screens.Add(ns);
            }



        }

        public virtual void eventUpdate(float deltaTime)
        {
            if (eventData.type == 1)
            {
                if ((((eventData_1)eventData).isLoop))
                {

                    if (type1tmp)
                    {

                        tmpTime += deltaTime / 1000;
                        if(tmpTime>= ((eventData_1)eventData).interval)
                        {
                            enable = false;
                            type1tmp = false;
                            tmpTime = 0;
                        }


                    }else
                    {
                        tmpTime += deltaTime / 1000;
                        if (tmpTime >= ((eventData_1)eventData).interval)
                        {
                            enable = true;
                            type1tmp = true;
                            tmpTime = 0;
                        }
                    }
                }
                
                }else if (eventData.type == 2)
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
