using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bounce.editor;
namespace Bounce
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        
        FPSCounter counter;

        public List<Screen> screens;
        public debugScreen debugScreen;
       
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
            Input.Initialize(this);
            Assets.Initialize(this);
            counter = new FPSCounter();
            base.Initialize();
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Input.onKeyDown(Keys.Q))
            {
                SettingForm f = new SettingForm(this);
                f.ShowDialog();
            }
            if (Input.onKeyDown(Keys.T)) { screens.Clear(); screens.Add(new BackScreen(this)); screens.Add(new UItestScreen(this)); }
            if (Input.onKeyDown(Keys.G))
            {
                screens.Clear();
               
                screens.Add(new GameScreen(this));
            }
            if (Input.onKeyDown(Keys.T))
            {
                screens.Clear();

                screens.Add(new storyScreen(this));
            }
            if (Input.onKeyDown(Keys.E))
            {
                screens.Clear();
                screens.Add(new EditorScreen(this));
            }
            if (Input.onKeyDown(Keys.W))
            {
                screens.Clear();
                screens.Add(new worldMapScreen(this));
            }

            if (Input.IsKeyDown(Keys.D) && Input.IsKeyDown(Keys.LeftShift))
            {
               // debugScreen.AutoScroll = !debugScreen.AutoScroll;
            }
            else if (Input.onKeyDown(Keys.D))
            {
                if (debugScreen.screenAlpha == 0) debugScreen.screenAlpha = 1;
                else if (debugScreen.screenAlpha == 1) debugScreen.screenAlpha = 0;
            }


            // TODO: Add your update logic here
            try
            {
                foreach (Screen s in screens)
                {
                    s.update(deltaTime);
                }
            }
            catch (Exception)
            {

                //throw;
            }
                
            
            


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

            spriteBatch.Begin(transformMatrix: GetScaleMatrix());
            spriteBatch.Draw(Assets.graphics.ui.cursor, new Rectangle(Input.getPosition().X-20, Input.getPosition().Y-10, 100, 100), Color.White);
            spriteBatch.End();

            debugScreen.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        public void ShowToast(string msg,float time)
        {
            screens.Add(new toast(this, msg, time));

        }
        public Matrix GetScaleMatrix()
        {
            var scaleX = (float)graphics.PreferredBackBufferWidth / 1280;
            var scaleY = (float)graphics.PreferredBackBufferHeight / 720;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
    }
}
