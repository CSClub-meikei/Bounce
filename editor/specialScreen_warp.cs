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
    class specialScreen_warp:Screen
    {
        NumUpDown mode;
        TextObject mL;
        eventEditScreen eventEditScreen;

        public specialScreen_warp(Game1 game, eventEditScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            eventEditScreen = screen;
            mode = new NumUpDown(game, this, Color.Black, 0, 30, 200, 50);
            mL = new TextObject(game, this, Assets.graphics.ui.font, "ワープID", Color.Black, 60, 10);

           // mode.max = 2;

            mode.changed += new EventHandler(((sender, e) =>
            {
               
                  eventEditScreen.chip.specialData = (int)mode.value;

            }));
            mode.value = eventEditScreen.chip.specialData;
            mode.text.text = mode.value.ToString();
            

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
