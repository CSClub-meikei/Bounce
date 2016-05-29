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
    class Switch: LevelObject
    {
        public bool _IsPush;
       
        public bool IsPush
        {
            get { return _IsPush; }
            set { if (!_IsPush && value)  if(specialData!=1)Assets.soundEffects.pushSwitch.Play(); _IsPush = value;  }
        }
        

        public Switch(Game1 game, Screen screen,eventData ed,int sp, int rotate, float x, float y, float width, float height) : base(game, screen, ed, x, y, width, height)
        {
            this.Texture = getChipTexture(mapChip.SWITCH, rotate);
            this.rotate = rotate;
            this.specialData = sp;
            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
        }
        public void push(float deltaTime)
        {
            this.Texture = Assets.graphics.game.Switch_p[rotate];
            tmpTime += deltaTime / 1000;
            if (tmpTime >= 0.5f)
            {
                IsPush = false;
                this.Texture = getChipTexture(mapChip.SWITCH, rotate);
                tmpTime = 0;
            }
        }
        public override void update(float delta)
        {
            base.update(delta);


            if (IsPush) push(delta);

        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            if (specialData == 1) return;
            base.Draw(batch, screenAlpha);
        }
        

    }
}
