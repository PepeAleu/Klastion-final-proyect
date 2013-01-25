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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Pause : Microsoft.Xna.Framework.DrawableGameComponent
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        StaticSprite pointer;
        Mapa menu;
        Mapa menuDegradado;
        List<Boton> botones;
        List<Boton> seleccionados;
        int[] posicionFrameY;
        int[] tamFrameX;
        int[] tamFrameY;
        int[] posicionFrameYs;
        int[] tamFrameXs;
        int[] tamFrameYs;
        Boolean[] pulsados;
        int posicionBotonY;
        int posicionBotonX;
        int count;
        int posicionSeleccionado;
        float direccion;
        float velocidad;
        int idBoton;
        //Variables de sonidos
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue trackCue;
        Cue musicCue;
        WaveBank musicaWave;
        SoundBank musicaBank;
        Boolean pauseB;
        MouseState currMouseStateLast;
        public Pause(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            menu.Draw2(gameTime, spriteBatch);
            //menuDegradado.Draw2(gameTime, spriteBatch);
            foreach (Sprite s in botones)
                s.Draw2(gameTime, spriteBatch);


            pointer.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            currMouseStateLast = Mouse.GetState();
            botones = new List<Boton>();
            seleccionados = new List<Boton>();
            posicionFrameY = new int[2];
            tamFrameX = new int[2];
            tamFrameY = new int[2];
            
            posicionBotonY = 350;
            posicionBotonX = 500;
            count = 0;
            posicionSeleccionado = 720;
            idBoton = 0;
            pauseB = true;

            posicionFrameY[0] = 400;
            posicionFrameY[1] = 431;
            tamFrameX[0] = 143;
            tamFrameX[1] = 86;
            tamFrameY[0] = 30;
            tamFrameY[1] = 32;
            

            menu = new Mapa(
            Game.Content.Load<Texture2D>(@"Images/pause"),
            new Vector2((1366/2)-200, 150),
            new Point(400, 400),
            new Point(0, 0),
            new Point(0, 0),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0.12f);
            base.LoadContent();

            

            pointer = new StaticSprite(
            Game.Content.Load<Texture2D>(@"Images/cursor"),
            new Vector2(0, 0),
            new Point(24, 28),
            new Point(5, 5),
            new Point(0, 0),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0.9f);

            for (int i = 0; i < 2; i++)
            {

                botones.Add(new Boton(
                Game.Content.Load<Texture2D>(@"Images/pause"),
                new Vector2(posicionBotonX, posicionBotonY),
                new Point(tamFrameX[i], tamFrameY[i]),
                new Point(0, 0),
                new Point(0, posicionFrameY[i]),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.2f)
                );
                posicionBotonX+= tamFrameX[i] + 100;
            }

            botones.Add(new Boton(
                Game.Content.Load<Texture2D>(@"Images/pause"),
                new Vector2(550, 450),
                new Point(41, 32),
                new Point(0, 0),
                new Point(0, 495),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.11f)
                );

            botones.Add(new Boton(
            Game.Content.Load<Texture2D>(@"Images/pause"),
            new Vector2(650, 450),
            new Point(49, 27),
            new Point(0, 0),
            new Point(0, 527),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0.11f)
            );

            botones.Add(new Boton(
            Game.Content.Load<Texture2D>(@"Images/pause"),
            new Vector2(600, 400),
            new Point(130, 32),
            new Point(0, 0),
            new Point(0, 463),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0.11f)
            );

            botones.Add(new Boton(
            Game.Content.Load<Texture2D>(@"Images/pause"),
            new Vector2(500, 300),
            new Point(159, 32),
            new Point(0, 0),
            new Point(0, 558),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0.2f)
            );

            botones.Add(new Boton(
            Game.Content.Load<Texture2D>(@"Images/pause"),
            new Vector2(670, 310),
            new Point(18, 15),
            new Point(0, 0),
            new Point(0, 613),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0.2f)
            );

            botones.Add(new Boton(
            Game.Content.Load<Texture2D>(@"Images/pause"),
            new Vector2(770, 307),
            new Point(20, 23),
            new Point(0, 0),
            new Point(0, 590),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0.2f)
            );

            
            
            // TODO: Add your initialization code here
            pulsados = new Boolean[botones.Count];
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            audioEngine = new AudioEngine(@"Content\Sonidos\sonidos.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Sonidos\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Sonidos\Sound Bank.xsb");
            musicaWave = new WaveBank(audioEngine, @"Content\Sonidos\Wave Music.xwb");
            musicaBank = new SoundBank(audioEngine, @"Content\Sonidos\Music Bank.xsb");
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);


            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //pulsado = boton1.getPulsado;
            MouseState currMouseState = Mouse.GetState();
            for (int i = 0; i < botones.Count; i++)
            {
                pulsados[i] = botones[i].getPulsado;
                if (botones[i].getPulsado)
                    idBoton = i;
                botones[i].Update(gameTime, Game.Window.ClientBounds);
            }

            if ((botones[1].getPulsado || botones[3].getPulsado) && currMouseState.LeftButton == ButtonState.Pressed && currMouseStateLast.LeftButton == ButtonState.Released)
            {
                MostrarPause();
            }

            pointer.Update(gameTime, Game.Window.ClientBounds);
            currMouseStateLast = currMouseState;
            base.Update(gameTime);
        }

        public Boolean[] getPulsado
        {
            get
            {
                return pulsados;
            }
        }
        private void reproducirSonido(String value)
        {
            trackCue = soundBank.GetCue(value);
            trackCue.Play();
        }
        public void reproducirMusica(String value)
        {
            musicCue = musicaBank.GetCue(value);
            musicCue.Play();
        }
        public void MostrarPause()
        {
            if (pauseB == false)
            {
                botones[2].getCapa = 0.11f;
                botones[3].getCapa = 0.11f;
                botones[4].getCapa = 0.11f;
                pauseB = true;
            }
            else
            {
                botones[2].getCapa = 0.2f;
                botones[3].getCapa = 0.2f;
                botones[4].getCapa = 0.2f;
                pauseB = false;
            }
        }
        public void pararMusica()
        {
            musicCue.Pause();
        }
    }
}
