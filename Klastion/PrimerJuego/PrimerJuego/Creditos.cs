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
    public class Creditos : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<Mapa> creditosList;
        List<Mapa> creditosList2;
        SpriteBatch spriteBatch;
        StaticSprite puntero;
        float tiempoEmpiezo;
        Boolean pararCreditos;

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

        public Creditos(Game game)
            : base(game)
        {
        }
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            creditosList = new List<Mapa>();
            creditosList2 = new List<Mapa>();
            tiempoEmpiezo = 0;
            pararCreditos = false;
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
            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (455 / 2), (768 / 2) - 145),
                new Point(455, 142),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366/2)-(807/2), 768),
                new Point(807, 104),
                new Point(0, 0),
                new Point(0, 160),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (324 / 2), 768 + 120),
                new Point(324, 45),
                new Point(0, 0),
                new Point(0, 600),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/pj"),
                new Vector2((1366 / 2) - (194 / 2), 950),
                new Point(194, 178),
                new Point(0, 0),
                new Point(0, 0),
                new Point(4, 0),
                new Vector2(0f, 0),
                0f,
                300, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (383 / 2), 1152),
                new Point(383, 97),
                new Point(0, 0),
                new Point(0, 274),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (324 / 2), 1152 + 120),
                new Point(324, 45),
                new Point(0, 0),
                new Point(0, 600),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/growlie"),
                new Vector2((1366 / 2) - (87 / 2), 1370),
                new Point(87, 88),
                new Point(0, 0),
                new Point(0, 0),
                new Point(3, 1),
                new Vector2(0f, 0),
                0f,
                500, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (375 / 2), 1470),
                new Point(375, 105),
                new Point(0, 0),
                new Point(0, 374),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (324 / 2), 1470 + 120),
                new Point(324, 45),
                new Point(0, 0),
                new Point(0, 600),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/kiel"),
                new Vector2((1366 / 2) - (180 / 2), 1670),
                new Point(180, 200),
                new Point(0, 0),
                new Point(0, 0),
                new Point(4, 1),
                new Vector2(0f, 0),
                0f,
                500, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (912 / 2), 1870),
                new Point(912, 121),
                new Point(0, 0),
                new Point(0, 479),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (498 / 2), 1870 + 120),
                new Point(498, 49),
                new Point(0, 0),
                new Point(0, 644),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (500 / 2), 1990 + 70),
                new Point(500, 50),
                new Point(0, 0),
                new Point(0, 692),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (626 / 2), 2060 + 70),
                new Point(626, 52),
                new Point(0, 0),
                new Point(0, 749),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));

            creditosList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/creditos"),
                new Vector2((1366 / 2) - (168 / 2), 2330),
                new Point(168, 174),
                new Point(0, 0),
                new Point(0, 809),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            foreach (Sprite s in creditosList)
                s.Draw2(gameTime, spriteBatch);
            foreach (Sprite s in creditosList2)
                s.Draw(gameTime, spriteBatch);
            puntero.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            if (tiempoEmpiezo > 2000 && pararCreditos == false)
            {
                foreach (Mapa s in creditosList)
                {
                    s.getMapaPosition -= new Vector2(0, 1);
                    s.Update(gameTime, Game.Window.ClientBounds);
                }
                foreach (Mapa s in creditosList2)
                {
                    s.getMapaPosition -= new Vector2(0, 1);
                    s.Update(gameTime, Game.Window.ClientBounds);
                }
            }
            else
                tiempoEmpiezo += gameTime.ElapsedGameTime.Milliseconds;

            if (creditosList[creditosList.Count - 1].getMapaPosition.Y < 300)
                pararCreditos = true;



            puntero.Update(gameTime, Game.Window.ClientBounds);
        }
        private void reproducirSonido(String value)
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
    }
}
