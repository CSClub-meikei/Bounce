using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Bounce
{
    public class uiObject:GraphicalGameObject
    {
        public event EventHandler GotFocus;
        public event EventHandler LostFocus;
        public event EventHandler Enter;
        public List<Keys> acceptKeys = new List<Keys>();
        bool _selected;
        public uiObject(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            acceptKeys.Add(Keys.Space);

        }
        public override void update(float delta)
        {
            base.update(delta);
            if (Input.onHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && GotFocus != null) { GotFocus(this, EventArgs.Empty); foreach (uiObject u in parent.Controls) if (u != null) { u.Selected = false; Selected = true; } }
            if (Input.onLeave(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && LostFocus != null) LostFocus(this, EventArgs.Empty);
            if (_selected)foreach (Keys k in acceptKeys) if (Input.onKeyUp(k)) if(Enter!=null)Enter(this, EventArgs.Empty);
            if(Input.IsHover(new Rectangle((int)actX, (int)actY, (int)Width, (int)Height)) && Input.OnMouseUp(Input.LeftButton)) if(Enter!=null)Enter(this, EventArgs.Empty);
            

        }
        public bool Selected
        {
            get { return _selected; }
            set {
               
                if (_selected != value)
                {
                    _selected = value;
                    if (GotFocus != null && _selected) GotFocus(this, EventArgs.Empty);
                    if (GotFocus != null && !_selected) LostFocus(this, EventArgs.Empty);
                }
                _selected = value;
            }
        }
        //objと自分自身が等価のときはtrueを返す
        public override bool Equals(object obj)
        {
            //objがnullか、型が違うときは、等価でない
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            //この型が継承できないクラスや構造体であれば、次のようにできる
            //if (!(obj is TestClass))

            //Numberで比較する
            uiObject c = (uiObject)obj;

            return (this.X ==c.X && this.Y == c.Y && this.Width == c.Width && this.Height == c.Height);
            //または、
            //return (this.Number.Equals(c.Number));
        }

        //Equalsがtrueを返すときに同じ値を返す
        public override int GetHashCode()
        {
            return (int)X ^ (int)Y ^ (int)Width ^ (int)Height;
        }
    }
}
