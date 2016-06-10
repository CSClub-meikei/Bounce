using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Bounce.socket;
namespace Bounce
{
    class clearScreen:Screen
    {
        GraphicalGameObject title;
        TextObject time;
        GameScreen screen;

        RankScreen rs;
        public clearScreen(Game1 game, GameScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            this.screen = screen;
            title = new GraphicalGameObject(game, this, Assets.graphics.game.clear, 320, 100, 640, 200);
            if(screen.disTime) time = new TextObject(game, this, Assets.graphics.ui.defultFont, "クリアタイム : 中間ポイント\nからの再開のため無し" + screen.time, Color.White, 400, 400);
            else time = new TextObject(game, this, Assets.graphics.ui.defultFont, "クリアタイム : " + Math.Round(screen.time, 2, MidpointRounding.AwayFromZero).ToString(), Color.White, 400, 400);
            Client.tcp.received += Tcp_received;
            if(!screen.disTime && game.enableNet) Client.tcp.send(new packetData(Client.tcp.id, userData.userid.ToString(), @const.request, "bounce,setTimeRecord,"+userData.userid + "," + userData.userName + "," + screen.world.map.title + "," + Math.Round(screen.time, 2, MidpointRounding.AwayFromZero).ToString()));
            if (game.enableNet)Client.tcp.send(new packetData(Client.tcp.id, userData.userid.ToString(), @const.request, "bounce,getTimeRecord,"+ screen.world.map.title ));

            title.addAnimator(2);
            title.animator[0].setLimit(0.5f);
            title.animator[1].setLimit(0.5f);
            title.animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.5F, 0.5F, 0F, 0.4F, 0.0F, 1F });
            title.animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
            time.alpha = 0;
            time.addAnimator(1);
            time.animator[0].setDelay(1);
            time.animator[0].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.2f });
            //animator.setDelay(4f);
           
          //  animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });
            setUIcell(1, 1);
        }

        private void Tcp_received(object sender, packetData e)
        {
            string[] sp = e.data.Split(',');

            if (sp[0] == "getTimeRecord")
            {
                List<rankingBoard> tmp = new List<rankingBoard>();
                int i = 0;
                int hIndex = -1;
                rs = new RankScreen(game, true);
                rs.animator.setDelay(2);
                rs.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.5f });
                for (i=0;i < sp.Length-1;i+=2)
                {
                    rs.ranking.Add(new rankingBoard(game, rs, i/2 + 1, sp[i + 1] + ":" + sp[i + 2], 200, i/2 * 80, 880, 80));
                    if (userData.userName == sp[i + 1] && Math.Round(screen.time, 2, MidpointRounding.AwayFromZero).ToString() == sp[i + 2])
                    {
                        hIndex = i/2;
                        rs.ranking[i/2].Highlight = true;
                        rs.ranking[i / 2].hi();
                    }
                    DebugConsole.write(screen.time.ToString() + "," + sp[i + 2]);
                }
                rs.hIndex = hIndex;


                    
                rs.animator.FinishAnimation += new EventHandler((sender2, e2) =>
                  {
                      rs.animator = new ScreenAnimator(rs,game);
                      rs.animator.setDelay(1f);
                      rs.showHighlight();
                  });
                rs.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.8f });
            }

        }

        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            title.update(deltaTime);
            time.update(deltaTime);
            if (rs != null) rs.update(deltaTime);

            if (Input.onKeyDown(Keys.Space))
            {
                animator.FinishAnimation += new EventHandler((sender, e) => {
                    screen.animator.FinishAnimation += new EventHandler((sender2, e2) => {
                        game.clearScreen();
                        // System.Windows.Forms.MessageBox.Show(screen.levelIndex.ToString());
                        if(screen.levelIndex==-1) game.AddScreen(new EndlessScreen(game));
                        else if (screen.levelIndex == 8) game.AddScreen(new endScreen(game));
                        else game.AddScreen(new worldMapScreen(game, screen.levelIndex, true));
                    });
                    screen.animating = true;
                    screen.animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });

                });
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });

            }
               

          

        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            title.Draw(batch, screenAlpha);
            time.Draw(batch, screenAlpha);
            if (rs != null) rs.Draw(batch);
        }
        
    }
}
