using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Bounce.socket;

namespace Bounce
{
    class LoginScreen:Screen
    {
        GraphicalGameObject back;
        TextObject title, label, textbox;
        public LoginScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            title = new TextObject(game, this, Assets.graphics.ui.defultFont, "ログイン", Color.Black, new Rectangle(0, 0, 1280, 300));
            label= new TextObject(game, this, Assets.graphics.ui.defultFont, "ID", Color.Black, 100,300);
            textbox= new TextObject(game, this, Assets.graphics.ui.defultFont, "", Color.Black, 200, 300);
            back = new GraphicalGameObject(game, this, Assets.getColorTexture(game, Color.White), 0, 0, 1280, 720);

            Client.tcp.received += Tcp_received;
            setUIcell(1, 1);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            title.update(deltaTime);
            label.update(deltaTime);
            textbox.update(deltaTime);

            if(Input.onKeyDown(Keys.D1) || Input.onKeyDown(Keys.NumPad1))
            {
                textbox.text += "1";
            }
            if (Input.onKeyDown(Keys.D2) || Input.onKeyDown(Keys.NumPad2))
            {
                textbox.text += "2";
            }
            if (Input.onKeyDown(Keys.D3) || Input.onKeyDown(Keys.NumPad3))
            {
                textbox.text += "3";
            }
            if (Input.onKeyDown(Keys.D4) || Input.onKeyDown(Keys.NumPad4))
            {
                textbox.text += "4";
            }
            if (Input.onKeyDown(Keys.D5) || Input.onKeyDown(Keys.NumPad5))
            {
                textbox.text += "5";
            }
            if (Input.onKeyDown(Keys.D6) || Input.onKeyDown(Keys.NumPad6))
            {
                textbox.text += "6";
            }
            if (Input.onKeyDown(Keys.D7) || Input.onKeyDown(Keys.NumPad7))
            {
                textbox.text += "7";
            }
            if (Input.onKeyDown(Keys.D8) || Input.onKeyDown(Keys.NumPad8))
            {
                textbox.text += "8";
            }
            if (Input.onKeyDown(Keys.D9) || Input.onKeyDown(Keys.NumPad9))
            {
                textbox.text += "9";
            }
            if (Input.onKeyDown(Keys.D0) || Input.onKeyDown(Keys.NumPad0))
            {
                textbox.text += "0";
            }
            if (Input.onKeyDown(Keys.Back) || Input.onKeyDown(Keys.Delete))
            {
                textbox.text = textbox.text.Remove(textbox.text.Length - 1);

            }
            if (Input.onKeyDown(Keys.Enter) || Input.onKeyDown(Keys.Space))
            {
                Client.tcp.send(new packetData(Client.tcp.id, textbox.text, @const.request, "login," + textbox.text + "," +"defult" + ",bounce"));
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            back.Draw(batch, screenAlpha);
            title.Draw(batch, screenAlpha);
            label.Draw(batch, screenAlpha);
            textbox.Draw(batch, screenAlpha);

        }
        private void Tcp_received(object sender, packetData e)
        {
            // MessageBox.Show(e.data);
            string[] sp = e.data.Split(',');
            if (sp[0] == "success")
            {
                userData.userName = sp[1];
                userData.userid = int.Parse(textbox.text);
                Client.tcp.send(new packetData(Client.tcp.id, userData.userid.ToString(), @const.request, "bounce,getSaveData"));

                animator.FinishAnimation += new EventHandler((sender2, e2) => {
                    game.screens.Remove(this);
                    Screen ns = new SplashScreen(game);
                    // ns.animator.setDelay(2f);
                    ns.animator.start(ScreenAnimator.fadeInOut, new float[] { 0, 1 });
                    game.screens.Add(ns);
                });
                animator.start(ScreenAnimator.fadeInOut, new float[] { 1, 0.5f });

            }
            else
            {
                textbox.text = "";
            }
        }
    }
}
