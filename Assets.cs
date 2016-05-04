using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Bounce
{
    public static class Assets
    {
       
     
       public static  class graphics
        {
            public static class ui
            {
                public static Texture2D titlelogo;
                public static Texture2D titleSquare;
                public static Texture2D wideButtonBack;
                public static Texture2D label_play;
                public static Texture2D label_settings;
                public static Texture2D label_gameselect;
                public static Texture2D label_back;
                public static Texture2D label_multiplay;
                public static Texture2D label_singleplay;
                public static Texture2D back_title;
                public static Texture2D cursor;
                public static Texture2D sp1;
                public static Texture2D sp2;

                public static SpriteFont font;
            }
            public static class game
            {
                public static Texture2D block;
                public static Texture2D frame;
                public static Texture2D ball;
                public static Texture2D thorn;
                public static Texture2D[] ball_animation;
            }
        }
        public static class bgm
        {
            public static Song bgm_title;
        }
        public static class soundEffects
        {
            public static SoundEffect d;
            public static SoundEffect s;
        }
        public static void Load(Game1 game)
        {
            ContentManager Content = game.Content;
            LoadGraphics(Content);
            LoadSoundEffects(Content);
            LoadBGM(Content);
        }
        public static void LoadGraphics(ContentManager Content)
        {
            Content.RootDirectory = "Content/graphics";

            LoadUI(Content);
            LoadGame(Content);
        }
        public static void LoadUI(ContentManager Content)
        {
            Content.RootDirectory = "Content/graphics/ui";
            graphics.ui.cursor = Content.Load<Texture2D>("cursor");
            graphics.ui.titlelogo = Content.Load<Texture2D>("title");
            graphics.ui.titleSquare = Content.Load<Texture2D>("title-Square");
            graphics.ui.wideButtonBack = Content.Load<Texture2D>("wideButtonBack");
            graphics.ui.label_play = Content.Load<Texture2D>("label-play");
            graphics.ui.label_settings = Content.Load<Texture2D>("label-settings");
            graphics.ui.back_title = Content.Load<Texture2D>("back-title");
            graphics.ui.sp1 = Content.Load<Texture2D>("sp1");
            graphics.ui.sp2 = Content.Load<Texture2D>("sp2");
            graphics.ui.label_gameselect = Content.Load<Texture2D>("label-GameSelect");
            graphics.ui.label_singleplay = Content.Load<Texture2D>("label-singleplay");
            graphics.ui.label_multiplay = Content.Load<Texture2D>("label-multiplay");
            graphics.ui.label_back = Content.Load<Texture2D>("back-label");
            graphics.ui.font= Content.Load<SpriteFont>("font");
        }
        public static void LoadGame(ContentManager Content)
        {
            Content.RootDirectory = "Content/graphics/game";
            graphics.game.block= Content.Load<Texture2D>("block");
            graphics.game.frame= Content.Load<Texture2D>("frame");
            graphics.game.ball = Content.Load<Texture2D>("ball");
            graphics.game.thorn = Content.Load<Texture2D>("thorn");




            Content.RootDirectory = "Content/graphics/game/animation/ball";
            graphics.game.ball_animation = new Texture2D[45];
            int i = 0;
            for(i=0;i<=44;i++)graphics.game.ball_animation[i] = Content.Load<Texture2D>(i.ToString());
        }
        public static void LoadSoundEffects(ContentManager Content)
        {
            Content.RootDirectory = "Content/soundEffects";
            soundEffects.d = Content.Load<SoundEffect>("decision21");
            soundEffects.s = Content.Load<SoundEffect>("cursor6");
        }
        public static void LoadBGM(ContentManager Content)
        {
            Content.RootDirectory = "Content/bgm";
            bgm.bgm_title = Song.FromUri("ELIMINATE_LOCKED.mp3", new Uri("Content/bgm/Blue_Ever.mp3", UriKind.Relative));
        }
        public static Texture2D getColorTexture(Game1 game,Color c)
        {
            Color[] color = new Color[1 * 1];
            color[0] = c;
            Texture2D t = new Texture2D(game.GraphicsDevice, 1, 1);
            t.SetData<Color>(color);

            return t;
        }
    }
}
