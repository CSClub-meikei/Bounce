using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

           
            screens = new List<Screen>();
            Input.Initialize(this);
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
            Assets.Load(this);
            screens.Add(new SplashScreen(this));
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
                screens.Add(new worldScreen(this));
                screens.Add(new GameScreen(this));
            }
                // TODO: Add your update logic here
                try
            {
foreach(Screen s in screens)
            {
                s.update(deltaTime);
            }

            }
            catch (Exception)
            {

               // throw;
            }
            
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
            spriteBatch.Draw(Assets.graphics.ui.cursor, new Rectangle(Input.getPosition().X, Input.getPosition().Y, 100, 100), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Matrix GetScaleMatrix()
        {
            var scaleX = (float)graphics.PreferredBackBufferWidth / 1280;
            var scaleY = (float)graphics.PreferredBackBufferHeight / 720;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
    }
}
