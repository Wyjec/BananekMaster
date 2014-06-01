using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace rokketz
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Random rnd = new Random();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState previousKeyboardState = Keyboard.GetState();
        KeyboardState currentKeyboardState;

        Texture2D starTexture;
        Texture2D rocketwflameTexture;
        Texture2D rocketwoflameTexture;
        Texture2D backgroundTexture;

        SpriteFont hud;

        Map map = new Map(20, 20);
        Player player;
        List<Entity> entities = new List<Entity>();
        Rectangle bounds;


        int score = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base. Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
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

            hud = Content.Load<SpriteFont>("hud");

            backgroundTexture = Content.Load<Texture2D>("nightsky");
            starTexture = Content.Load<Texture2D>("star");
            rocketwflameTexture = Content.Load<Texture2D>("rocketwflame");
            rocketwoflameTexture = Content.Load<Texture2D>("rocketwoflame");

            bounds = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            Restart();
       // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            if(player != null)
                score += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            currentKeyboardState = Keyboard.GetState();
            KeyboardControl(currentKeyboardState, previousKeyboardState);
            if (player != null)
                player.UpdateControl(currentKeyboardState, previousKeyboardState);
            previousKeyboardState = currentKeyboardState;
            // TODO: Add your update logic here

            foreach (Entity e in entities)
                e.Update(gameTime, bounds);
            base.Update(gameTime);

            if(player != null)
                UpdateCollisions();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            map.Draw(spriteBatch);

            foreach (Entity e in entities)
                e.Draw(spriteBatch);

            spriteBatch.DrawString(hud, score.ToString(), Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void KeyboardControl(KeyboardState keyboardstate, KeyboardState previousKeyboardState)
        {
            if(MyHelper.KeyPressed(keyboardstate, previousKeyboardState, Keys.F))
                        graphics.ToggleFullScreen();
            
            if(MyHelper.KeyPressed(keyboardstate, previousKeyboardState, Keys.Escape))
                        Exit();

            if (MyHelper.KeyPressed(keyboardstate, previousKeyboardState, Keys.R))
                Restart();
                
        }

        protected void UpdateCollisions()
        {
            foreach (Entity e in entities.GetRange(1, entities.Count - 1))
            {
                if (player.CheckCollision(e))
                {
                    entities.RemoveAt(0);
                    player = null;
                    break;
                }
            }
        }

        protected void Restart()
        {
            entities.Clear();
            score = 0;

            map.GenerateMap(DateTime.Now.Millisecond, Content);

            player = new Player(new Vector2(bounds.Center.X, bounds.Center.Y),
                                rocketwflameTexture, Vector2.Zero, Vector2.Zero,
                                new Vector2(100.0f, 50.0f));
            entities.Add(player);

            for (int i = 0; i < 5; i++)
            {
                Entity tempEntity;
                do
                {
                    tempEntity = new Entity(new Vector2(rnd.Next(graphics.GraphicsDevice.Viewport.Width), rnd.Next(graphics.GraphicsDevice.Viewport.Height)),
                                            starTexture,
                                            new Vector2(rnd.Next(-100, 100), rnd.Next(-100, 100)),
                                            new Vector2(rnd.Next(-30, 30), rnd.Next(-30, 30)),
                                            new Vector2(rnd.Next(100)));
                } while (player.CheckCollision(tempEntity));

                entities.Add(tempEntity);
            }
     
        }
    }
}
