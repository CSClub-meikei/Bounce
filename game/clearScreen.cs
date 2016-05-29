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
    class clearScreen:Screen
    {
        GraphicalGameObject title;
        TextObject time;
        public clearScreen(Game1 game, GameScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            title = new GraphicalGameObject(game, this, Assets.graphics.game.clear, 320, 100, 640, 200);
            if(screen.disTime) time = new TextObject(game, this, Assets.graphics.ui.defultFont, "クリアタイム : 中間ポイント\nからの再開のため無し" + screen.time, Color.White, 400, 400);
            else time = new TextObject(game, this, Assets.graphics.ui.defultFont, "クリアタイム : " + Math.Round(screen.time, 2, MidpointRounding.AwayFromZero).ToString(), Color.White, 400, 400);

            title.addAnimator(2);
            title.animator[0].setLimit(0.5f);
            title.animator[1].setLimit(0.5f);
            title.animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.5F, 0.5F, 0F, 0.4F, 0.0F, 1F });
            title.animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
            time.alpha = 0;
            time.addAnimator(1);
            time.animator[0].setDelay(1);
            time.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
            animator.setDelay(4f);
            animator.FinishAnimation += new EventHandler((sender, e) => {
                screen.animator.FinishAnimation += new EventHandler((sender2, e2) => {
                    game.screens.Clear();
                    game.screens.Add(new worldMapScreen(game));
                });
                screen.animating=true;
                screen.animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });

            });
            animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            title.update(deltaTime);
            time.update(deltaTime);

        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            title.Draw(batch, screenAlpha);
            time.Draw(batch, screenAlpha);

        }
    }
}
