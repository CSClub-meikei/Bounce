using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Bounce.editor
{
    class FileMenu:Screen
    {
        WideButton newfile, open, save, savenew;
        GraphicalGameObject back;
        public event EventHandler close;

        EditorScreen EditorScreen;

        public FileMenu(Game1 game, EditorScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            this.EditorScreen = screen;
            newfile = new WideButton(game, this, Assets.graphics.ui.back_uibutton, Assets.graphics.ui.button_newfile, 0, 0, 200, 70);
            open = new WideButton(game, this, Assets.graphics.ui.back_uibutton, Assets.graphics.ui.button_openfile, 0, 70, 200, 70);
            save = new WideButton(game, this, Assets.graphics.ui.back_uibutton, Assets.graphics.ui.button_savefile, 0, 140, 200, 70);
            savenew = new WideButton(game, this, Assets.graphics.ui.back_uibutton, Assets.graphics.ui.button_savenewfile, 0, 210, 200, 70);
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_submenu, -9, -7, 300, 350);
            newfile.enableSound = false;
            open.enableSound = false;
            save.enableSound = false;
            savenew.enableSound = false;

            newfile.Enter += new EventHandler(this.newMap);
            open.Enter += new EventHandler(this.openMap);
            save.Enter += new EventHandler(this.saveMap);
            savenew.Enter += new EventHandler(this.saveNewMap);
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            newfile.update(deltaTime);
            open.update(deltaTime);
            save.update(deltaTime);
            savenew.update(deltaTime);
            back.update(deltaTime);
            if(!Input.IsHover(new Rectangle(X,Y,(int)back.Width,(int)back.Height)) && Input.OnMouseDown(Input.LeftButton)){
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f });
                animator.FinishAnimation += new EventHandler((sernder, e) => { if (close != null) close(this, EventArgs.Empty); });
               
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            back.Draw(batch, screenAlpha);
            newfile.Draw(batch, screenAlpha);
            open.Draw(batch, screenAlpha); ;
            save.Draw(batch, screenAlpha); ;
            savenew.Draw(batch, screenAlpha); ;

        }
        public void newMap(object sender, EventArgs e)
        {
            EditorScreen.init();
            animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f });
            animator.FinishAnimation += new EventHandler((sernder2, e2) => { if (close != null) close(this, EventArgs.Empty); });
        }
        public void openMap(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Bounceマップデータ .bmd|*.bmd";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EditorScreen.Load(dialog.FileName);
                EditorScreen.filepath = dialog.FileName;
            }
            animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f });
            animator.FinishAnimation += new EventHandler((sernder2, e2) => { if (close != null) close(this, EventArgs.Empty); });
        }
        public void saveMap(object sender, EventArgs e)
        {
            if (EditorScreen.filepath == "")saveNewMap(sender, e);
            else EditorScreen.Save(EditorScreen.filepath);

            animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f });
            animator.FinishAnimation += new EventHandler((sernder2, e2) => { if (close != null) close(this, EventArgs.Empty); });
        }
        public void saveNewMap(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Filter = "Bounceマップデータ .bmd|*.bmd";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EditorScreen.filepath = dialog.FileName;
                EditorScreen.Save(EditorScreen.filepath);

            }
        }
    }
}
