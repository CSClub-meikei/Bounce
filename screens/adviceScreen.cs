using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bounce.story;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce
{
    class adviceScreen:Screen
    {
        Character ch;
        GraphicalGameObject back;
        TextObject label;
        public event EventHandler closed;

        public adviceScreen(Game1 game,int ch,int face,string msg, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            X = 100;
            Y = 50;
            setUIcell(1, 1);
            if (ch == 1)
            {
                this.ch = new Character(game, this, Assets.graphics.Character.man, 50, 270, 200, 300);
                this.ch.face = face;

            }else if(ch==2)
            {

            }

            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_dialog,0, 0, 1100, 620);
            label = new TextObject(game, this, Assets.graphics.ui.defultFont, msg, Color.Black, 220, 100);


        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            ch.update(deltaTime);
            back.update(deltaTime);
            label.update(deltaTime);

            if (Input.onKeyDown(Keys.Space))
            {
                animator.FinishAnimation += new EventHandler((sender, e) => {
                    if (closed != null) closed(this, EventArgs.Empty);
                    game.screens.Remove(this);

                });
                animator.setDelay(0);
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f });

            }

        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);

            back.Draw(batch, screenAlpha);
            ch.Draw(batch, screenAlpha);
            label.Draw(batch, screenAlpha);

        }
    }
}
