using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Bounce.uiControl;
namespace Bounce.editor
{
    class mapSettingScreen:Screen
    {
        NumUpDown test;
        GraphicalGameObject back;
        checkBox c;
        public event EventHandler close;

        public mapSettingScreen(Game1 game, EditorScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            test = new NumUpDown(game, this, Color.White, 0, 0, 300, 80);
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_dialog, 0, 0, 800, 400);
            c = new checkBox(game, this, 0, 200, 80, 80);
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            test.update(deltaTime);
            back.update(deltaTime);
            c.update(deltaTime);
            if (!Input.IsHover(new Rectangle(X, Y, (int)back.Width, (int)back.Height)) && Input.OnMouseDown(Input.LeftButton))
            {
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f });
                animator.FinishAnimation += new EventHandler((sernder, e) => { if (close != null) close(this, EventArgs.Empty); });

            }
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            back.Draw(batch, screenAlpha);
            test.Draw(batch, screenAlpha);
            c.Draw(batch, screenAlpha);
        }
    }
}
