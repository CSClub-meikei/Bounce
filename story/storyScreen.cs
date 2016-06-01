using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Bounce.story;
using Microsoft.Xna.Framework.Media;

namespace Bounce
{
    class storyScreen : Screen
    {

        GraphicalGameObject back_comment, back;

        SerifTextObject text;
        Character man;
        storyContent story;

        public int id = 0;

        public storyScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            back_comment = new GraphicalGameObject(game, this, Assets.graphics.ui.back_storyComment, 0, 500, 1280, 230);
            back = new GraphicalGameObject(game, this, Assets.graphics.worldMap.labo, 0, 0, 1280, 720);
            man = new Character(game, this, Assets.graphics.Character.man, 1280, 0, 350, 550);


            setUIcell(1, 1);
            text = new SerifTextObject(game, this, Assets.graphics.ui.defultFont, " ", Color.Black, 0.05f, 50, 510);
            story = new storyContent();

            story.storyData.Add(new storyData_1(1, "こんにちは。\nストーリー画面のテストです。\n", 0.02f));
            story.storyData.Add(new storyData_2(true, true, 1, 0, 0));
            story.storyData.Add(new storyData_1(1, "キャラ登場\n", 0.02f));
            story.storyData.Add(new storyData_2(true, true, 1, 1, 0));
            story.storyData.Add(new storyData_1(1, "表情を変えることができます。（笑）\n", 0.02f));
            story.storyData.Add(new storyData_2(true, true, 1, 2, 0));
            story.storyData.Add(new storyData_1(1, "表情を変えることができます。（泣）\n", 0.02f));
            story.storyData.Add(new storyData_2(true, false, 1, 2, 0));
            story.storyData.Add(new storyData_1(1, "喋っていない時などに薄くできます。\n", 0.02f));
            story.storyData.Add(new storyData_2(true, true, 1, 1, 0));
            story.storyData.Add(new storyData_1(1, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(1, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_2(false, true, 1, 2, 0));
            story.storyData.Add(new storyData_1(1, "キャラ退場", 0.02f));
            story.storyData.Add(new storyData_3(1));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_3(2));
            nextLoad(null, null);
           // text.finish += new EventHandler(this.nextLoad);
            text.play = true;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Assets.bgm.story);
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            if (Input.onKeyDown(Keys.Space))nextLoad(null,null);
            back_comment.update(deltaTime);
            text.update(deltaTime);
            back.update(deltaTime);
            man.update(deltaTime);

        }
        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch, screenAlpha);
            man.Draw(batch, screenAlpha);
            back_comment.Draw(batch, screenAlpha);
            text.Draw(batch, screenAlpha);

        }
        public void nextLoad(object sender, EventArgs e)
        {
            DebugConsole.write("id:"+id.ToString());
            if (story.storyData[id] is storyData_1)
            {
                text.setData((storyData_1)story.storyData[id]);
                text.next();
                id++;
            }
            else if (story.storyData[id] is storyData_2)
            {
                if ((((storyData_2)story.storyData[id]).ch == 1))
                {
                    DebugConsole.write(((storyData_2)story.storyData[id]).mode.ToString());
                    man.face = ((storyData_2)story.storyData[id]).mode;
                    man.active = ((storyData_2)story.storyData[id]).active;
                    if (((storyData_2)story.storyData[id]).isShow)
                    {
                        man.show(new Point(900, 0));

                    }
                    else
                    {
                        man.hide(new Point(1300, 0));
                    }
                    id++;
                    nextLoad(null, null);
                }
               
            }
            else if (story.storyData[id] is storyData_3)
            {
                if (((storyData_3)story.storyData[id]).command == 1)
                {
                    text.clear();
                    id++;
                    nextLoad(null, null);
                }
                else if (((storyData_3)story.storyData[id]).command == 2)
                {
                    game.screens.Clear();
                    game.screens.Add(new worldMapScreen(game));
                }
            }
           
        }
    }
}
