using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce.editor
{
    class NumUpDown:uiObject
    {
        GraphicalGameObject back;
        public SimpleButton up, down;
        public TextObject text;
        int Oldwheel;

        public float value=0;
        public float step=1;
        public float max = 100;
        public float min = 0;


        public NumUpDown(Game1 game, Screen screen,Color color, float x, float y, float width, float height) : base(game, screen, null, x, y, width, height)
        {
            back = new GraphicalGameObject(game, parent, Assets.graphics.ui.back_textbox, (int)X + height + 10, (int)Y + 10, width - height * 2 - 20, height - 20);
            up = new SimpleButton(game, parent, Assets.graphics.ui.arrowR, (int)X + height + 10 + width - height * 2 - 20, (int)Y, height, height);
            down = new SimpleButton(game, parent, Assets.graphics.ui.arrowL, (int)X, (int)Y, height, height);
            text = new TextObject(game, parent, Assets.graphics.ui.font, "0", color, (int)X+height + 15, (int)Y+15);

            up.Enter += new EventHandler((sender, e) => { value += step;if (value > max) value = min; if (value < min) value = max; text.text = value.ToString(); });
            down.Enter += new EventHandler((sender, e) => { value -= step; if (value > max) value = min; if (value < min) value = max; text.text = value.ToString(); });
        }
        public override void update(float delta)
        {
            base.update(delta);
            back.update(delta);
            up.update(delta);
            down.update(delta);
            text.update(delta);

            back.alpha = alpha;
            up.alpha = alpha;
            down.alpha = alpha;
            text.alpha = alpha;
            if (Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)))
            {

                value += (int)((Input.getWheel() - Oldwheel) / 120);


                Oldwheel = Input.getWheel();

            }
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            
            back.Draw(batch, screenAlpha);
            up.Draw(batch, screenAlpha);
            down.Draw(batch, screenAlpha);
            text.Draw(batch, screenAlpha);

        }
    }
}
