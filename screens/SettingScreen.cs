using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Bounce
{
    class SettingScreen : Screen
    {
        TextObject bgm, effect;
        Slider sb, se;
        
        public SettingScreen(Game1 game,UItestScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            bgm = new TextObject(game, this, Assets.graphics.ui.defultFont, "BGM音量", Color.White, 50, 200);
            effect = new TextObject(game, this, Assets.graphics.ui.defultFont, "エフェクト音量", Color.White, 50, 400);

            sb = new Slider(game, this, new Point(400, 200), 400, 200, 500, 80);
            se = new Slider(game, this, new Point(400, 400), 400, 400, 500, 80);
            sb.MaxValue = 1;
            se.MaxValue = 1;
            sb.step = 0.1f;
            se.step = 0.1f;
            sb.setValue(game.settingData.BGM_volume);
            se.setValue(game.settingData.Effect_volume);
            sb.change += new EventHandler((sender, e) =>
              {
                  game.settingData.BGM_volume = sb.getValue();
                  MediaPlayer.Volume = sb.getValue();
              });
            se.change += new EventHandler((sender, e) =>
              {
                  game.settingData.Effect_volume = se.getValue();
              });
            setUIcell(1, 3);
            Controls[0, 0] = sb;
            Controls[0, 1] = se;
            Controls[0, 2] = new WideButton(game, this, Assets.graphics.ui.wideButtonBack, Assets.graphics.ui.label_return, 600, 500, 540, 120);
            Controls[0, 2].Enter += new EventHandler((sender, e) =>
               {
                   game.removeScreen(this);
                   screen.screenAlpha = 1;
                   screen.enable = true;
               });
            
        }

        public override void update(float deltaTime)
        {
            base.update(deltaTime);
           
            se.update(deltaTime);
            sb.update(deltaTime);

        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);

            bgm.Draw(batch, screenAlpha);
            effect.Draw(batch, screenAlpha);
            se.Draw(batch, screenAlpha);
            sb.Draw(batch, screenAlpha);


        }
    }
}
