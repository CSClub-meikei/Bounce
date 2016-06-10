using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bounce.editor;
using Bounce.socket;
using System.Runtime.Serialization;
using System.Xml;

namespace Bounce
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public SettingData settingData;

        FPSCounter counter;

        public assistScreen assistScreen;

        bool OnclearScreen;

        public List<Screen> screens;
        public List<Screen> Rscreens;
        public List<Screen> Ascreens;
        public List<Screen> Iscreens;
        public debugScreen debugScreen;

        public bool enableNet = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;
            screens = new List<Screen>();
            Rscreens = new List<Screen>();
            Ascreens = new List<Screen>();
            Iscreens = new List<Screen>();
            Input.Initialize(this);
            Assets.Initialize(this);
            counter = new FPSCounter();
            base.Initialize();
           // IsMouseVisible = false;

            Window.Title = "Bounce build:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;
            Window.IsBorderless = true;
            Window.Position = Point.Zero;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugScreen = new debugScreen(this);
            debugScreen.screenAlpha = 0;
            screens.Add(new AssetsLoadScreen(this));
           
            string[] cmds = System.Environment.GetCommandLineArgs();
            
            if (cmds.Length >=2)
            {
                if (cmds[1] == "enableNetWork")
                {

                    Client.connect(this);
                    Client.tcp.startReceive();
                    enableNet = true;
                   
                    //保存元のファイル名
                    string fileName = @"tmp.tmp";

                    //DataContractSerializerオブジェクトを作成
                    DataContractSerializer serializer =
                        new DataContractSerializer(typeof(SettingData));
                    //読み込むファイルを開く
                    XmlReader xr = XmlReader.Create(fileName);
                    //XMLファイルから読み込み、逆シリアル化する
                    settingData = (SettingData)serializer.ReadObject(xr);
                    //ファイルを閉じる
                    xr.Close();
                    // System.Windows.Forms.MessageBox.Show(game.settingData.BGM_volume.ToString());
                    // DebugConsole.write(game.settingData.BGM_volume.ToString());

                }
            }
            if (!enableNet)
            {
                userData.userName = "プレイヤー";
                settingData = new SettingData();
                settingData.init(this);
                if (!System.IO.File.Exists("saveData.save"))
                {
                   
                    settingData.save();
                }
            }
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float deltaTime = counter.getDeltaTime(gameTime);
            // Window.Title = (1 / counter.getDeltaTime(gameTime) ).ToString();

          
            if (Input.onKeyDown(Keys.Q))
            {
                SettingForm f = new SettingForm(this);
                f.ShowDialog();
            }
          
            if (Input.onKeyDown(Keys.G))
            {
               // screens.Clear();
               
              //  screens.Add(new GameScreen(this,0));
            }
            if (Input.onKeyDown(Keys.T))
            {
                screens.Clear();

                screens.Add(new BackScreen(this)); screens.Add(new UItestScreen(this));
            }
            if (Input.onKeyDown(Keys.E))
            {
                screens.Clear();
                screens.Add(new EditorScreen(this));
            }
            if (Input.onKeyDown(Keys.W))
            {
              //  screens.Clear();
              //  screens.Add(new worldMapScreen(this,0));
            }
            if (Input.onKeyDown(Keys.C))
            {
               // screens.Add(new adviceScreen(this,1,0,"メッセージのテストです\nここにアドバイス等が表示されます"));
            }
            if (Input.IsKeyDown(Keys.S))
            {
               // clearScreen();
               // AddScreen(new endScreen(this));
            }
            else if (Input.onKeyDown(Keys.D))
            {
              //  if (debugScreen.screenAlpha == 0) debugScreen.screenAlpha = 1;
              //  else if (debugScreen.screenAlpha == 1) debugScreen.screenAlpha = 0;
            }


            // TODO: Add your update logic here
          
                foreach (Screen s in screens)
                {
                    s.update(deltaTime);
                }
            if (OnclearScreen)
            {
                screens.Clear();
                OnclearScreen = false;
            }
            foreach (Screen s in Rscreens)
            {
                screens.Remove(s);
            }
            Rscreens.Clear();
            foreach (Screen s in Ascreens)
            {
                screens.Add(s);
            }
            Ascreens.Clear();
            foreach (Screen s in Iscreens)
            {
                screens.Insert(0,s);
            }
            Iscreens.Clear();

            if(assistScreen!=null)assistScreen.update(deltaTime);

            debugScreen.update(deltaTime);
            Input.update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            foreach(Screen s in screens)
            {
                s.Draw(spriteBatch);
            }
            if (assistScreen != null) assistScreen.Draw(spriteBatch);
            spriteBatch.Begin(transformMatrix: GetScaleMatrix());
            spriteBatch.Draw(Assets.graphics.ui.cursor, new Rectangle(Input.getPosition().X-20, Input.getPosition().Y-10, 100, 100), Color.White);
            spriteBatch.End();

            debugScreen.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        public void ShowToast(string msg,float time)
        {
            AddScreen(new toast(this, msg, time));

        }
        public void assist(int mode,bool show)
        {
            assistScreen.setShow(mode, show);
        }
        public Matrix GetScaleMatrix()
        {
            var scaleX = (float)graphics.PreferredBackBufferWidth / 1280;
            var scaleY = (float)graphics.PreferredBackBufferHeight / 720;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
        public void removeScreen(Screen screen)
        {
            Rscreens.Add(screen);
        }
        public void AddScreen(Screen screen)
        {
            Ascreens.Add(screen);
        }
        public void InsertScreen(Screen screen)
        {
            Iscreens.Add(screen);
        }
        public void clearScreen()
        {
            OnclearScreen = true;
        }
    }
}
