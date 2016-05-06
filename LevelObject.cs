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
    class LevelObject:GraphicalGameObject
    {

        public bool _flag;
        public int flagType;
        public int flagNum;
        public Point moveLocation;
        public int EventVisible;
        public event EventHandler flagChanged;
        new worldScreen parent;


        public bool flag
        {
            get { return _flag; }
            set { _flag = value; if(flagChanged!=null)flagChanged(this, EventArgs.Empty); }
        }
        public LevelObject(Game1 game, Screen screen, Texture2D Texture, float x, float y, float width, float height) : base(game, screen, Texture, x, y, width, height)
        {
            parent = (worldScreen)screen;
        }
        public override void update(float delta)
        {
           if(flagNum!=0) if (parent.flags[flagNum] && !flag) flag = true;

            base.update(delta);
        }

    }
}
