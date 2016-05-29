using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bounce.editor;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Bounce
{
    class eventEditScreen_4:Screen
    {
        NumUpDown character,face;
        TextObject mL;
        eventEditScreen eventEditScreen;
        SimpleButton msg;
        public eventEditScreen_4(Game1 game, eventEditScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            eventEditScreen = screen;
            character = new NumUpDown(game, this, Color.Black, 0, 30, 200, 50);
            face = new NumUpDown(game, this, Color.Black, 0, 80, 200, 50);
            mL = new TextObject(game, this, Assets.graphics.ui.font, "Character", Color.Black, 60, 0);
            msg = new SimpleButton(game, this, Assets.graphics.ui.button_editMsg, 0, 150, 180, 30);

            msg.Enter += new EventHandler((sender, e) =>
              {
                 
                  inputBox f = new inputBox(((eventData_4)eventEditScreen.chip.eventData).msg);
                  f.ShowDialog();
              ((eventData_4)eventEditScreen.chip.eventData).msg = f.msg;
          });


            character.max = 2;

            character.changed += new EventHandler(((sender, e) =>
            {
                if (character.value == 0)
                {
                    character.text.text = "なし";
                }
                else if (character.value == 1)
                {
                    character.text.text = "男";
                }
                else if (character.value == 2)
                {
                    character.text.text = "女";
                }
                  ((eventData_4)eventEditScreen.chip.eventData).character = (int)character.value;

            }));
            character.value = ((eventData_4)eventEditScreen.chip.eventData).character;
            character.text.text = character.value.ToString();
            if (character.value == 0)
            {
                character.text.text = "なし";
            }
            else if (character.value == 1)
            {
                character.text.text = "男";
            }
            else if (character.value == 2)
            {
                character.text.text = "女";
            }


            face.max = 2;

            face.changed += new EventHandler(((sender, e) =>
            {
                if (face.value == 0)
                {
                    face.text.text = "普通";
                }
                else if (face.value == 1)
                {
                    face.text.text = "笑い";
                }
                else if (face.value == 2)
                {
                    face.text.text = "泣き";
                }
                  ((eventData_4)eventEditScreen.chip.eventData).face = (int)face.value;

            }));
            face.value = ((eventData_4)eventEditScreen.chip.eventData).face;
            face.text.text = face.value.ToString();
            if (face.value == 0)
            {
                face.text.text = "普通";
            }
            else if (face.value == 1)
            {
                face.text.text = "笑い";
            }
            else if (face.value == 2)
            {
                face.text.text = "泣き";
            }

            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            character.update(deltaTime);
            mL.update(deltaTime);
            msg.update(deltaTime);
            face.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            character.Draw(batch, screenAlpha);
            mL.Draw(batch, screenAlpha);
            face.Draw(batch, screenAlpha);
            msg.Draw(batch, screenAlpha);
        }
    }
}
