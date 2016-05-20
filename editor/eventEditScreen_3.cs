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
    class eventEditScreen_3:Screen
    {
        NumUpDown mode;
        TextObject mL;
        eventEditScreen eventEditScreen;

        public eventEditScreen_3(Game1 game, eventEditScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            eventEditScreen = screen;
            mode = new NumUpDown(game, this, Color.Black, 0, 30, 200, 50);
            mL = new TextObject(game, this, Assets.graphics.ui.font, "作動モード", Color.Black, 60, 0);

            mode.max = 2;

            mode.changed += new EventHandler(((sender, e) =>
              {
                  if (mode.value == 0)
                  {
                      mode.text.text = "有効化";
                  }else if (mode.value == 1)
                  {
                      mode.text.text = "無効化";
                  }
                  else if (mode.value == 2)
                  {
                      mode.text.text = "トグル";
                  }
                  ((eventData_3)eventEditScreen.chip.eventData).mode = (int)mode.value;

              }));
            mode.value = ((eventData_3)eventEditScreen.chip.eventData).mode;
            mode.text.text = mode.value.ToString();
            if (mode.value == 0)
            {
                mode.text.text = "有効化";
            }
            else if (mode.value == 1)
            {
                mode.text.text = "無効化";
            }
            else if (mode.value == 2)
            {
                mode.text.text = "トグル";
            }


            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            mode.update(deltaTime);
            mL.update(deltaTime);


        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            mode.Draw(batch, screenAlpha);
            mL.Draw(batch, screenAlpha);

        }

    }
}
