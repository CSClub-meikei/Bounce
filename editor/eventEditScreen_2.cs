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
    class eventEditScreen_2:Screen
    {

        NumUpDown speed, interval;
        checkBox isLoop;
        TextObject speedLabel, inLabel,chLabel;
        SimpleButton editButton;
        eventEditScreen eventEditScreen;

        public eventEditScreen_2(Game1 game, eventEditScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {

            speed = new NumUpDown(game, this, Color.Black, 0, 40, 200, 50);
           
            isLoop = new checkBox(game, this, 20, 90, 40, 40);
            chLabel = new TextObject(game, this, Assets.graphics.ui.font, "繰り返す", Color.Black, 80, 100);
            speedLabel = new TextObject(game, this, Assets.graphics.ui.font, "移動スピード", Color.Black, 60, 20);
            editButton = new SimpleButton(game, this, Assets.graphics.ui.moveEditButton, 0, 230, 180, 50);
            inLabel = new TextObject(game, this, Assets.graphics.ui.font, "一時停止時間", Color.Black, 60, 140);
            interval = new NumUpDown(game, this, Color.Black, 0, 160, 200, 50);
            interval.step = 0.1f;

            eventEditScreen = screen;
            setUIcell(1, 1);

            editButton.Enter += new EventHandler((sender, e) =>
              {
                  eventEditScreen.EditorScreen.startMoveEdit(null, null);
              });


            //////load////

           speed.value = ((eventData_2)eventEditScreen.chip.eventData).speed;
            speed.text.text = speed.value.ToString();
            isLoop.value = ((eventData_2)eventEditScreen.chip.eventData).isLoop;

            interval.value = ((eventData_2)eventEditScreen.chip.eventData).interval;
            interval.text.text = interval.value.ToString();

            speed.changed += new EventHandler((sender, e) =>
              {
                  ((eventData_2)eventEditScreen.chip.eventData).speed = (int)speed.value;
              });
            interval.changed += new EventHandler((sender, e) =>
              {
                  ((eventData_2)eventEditScreen.chip.eventData).interval = interval.value;
              });
            isLoop.Enter += new EventHandler((sender, e) =>
              {
                  ((eventData_2)eventEditScreen.chip.eventData).isLoop = isLoop.value;
              });
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);

            speed.update(deltaTime);
            isLoop.update(deltaTime);
            chLabel.update(deltaTime);
            speedLabel.update(deltaTime);
            editButton.update(deltaTime);
            inLabel.update(deltaTime);
            if (isLoop.value)
            {
                interval.update(deltaTime);
                interval.alpha = 1;
            }
            else
            {
                interval.alpha = 0.1f;
            }


          
           
            


        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            speed.Draw(batch, screenAlpha);
            isLoop.Draw(batch, screenAlpha);
            chLabel.Draw(batch, screenAlpha);
            interval.Draw(batch, screenAlpha);
            speedLabel.Draw(batch, screenAlpha);
            editButton.Draw(batch, screenAlpha);
            inLabel.Draw(batch, screenAlpha);
            //num.Draw(batch, screenAlpha);

        }
    }
}
