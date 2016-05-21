using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Bounce
{
    class ChipToolBarChip : uiObject
    {
        public int ChipNum = 0;
        public event EventHandler Drag;


        public ChipToolBarChip(Game1 game, Screen screen,int type, float x, float y, float width, float height, float fx = 0, float fy = 0, float fwidth = 0, float fheight = 0) : base(game, screen, null, x, y, width, height)
        {
            ChipNum = type;
            Texture = getChipTexture(type);
            origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            GotFocus += new EventHandler(this.select);
            LostFocus += new EventHandler(this.unselect);
            alpha = 0.8f;

        }
        public override void update(float delta)
        {
            if (Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && Input.OnMouseDown(Input.LeftButton) && Drag != null)
                Drag(this, EventArgs.Empty);

            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            base.Draw(batch, screenAlpha);
        }
        public void select(object sender, EventArgs e)
        {
            alpha = 1;
        }
        public void unselect(object sender, EventArgs e)
        {
            alpha = 0.8f;
        }
        public Texture2D getChipTexture(int num)
        {
            Texture2D res = null;
            switch (num)
            {
                case mapChip.BLOCK:
                    res = Assets.graphics.game.block;
                    break;
                case mapChip.THORN:
                    res = Assets.graphics.game.thorn[0];
                    break;
                case mapChip.SWITCH:
                    res = Assets.graphics.game.Switch[0];
                    break;
                case mapChip.SHPOINT:
                    res = Assets.graphics.game.changePoint;
                    break;
                case mapChip.WARPPOINT:
                    res = Assets.graphics.game.warpPoint[0];
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
    }
}
