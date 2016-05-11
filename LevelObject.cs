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
        new worldScreen parent;


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
                    res = Assets.graphics.game.block;
                    break;
            }


            return res;

        }
    }
}
