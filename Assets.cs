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
        public delegate void AssetsLoadEventHandler(object sender, AssetsLoadEventArgs e);

        public static event AssetsLoadEventHandler progress;
        public static event AssetsLoadEventHandler Loaded;
        public static ContentManager Content;

        public  class AssetsLoadEventArgs : EventArgs
        {
            public int progress;
            public string message;
        }

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
                public static Texture2D HL;
                public static Texture2D back_ChipToolbar;
                public static Texture2D trashBox;
                public static Texture2D arrowA;
                public static Texture2D arrowS;
                public static Texture2D arrowR;
                public static Texture2D arrowL;
                public static Texture2D moveEditButton;

                public static Texture2D check;

                public static Texture2D button_file_d;
                public static Texture2D button_file_h;

                public static Texture2D back_uibutton;

                public static Texture2D button_newfile;
                public static Texture2D button_openfile;
                public static Texture2D button_savefile;
                public static Texture2D button_savenewfile;

                public static Texture2D button_mapSetting_d;
                public static Texture2D button_mapSetting_h;
                public static Texture2D back_submenu;

                public static Texture2D back_textbox;
                public static Texture2D back_dialog;


                public static Texture2D startChip;
                public static Texture2D button_testplay;


                public static SpriteFont font;
            }
            public static class game
            {
                public static Texture2D block;
                public static Texture2D frameW;
                public static Texture2D frameH;
                public static Texture2D ball;
                public static Texture2D changePoint;
                public static Texture2D[] Switch;
                public static Texture2D[] thorn;
                public static Texture2D[] ball_animation;
                public static Texture2D goal;
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
            public static SoundEffect glass;
        }
        public static void Initialize(Game1 game)
        {
            Content = game.Content;
            Content.RootDirectory = "Content/graphics/ui";
            graphics.ui.font = Content.Load<SpriteFont>("font");
            Content.RootDirectory = "Content";
        }
        public static void Load(Game1 game)
        {
            report(0, "グラフィックを読み込んでいます...");
            LoadGraphics(Content);
            report(70, "サウンドエフェクトを読み込んでいます...");
            LoadSoundEffects(Content);
            report(85, "BGMを読み込んでいます...");
            LoadBGM(Content);
            report(100, "処理中...");
            Loaded(null, null);
        }
        public static void LoadGraphics(ContentManager Content)
        {
            
            Content.RootDirectory = "Content/graphics";
            report(0, "グラフィックを読み込んでいます... 1/2 (UI)");
            LoadUI(Content);
            report(30, "グラフィックを読み込んでいます... 2/2 (GAME)");
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
            graphics.ui.HL = Content.Load<Texture2D>("HL");
            graphics.ui.arrowA = Content.Load<Texture2D>("arrow+");
            graphics.ui.arrowS = Content.Load<Texture2D>("arrow-");
            graphics.ui.arrowR = Content.Load<Texture2D>("arrowR");
            graphics.ui.arrowL = Content.Load<Texture2D>("arrowL");
            graphics.ui.back_ChipToolbar = Content.Load<Texture2D>("back-ChipToolbar");
            graphics.ui.trashBox = Content.Load<Texture2D>("trashBox");
            graphics.ui.moveEditButton = Content.Load<Texture2D>("moveEditButton");

            graphics.ui.check = Content.Load<Texture2D>("check");

            graphics.ui.back_submenu = Content.Load<Texture2D>("back-submenu");

            graphics.ui.back_uibutton = Content.Load<Texture2D>("back-uibutton");
            graphics.ui.button_file_d = Content.Load<Texture2D>("button-file-d");
            graphics.ui.button_file_h = Content.Load<Texture2D>("button-file-h");
            graphics.ui.button_newfile = Content.Load<Texture2D>("button-newfile");
            graphics.ui.button_openfile = Content.Load<Texture2D>("button-openfile");
            graphics.ui.button_savefile = Content.Load<Texture2D>("button-savefile");
            graphics.ui.button_savenewfile = Content.Load<Texture2D>("button-savenewfile");

            graphics.ui.button_mapSetting_d = Content.Load<Texture2D>("button-mapSetting-d");
            graphics.ui.button_mapSetting_h = Content.Load<Texture2D>("button-mapSetting-h");

            graphics.ui.back_textbox = Content.Load<Texture2D>("back-textbox");
            graphics.ui.back_dialog = Content.Load<Texture2D>("back-dialog");

            graphics.ui.startChip = Content.Load<Texture2D>("startChip");
            graphics.ui.button_testplay = Content.Load<Texture2D>("button-testPlay");

            graphics.ui.font = Content.Load<SpriteFont>("font");
        }
        public static void LoadGame(ContentManager Content)
        {
            int i = 0;

            Content.RootDirectory = "Content/graphics/game";
            graphics.game.block= Content.Load<Texture2D>("block");
            graphics.game.frameW= Content.Load<Texture2D>("frameW");
            graphics.game.frameH = Content.Load<Texture2D>("frameH");
            graphics.game.ball = Content.Load<Texture2D>("ball");
            graphics.game.changePoint = Content.Load<Texture2D>("changePoint");
            graphics.game.Switch = new Texture2D[4];
            graphics.game.thorn = new Texture2D[4];
            graphics.game.goal= Content.Load<Texture2D>("goal");
            for (i = 0; i <= 3; i++) graphics.game.Switch[i] = Content.Load<Texture2D>("switch" + (i + 1).ToString());
            for (i=0;i<=3;i++) graphics.game.thorn[i] = Content.Load<Texture2D>("thorn"+(i+1).ToString());




            Content.RootDirectory = "Content/graphics/game/animation/ball";
            graphics.game.ball_animation = new Texture2D[45];
           
            for(i=0;i<=44;i++)graphics.game.ball_animation[i] = Content.Load<Texture2D>(i.ToString());
        }
        public static void LoadSoundEffects(ContentManager Content)
        {
            Content.RootDirectory = "Content/soundEffects";
            soundEffects.d = Content.Load<SoundEffect>("decision21");
            soundEffects.s = Content.Load<SoundEffect>("cursor6");
            soundEffects.glass = Content.Load<SoundEffect>("glass-break2");
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
        public static void report(int i,string s)
        {
            AssetsLoadEventArgs e = new AssetsLoadEventArgs();
            e.progress = i;
            e.message = s;
            if(progress!=null)progress(null, e);
        }
    }
}
