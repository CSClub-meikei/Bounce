using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bounce.uiControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Bounce.editor
{
    class eventEditScreen_1:Screen
    {
        NumUpDown mode, interval;
        checkBox isLoop;
        TextObject chLabel,modeLabel,inLabel;
        eventEditScreen eventEditScreen;

        public eventEditScreen_1(Game1 game, eventEditScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            mode = new NumUpDown(game, this, Color.Black, 0,40, 200, 50);
            mode.max = 1;
            mode.text.text = "消滅";
            mode.changed += new EventHandler((sender, e) =>
              {
                  if (mode.value == 0)
                  {
                      mode.text.text = "消滅";
                  }
                  else
                  {
                      mode.text.text = "出現";
                  }
              });
           
            isLoop = new checkBox(game, this, 20, 90, 40, 40);
            chLabel = new TextObject(game,this, Assets.graphics.ui.font, "繰り返す", Color.Black, 80, 100);
            modeLabel = new TextObject(game, this, Assets.graphics.ui.font, "作動モード", Color.Black, 60, 20);
           
            inLabel = new TextObject(game, this, Assets.graphics.ui.font, "間隔", Color.Black, 60, 140);
            interval = new NumUpDown(game, this, Color.Black, 0, 160, 200, 50);
            interval.step = 0.1f;
            
            eventEditScreen = screen;
            setUIcell(1, 1);


            //////load////

            mode.value = ((eventData_1)eventEditScreen.chip.eventData).mode;
            if (mode.value == 0)
            {
                mode.text.text = "消滅";
            }
            else
            {
                mode.text.text = "出現";
            }

            isLoop.value = ((eventData_1)eventEditScreen.chip.eventData).isLoop;
            
            interval.value = ((eventData_1)eventEditScreen.chip.eventData).interval;
            interval.text.text = interval.value.ToString();
           

        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);

            mode.update(deltaTime);
            isLoop.update(deltaTime);
            chLabel.update(deltaTime);
            modeLabel.update(deltaTime);
            
            inLabel.update(deltaTime);
            if (isLoop.value)
            {
                interval.update(deltaTime);
                interval.alpha = 1;
            }else
            {
                interval.alpha = 0.1f;
            }
           

            ((eventData_1)eventEditScreen.chip.eventData).mode = (int)mode.value;
            ((eventData_1)eventEditScreen.chip.eventData).isLoop = isLoop.value;
            ((eventData_1)eventEditScreen.chip.eventData).interval = interval.value;
           

        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            mode.Draw(batch, screenAlpha);
            isLoop.Draw(batch, screenAlpha);
            chLabel.Draw(batch, screenAlpha);
            interval.Draw(batch, screenAlpha);
            modeLabel.Draw(batch, screenAlpha);
           
            inLabel.Draw(batch, screenAlpha);
            //num.Draw(batch, screenAlpha);

        }
    }
}
