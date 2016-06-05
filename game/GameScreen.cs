using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Bounce
{
    class GameScreen:Screen
    {

        TextObject timeLabel;
        public float time;
        public bool disTime = false;
        public worldScreen world;
        public readyScreen readyScreen;
        public clearScreen clearScreen;

        public string filePath;

        public Point savePoint;

        public bool animating=true;

        public int levelIndex=0;

        public GameScreen(Game1 game,int levelIndex,string filePath ="", int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            this.filePath = filePath;
            if (this.filePath == ""){
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.Filter = "Bounceマップデータ .bmd|*.bmd";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.filePath = dialog.FileName;
                    animating = false;
                }
                
            }
           // System.Windows.Forms.MessageBox.Show(levelIndex.ToString());
            timeLabel = new TextObject(game, this, Assets.graphics.ui.font, "time: --", Color.White,0,0);
            world = new worldScreen(game,this.filePath, false,Point.Zero);
            world.onClear += new EventHandler(this.clear);
            readyScreen = new readyScreen(game, this);
            setUIcell(1, 1);
            this.levelIndex = levelIndex;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Assets.bgm.bgm1);
        }
        public override void update(float deltaTime)
        {
           // DebugConsole.write("in:"+levelIndex.ToString());
            base.update(deltaTime);
            if (animating)
            {
                world.screenAlpha = screenAlpha;
                readyScreen.screenAlpha = screenAlpha;
                return;
            }
            readyScreen.update(deltaTime);
            if(world.Status==worldScreen.CLEARED)clearScreen.update(deltaTime);
            if (world.Status == worldScreen.DIED)
            {
                time = 0;
                savePoint = world.savePoint;
                if (savePoint != Point.Zero)
                {
                    disTime = true;
                    timeLabel = new TextObject(game, this, Assets.graphics.ui.font, "time: 中間地点からのため無効", Color.White, 0, 0);
                }
                
                world = new worldScreen(game, filePath, false, savePoint);
                world.onClear += new EventHandler(this.clear);
                readyScreen = new readyScreen(game, this);
            } 
            world.update(deltaTime);
            timeLabel.update(deltaTime);
            if (world.Status == worldScreen.RUNNING && !disTime)
            {
                time += deltaTime / 1000;
                timeLabel.text = "Time : " + Math.Round(time,2, MidpointRounding.AwayFromZero).ToString();
            }
            

           
        }
        public override void Draw(SpriteBatch batch)
        {
            world.Draw(batch);
            timeLabel.Draw(batch, screenAlpha);
            readyScreen.Draw(batch);
            if (world.Status == worldScreen.CLEARED) clearScreen.Draw(batch);
            base.Draw(batch); 
        }
        public void clear(object sender,EventArgs e)
        {
            world.animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f, 0.5f });
            clearScreen = new clearScreen(game, this);
        }
    }
}
