using Bounce.uiControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Bounce.editor
{
    class menuBar:Screen
    {
        EditorScreen EditorScreen;

        ToggleButton fileButton,settingButton;
        SimpleButton testplay;
         List<Screen> subscreen=new List<Screen>();
        List<Screen> Removescreen = new List<Screen>();

        public menuBar(Game1 game,EditorScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            this.EditorScreen = screen;


            fileButton = new ToggleButton(game, this, Assets.graphics.ui.button_file_d, Assets.graphics.ui.button_file_h, 0, 0, 200, 70);
            fileButton.Enter += new EventHandler((sender, e) => {
                FileMenu ns = new FileMenu(game, EditorScreen, 0, 70);
                ns.close += new EventHandler((sender2, e2) => { Removescreen.Add(ns); });
                ns.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.2f });
                subscreen.Add(ns);
            });
            settingButton = new ToggleButton(game, this, Assets.graphics.ui.button_mapSetting_d, Assets.graphics.ui.button_mapSetting_h, 200, 0, 70, 70);
            settingButton.Enter += new EventHandler((sender, e) => {
                mapSettingScreen ns = new mapSettingScreen(game, EditorScreen, 240, 180);
                ns.close += new EventHandler((sender2, e2) => { Removescreen.Add(ns); });
                ns.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 0.2f });
                subscreen.Add(ns);
            });

            testplay = new SimpleButton(game, this, Assets.graphics.ui.button_testplay, 1200, 0, 70, 70);
            testplay.Enter += new EventHandler(this.testPlay);
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            testplay.update(deltaTime);
            if (EditorScreen.testPlaying) return;
            base.update(deltaTime);
            fileButton.update(deltaTime);
            settingButton.update(deltaTime);
            
            foreach (Screen s in subscreen) s.update(deltaTime);
            foreach (Screen s in Removescreen) subscreen.Remove(s);
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            fileButton.Draw(batch, screenAlpha);
            settingButton.Draw(batch, screenAlpha);
            testplay.Draw(batch, screenAlpha);
            foreach (Screen s in subscreen) s.Draw(batch);
        }
        public void  testPlay(object sender,EventArgs e)
        {
            if (!EditorScreen.testPlaying)
            {
               
                EditorScreen.startTestPlay();
            }
            else
            {
              EditorScreen.stopTestPlay(sender,e);
            }
            
        }
      
    }
}
