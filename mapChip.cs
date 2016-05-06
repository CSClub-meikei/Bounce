using System;
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
    class mapChip:GraphicalGameObject
    {
        const int size = 40;
        public int fx, fy;
        public int rx, ry;
        public bool moving=false;
        public bool selected = false;
        public String ssize;
        new EditorScreen parent;
        public int mode = 0;
        public int rotate=0;

        public int flagType;
        public int flagNum;
        public int mx, my;
        public int EventVisible;
        public int drawMode = 1;
        public int isEventEditting;
        public event EventHandler Onselect;
        public mapChip(Game1 game, Screen screen, Texture2D Texture,int mode,int rotate,int drawMode, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            this.mode = mode;
            this.rotate = rotate;
            this.drawMode = drawMode;

            mx =(int) X;
            my = (int)Y;
            parent = (EditorScreen)screen;
        }
        public override void update(float delta)
        {

            if (Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && Input.OnMouseDown(Input.LeftButton))
            {
                fx = (int)(Input.getPosition().X - parent.X - actX);
                fy = (int)(Input.getPosition().Y - parent.Y - actY);
                moving = true;
                selected = true;
                parent.label.text = "イベント情報: \n" + "　イベントモード : " + flagType + "\n　フラグ番号 : " + flagNum +  "\n　EventVisible : " + EventVisible + "\n　移動先 : " + mx + "," + my;

                if (Onselect != null) Onselect(this, EventArgs.Empty);
            }
            if(moving && Input.IsMouseDown(Input.LeftButton))
            {
                X = (int)((Input.getPosition().X-fx-parent.X*2)/40)*40;
                Y = (int)((Input.getPosition().Y-fy - parent.Y*2) / 40) * 40;
                if (flagType != 2)
                {
                    mx = (int)X;
                    my = (int)Y;
                }
              //  if ((Input.getPosition().X < 80)) parent.X +=40;
              //  if ((Input.getPosition().X > 1200)) parent.X -= 40;
              //  if ((Input.getPosition().Y < 80)) parent.Y +=40;
              //  if ((Input.getPosition().X > 680)) parent.Y -=40;
            }
            if (Input.OnMouseUp(Input.LeftButton))
            {
                moving = false;
                
            }

          if(selected && isEventEditting == 0) num();
            if (isEventEditting!=0) EventSetting();
            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            if (drawMode == 1)
            {
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                int i = 0;
                int j = 0;
                for (i = 0; i < Width / size; i++)
                {
                    for (j = 0; j < Height / size; j++)
                    {
                        batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha);
                        batch.Draw(Texture, destinationRectangle: new Rectangle((int)(mx + parent.X + i * size), (int)(my + parent.Y + j * size), (int)size, (int)size), color: Color.White * 0.5f * screenAlpha);
                    }

                }
                batch.End();
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                if (selected)
                {
                    batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)actX + (int)((Texture.Width / 2) * (Width / Texture.Width)), (int)actY + (int)((Texture.Height / 2) * (Height / Texture.Height)), (int)Width, (int)Height), color: Color.White * 0.5f * screenAlpha, rotation: angle, origin: origin);
                    batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)mx + parent.X + (int)((Texture.Width / 2) * (Width / Texture.Width)), (int)my + parent.Y + (int)((Texture.Height / 2) * (Height / Texture.Height)), (int)Width, (int)Height), color: Color.White * 0.2f * screenAlpha, rotation: angle, origin: origin);
                }
                batch.End();
            }else if (drawMode == 2)
            {
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX), (int)(actY), (int)Width, (int)Height), color: Color.White * alpha * screenAlpha);
                batch.Draw(Texture, destinationRectangle: new Rectangle((int)(mx + parent.X ), (int)(my + parent.Y ), (int)Width, (int)Height), color: Color.White * 0.5f * screenAlpha);

                batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)actX , (int)actY , (int)Width, (int)Height), color: Color.White * 0.5f * screenAlpha);
                batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)mx + parent.X , (int)my + parent.Y, (int)Width, (int)Height), color: Color.White * 0.2f * screenAlpha);

                batch.End();
            }
        }
        public void num()
        {
            if (Input.onKeyDown(Keys.Delete)) parent.Rchips.Add(this);
            if (Input.onKeyDown(Keys.Space) && mode==2)
            {
                rotate++;
                if (rotate == 4) rotate = 0;
                Texture = Assets.graphics.game.thorn[rotate];
            }
            if (Input.onKeyDown(Keys.Space) && mode == 3)
            {
                rotate++;
                if (rotate == 4) rotate = 0;
                Texture = Assets.graphics.game.Switch[rotate];
            }
            if (Input.onKeyDown(Keys.X)) { isEventEditting = 1; parent.label.text = "イベント設定モード => モード選択"; }
            if (Input.onKeyDown(Keys.Right)) Width += 40;
            if (Input.onKeyDown(Keys.Down)) Height += 40;
            if (Input.onKeyDown(Keys.Left) && Width!=40) Width -= 40;
            if (Input.onKeyDown(Keys.Up) && Height!=40) Height-= 40;
          //  Console.WriteLine("w:" + Width.ToString() + "h:" + Height.ToString());
        }
        public void EventSetting()
        {
            switch (isEventEditting)
            {
                case 1:
                    if (mode == 3) { flagType = 3; isEventEditting = 2; }
                    if (Input.onKeyDown(Keys.Right))flagType+=1;
                    if (Input.onKeyDown(Keys.Left)) flagType -= 1;
                    if (Input.onKeyDown(Keys.Enter)&& flagType==1) isEventEditting = 2;
                    if (Input.onKeyDown(Keys.Enter) && flagType == 2) { isEventEditting = 2; mx = (int)X; my =(int) Y; }
                    parent.label.text = "イベント設定モード => モード選択 : "+ flagType + "\nEnterで次へ";
                    break;
                case 2:
                    if (flagType == 1)
                    {

                        if (Input.onKeyDown(Keys.Right))EventVisible=1;
                       
                        if (Input.onKeyDown(Keys.Left)) EventVisible = 0;

                        string str="";
                        if (EventVisible == 0) str = "動作で非表示";
                        if (EventVisible == 1) str = "動作で表示";
                        parent.label.text = "イベント設定モード => モード選択 : " + flagType + "\n　=>動作設定 : " + str + "\n 左右で移動、Enterで次へ";
                        if (Input.onKeyDown(Keys.Enter)) isEventEditting = 3;
                    }
                    else if (flagType == 2)
                    {
                        if (Input.onKeyDown(Keys.Right)) mx += 40;
                        if (Input.onKeyDown(Keys.Down)) my += 40;
                        if (Input.onKeyDown(Keys.Left)) mx -= 40;
                        if (Input.onKeyDown(Keys.Up)) my -= 40;

                        parent.label.text = "イベント設定モード => モード選択 : " + flagType + "\n　=>移動先を選択" + "\n 左右で移動、Enterで次へ";
                        if (Input.onKeyDown(Keys.Enter)) isEventEditting = 3;
                    }
                    else if (flagType == 3)
                    {
                        if (Input.onKeyDown(Keys.Right)) EventVisible++;

                        if (Input.onKeyDown(Keys.Left)) EventVisible--;
                        if (EventVisible == 3) EventVisible = 0;
                        if (EventVisible == -1) EventVisible = 2;
                        string str = "";
                        if (EventVisible == 0) str = "動作でフラグを有効";
                        if (EventVisible == 1) str = "動作でフラグを無効";
                        if (EventVisible == 2) str = "動作でフラグをトグル";
                        parent.label.text = "イベント設定モード => モード選択 : " + flagType + "\n　=>動作設定 : " + str + "\n 左右で移動、Enterで次へ";
                        if (Input.onKeyDown(Keys.Enter)) isEventEditting = 3;
                    }


                        break;
                case 3:

                    if (flagType == 1)
                    {

                        if (Input.onKeyDown(Keys.Right)) flagNum += 1;
                        if (Input.onKeyDown(Keys.Down)) flagNum -= 10;
                        if (Input.onKeyDown(Keys.Left)) flagNum -= 1;
                        if (Input.onKeyDown(Keys.Up)) flagNum += 10;

                        parent.label.text = "イベント設定モード => モード選択 : " + flagType + "\n　=>動作を選択" + "\n　　=>フラグ番号を設定 : " + flagNum + "\n 上下左右で選択、Enterで終了";
                        if (Input.onKeyDown(Keys.Enter)) isEventEditting =4;

                    }
                    else if (flagType == 2)
                    {
                        if (Input.onKeyDown(Keys.Right)) flagNum += 1;
                        if (Input.onKeyDown(Keys.Down)) flagNum -= 10;
                        if (Input.onKeyDown(Keys.Left)) flagNum -= 1;
                        if (Input.onKeyDown(Keys.Up)) flagNum += 10;

                        parent.label.text = "イベント設定モード => モード選択 : " + flagType + "\n　=>移動先を選択" + "\n　　=>フラグ番号を設定 : " + flagNum + "\n 上下左右で選択、Enterで終了";
                        if (Input.onKeyDown(Keys.Enter)) isEventEditting = 4;
                    }
                    else if (flagType == 3)
                    {

                        if (Input.onKeyDown(Keys.Right)) flagNum += 1;
                        if (Input.onKeyDown(Keys.Down)) flagNum -= 10;
                        if (Input.onKeyDown(Keys.Left)) flagNum -= 1;
                        if (Input.onKeyDown(Keys.Up)) flagNum += 10;

                        parent.label.text = "イベント設定モード => モード選択 : " + flagType + "\n　=>動作を選択" + "\n　　=>フラグ番号を設定 : " + flagNum + "\n 上下左右で選択、Enterで終了";
                        if (Input.onKeyDown(Keys.Enter)) isEventEditting = 4;

                    }

                    break;
                case 4:

                    parent.label.text = "設定完了";
                    isEventEditting = 0;

                    break;
            }
        }
    }
}
