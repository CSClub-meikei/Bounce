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
    public class Screen
    {
        protected ContentManager Content;
        protected Game1 game;
        public float screenAlpha = 1.0F;
        public ScreenAnimator animator;
        public int X, Y;
        public uiObject[,] Controls;
        Point _selectedItem;
        Point maxItem;
        public Point selectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value;
               
               foreach (uiObject o in Controls) if(o!=null)o.Selected = false;

                if (_selectedItem.X == -1) _selectedItem = new Point(maxItem.X - 1, _selectedItem.Y);
                if (_selectedItem.X == maxItem.X) _selectedItem = new Point(0, _selectedItem.Y);
                if (_selectedItem.Y == -1) _selectedItem = new Point(_selectedItem.X, maxItem.Y-1);
                if (_selectedItem.Y == maxItem.Y) _selectedItem = new Point(_selectedItem.X, 0);
               
                if(Controls[_selectedItem.X, _selectedItem.Y] != null)Controls[_selectedItem.X,_selectedItem.Y].Selected = true;
            }
        }

        public Screen(Game1 game,int sx=0,int sy=0)
        {
            this.Content = game.Content;
            this.game = game;
            X = sx;
            Y = sy;
            animator = new ScreenAnimator(this, game);

        }
        public virtual void update(float deltaTime)
        {
            if (Input.onKeyDown(Keys.Up)) selectedItem = new Point(selectedItem.X, selectedItem.Y-1);
            if (Input.onKeyDown(Keys.Down)) selectedItem = new Point(selectedItem.X, selectedItem.Y+1);
            if (Input.onKeyDown(Keys.Right)) selectedItem = new Point(selectedItem.X+1 , selectedItem.Y);
            if (Input.onKeyDown(Keys.Left)) selectedItem = new Point(selectedItem.X-1 , selectedItem.Y);
            
            foreach (uiObject o in Controls) if (o != null) o.update(deltaTime);
            animator.update(deltaTime);
           // Console.WriteLine(selectedItem.ToString());
        }
        public virtual void Draw(SpriteBatch batch)
        {
            foreach (uiObject o in Controls) if (o != null) o.Draw(batch,screenAlpha);
        }
        public void setUIcell(int x,int y)
        {
            Controls = new uiObject[x,y];
            maxItem = new Point(x, y);
        }
        public Point find(uiObject o)
        {
            int i = 0;
            int j = 0;
            for (i = 0; maxItem.X - 1 > i; i++)
            {
                for (j = 0; j < maxItem.Y - 1; j++)
                {
                    if (o.Equals(Controls[i, j])) return new Point(i, j);
                }
            }
            
            return new Point(-1, -1);
        }
    }
}
