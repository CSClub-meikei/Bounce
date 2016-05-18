using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Bounce.editor
{
    class eventEditScreen:Screen
    {
        Screen special, eventdataScreen;
        public mapChip chip;
        NumUpDown eventTypeSelector,num;
        TextObject numLabel;


        GraphicalGameObject back;
        public eventEditScreen(Game1 game, EditorScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            eventTypeSelector = new NumUpDown(game, this, Color.Black, 0, 0, 200, 50);
            num = new NumUpDown(game, this, Color.Black, 0, 500, 200, 50);

            eventTypeSelector.text.text = "なし";
            eventTypeSelector.up.Enter += new EventHandler(this.changeEventType);
            eventTypeSelector.down.Enter += new EventHandler(this.changeEventType);
            num.up.Enter += new EventHandler(this.changeEventType);
            num.down.Enter += new EventHandler(this.changeEventType);
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_dialog, -10, -40, 210, 720);
            
            numLabel = new TextObject(game, this, Assets.graphics.ui.font, "イベント番号", Color.Black, 60, 480);
            setUIcell(1, 1);
        }
        public void load(mapChip chip)
        {
            eventdataScreen = null;
            special = null;
            eventTypeSelector.value = chip.eventData.type;
            eventTypeSelector.text.text = GetEventName((int)eventTypeSelector.value);
            num.value =chip.eventData.num;
            num.text.text = num.value.ToString();

            this.chip = chip;
            switch (chip.type)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;

            }

            switch (chip.eventData.type)
            {
                case 0:


                    break;

                case 1:

                    eventdataScreen = new eventEditScreen_1(game, this,0,150);


                    break;

                case 2:

                    break;

                case 3:

                    break;
            }

        }

        public string GetEventName(int num)
        {
            string res="定義なし";
            switch (num)
            {
                case 0:

                    res = "なし";
                    break;

                case 1:


                    res = "出現、消滅";
                    break;

                case 2:

                    res = "移動";

                    break;

                case 3:
                    res = "イベント作動";

                    break;
            }
            return res;
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            if (eventdataScreen != null)
            {
                eventdataScreen.update(deltaTime);
                eventdataScreen.X = X;
               // eventdataScreen.Y = Y;
            }
            if (special != null)
            {
                special.update(deltaTime);
                special.X = X;
               // special.Y = Y;
            }
            back.update(deltaTime);
            eventTypeSelector.update(deltaTime);
            num.update(deltaTime);
           
            numLabel.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            back.Draw(batch, screenAlpha);
            if (eventdataScreen != null) eventdataScreen.Draw(batch);
            if (special != null) special.Draw(batch);
            eventTypeSelector.Draw(batch, screenAlpha);
            numLabel.Draw(batch, screenAlpha);
            num.Draw(batch, screenAlpha);
        }
        public void changeEventType(object sender,EventArgs e)
        {
            if (chip == null) return;
            eventTypeSelector.text.text = GetEventName((int)eventTypeSelector.value);
            chip.eventData.type = (int)eventTypeSelector.value;
            switch (chip.eventData.type)
            {
                case 0:
                    chip.eventData = new eventData();
                    break;

                case 1:
                    chip.eventData = new eventData_1();
                    break;

                case 2:
                    chip.eventData = new eventData_2();
                    break;
            }
            chip.eventData.type = (int)eventTypeSelector.value;
            eventdataScreen = null;
            special = null;
            eventTypeSelector.value = chip.eventData.type;
            chip.eventData.num = (int) num.value;


            switch (chip.type)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;

            }

            switch (chip.eventData.type)
            {
                case 0:


                    break;

                case 1:

                    eventdataScreen = new eventEditScreen_1(game, this, 0, 150);


                    break;

                case 2:

                    break;

                case 3:

                    break;
            }
            DebugConsole.write("type:" + chip.eventData.type.ToString());
        }
    }
}
