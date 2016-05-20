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
    class GameScreen:Screen
    {

        TextObject time;
        public worldScreen world;
        public readyScreen readyScreen;
        public clearScreen clearScreen;
        public GameScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Bounceマップデータ .bmd|*.bmd";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               
            }

            time = new TextObject(game, this, Assets.graphics.ui.font, "time: 0", Color.White,0,0);
            world = new worldScreen(game, dialog.FileName, false);
            world.onClear += new EventHandler(this.clear);
            readyScreen = new readyScreen(game, this);
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            readyScreen.update(deltaTime);
            if(world.Status==worldScreen.CLEARED)clearScreen.update(deltaTime);
            world.update(deltaTime);
            time.update(deltaTime);

            

            base.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            world.Draw(batch);
            time.Draw(batch, screenAlpha);
            readyScreen.Draw(batch);
            if (world.Status == worldScreen.CLEARED) clearScreen.Draw(batch);
            base.Draw(batch); 
        }
        public void clear(object sender,EventArgs e)
        {
            clearScreen = new clearScreen(game, this);
        }
    }
}
