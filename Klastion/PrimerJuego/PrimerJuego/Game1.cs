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

namespace PrimerJuego
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState currMouseState;
        KeyboardState ksLast = Keyboard.GetState();
        SpriteManager spriteManager;
        SpriteManagerTwo spriteManagerTwo;
        SpriteManagerThree spriteManagerThree;
        Menu menu;
        GameOver gameover;
        SelectLevel selectlvl;
        Pause pause;
        Creditos creditos;
        Conf conf;
        Boolean pauseB = false;
        enum GameState { Start, InGame, InGame2, InGame3, SelectLevel, GameOver, GameOver2, GameOver3, Pause, creditos, Conf };
        GameState currentGameState = GameState.Start;
        
        AudioEngine audioEngine;
        AudioCategory musicCategory;
        float musicVolume = 1.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
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
            audioEngine = new AudioEngine(@"Content\Sonidos\sonidos.xgs");
            musicCategory = audioEngine.GetCategory("Music");

            spriteManager = new SpriteManager(this);
            spriteManagerTwo = new SpriteManagerTwo(this);
            spriteManagerThree = new SpriteManagerThree(this);
            menu = new Menu(this);
            gameover = new GameOver(this);
            selectlvl = new SelectLevel(this);
            pause = new Pause(this);
            creditos = new Creditos(this);
            conf = new Conf(this);
            Components.Add(menu);
            menu.Enabled = true;
            //Components.Add(spriteManager);
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
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.IsFullScreen = true;
            
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
            graphics.ApplyChanges();
            currMouseState = Mouse.GetState();

            
            switch (currentGameState)
            {
                case GameState.Start:

                    if (menu.getPulsado[1] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        menu.pararMusica();
                        currentGameState = GameState.InGame;
                        menu.Enabled = false;
                        Components.Remove(menu);
                        Components.Add(spriteManager);
                        spriteManager.setVolumen(musicVolume);
                        spriteManager.Enabled = true;
                    }
                    else if (menu.getPulsado[0] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        menu.pararMusica();
                        currentGameState = GameState.SelectLevel;
                        menu.Enabled = false;
                        Components.Remove(menu);
                        selectlvl.Enabled = true;
                        Components.Add(selectlvl);
                    }
                    else if (menu.getPulsado[2] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        Components.Add(conf);
                        conf.Enabled = true;
                        conf.setVolumen(musicVolume);
                        currentGameState = GameState.Conf;
                        menu.pararMusica();
                        menu.Enabled = false;
                        Components.Remove(menu);
                        //Components.Add(creditos);
                        //creditos.setVolumen(musicVolume);
                        //creditos.Enabled = true;
                        //currentGameState = GameState.creditos;
                    }
                    else if (menu.getPulsado[3] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        //menu.pararMusica();
                        //this.Exit();
                    }
                    break;
                case GameState.InGame:
                    if (spriteManager.getMuertoDelTo)
                    {
                        Components.Add(gameover);
                        gameover.Enabled = true;
                        currentGameState = GameState.GameOver;
                    }
                    if (spriteManager.getCompletoDelTo)
                    {
                        spriteManager.Enabled = false;
                        Components.Remove(spriteManager);
                        spriteManager.pararMusica();
                        spriteManagerTwo.Enabled = true;
                        Components.Add(spriteManagerTwo);
                        spriteManagerTwo.setVolumen(musicVolume);
                        currentGameState = GameState.InGame2;
                    }
                    //PAUSAMEEEEEEEEEEE
                    Pausar(spriteManager);
                    break;
                case GameState.InGame2:
                    if (spriteManagerTwo.getMuertoDelTo)
                    {
                        Components.Add(gameover);
                        gameover.Enabled = true;
                        currentGameState = GameState.GameOver2;
                    }
                    if (spriteManagerTwo.getCompletoDelTo)
                    {
                        spriteManagerTwo.Enabled = false;
                        Components.Remove(spriteManagerTwo);
                        spriteManagerTwo.pararMusica();
                        spriteManagerThree.Enabled = true;
                        Components.Add(spriteManagerThree);
                        spriteManagerThree.setVolumen(musicVolume);
                        currentGameState = GameState.InGame3;
                    }
                    Pausar(spriteManagerTwo);
                    break;
                case GameState.InGame3:
                    if (spriteManagerThree.getMuertoDelTo)
                    {
                        Components.Add(gameover);
                        gameover.Enabled = true;
                        currentGameState = GameState.GameOver3;
                    }
                    if (spriteManagerThree.getCompletoDelTo)
                    {
                        spriteManagerThree.Enabled = false;
                        Components.Remove(spriteManagerThree);
                        spriteManagerThree.pararMusica();
                        Components.Add(creditos);
                        creditos.setVolumen(musicVolume);
                        creditos.Enabled = true;
                        currentGameState = GameState.creditos;
                    }
                    Pausar(spriteManagerThree);
                    break;
                case GameState.SelectLevel:
                    if (selectlvl.getPulsado[0] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        //selectlvl.pararMusica();
                        currentGameState = GameState.InGame;
                        selectlvl.Enabled = false;
                        Components.Remove(selectlvl);
                        spriteManager.Enabled = true;
                        Components.Add(spriteManager);
                        spriteManager.setVolumen(musicVolume);
                    }
                    else if (selectlvl.getPulsado[1] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.InGame2;
                        selectlvl.Enabled = false;
                        Components.Remove(selectlvl);
                        spriteManagerTwo.Enabled = true;
                        Components.Add(spriteManagerTwo);
                        spriteManagerTwo.setVolumen(musicVolume);
                    }
                    else if (selectlvl.getPulsado[2] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.InGame3;
                        selectlvl.Enabled = false;
                        Components.Remove(selectlvl);
                        spriteManagerThree.Enabled = true;
                        Components.Add(spriteManagerThree);
                        spriteManagerThree.setVolumen(musicVolume);
                    }
                    break;
                case GameState.GameOver:
                    if (gameover.getPulsado[0] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.Start;
                        gameover.Enabled = false;
                        spriteManager.Enabled = false;
                        menu.Enabled = true;
                        Components.Remove(spriteManager);
                        Components.Remove(gameover);
                        Components.Add(menu);
                        menu.setVolumen(musicVolume);
                        spriteManager.pararMusica();
                    }
                    else if (gameover.getPulsado[1] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.InGame;
                        gameover.Enabled = false;
                        spriteManager.Enabled = false;
                        Components.Remove(spriteManager);
                        Components.Remove(gameover);
                        spriteManager.Enabled = true;
                        Components.Add(spriteManager);
                        spriteManager.setVolumen(musicVolume);
                    }
                    break;
                case GameState.GameOver2:
                    if (gameover.getPulsado[0] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.Start;
                        gameover.Enabled = false;
                        spriteManagerTwo.pararMusica();
                        spriteManagerTwo.Enabled = false;
                        menu.Enabled = true;
                        
                        Components.Remove(spriteManagerTwo);
                        Components.Remove(gameover);
                        Components.Add(menu);
                        menu.setVolumen(musicVolume);
                        
                    }
                    else if (gameover.getPulsado[1] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.InGame2;
                        gameover.Enabled = false;
                        spriteManagerTwo.pararMusica();
                        spriteManagerTwo.Enabled = false;
                        Components.Remove(spriteManagerTwo);
                        Components.Remove(gameover);
                        spriteManagerTwo.Enabled = true;
                        Components.Add(spriteManagerTwo);
                        spriteManagerTwo.setVolumen(musicVolume);
                    }
                    break;
                case GameState.GameOver3:
                    if (gameover.getPulsado[0] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.Start;
                        gameover.Enabled = false;
                        spriteManagerThree.Enabled = false;
                        menu.Enabled = true;
                        Components.Remove(spriteManagerThree);
                        Components.Remove(gameover);
                        Components.Add(menu);
                        menu.setVolumen(musicVolume);
                        spriteManagerThree.pararMusica();
                    }
                    else if (gameover.getPulsado[1] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.InGame;
                        gameover.Enabled = false;
                        spriteManagerThree.Enabled = false;
                        Components.Remove(spriteManagerThree);
                        Components.Remove(gameover);
                        spriteManagerThree.Enabled = true;
                        Components.Add(spriteManagerThree);
                        spriteManagerThree.setVolumen(musicVolume);
                    }
                    break;
                case GameState.creditos:
                    Pausar(creditos);
                    break;
                case GameState.Pause:
                    
                    break;
                case GameState.Conf:
                    if (conf.getPulsado[0] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        conf.pararMusica();
                        conf.Enabled = false;
                        Components.Remove(conf);
                        
                        menu.Enabled = true;
                        Components.Add(menu);
                        menu.setVolumen(musicVolume);
                        currentGameState = GameState.Start;
                    }
                    else if (conf.getPulsado[1] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        musicVolume = MathHelper.Clamp(musicVolume - 0.01f, 0.0f, 2.0f);
                        conf.setVolumen(musicVolume);
                    }
                    else if (conf.getPulsado[2] && currMouseState.LeftButton == ButtonState.Pressed)
                    {
                        musicVolume = MathHelper.Clamp(musicVolume + 0.01f, 0.0f, 2.0f);
                        conf.setVolumen(musicVolume);
                    }
                    break;
            }
            
                
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            currMouseState = Mouse.GetState();
            
            switch (currentGameState)
            {
                case GameState.Start:
                    GraphicsDevice.Clear(Color.AliceBlue);
                    // Draw text for intro splash screen
                    spriteBatch.Begin();
                    
                    spriteBatch.End();
                    break;
                case GameState.InGame:
                    GraphicsDevice.Clear(Color.White);
                    //spriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend);
                  spriteBatch.Begin();
                    
                    spriteBatch.End();
                    
                    break;
                case GameState.GameOver:
                    break;
                case GameState.creditos:
                    GraphicsDevice.Clear(Color.Black);
                    break;
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void Pausar(GameComponent componente)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape) && ksLast.IsKeyUp(Keys.Escape))
            {
                if (pauseB)
                {
                    componente.Enabled = true;
                    pause.Enabled = false;
                    Components.Remove(pause);
                    pauseB = false;
                }
                else
                {
                    componente.Enabled = false;
                    pause.Enabled = true;
                    Components.Add(pause);
                    pauseB = true;
                }


            }
            if (pauseB)
            {
                if (pause.getPulsado[0] && currMouseState.LeftButton == ButtonState.Pressed)
                {
                    componente.Enabled = true;
                    pause.Enabled = false;
                    Components.Remove(pause);
                    pauseB = false;
                }
                if (pause.getPulsado[2] && currMouseState.LeftButton == ButtonState.Pressed)
                {

                    spriteManager.Enabled = false;

                    pause.Enabled = false;
                    Components.Remove(pause);
                    Components.Remove(componente);
                    componente.Enabled = false;
                    if (componente == spriteManager)
                        spriteManager.pararMusica();
                    else if (componente == spriteManagerTwo)
                        spriteManagerTwo.pararMusica();
                    else if (componente == spriteManagerThree)
                        spriteManagerThree.pararMusica();
                    else if (componente == creditos)
                        creditos.pararMusica();
                    Components.Add(menu);
                    menu.setVolumen(musicVolume);
                    menu.Enabled = true;
                    
                    pauseB = false;
                    currentGameState = GameState.Start;
                }
                if (pause.getPulsado[6] && currMouseState.LeftButton == ButtonState.Pressed)
                    musicVolume = MathHelper.Clamp(musicVolume - 0.01f, 0.0f, 2.0f);
                else if (pause.getPulsado[7] && currMouseState.LeftButton == ButtonState.Pressed)
                    musicVolume = MathHelper.Clamp(musicVolume + 0.01f, 0.0f, 2.0f);

                if(componente == spriteManager)
                    spriteManager.setVolumen(musicVolume);
                else if(componente == spriteManagerTwo)
                    spriteManagerTwo.setVolumen(musicVolume);
                else if (componente == spriteManagerThree)
                    spriteManagerThree.setVolumen(musicVolume);
                else if (componente == creditos)
                    creditos.setVolumen(musicVolume);

                audioEngine.Update();
                
            }
            ksLast = ks;
        }

        
    }

}
