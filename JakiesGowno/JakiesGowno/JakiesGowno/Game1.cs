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
using Microsoft.Xna.Framework.Net;

namespace JakiesGowno
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum GameState
        {
            Menu,
            Game
        }

        GameState gameState = GameState.Menu;

        Network network;
        Chat chat = new Chat();

        SpriteFont hudFont;
        public SpriteFont nickFont;
        SpriteFont chatFont;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Player player;
        List<Player> players;

        Texture2D playerTexture;
        Texture2D pixelTexture;
        Texture2D titleScreen;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        ServerMap map = new ServerMap();

        bool drawHud = false;
        float gravity = 0.2f;
        float constSpeedX = 2;
        float constSpeedY = 5;
        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 800;            
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
            base.Initialize();

            Animation tempAnimation = new Animation();
            tempAnimation.Initialize(playerTexture, 9, TimeSpan.FromMilliseconds(100), true);

            chat.Initialize(chatFont, new Vector2(10,340), new Vector2(10,440), Chat.Mode.NoBackground, 6);                       
            players = new List<Player>();
            players.Add(new Player());
            players[0].Initialization(tempAnimation, Vector2.Zero, Vector2.Zero, new Vector2(0, gravity), "Jelcynov");
            player = players[0];
            network = new Network(players, Content, map);
            map.Initialize(GraphicsDevice, player.position);

            titleScreen = Content.Load<Texture2D>("titlescreen");

            chat.WelcomeMessage();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixelTexture = Content.Load<Texture2D>("pixel");
            playerTexture = Content.Load<Texture2D>("banan");
            
            hudFont = Content.Load<SpriteFont>("hud");
            nickFont = Content.Load<SpriteFont>("nickFont");
            chatFont = Content.Load<SpriteFont>("chatFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Enter))
                this.Exit();

            if (currentKeyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter)) 
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftAlt))
                {
                    graphics.ToggleFullScreen();
                }
                else
                {
                    string temp;
                    temp = chat.ToggleEnable();
                    if (temp != null)
                    {
                        if (temp[0] == '/')
                            SystemCommand(temp.Substring(1));
                        else
                        {
                            if (!network.SendChat(temp))

                                chat.AddMessage(new Message("Not connected to server. F1 to connect.", Color.Red));
                        }
                    }
                }
            }

            if (chat.typeEnabled)
                chat.UpdateInput(currentKeyboardState, previousKeyboardState);
            else
                UpdatePlayerInput(currentKeyboardState, previousKeyboardState);

            Packet packetMenu = network.ReceivedMessage(chat);

            switch(gameState)
            {
                case GameState.Menu:
                    if (packetMenu == Packet.AreaParam)
                    {
                        chat.Initialize(chatFont, new Vector2(10, 10), new Vector2(10, 440));           
                        gameState = GameState.Game;
                    }

                    if (currentKeyboardState.IsKeyDown(Keys.F1) && previousKeyboardState.IsKeyUp(Keys.F1))
                    {
                        network.TryJoin(player.nick);
                    }
                break;
            
                case GameState.Game:
                if (map.Update(player.position, Content))
                {
                    network.NodesRequest();
                }

                if (currentKeyboardState.IsKeyDown(Keys.F10) && previousKeyboardState.IsKeyUp(Keys.F10))
                    player.position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 - 100);             
                if (currentKeyboardState.IsKeyDown(Keys.F11))
                    player.acc.Y -= 0.01f;
                if (currentKeyboardState.IsKeyDown(Keys.F12))
                    player.acc.Y += 0.01f;
                if (currentKeyboardState.IsKeyDown(Keys.Tab) && previousKeyboardState.IsKeyUp(Keys.Tab))
                    drawHud = !drawHud;

                foreach (Player tPlayer in players)
                {
                    tPlayer.Update(gameTime);
                    UpdatePlayer(tPlayer);
                }
                network.Update(gameTime);
                base.Update(gameTime);
                break;
            }
        }

        private void UpdatePlayer(Player updatePlayer)
        {
            bool collide = false;

            updatePlayer.speed.Y += updatePlayer.acc.Y;
            updatePlayer.position.Y += updatePlayer.speed.Y;

            collide = map.CheckCollision(new Rectangle((int)updatePlayer.position.X - updatePlayer.width / 2, (int)updatePlayer.position.Y - updatePlayer.height / 2, updatePlayer.width, updatePlayer.height));
   
            if (collide)
            {
                updatePlayer.position.Y -= updatePlayer.speed.Y;
                if (updatePlayer.speed.Y > 0)
                {
                    updatePlayer.inAir = false;
                    updatePlayer.doubleJump = false;
                }
                updatePlayer.speed.Y = 0f;
            }
            else
            {
                updatePlayer.inAir = true;
            }

            updatePlayer.speed.X += updatePlayer.acc.X;
            updatePlayer.position.X += updatePlayer.speed.X;
            collide = map.CheckCollision(new Rectangle((int)updatePlayer.position.X - updatePlayer.width / 2, (int)updatePlayer.position.Y - updatePlayer.height / 2, updatePlayer.width, updatePlayer.height));
         
            if (collide)
            {
                updatePlayer.position.X -= updatePlayer.speed.X;
                updatePlayer.speed.X = 0f;
            }
        }

        public void UpdatePlayerInput(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
                player.speed.X = -constSpeedX;
            if (keyboardState.IsKeyDown(Keys.Right))
                player.speed.X = constSpeedX;
            if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
            {
                if (!player.inAir)
                {
                    player.speed.Y = -constSpeedY;
                }
                if (player.inAir && !player.doubleJump)
                {
                    player.doubleJump = true;
                    player.speed.Y = -constSpeedY;
                }
            }
            if (keyboardState.IsKeyUp(Keys.Left) && previousKeyboardState.IsKeyDown(Keys.Left))
                player.speed.X = 0f;
            if (keyboardState.IsKeyUp(Keys.Right) && previousKeyboardState.IsKeyDown(Keys.Right))
                player.speed.X = 0f;
            if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Right))
                player.speed.X = 0f;

            if (keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space)) // enter
            {
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    //map.Dig(DigDirection.Left, player.position);
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    //map.Dig(DigDirection.Right, player.position);
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    //map.Dig(DigDirection.Down, player.position);
                }
                else if (keyboardState.IsKeyDown(Keys.Up))
                {
                    //map.Dig(DigDirection.Up, player.position);
                }
            }
        }
       
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            frameCounter++;
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:
                    spriteBatch.Draw(titleScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.TitleSafeArea.Width, GraphicsDevice.Viewport.TitleSafeArea.Height), Color.White);
                    break;

                case GameState.Game:
                    map.Draw(spriteBatch, player.position);
                    player.Draw(spriteBatch, Render.Fixed, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width/2, GraphicsDevice.Viewport.TitleSafeArea.Height/2), pixelTexture, nickFont);
                    for (int i = 1; i < players.Count; i++)
                    {
                        players[i].Draw(spriteBatch, Render.Normal, player.position - new Vector2(GraphicsDevice.Viewport.TitleSafeArea.Width / 2, GraphicsDevice.Viewport.TitleSafeArea.Height / 2), pixelTexture, nickFont);
                    }
                    break;
            }
            chat.Draw(spriteBatch, pixelTexture);
            DrawHud(spriteBatch);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void DrawHud(SpriteBatch spriteBatch)
        {
            if (drawHud)
            {
                spriteBatch.Draw(pixelTexture, new Rectangle(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y, GraphicsDevice.Viewport.TitleSafeArea.Width, GraphicsDevice.Viewport.TitleSafeArea.X + 50), Color.DarkRed);
                spriteBatch.DrawString(hudFont, "FPS: " + frameRate + " Gravity: " + gravity, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
                spriteBatch.DrawString(hudFont, "SpeedX: " + player.speed.X + " SpeedY: " + player.speed.Y + " positionX: " + player.position.X + " positionY: " + player.position.Y + " acc.x: " + player.acc.X + " acc.y: " + player.acc.Y, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 15), Color.White);
                spriteBatch.DrawString(hudFont, "bConnected: " + network.bConnected.ToString() + " ,currentAreaRow: " + ServerMap.ActiveAreaRow(player.position) + " ,currentAreaCol: " + ServerMap.ActiveAreaCol(player.position) + " ,activeAreaSeed: " + map.CurrentAreaSeed(player.position), new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);
            }
        }

        public void SystemCommand(string command)
        {
            string[] args = command.Split(' ');
            switch (args[0])
            {
                case "nick":
                    if (args.Length > 1 && args[1] != "")
                        player.nick = args[1];
                    chat.AddMessage(new Message("Changed nickname to: " + player.nick, Color.Green));
                    break;
                case "help":
                    chat.HelpMessage();
                    break;
                case "chat":
                    if (args.Length > 1 && args[1] != "")
                    {
                        int chatMode;
                        if(int.TryParse(args[1], out chatMode))
                            chat.mode = (Chat.Mode)chatMode;
                    }
                    break;
            }
        }
    } 
}
