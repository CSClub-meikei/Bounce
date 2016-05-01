using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
namespace Bounce
{
    class UItestScreen:Screen
    {
        GraphicalGameObject title,sq1,sq2,back;
        float sqangle;

        public UItestScreen(Game1 game) : base(game)
        {
            title = new GraphicalGameObject(game,this,Assets.graphics.ui.titlelogo,200,25,880,350);
            sq1 = new GraphicalGameObject(game, this, Assets.graphics.ui.titleSquare,200,50,100,100);
            sq2 = new GraphicalGameObject(game, this, Assets.graphics.ui.titleSquare,1000,250,100,100);
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_title, 0, 0, 1280, 1280);
            back.alpha = 0.3f;
            setUIcell(1,2);
            Controls[0,0]=(new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_play, 370, 400, 540, 120));
            Controls[0,1]=(new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_settings, 370, 550, 540, 120));
            selectedItem = new Point(0,0);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Assets.bgm.bgm_title);   
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);

            title.update(deltaTime);
            sqangle += 1;
            sq1.setAngle(sqangle);
            sq2.setAngle(-sqangle);
            sq1.update(deltaTime);
            sq2.update(deltaTime);
            back.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {

            back.Draw(batch, screenAlpha);
            title.Draw(batch, screenAlpha);
            sq1.Draw(batch, screenAlpha);
            sq2.Draw(batch, screenAlpha);

            base.Draw(batch);
        }
    }
}
