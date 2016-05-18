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
    class LevelObject:GraphicalGameObject
    {

        public bool _flag;
        public bool _startFlagDelay;
        public float FlagDelayTime;
        public eventData eventData;
        public event EventHandler flagChanged;
        public bool enable=true;
        new worldScreen parent;

        int event1Loop_tmp=0;

        public bool flag
        {
            get { return _flag; }
            set {
                _flag = value;
                if (eventData.delay == 0) flagChanged?.Invoke(this, EventArgs.Empty);
                else _startFlagDelay = true;
            }
        }
        public LevelObject(Game1 game, Screen screen ,eventData ed, float x, float y, float width, float height) : base(game, screen, null, x, y, width, height)
        {
            parent = (worldScreen)screen;
            eventData=ed;
            flagChanged += new EventHandler(this.flagEvent);
            if (eventData.type == 1)
            {
                if (((eventData_1)eventData).mode == 1) enable = false;
            }
        }
        public override void update(float delta)
        {
           if(eventData.num!=0) if (parent.flags[eventData.num] && !flag) flag = true;

            if (_startFlagDelay)
            {
                FlagDelayTime += delta / 1000;
                if (FlagDelayTime >= eventData.delay)
                {
                    flagChanged?.Invoke(this, EventArgs.Empty);
                    _startFlagDelay = false;
                }
            }

            base.update(delta);
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
                    res = Assets.graphics.game.block;
                    break;
                case mapChip.GUMPOINT:
                    res = Assets.graphics.game.block;
                    break;
                case mapChip.GOAL:
                    res = Assets.graphics.game.goal;
                    break;
            }


            return res;

        }

        public virtual void flagEvent(object sender,EventArgs e)
        {
            if (eventData.type == 1)
            {

                if (((eventData_1)eventData).mode == 0)
                {
                    enable = false;
                }
                else if (((eventData_1)eventData).mode == 1)
                {
                    enable = true;
                }
                if ((((eventData_1)eventData).isLoop)){

                    addAnimator(1);
                    animator[0].setDelay((((eventData_1)eventData).interval));
                    animator[0].FinishAnimation += new EventHandler((sender2, e2) => {
                        if (event1Loop_tmp == 1) event1Loop_tmp = 0;
                        else event1Loop_tmp = 1;
                        animator[0].start(GameObjectAnimator.fadeInOut, new float[] { event1Loop_tmp, 0 });

                    });
                    if (((eventData_1)eventData).mode == 0)
                    {
                        event1Loop_tmp = 1;
                        animator[0].start(GameObjectAnimator.fadeInOut, new float[] { event1Loop_tmp, 0 });
                    }
                    if (((eventData_1)eventData).mode == 1)
                        event1Loop_tmp = 0;
                    animator[0].start(GameObjectAnimator.fadeInOut, new float[] { event1Loop_tmp, 0 });

                }

            }
            else if (eventData.type == 2)
            {

                animator[0].start(GameObjectAnimator.SLIDE, new float[] { 0, ((eventData_2)eventData).X, ((eventData_2)eventData).Y, ((eventData_2)eventData).speed, -1 });

                DebugConsole.write("イベント作動！！！！！！！");


            }
        }

    }
}
