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
    public class Conf : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<Boton> botones;
        List<Mapa> imagenes;
        SpriteBatch spriteBatch;
        StaticSprite puntero;
        Boolean[] pulsados;

        //Variables de sonidos
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        WaveBank waveBank2;
        SoundBank soundBank2;
        Cue trackCue;
        Cue musicCue;
        Cue ambientCue;
        Cue trackCue2;
        WaveBank musicaWave;
        SoundBank musicaBank;
        WaveBank ambientWave;
        SoundBank ambientBank;
        AudioCategory musicCategory;

        public Conf(Game game)
            : base(game)
        {
        }
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            botones = new List<Boton>();
            imagenes = new List<Mapa>();

            audioEngine = new AudioEngine(@"Content\Sonidos\sonidos.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Sonidos\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Sonidos\Sound Bank.xsb");
            waveBank2 = new WaveBank(audioEngine, @"Content\Sonidos\Wave Bank 2.xwb");
            soundBank2 = new SoundBank(audioEngine, @"Content\Sonidos\Sound Bank 2.xsb");
            musicaWave = new WaveBank(audioEngine, @"Content\Sonidos\Wave Music.xwb");
            musicaBank = new SoundBank(audioEngine, @"Content\Sonidos\Music Bank.xsb");
            ambientWave = new WaveBank(audioEngine, @"Content\Sonidos\Wave Ambient.xwb");
            ambientBank = new SoundBank(audioEngine, @"Content\Sonidos\Ambient Bank.xsb");
            musicCategory = audioEngine.GetCategory("Default");
            musicCategory.SetVolume(0);
            reproducirMusica("batidora");


            puntero = new StaticSprite(
            Game.Content.Load<Texture2D>(@"Images/cursor"),
            new Vector2(0, 0),
            new Point(24, 28),
            new Point(5, 5),
            new Point(0, 0),
            new Point(0, 0),
            Vector2.Zero,
            0f,
            1000, 0.9f);
            imagenes.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/configuracion"),
                new Vector2(0, 0),
                new Point(1366, 768),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.1f));

            botones.Add(new Boton(
                Game.Content.Load<Texture2D>(@"Images/configuracion"),
                new Vector2(35, 679),
                new Point(182, 53),
                new Point(0, 0),
                new Point(0, 796),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.2f));

            botones.Add(new Boton(
                Game.Content.Load<Texture2D>(@"Images/configuracion"),
                new Vector2(263, 152),
                new Point(17, 6),
                new Point(0, 0),
                new Point(0, 768),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.2f));

            botones.Add(new Boton(
                Game.Content.Load<Texture2D>(@"Images/configuracion"),
                new Vector2(292, 146),
                new Point(18, 18),
                new Point(0, 0),
                new Point(0, 775),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.2f));


            pulsados = new Boolean[botones.Count];

        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            foreach (Sprite s in imagenes)
                s.Draw2(gameTime, spriteBatch);
            foreach (Sprite s in botones)
                s.Draw2(gameTime, spriteBatch);
            puntero.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < botones.Count; i++)
            {
                pulsados[i] = botones[i].getPulsado;
                botones[i].Update(gameTime, Game.Window.ClientBounds);
            }
            
            puntero.Update(gameTime, Game.Window.ClientBounds);
        }
        public void reproducirSonido(String value)
        {
            trackCue = soundBank.GetCue(value);
            trackCue.Play();
        }
        private void reproducirSonido2(String value)
        {
            trackCue2 = soundBank2.GetCue(value);
            trackCue2.Play();
        }
        private void reproducirMusica(String value)
        {
            musicCue = musicaBank.GetCue(value);
            musicCue.Play();
        }
        private void reproducirAmbiente(String value)
        {
            ambientCue = ambientBank.GetCue(value);
            ambientCue.Play();
        }
        private void pararMusica(Cue value)
        {
            if (value.IsPlaying)
                value.Pause();
        }
        public void pararMusica()
        {
            if (musicCue.IsPlaying)
                musicCue.Pause();
        }
        public void setVolumen(float musicVolume)
        {
            musicCategory.SetVolume(musicVolume);
        }
        public Boolean[] getPulsado
        {
            get
            {
                return pulsados;
            }
        }
    }
}
