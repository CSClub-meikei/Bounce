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
    public class debugScreen:Screen
    {
         
        TextObject console;
        GraphicalGameObject back;
        public const int fontSize = 15;
        public static bool _AutoScroll;
        int line;
        public static bool AutoScroll
        {
            get { return _AutoScroll; }
            set { _AutoScroll = value; }
        }
        public debugScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            AutoScroll = true;
            setUIcell(1, 1);
            console = new TextObject(game, this, Assets.graphics.ui.font, "debug Console", Color.White, 0, 0);
            back = new GraphicalGameObject(game, this, Assets.getColorTexture(game, Color.Black), 0, 0, 1280, 200);
            back.alpha = 0.5f;
            DebugConsole.updated += new EventHandler((sender, e) => { console.text = DebugConsole.getOutput(line, line +6);if (AutoScroll) line = DebugConsole.lines-6; });
        }
        public override void update(float deltaTime)
        {
            if(!AutoScroll)line=(Input.getWheel() / 120);
           // Console.WriteLine(DebugConsole.getOutput(line, line + 5));
            console.text = DebugConsole.getOutput(line, line + 6);
           // console.Y = line * fontSize;
            //if (_AutoScroll) console.Y = DebugConsole.lines * fontSize;
            console.update(deltaTime);
            back.update(deltaTime);
            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch, screenAlpha);
            console.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
    }
}
