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
    class levelSelectList:uiObject
    {
        GraphicalGameObject back;
        TextObject label;
        public string name;
        public string path;

        public levelSelectList(Game1 game, Screen screen,string name,string path, float x, float y, float width, float height, float fx = 0, float fy = 0, float fwidth = 0, float fheight = 0) : base(game, screen, null, x, y, width, height)
        {
            this.name = name;
            this.path = path;

            back = new GraphicalGameObject(game, screen, Assets.graphics.ui.ranking, x-5, y-5, width+10, height+10);
            back.alpha = 0;
            back.addAnimator(2);
            label = new TextObject(game,screen, Assets.graphics.ui.defultFont, name, Color.Black, x + 20, y + 10);

            GotFocus += got;
            LostFocus += lost;
            Enter += enter;
        }

        public void got(object sender,EventArgs e)
        {
            Assets.soundEffects.d.Play(game.settingData.Effect_volume, 1, 1);
            back.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });

        }
        public void lost(object sender,EventArgs e)
        {
            back.animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.2f });
        }
        public void enter(object sender, EventArgs e)
        {
            Assets.soundEffects.s.Play(game.settingData.Effect_volume, 1, 1);
        }
        public override void update(float delta)
        {
            base.update(delta);
            back.update(delta);
            label.update(delta);

        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            back.Draw(batch, screenAlpha);
            label.Draw(batch, screenAlpha);
          //  base.Draw(batch, screenAlpha);
        }
    }
}
