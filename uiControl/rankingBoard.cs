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
    class rankingBoard:GraphicalGameObject
    {
        GraphicalGameObject back;
     
        TextObject RankLabel,label;
        public bool Highlight;
        public rankingBoard(Game1 game, Screen screen, int rank,string text, float x, float y, float width, float height) : base(game, screen, null, x, y, width, height)
        {
            Texture2D b=Assets.graphics.ui.ranking;
            if (rank == 1)
            {
                 b = Assets.graphics.ui.ranking_1;
            }
            else if(rank==2)
            {
             b = Assets.graphics.ui.ranking_2;
            }
            else if (rank == 3)
            {
                b = Assets.graphics.ui.ranking_3;
            }
            back = new GraphicalGameObject(game, screen, b, x, y, width, height);
           
            RankLabel = new TextObject(game, screen, Assets.graphics.ui.defultFont, rank.ToString(), Color.Black, new Rectangle((int)x, (int)y, 0, (int)height));

            label = new TextObject(game, screen, Assets.graphics.ui.defultFont, text, Color.Black, new Rectangle((int)(x + height), (int)y, 0, (int)height));
        
        }
        public override void update(float delta)
        {
            back.setLocation(X, Y);
            RankLabel.setLocation(X, Y);
            label.setLocation(X+Height, Y);

            back.update(delta);
            RankLabel.update(delta);
            label.update(delta);
           
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            back.Draw(batch, alpha);
            RankLabel.Draw(batch, alpha);
            label.Draw(batch, alpha);
           
        }
        public void hi()
        {
            back.addAnimator(1);
            back.animator[0].start(GameObjectAnimator.FLASH, new float[] { 1, 0.2f, 0.2f, 0.2f, -1 });
        }
    }
}
