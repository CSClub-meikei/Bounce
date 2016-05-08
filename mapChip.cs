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
        new EditorScreen parent;
        
        public const int BLOCK = 1;
        public const int THORN = 2;
        public const int SWITCH = 3;
        public const int SHPOINT = 4;
        public const int WARPPOINT = 5;
        public const int GUMPOINT = 6;
        public const int GOAL = 7;

        public const int DEFULT = 0;
        public const int MOVING = 1;
        public const int RESIZEING = 2;

        const int size = 40;
        public int type=0;
        public int rotate = 0;
        public int DrawMode = 1;

        public int eventType=0;
        public int eventNum = 0;
        public int[] eventData = new int[2];

        public int clickedX, clickedY;
        public int EditingMode = 0;
        public bool _isSelect=false;
        public int ResizeMode = 0;
        public int FirstX,FirstY,FirstWidth, FirstHeight;
        public float doubleClicktime = 0;
        public bool doubleClickFlag = false;
        public bool AllowResize = true;
        public bool ShowMoveLocation = true;

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

            onDoubleClick += new EventHandler(this.Rotate);


        }

        public override void update(float delta)
        {
            if(Input.OnMouseDown(Input.LeftButton) && !Input.IsHover(new Rectangle((int)actX-10, (int)actY-10, (int)Width+20, (int)Height+20)) && !Input.IsHover(new Rectangle((int)parent.eventEditScreen.X, (int)parent.eventEditScreen.Y, (int)300, (int)400)))
            {
                isSelected = false;
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
            
            if (Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && Input.OnMouseDown(Input.LeftButton) && !isSelected)
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

            if (eventType == 0)
            {
                eventData[0] =(int) X;
                eventData[1] = (int)Y;
            }
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
            if (Input.onKeyDown(Keys.Delete)) { parent.RemoveChips.Add(this); DebugConsole.write("削除"); }
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
                DebugConsole.write("2版");
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
                DebugConsole.write("3版");
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
                DebugConsole.write("4版");
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
                DebugConsole.write("5版");
                EditingMode = RESIZEING;
                ResizeMode = 5;
                clickedX = Input.getPosition().X;
                clickedY = Input.getPosition().Y;
                FirstWidth = (int)Width;
                FirstHeight = (int)Height;
                FirstX = (int)X;
                FirstY = (int)Y;
            }

        }
        public void updateMoving(float deltaTime)
        {

           
            if (Input.IsMouseDown(Input.LeftButton))
            {
                X = (int)((Input.getPosition().X - clickedX - parent.X * 2) / 40) * 40;
                Y = (int)((Input.getPosition().Y - clickedY - parent.Y * 2) / 40) * 40;
                
            }
            if (Input.OnMouseUp(Input.LeftButton))
            {
                EditingMode = DEFULT;

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
                        Width = FirstWidth + (int)((Input.getPosition().X - clickedX) / 40) * 40;

                        break;
                    case 2:
                        Height = FirstHeight + (int)((Input.getPosition().Y - clickedY) / 40) * 40;

                        break;
                    case 3:
                        Width = FirstWidth - (int)((Input.getPosition().X - clickedX) / 40) * 40;
                        X = FirstX + (int)((Input.getPosition().X - clickedX) / 40) * 40;
                        break;
                    case 4:
                        Height = FirstHeight - (int)((Input.getPosition().Y - clickedY) / 40) * 40;
                        Y = FirstY + (int)((Input.getPosition().Y - clickedY) / 40) * 40;
                        break;
                    case 5:
                        Width = FirstWidth + (int)((Input.getPosition().X - clickedX) / 40) * 40;
                        Height = FirstHeight + (int)((Input.getPosition().Y - clickedY) / 40) * 40;
                        break;
                }

                if (Width < 40) { Width = 40; X = FirstX; }
                if (Height < 40) { Height = 40; Y = FirstY; }

            }else if (Input.OnMouseUp(Input.LeftButton))
            {
                EditingMode = DEFULT;
            }
            
        }





        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            if (DrawMode == 1)
            {
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                int i = 0;
                int j = 0;
                for (i = 0; i < Width / size; i++)
                {
                    for (j = 0; j < Height / size; j++)
                    {
                        batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX + i * size), (int)(actY + j * size), (int)size, (int)size), color: Color.White * alpha * screenAlpha);
                        if(eventType==2 && ShowMoveLocation) batch.Draw(Texture, destinationRectangle: new Rectangle((int)(eventData[0] + parent.X + i * size), (int)(eventData[1] + parent.Y + j * size), (int)size, (int)size), color: Color.White * 0.5f * screenAlpha);
                    }

                }
                batch.End();
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                if (isSelected)
                {
                    batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)actX , (int)actY , (int)Width, (int)Height), color: Color.White * 0.5f * alpha * screenAlpha);
                    if (eventType == 2 && ShowMoveLocation) batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)eventData[0] + parent.X , (int)eventData[1] + parent.Y, (int)Width, (int)Height), color: Color.White * 0.2f * alpha * screenAlpha);
                }
                batch.End();
            }
            else if (DrawMode == 2)
            {
                batch.Begin(transformMatrix: game.GetScaleMatrix());
                batch.Draw(Texture, destinationRectangle: new Rectangle((int)(actX), (int)(actY), (int)Width, (int)Height), color: Color.White * alpha * screenAlpha);
                if (eventType == 2) batch.Draw(Texture, destinationRectangle: new Rectangle((int)(eventData[0] + parent.X), (int)(eventData[1] + parent.Y), (int)Width, (int)Height), color: Color.White * 0.5f * alpha * screenAlpha);

                batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)actX, (int)actY, (int)Width, (int)Height), color: Color.White * 0.5f * screenAlpha);
                if (eventType == 2 && ShowMoveLocation) batch.Draw(Assets.graphics.ui.HL, destinationRectangle: new Rectangle((int)eventData[0] + parent.X, (int)eventData[1] + parent.Y, (int)Width, (int)Height), color: Color.White * 0.2f * alpha * screenAlpha);

                batch.End();
            }
            batch.Begin(transformMatrix: game.GetScaleMatrix());
            foreach (GameObjectAnimator a in animator)
            {
                a.Draw(batch, screenAlpha);
            }
            batch.End();
        }
        public void Rotate(object sender,EventArgs e)
        {
            rotate++;
            if (rotate == 4) rotate = 0;
            
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
                    res = Assets.graphics.game.block;
                    break;
                case GUMPOINT:
                    res = Assets.graphics.game.block;
                    break;
                case GOAL:
                    res = Assets.graphics.game.block;
                    break;
            }


            return res;

        }

    }
}
