using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;

namespace Bounce
{
    class RankScreen:Screen
    {
        public  List<rankingBoard> ranking = new List<rankingBoard>();
        bool Highlight;
        public int hIndex;
        public RankScreen(Game1 game,bool Highlight =false, int hIndex = -1,int sx = 0, int sy = 0) : base(game, sx, sy)
        {
           
            this.Highlight = Highlight;
            this.hIndex = hIndex;
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            if (Input.IsKeyDown(Keys.Up))
            {
                Y += 5;
            }
            if (Input.IsKeyDown(Keys.Down))
            {
                Y -= 5;
            }
            if (Input.onKeyDown(Keys.Space))
            {
                animator = new ScreenAnimator(this, game);
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });
            }
                base.update(deltaTime);
            foreach (rankingBoard r in ranking) r.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            foreach (rankingBoard r in ranking) r.Draw(batch, screenAlpha);
        }
        public void showHighlight()
        {
            DebugConsole.write("hi:" + hIndex.ToString());
            if (Highlight && hIndex >8)
            {
                DebugConsole.write("ggggggg");
                animator.start(ScreenAnimator.SLIDE, new float[] { 2, 0, -hIndex* 80+500, -1, -1, 3, 3 });
            }
        }
    }
}
