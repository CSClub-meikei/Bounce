﻿using System;
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
    class eventEditScreen_p:Screen
    {
        mapChip chip;
        SimpleButton modeUp, modeDown, flagUp, flagDown,MoveSetting,optionUp,optionDown;
        TextObject modeLabel, flagLabel,optionLabel;
        //moveScreenObject back;
        GraphicalGameObject back;
        bool moveEditting = false;
        public event EventHandler MoveEdit;

        public eventEditScreen_p(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            setUIcell(2, 4);
            modeLabel = new TextObject(game, this, Assets.graphics.ui.font, "0", Color.White, 100, 0);
            flagLabel = new TextObject(game, this, Assets.graphics.ui.font, "0", Color.White, 100,160);
            modeUp = new SimpleButton(game, this, Assets.graphics.ui.arrowR, 200, 0, 40, 40);
            modeDown = new SimpleButton(game, this, Assets.graphics.ui.arrowL, 0, 0, 40, 40);
            flagUp = new SimpleButton(game, this, Assets.graphics.ui.arrowR, 200, 160, 40, 40);
            flagDown = new SimpleButton(game, this, Assets.graphics.ui.arrowL, 0, 160, 40, 40);
            MoveSetting = new SimpleButton(game, this, Assets.graphics.ui.moveEditButton, 0, 80, 250, 50);
            optionUp = new SimpleButton(game, this, Assets.graphics.ui.arrowR, 200, 80, 40, 40);
            optionDown = new SimpleButton(game, this, Assets.graphics.ui.arrowL, 0, 80, 40, 40);
            back = new GraphicalGameObject(game, this, Assets.getColorTexture(game,Color.Black), 0, 0, 250, 720);
            optionLabel = new TextObject(game, this, Assets.graphics.ui.font, "0", Color.White, 100, 80);

            MoveSetting.Enter += new EventHandler((sender, e) => { if (chip != null) { moveEditting = true; MoveEdit(this, EventArgs.Empty); } });
            modeUp.Enter += new EventHandler((sender, e) => {
                if (chip != null) {
                    chip.eventData.type++;
                    chip.eventData = getEventInstance(chip.eventData.type);
                    if (chip.eventData.type == 2) { ((eventData_2)chip.eventData).X = (int)chip.X;
                        ((eventData_2)chip.eventData).Y = (int)chip.Y;
                    }
                   // System.Windows.Forms.MessageBox.Show(chip.eventData.type.ToString());
                }
            });

            modeDown.Enter += new EventHandler((sender, e) => {

                if (chip != null)
                {
                    chip.eventData.type--;
                    chip.eventData = getEventInstance(chip.eventData.type);
                    if (chip.eventData.type == 2)
                    {
                        ((eventData_2)chip.eventData).X = (int)chip.X;
                        ((eventData_2)chip.eventData).Y = (int)chip.Y;
                    }
                  //  System.Windows.Forms.MessageBox.Show(chip.eventData.type.ToString());
                }

            });

            flagUp.Enter += new EventHandler((sender, e) => { if (chip != null) chip.eventData.num++; });
            flagDown.Enter += new EventHandler((sender, e) => { if (chip != null) chip.eventData.num--; });
            optionUp.Enter += new EventHandler((sender, e) => { if (chip != null && chip.eventData is eventData_1) ((eventData_1)chip.eventData).mode=1; });
            optionDown.Enter += new EventHandler((sender, e) => { if (chip != null && chip.eventData is eventData_1) ((eventData_1)chip.eventData).mode = 0; });

            Controls[0, 0] = modeDown;
            Controls[1, 0] = modeUp;
            Controls[0, 2] = flagDown;
            Controls[1, 2] = flagUp;
            Controls[0, 1] = MoveSetting;
            Controls[0, 1] = optionDown;
            Controls[1, 1] = optionUp;

            back.alpha = 0.5f;
        }
        public void Load(mapChip chip)
        {
            this.chip = chip;
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            if (chip != null)
            {
                flagLabel.text = chip.eventData.num.ToString();
                modeLabel.text = getEventTitle(chip.eventData.type);
                
                if (chip.eventData.type == 0)
                {
                    
                    Controls[0, 1] = null;
                    Controls[1, 1] = null;
                }
                else if (chip.eventData.type == 1)
                {
                    Controls[0, 1] = optionDown;
                    Controls[1, 1] = optionUp;
                    optionLabel.text = ((eventData_1)chip.eventData).mode.ToString();
                }
                else if (chip.eventData.type == 2)
                {
                    Controls[0, 1] = MoveSetting;
                    Controls[1, 1] = null;
                }

            }
            back.update(deltaTime);
            flagLabel.update(deltaTime);
            modeLabel.update(deltaTime);
            optionLabel.update(deltaTime);

         

                if (moveEditting)
            {
               // if (Input.onKeyDown(Keys.Right))
               // {
               //     chip.eventData[0] += 40;
               // }else if (Input.onKeyDown(Keys.Left))
               // {
              //      chip.eventData[0] -= 40;
              //  }
              //  else if (Input.onKeyDown(Keys.Up))
              //  {
              //      chip.eventData[1] -= 40;
              //  }
             //   else if (Input.onKeyDown(Keys.Down))
             //   {
             //       chip.eventData[1] += 40;
             //   }
            }


           
        }
        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch, screenAlpha);
            flagLabel.Draw(batch, screenAlpha);
            modeLabel.Draw(batch, screenAlpha);
            optionLabel.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
        public string getEventTitle(int num)
        {
            switch (num)
            {
                case 0:
                    return "イベント無し";

                case 1:
                    return "出現、消滅";
                    
                case 2:
                    return "移動";
                   

            }

            return "イベントが定義されていません。";

        }
        public eventData getEventInstance(int num)
        {
            eventData res = new eventData();
            res.type = 0;
            switch (num)
            {
                case 0:
                    res = new eventData();
                    res.type = 0;
                    break;
                case 1:
                    res = new eventData_1();
                    res.type = 1;
                    break;
                case 2:
                    res = new eventData_2();
                    res.type = 2;
                    break;

            }
            return res;
        }
    }
}
