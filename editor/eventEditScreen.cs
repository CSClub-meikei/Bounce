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
        NumUpDown eventTypeSelector,num,delay;
        TextObject numLabel,deLabel;
        public EditorScreen EditorScreen;

        public GraphicalGameObject back;
        public eventEditScreen(Game1 game, EditorScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            eventTypeSelector = new NumUpDown(game, this, Color.Black, 0, 0, 200, 50);
            num = new NumUpDown(game, this, Color.Black, 0, 500, 200, 50);
            delay = new NumUpDown(game, this, Color.Black, 0, 440, 200, 50);
            delay.step = 0.1f;
            eventTypeSelector.text.text = "なし";
            eventTypeSelector.changed += new EventHandler(this.changeEventType);
            
            num.changed += new EventHandler((sender, e) => { chip.eventData.num = (int)num.value; });
            eventTypeSelector.max = 2;



            delay.changed += new EventHandler((sender, e) => { chip.eventData.delay = delay.value; });
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_dialog, -10, -40, 210, 720);
            
            numLabel = new TextObject(game, this, Assets.graphics.ui.font, "イベント番号", Color.Black, 60, 480);
            deLabel = new TextObject(game, this, Assets.graphics.ui.font, "遅延", Color.Black, 60, 420);
            setUIcell(1, 1);

            EditorScreen = screen;
        }
        public void load(mapChip chip)
        {
            eventdataScreen = null;
            special = null;
            eventTypeSelector.value = chip.eventData.type;
            eventTypeSelector.text.text = GetEventName((int)eventTypeSelector.value);
            num.value =chip.eventData.num;
            num.text.text = num.value.ToString();
            delay.value = chip.eventData.delay;
            delay.text.text = delay.value.ToString();

            this.chip = chip;
            switch (chip.type)
            {
                case 1:
                    eventTypeSelector.min = 0;
                    eventTypeSelector.max = 2;
                    break;
                case 2:
                    eventTypeSelector.min = 0;
                    eventTypeSelector.max = 2;
                    break;
                case 3:
                    int tmp = chip.eventData.num;

                    //chip.eventData = new eventData_3();
                    //chip.eventData.num= tmp;
                    eventTypeSelector.min = 3;
                    eventTypeSelector.max = 4;
                    //eventTypeSelector.value = 3;
                  

                    eventTypeSelector.text.text = GetEventName((int)eventTypeSelector.value);
                    special = new specialScreen_switch(game, this, 0, 600);
                    break;
                case 4:
                    eventTypeSelector.min = 0;
                    eventTypeSelector.max = 2;
                    break;
                case 5:
                    eventTypeSelector.min = 0;
                    eventTypeSelector.max = 2;
                    special = new specialScreen_warp(game, this, 0, 600);
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
                    eventdataScreen = new eventEditScreen_2(game, this, 0, 150);
                    break;

                case 3:
                    eventdataScreen = new eventEditScreen_3(game, this, 0, 150);
                    break;

                case 4:
                    eventdataScreen = new eventEditScreen_4(game, this, 0, 150);
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
                case 4:
                    res = "メッセージ表示";
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
            delay.update(deltaTime);
            numLabel.update(deltaTime);
            deLabel.update(deltaTime);
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
            delay.Draw(batch, screenAlpha);
            deLabel.Draw(batch, screenAlpha);
        }
        public void changeEventType(object sender,EventArgs e)
        {
            if (chip == null) return;
            eventTypeSelector.text.text = GetEventName((int)eventTypeSelector.value);
            chip.eventData.type = (int)eventTypeSelector.value;
            int tmpnum = (int)num.value;
            float tmpdelay = delay.value;
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
                    ((eventData_2)chip.eventData).X = (int)chip.X;
                    ((eventData_2)chip.eventData).Y = (int)chip.Y;
                    break;
                case 3:
                    chip.eventData = new eventData_3();
                    break;
                case 4:
                    chip.eventData = new eventData_4();
                    break;
            }
            chip.eventData.type = (int)eventTypeSelector.value;
            eventdataScreen = null;
            special = null;
            eventTypeSelector.value = chip.eventData.type;
            chip.eventData.num = tmpnum;
            chip.eventData.delay = tmpdelay;
            delay.value = chip.eventData.delay;

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
                    eventdataScreen = new eventEditScreen_2(game, this, 0, 150);
                    break;

                case 3:
                    eventdataScreen = new eventEditScreen_3(game, this, 0, 150);
                    break;

                case 4:
                    eventdataScreen = new eventEditScreen_4(game, this, 0, 150);
                    break;
            }
            DebugConsole.write("type:" + chip.eventData.type.ToString());
        }
    }
}
