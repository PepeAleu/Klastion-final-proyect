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
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        StaticSprite pointer;
        Mapa menu;
        List<Boton> botones;
        List<Boton> seleccionados;
        List<Boton> seguros;
        int[] posicionFrameY;
        int[] tamFrameX;
        int[] tamFrameY;
        int[] posicionFrameYs;
        int[] tamFrameXs;
        int[] tamFrameYs;
        Boolean[] pulsados;
        int posicionBotonY;
        int posicionBotonX;
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
        Boolean mostrarSeguro;
        AudioCategory musicCategory;

        public Menu(Game game)
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

            foreach (Sprite s in seleccionados)
                s.Draw2(gameTime, spriteBatch);

            if (mostrarSeguro == true)
            {
                foreach (Sprite s in seguros)
                    s.Draw2(gameTime, spriteBatch);
            }

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
            seguros = new List<Boton>();
            posicionFrameY = new int[4];
            tamFrameX = new int[4];
            tamFrameY = new int[4];
            posicionFrameYs = new int[4];
            tamFrameXs = new int[4];
            tamFrameYs = new int[4];
            pulsados = new Boolean[4];
            posicionBotonY = 276;
            posicionBotonX = 730;
            posicionSeleccionado = 720;
            direccion = 0;
            velocidad = 50;
            idBoton = 0;
            mostrarSeguro = false;

            posicionFrameY[0] = 1346;
            posicionFrameY[1] = 1375;
            posicionFrameY[2] = 1407;
            posicionFrameY[3] = 1443;
            tamFrameX[0] = 164;
            tamFrameX[1] = 234;
            tamFrameX[2] = 77;
            tamFrameX[3] = 177;
            tamFrameY[0] = 29;
            tamFrameY[1] = 31;
            tamFrameY[2] = 30;
            tamFrameY[3] = 35;
            posicionFrameYs[0] = 901;
            posicionFrameYs[1] = 991;
            posicionFrameYs[2] = 1125;
            posicionFrameYs[3] = 1254;
            tamFrameXs[0] = 504;
            tamFrameXs[1] = 434;
            tamFrameXs[2] = 543;
            tamFrameXs[3] = 202;
            tamFrameYs[0] = 90;
            tamFrameYs[1] = 134;
            tamFrameYs[2] = 129;
            tamFrameYs[3] = 92;

            menu = new Mapa(
            Game.Content.Load<Texture2D>(@"Images/menuSprite"),
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

            for (int i = 0; i < 4; i++)
            {

                botones.Add(new Boton(
                Game.Content.Load<Texture2D>(@"Images/menuSprite"),
                new Vector2(1000, posicionBotonY),
                new Point(tamFrameX[i], tamFrameY[i]),
                new Point(0, posicionFrameY[i]),
                new Point(0, posicionFrameY[i]),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.1f)
                );
                posicionBotonY += tamFrameY[i] + 30;
            }

            for (int i = 0; i < 4; i++)
            {

                seleccionados.Add(new Boton(
                Game.Content.Load<Texture2D>(@"Images/menuSprite"),
                new Vector2(posicionBotonX, 85),
                new Point(tamFrameXs[i], tamFrameYs[i]),
                new Point(0, posicionFrameYs[i]),
                new Point(0, posicionFrameYs[i]),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.1f)
                );
                posicionBotonX += tamFrameXs[i] + 300;
            }

                seguros.Add(new Boton(
                Game.Content.Load<Texture2D>(@"Images/menuSprite"),
                new Vector2(525, 380),
                new Point(33,33),
                new Point(0,0),
                new Point(0, 1687),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.89f)
                );

                seguros.Add(new Boton(
                    Game.Content.Load<Texture2D>(@"Images/menuSprite"),
                    new Vector2(675, 380),
                    new Point(51, 33),
                    new Point(0, 0),
                    new Point(0, 1720),
                    new Point(0, 0),
                    Vector2.Zero,
                    0f,
                    1000, 0.89f)
                    );

                seguros.Add(new Boton(
                        Game.Content.Load<Texture2D>(@"Images/menuSprite"),
                        new Vector2(450, 250),
                        new Point(350, 210),
                        new Point(0, 0),
                        new Point(0, 1478),
                        new Point(0, 0),
                        Vector2.Zero,
                        0f,
                        1000, 0.89f)
                        );
            
            // TODO: Add your initialization code here
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            audioEngine = new AudioEngine(@"Content\Sonidos\sonidos.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Sonidos\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Sonidos\Sound Bank.xsb");
            musicaWave = new WaveBank(audioEngine, @"Content\Sonidos\Wave Music.xwb");
            musicaBank = new SoundBank(audioEngine, @"Content\Sonidos\Music Bank.xsb");
            musicCategory = audioEngine.GetCategory("Default");
            reproducirMusica("menu");
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
            for (int i = 0; i < botones.Count; i++ )
            {
                pulsados[i] = botones[i].getPulsado;

                
                if (botones[i].getPulsado)
                    idBoton = i;
                
                    
                        seleccionados[i].getPosicionBotones += new Vector2(velocidad * direccion, 0);

                botones[i].Update(gameTime, Game.Window.ClientBounds);
            }

            if (botones[3].getPulsado && currMouseState.LeftButton == ButtonState.Pressed)
            {
                mostrarSeguro = true;
            }
            if (seguros[1].getPulsado && currMouseState.LeftButton == ButtonState.Pressed)
            {
                mostrarSeguro = false;
            }
            if (seguros[0].getPulsado && currMouseState.LeftButton == ButtonState.Pressed)
            {
                Game.Exit();
            }
            
            if (seleccionados[idBoton].getPosicionBotones.X < posicionSeleccionado - 26)
                direccion = 1;
            else if (seleccionados[idBoton].getPosicionBotones.X > posicionSeleccionado + 26)
                direccion = -1;
            else if (seleccionados[idBoton].getPosicionBotones.X >= posicionSeleccionado - 26 && seleccionados[idBoton].getPosicionBotones.X <= posicionSeleccionado + 26)
                direccion = 0;

            pointer.Update(gameTime, Game.Window.ClientBounds);
            foreach (Sprite s in seguros)
                s.Update(gameTime, Game.Window.ClientBounds);
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
            if(!musicCue.IsPlaying)
                musicCue.Play();
        }
        public void pararMusica()
        {
            musicCue.Pause();
        }
        public void setVolumen(float musicVolume)
        {
            musicCategory.SetVolume(musicVolume);
        }
    }
}
