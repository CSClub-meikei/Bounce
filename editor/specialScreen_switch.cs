using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Bounce.editor;
using Bounce.uiControl;
namespace Bounce
{
    class specialScreen_switch:Screen
    {
        checkBox mode;
        TextObject mL;
        eventEditScreen eventEditScreen;

        public specialScreen_switch(Game1 game, eventEditScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            eventEditScreen = screen;
            mode = new checkBox(game, this, 0, 30, 50, 50);
            mL = new TextObject(game, this, Assets.graphics.ui.font, "非表示", Color.Black, 60, 10);

            // mode.max = 2;

            mode.Enter += new EventHandler(((sender, e) =>
            {
                if (mode.value) eventEditScreen.chip.specialData = 1;
                else eventEditScreen.chip.specialData =0;


            }));
             if (eventEditScreen.chip.specialData==1) mode.value = true;
                else mode.value = false;


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
