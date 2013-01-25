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
    public class GameOver : Microsoft.Xna.Framework.DrawableGameComponent
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
        AudioCategory musicCategory;
        public GameOver(Game game)
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
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            botones = new List<Boton>();
            seleccionados = new List<Boton>();
            posicionFrameY = new int[2];
            tamFrameX = new int[2];
            tamFrameY = new int[2];
            pulsados = new Boolean[2];
            posicionBotonY = 450;
            posicionBotonX = 540;
            count = 0;
            posicionSeleccionado = 720;
            idBoton = 0;

            posicionFrameY[0] = 768;
            posicionFrameY[1] = 805;
            tamFrameX[0] = 41;
            tamFrameX[1] = 225;
            tamFrameY[0] = 37;
            tamFrameY[1] = 38;
            

            menu = new Mapa(
            Game.Content.Load<Texture2D>(@"Images/gameover"),
            new Vector2(0, 0),
            new Point(1366, 768),
            new Point(0, 0),
            new Point(0, 0),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0f);

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
                Game.Content.Load<Texture2D>(@"Images/gameover"),
                new Vector2(posicionBotonX, posicionBotonY),
                new Point(tamFrameX[i], tamFrameY[i]),
                new Point(0, posicionFrameY[i]),
                new Point(0, posicionFrameY[i]),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.2f)
                );
                posicionBotonX+= tamFrameX[i] + 100;
            }

            
            // TODO: Add your initialization code here
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


            pointer.Update(gameTime, Game.Window.ClientBounds);
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
        public void pararMusica()
        {
            musicCue.Pause();
        }
    }
}
