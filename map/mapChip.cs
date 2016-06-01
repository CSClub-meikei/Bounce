using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;
using System.Xml;

using Bounce.editor;
namespace Bounce
{
    [DataContract]
    public class mapChip:GraphicalGameObject
    {
        new EditorScreen parent;
        
        public const int BLOCK = 1;
        public const int THORN = 2;
        public const int SWITCH = 3;
        public const int SHPOINT = 4;
        public const int WARPPOINT = 5;
        public const int GUMPOINT = 6;
        public const int GOAL = 7;
        public const int START = 10;
        public const int ACCEL = 8;
        public const int SAVEPOINT = 9;

        public const int DEFULT = 0;
        public const int MOVING = 1;
        public const int RESIZEING = 2;

        int size = 40;
        [DataMember]public int type=0;
        [DataMember]public int rotate = 0;
        public int DrawMode = 1;

        [DataMember]public eventData eventData;
        [DataMember]public int specialData;

        public int clickedX, clickedY;
        public int EditingMode = 0;
        public bool _isSelect=false;
        public int ResizeMode = 0;
        public int FirstX,FirstY,FirstWidth, FirstHeight;
        public float doubleClicktime = 0;
        public bool doubleClickFlag = false;
        public bool AllowResize = true;
        public bool ShowMoveLocation = true;
        public bool AllowDelete=true;
        public bool EditChipMode = false;
        public bool nazo = false;
        public Texture2D red;
        public Texture2D orange;
        public bool eventHilight;
        Texture2D water;

        public event EventHandler onClick;
        public event EventHandler onDoubleClick;
        public event EventHandler onUnSelect;

        public bool isSelected
        {
            get { return _isSelect; }
            set {
                _isSelect = value;
                if (_isSelect)
                {
                    parent.eventEditScreen.load(this);
                    EditingMode = MOVING;
                }
            }
        }

        
        public mapChip(Game1 game, Screen screen, int type, int rotate,float x, float y, float width, float height) : base(game, screen, null, x, y, width, height)
        {
            parent = (EditorScreen)screen;
            this.Texture = getChipTexture(type);
            this.type = type;
            this.rotate = rotate;
            eventData = new eventData();
            onDoubleClick += new EventHandler(this.Rotate);
            if (type == ACCEL)
            {
                size = 120;
                Width = 120;
                Height = 120;
                water = Assets.getColorTexture(game, new Color(0, 145, 227));
            }
            else if (type == SWITCH)
            {
                eventData = new eventData_3();
                eventData.type = 3;
            }
            red = Assets.getColorTexture(game, Color.Red);
            orange = Assets.getColorTexture(game, Color.Orange);
            addAnimator(2);
        }
        public void reLoad(Game1 game, Screen screen)
        {
            this.game = game;
            this.Content = game.Content;

            this.parent = (EditorScreen)screen;
            base.parent = screen;
            this.Texture = getChipTexture(this.type);
            if (Texture != null)
            {

                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
            animator = new List<GameObjectAnimator>();


            AllowResize = true;
            ShowMoveLocation = true;
            actX = X;
            actY = Y;
            onDoubleClick += new EventHandler(this.Rotate);
            DrawMode = 1;
            alpha = 1;

            if (type == ACCEL)
            {
                size = 120;
               
                water = Assets.getColorTexture(game, new Color(0, 145, 227));
            }
            red = Assets.getColorTexture(game, Color.Red);
            orange = Assets.getColorTexture(game, Color.Orange);
            addAnimator(2);
        }

        public override void update(float delta)
        {
            if (parent.Moveeditting && !EditChipMode) goto n;
            if (Input.OnMouseDown(Input.LeftButton) && !Input.IsHover(new Rectangle((int)actX - 10, (int)actY - 10, (int)Width + 20, (int)Height + 20)) && isSelected && !Input.IsHover(new Rectangle(parent.eventEditScreen.X, parent.eventEditScreen.Y, 200, 720)))

            {
              //  System.Windows.Forms.MessageBox.Show("un");
                isSelected = false;
                parent.RefreshMap();
                parent.selectedChips.Remove(this);
                if(onUnSelect!=null)onUnSelect(this, EventArgs.Empty);
            }
            if (Input.IsHover(new Rectangle((int)actX , (int)actY, (int)Width, (int)Height)) && Input.OnMouseDown(Input.LeftButton)){
                if (doubleClickFlag)
                {
                    if (doubleClicktime < 0.5f)
                    {
                       if(onDoubleClick!=null) onDoubleClick(this, EventArgs.Empty);
                    }else
                    {
                        doubleClicktime = 0;
                    }
                }else
                {
                    doubleClickFlag = true;
                }
               
            }
            if (doubleClickFlag)
            {
                doubleClicktime += delta/1000;
            }
            
            if (Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && Input.OnMouseDown(Input.LeftButton) && !isSelected && !Input.IsHover(new Rectangle((int)parent.eventEditScreen.X, (int)parent.eventEditScreen.Y, (int)parent.eventEditScreen.back.Width, (int)parent.eventEditScreen.back.Height)))
            {
                clickedX = (int)(Input.getPosition().X - parent.X - actX);
                clickedY = (int)(Input.getPosition().Y - parent.Y - actY);
                if (onClick != null) onClick(this, EventArgs.Empty);//EditScreenに選択されたことを伝える、オブジェクトとの重なり等で選択されるか判別され、OKならselected=trueされる
            }
            else if (Input.IsHover(new Rectangle((int)actX+10, (int)actY+10, (int)Width-20, (int)Height-20)) && Input.OnMouseDown(Input.LeftButton) && isSelected && EditingMode==DEFULT)
            {
                clickedX = (int)(Input.getPosition().X - parent.X - actX);
                clickedY = (int)(Input.getPosition().Y - parent.Y - actY);
                EditingMode = MOVING;
            }
            if (EditChipMode)
            {
               // clickedX -= parent.X;
              //  clickedY -= parent.Y;
            }

            if (_isSelect)
            {
                switch (EditingMode)
                {

                    case DEFULT:
                        updateDefult(delta);
                        break;
                    case MOVING:
                        updateMoving(delta);
                        break;
                    case RESIZEING:
                        updateResize(delta);
                        break;
                }
            }

           n:
            foreach(GameObjectAnimator a in animator)
            {
                if (a.type == GameObjectAnimator.GLOW)
                {
                   a.tmp[0] = (float)X;
                    a.tmp[1] = (float)Y;
                }
            }
            
            base.update(delta);
        }
        public void updateDefult(float deltaTime)
        {
            if (Input.onKeyDown(Keys.Delete)&&AllowDelete) { parent.RemoveChips.Add(this); DebugConsole.write("削除"); }
            if (Input.IsHover(new Rectangle((int)(actX + Width - 10), (int)actY+10, (int)20, (int)Height-20)) && Input.OnMouseDown(Input.LeftButton))
            {
                EditingMode = RESIZEING;
                ResizeMode = 1;

                clickedX = Input.getPosition().X;
                clickedY = Input.getPosition().Y;
                FirstWidth = (int)Width;
                FirstHeight = (int)Height;
                FirstX = (int)X;
                FirstY = (int)Y;
                
            }
            else if (Input.IsHover(new Rectangle((int)(actX+10), (int)(actY+Height-10), (int)Width-20, (int)20)) && Input.OnMouseDown(Input.LeftButton))
            {
               // DebugConsole.write("2版");
                EditingMode = RESIZEING;
                ResizeMode = 2;
                clickedX = Input.getPosition().X;
                clickedY = Input.getPosition().Y;
                FirstWidth = (int)Width;
                FirstHeight = (int)Height;
                FirstX = (int)X;
                FirstY = (int)Y;
            }
            else if (Input.IsHover(new Rectangle((int)(actX -10), (int)(actY + 10), (int)20, (int)Height-20)) && Input.OnMouseDown(Input.LeftButton))
            {
               // DebugConsole.write("3版");
                EditingMode = RESIZEING;
                ResizeMode = 3;
                clickedX = Input.getPosition().X;
                clickedY = Input.getPosition().Y;
                FirstWidth = (int)Width;
                FirstHeight = (int)Height;
                FirstX = (int)X;
                FirstY = (int)Y;
            }
            else if (Input.IsHover(new Rectangle((int)(actX+10), (int)(actY -10), (int)Width-20, (int)20)) && Input.OnMouseDown(Input.LeftButton))
            {
              //  DebugConsole.write("4版");
                EditingMode = RESIZEING;
                ResizeMode = 4;
                clickedX = Input.getPosition().X;
                clickedY = Input.getPosition().Y;
                FirstWidth = (int)Width;
                FirstHeight = (int)Height;
                FirstX = (int)X;
                FirstY = (int)Y;
            }else if (Input.IsHover(new Rectangle((int)(actX + Width-10), (int)(actY + Height-10), (int)20, (int)20)) && Input.OnMouseDown(Input.LeftButton))
            {
               // DebugConsole.write("5版");
                EditingMode = RESIZEING;
                ResizeMode = 5;
                clickedX = Input.getPosition().X;
                clickedY = Input.getPosition().Y;
                FirstWidth = (int)Width;
                FirstHeight = (int)Height;
                FirstX = (int)X;
                FirstY = (int)Y;
            }

            if (Input.OnMouseDown(Input.RightButton))
            {
                parent.InsertChips.Add(this);
                
            }

        }
        public void updateMoving(float deltaTime)
        {

           
            if (Input.IsMouseDown(Input.LeftButton))
            {
                X = (int)((Input.getPosition().X - clickedX - parent.X * 2) / 40) * 40;
                Y = (int)((Input.getPosition().Y - clickedY - parent.Y * 2) / 40) * 40;
                if (EditChipMode && !nazo)
                {
                    X = (int)((Input.getPosition().X - clickedX - parent.X ) / 40) * 40;
                    Y = (int)((Input.getPosition().Y - clickedY - parent.Y) / 40) * 40;
                }
            }
            if (Input.OnMouseUp(Input.LeftButton))
            {
                EditingMode = DEFULT;
                nazo = true;
               // parent.RefreshMap();
            }



        }
        public void updateResize(float deltaTime)
        {
            if (!AllowResize) { EditingMode = DEFULT; ResizeMode = 0; return; }
            if (Input.IsMouseDown(Input.LeftButton))
            {
                switch (ResizeMode)
                {
                    case 1:
                        Width = FirstWidth + (int)((Input.getPosition().X - clickedX) / size) * size;

                        break;
                    case 2:
                        Height = FirstHeight + (int)((Input.getPosition().Y - clickedY) / size) * size;

                        break;
                    case 3:
                        Width = FirstWidth - (int)((Input.getPosition().X - clickedX) / size) * size;
                        X = FirstX + (int)((Input.getPosition().X - clickedX) / size) * size;
                        break;
                    case 4:
                        Height = FirstHeight - (int)((Input.getPosition().Y - clickedY) / size) * size;
                        Y = FirstY + (int)((Input.getPosition().Y - clickedY) / size) * size;
                        break;
                    case 5:
                        Width = FirstWidth + (int)((Input.getPosition().X - clickedX) / size) * size;
                        Height = FirstHeight + (int)((Input.getPosition().Y - clickedY) / size) * size;
                        break;
                }

                if (Width < size) { Width = size; X = FirstX; }
                if (Height < size) { Height = size; Y = FirstY; }

            }else if (Input.OnMouseUp(Input.LeftButton))
            {
                EditingMode = DEFULT;
                //parent.RefreshMap();
            }
            
        }





        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            if (DrawMode == 1)
            {
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                int i = 0;
                int j = 0;
                if (size == 0) size = 40;
                for (i = 0; i < Width / size; i++)
                {
                    for (j = 0; j < Height / size; j++)
                    {
                        if(type==ACCEL && rotate == 2 && j!=0)//water用
                        {
                            batch.Draw(water, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha*0.7f);
                            if (eventData.type == 2 && ShowMoveLocation) batch.Draw(water, destinationRectangle: new Rectangle((int)(((eventData_2)eventData).X + parent.X + i * size), (int)(((eventData_2)eventData).Y + parent.Y + j * size), (int)size, (int)size), color: Color.White * 0.5f * screenAlpha);
                        }else if(type == ACCEL && rotate == 2)
                        {
                            batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha*0.7f);
                            if (eventData.type == 2 && ShowMoveLocation) batch.Draw(Texture, destinationRectangle: new Rectangle((int)(((eventData_2)eventData).X + parent.X + i * size), (int)(((eventData_2)eventData).Y + parent.Y + j * size), (int)size, (int)size), color: Color.White * 0.5f * screenAlpha);
                        }
                        else
                        {
                            batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha);
                            if (eventData.type == 2 && ShowMoveLocation) batch.Draw(Texture, destinationRectangle: new Rectangle((int)(((eventData_2)eventData).X + parent.X + i * size), (int)(((eventData_2)eventData).Y + parent.Y + j * size), (int)size, (int)size), color: Color.White * 0.5f * screenAlpha);
                        }
                        
                    }

                }
                batch.End();
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                if (isSelected)
                {
                    batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)actX , (int)actY , (int)Width, (int)Height), color: Color.White * 0.5f * alpha * screenAlpha);
                    if (eventData.type == 2 && ShowMoveLocation) batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)((eventData_2)eventData).X + parent.X , (int)((eventData_2)eventData).Y + parent.Y, (int)Width, (int)Height), color: Color.White * 0.2f * alpha * screenAlpha);
                }
                batch.End();
            }
            else if (DrawMode == 2)
            {
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX), (int)(actY), (int)Width, (int)Height), color: Color.White * alpha * screenAlpha);
                if (eventData.type == 2) batch.Draw(Texture, destinationRectangle: new Rectangle((int)(((eventData_2)eventData).X + parent.X), (int)(((eventData_2)eventData).Y + parent.Y), (int)Width, (int)Height), color: Color.White * 0.5f * alpha * screenAlpha);

                batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)actX, (int)actY, (int)Width, (int)Height), color: Color.White * 0.5f * screenAlpha);
                if (eventData.type == 2 && ShowMoveLocation) batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)((eventData_2)eventData).X + parent.X, (int)((eventData_2)eventData).Y + parent.Y, (int)Width, (int)Height), color: Color.White * 0.2f * alpha * screenAlpha);

                batch.End();
            }
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            foreach (GameObjectAnimator a in animator)
            {
                a.Draw(batch, screenAlpha);
            }
            batch.End();

            if (parent.showEventIcon)
            {
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                float al = 0.6f;
                if (isSelected || eventHilight) al = 1;
                switch (eventData.type)
                {
                    case 1:
                        batch.Draw(orange, new Rectangle((int)actX, (int)actY - 20, 20, 20), Color.White * al*parent.screenAlpha);
                        batch.DrawString(Assets.graphics.ui.font, eventData.num.ToString(), new Vector2((int)actX, (int)actY - 20), Color.White * al * parent.screenAlpha);

                        batch.Draw(orange, new Rectangle((int)actX + 20, (int)actY - 20, 20, 20), Color.White * al * parent.screenAlpha);
                        if (((eventData_1)eventData).mode == 0)
                        {
                            batch.DrawString(Assets.graphics.ui.font, "消", new Vector2((int)actX + 20, (int)actY - 20), Color.White * al * parent.screenAlpha);
                        }
                        else
                        {
                            batch.DrawString(Assets.graphics.ui.font, "出", new Vector2((int)actX + 20, (int)actY - 20), Color.White * al * parent.screenAlpha);
                        }
                        if (((eventData_1)eventData).isLoop)
                        {
                            batch.Draw(orange, new Rectangle((int)actX + 40, (int)actY - 20, 20, 20), Color.White * al * parent.screenAlpha);
                            batch.Draw(Assets.graphics.ui.repeat, new Rectangle((int)actX + 40, (int)actY - 20, 20, 20), Color.White * al * parent.screenAlpha);
                        }


                        break;
                    case 2:
                        batch.Draw(orange, new Rectangle((int)actX, (int)actY - 20, 20, 20), Color.White * al * parent.screenAlpha);
                        batch.DrawString(Assets.graphics.ui.font, eventData.num.ToString(), new Vector2((int)actX, (int)actY - 20), Color.White * al * parent.screenAlpha);
                        batch.Draw(orange, new Rectangle((int)actX + 20, (int)actY - 20, 20, 20), Color.White * al * parent.screenAlpha);
                        batch.DrawString(Assets.graphics.ui.font, "移", new Vector2((int)actX + 20, (int)actY - 20), Color.White * al * parent.screenAlpha);
                        if (((eventData_2)eventData).isLoop)
                        {
                            batch.Draw(orange, new Rectangle((int)actX + 40, (int)actY - 20, 20, 20), Color.White * al * parent.screenAlpha);
                            batch.Draw(Assets.graphics.ui.repeat, new Rectangle((int)actX + 40, (int)actY - 20, 20, 20), Color.White * al * parent.screenAlpha);
                        }

                        break;
                    case 3:
                        batch.Draw(red, new Rectangle((int)actX, (int)actY - 20, 20, 20), Color.White * al * parent.screenAlpha);
                        batch.DrawString(Assets.graphics.ui.font, eventData.num.ToString(), new Vector2((int)actX, (int)actY - 20), Color.White * al * parent.screenAlpha);

                        break;
                }
                batch.End();
            }
           



        }
        public void Rotate(object sender,EventArgs e)
        {
            rotate++;
            if (rotate == 5) rotate = 0;
            if (type == ACCEL && rotate == 3) rotate = 0;

            Texture = getChipTexture(type);

        }
        public Texture2D getChipTexture(int num)
        {
            Texture2D res = null;
            switch (num)
            {
                case BLOCK:
                    res = Assets.graphics.game.block;
                    break;
                case THORN:
                    res = Assets.graphics.game.thorn[rotate];
                    break;
                case SWITCH:
                    res = Assets.graphics.game.Switch[rotate];
                    break;
                case SHPOINT:
                    res = Assets.graphics.game.changePoint;
                    break;
                case WARPPOINT:
                    res = Assets.graphics.game.warpPoint[rotate];
                    break;
                case GUMPOINT:
                    res = Assets.graphics.game.block;
                    break;
                case GOAL:
                    res = Assets.graphics.game.goal;
                    break;
                case START:
                    res = Assets.graphics.ui.startChip;
                    break;
                case ACCEL:
                    if (rotate == 0)
                    {
                        res = Assets.graphics.game.accel[0];
                    }
                    else if(rotate==1)
                    {
                        res = Assets.graphics.game.brake[0];
                    }else if (rotate == 2)
                    {
                        res = Assets.graphics.game.water[0];
                    }
                    break;
                case SAVEPOINT:
                    res = Assets.graphics.game.savePoint[0];

                    break;
            }


            return res;

        }

    }
}
