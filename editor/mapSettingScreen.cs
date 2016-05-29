using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Bounce.uiControl;
namespace Bounce.editor
{
    class mapSettingScreen:Screen
    {
        NumUpDown tex,level;
        GraphicalGameObject back;
        TextObject label1,label2;
        SimpleButton title, creator;
        checkBox c;
        mapData tmp;
        public event EventHandler close;

        public mapSettingScreen(Game1 game, EditorScreen screen, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            X = 100;
            Y = 50;
            tex = new NumUpDown(game, this, Color.Black, 300, 100, 200, 50);
            level = new NumUpDown(game, this, Color.Black, 300, 150, 200, 50);
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_dialog, 0, 0, 1100, 620);
            label1 = new TextObject(game, this, Assets.graphics.ui.defultFont, "テクスチャ", Color.Black, 50, 100);
            label2 = new TextObject(game, this, Assets.graphics.ui.defultFont, "レベル", Color.Black, 50, 150);
            c = new checkBox(game, this, 0, 200, 80, 80);
            
            tex.changed+=new EventHandler((sender, e) => {
                
                Assets.LoadGame(game.Content, (int)tex.value);DebugConsole.write("loaded");
                tex.text.text = tex.value.ToString();
                screen.map.texSet = (int)tex.value;
                screen.Save(screen.filepath + "_tmp");
                screen.Load(screen.filepath + "_tmp");
            });
            level.changed += new EventHandler((sender, e) => {
                screen.map.level = (int)level.value;
                level.text.text = level.value.ToString();
            });
            tex.value = screen.map.texSet;
            level.value = screen.map.level;
            tex.text.text = tex.value.ToString();
            level.text.text = level.value.ToString();
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            tex.update(deltaTime);
            level.update(deltaTime);
            back.update(deltaTime);
            label1.update(deltaTime);
            label2.update(deltaTime);
            //  c.update(deltaTime);
            if (!Input.IsHover(new Rectangle(X, Y, (int)back.Width, (int)back.Height)) && Input.OnMouseDown(Input.LeftButton))
            {
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.2f });
                animator.FinishAnimation += new EventHandler((sernder, e) => { if (close != null) close(this, EventArgs.Empty); });

            }
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            back.Draw(batch, screenAlpha);
            tex.Draw(batch, screenAlpha);
            level.Draw(batch, screenAlpha);
            label1.Draw(batch, screenAlpha);
            label2.Draw(batch, screenAlpha);
            //    c.Draw(batch, screenAlpha);
        }
    }
}
