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

namespace Bounce
{
    class storyScreen:Screen
    {

        GraphicalGameObject back_comment,back;
        Character man;
        SerifTextObject text;

        storyContent story;

        public int id = 0;

        public storyScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            back_comment = new GraphicalGameObject(game, this, Assets.graphics.ui.back_storyComment, 0, 500, 1280, 230);
            back = new GraphicalGameObject(game, this, Assets.graphics.ui.back_title, 0, 0, 1280, 720);
            setUIcell(1, 1);
            text = new SerifTextObject(game, this, Assets.graphics.ui.defultFont," ", Color.Black, 0.05f, 50, 510);
            story = new storyContent();
            story.storyData.Add( new storyData_1(1,"テストメッセージ。\n",0.02f));
            story.storyData.Add(new storyData_1(1, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(1, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(1, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(1, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(1, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(1, " ", 0.02f));
            story.storyData.Add(new storyData_3(1));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));
            story.storyData.Add(new storyData_1(0, "テストメッセージ。\n", 0.02f));

            nextLoad(null, null);
            text.finish += new EventHandler(this.nextLoad);
            text.play = true;
        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            if (Input.onKeyDown(Keys.Space)) text.next();
            back_comment.update(deltaTime);
            text.update(deltaTime);
            back.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch, screenAlpha);
            back_comment.Draw(batch, screenAlpha);
            text.Draw(batch, screenAlpha);

        }
        public void nextLoad(object sender,EventArgs e)
        {
           
            if(story.storyData[id] is storyData_1)
            {
                text.setData((storyData_1)story.storyData[id]);
            }else if(story.storyData[id] is storyData_3)
            {
                if (((storyData_3)story.storyData[id]).command == 1)
                {
                    text.clear();
                    id++;
                    nextLoad(null, null);
                }
            }


            id++;
        }

    }
}
