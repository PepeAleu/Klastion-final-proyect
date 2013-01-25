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
    public class SpriteManagerThree : Microsoft.Xna.Framework.DrawableGameComponent
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        UserControlledSprite player;
        StaticSprite pointer;
        KeyboardState ks;
        MouseState currMouseState;
        MouseState lastMouseState;
        float gravedad;
        Boolean cayendo;
        
        int colision;
        int colisiond;
        int colisioni;
        float parada;
        int alturaTerreno;
        float jumpTimer;
        float tiempoVelocidadSalto;
        Mundo mundo1;
        Mapa final;
        public enum DisparoState { Quieto, Andando, Disparando };
        DisparoState currentDisparoState;
        public enum GravedadState { Suelo, Aire, Caida };
        public enum JefeState { Normal, Preparandose, Agresivo };
        GravedadState currentGravedadState;
        Vector2 posaux;
        Boolean borrarBala;
        int filas;
        int columnas;
        int count;

        String texto;
        List<AutomatedSprite> spriteList;
        List<Disparo> disparoList;
        List<Disparo> lluviaList;
        List<Disparo> lluviaList2;
        List<Mapa> mapaList;
        List<Mapa> terrenoList;
        List<Mapa> terrenoList2;
        List<Mapa> terrenoListSuelo;
        List<Mapa> fondoList1;
        List<Mapa> fondoList2;
        List<Mapa> disparoJefeList;
        List<Mapa> tigres;
        List<Point> map;
        List<Jefe> jefeList;
        List<Mapa> vidaList;
        List<Mapa> vidaList2;
        List<Mapa> vidaListKiel;
        List<Mapa> vidaList2Kiel;
        SpriteFont puntuacionFuente;
        float maxPosBloque;
        int terrenoTiempoPasado;
        int terrenoDireccion;
        int terrenoDireccionAnterior;
        int terrenoTiempo;
        int countTerreno;
        float temblorTerrenoTime;
        int temblorTerrenoElec;
        float temblorTerrenoNivel;
        float arrastre;
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
        //Lluvia
        int densidadLluvia;
        float tiempoLluvia;
        int densidadLluvia2;
        float tiempoLluvia2;
        float velocidadLluvia;
        int viento;

        int enemySpawnMinMilliseconds;
        int enemySpawnMaxMilliseconds;
        int enemyMinSpeed;
        int enemyMaxSpeed;

        int nextSpawnTime;

        float comodidad;

        //Variables jefe
        float respawnJefeMin;
        float respawnJefeMax;

        //Posiciones
        float posicionTerrenoX;
        float posicionTerrenoY;
        float posicionTerrenoBase;
        float posicionFondo1;
        float posicionFondo2;

        //Disparo
        float delayDisparo;
        float delayDisparoCurrent;
        float animaDisparo;
        //Monsturo
        float heridoDelayCurrent;
        //Mapa
        float cambioMapaDelay;
        float cambioMapaDelayCurrent;
        float hastaMapa1;
        float hastaMapa2;
        ParteMapa currentParteMapa;
        public enum ParteMapa { Suelo, Saltitos };
        int numFondo1;
        int numFondo2;
        int numTerreno1;
        float capaTerreno;

        //Muerte
        Boolean caido;
        List<float> puntosControl;
        float puntoControl;
        float vidaPos;
        float tiempoHerido;
        float tiempoMuerto;
        Boolean muertoDelTo;
        Point collisionOffSet;

        //Final
        float tiempoFin;
        Mapa complete;
        Boolean completoDelTo;

        //Dimensiones
        Boolean fondo;

        //Jefe final
        List<JefeFinal> kiel;
        float posxDisparoKiel;
        Boolean tocaRayoKiel;
        float tiempoTocaRayoKiel;
        float vidaPosKiel;
        Boolean kielMuerto;
        float tiempoMuertoKiel;
        Boolean kielMuertoSprite;
        Boolean estaEnSuSitio;
        float tiempoMuertoKielTotal;

        public SpriteManagerThree(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()//+++++++++++++++++++++++         INITIALIZE        +++++++++++++++++
        {
            rnd = new Random();
            texto = "";

            tiempoMuertoKielTotal = 7000;
            estaEnSuSitio = false;
            kielMuertoSprite = false;
            tiempoMuertoKiel = 0;
            kielMuerto = false;
            tocaRayoKiel = false;
            tiempoTocaRayoKiel = 5000;
            currMouseState = Mouse.GetState();
            lastMouseState = Mouse.GetState();
            fondo = false;
            completoDelTo = false;
            tiempoFin = 0;
            collisionOffSet = Point.Zero;
            cayendo = false;
            tiempoLluvia = 0;
            densidadLluvia2 = 500;
            tiempoLluvia2 = 0;
            colision = 0;
            colisiond = 0;
            colisioni = 0;
            parada = 0;
            alturaTerreno = 4;
            jumpTimer = -10f;
            tiempoVelocidadSalto = 0;
            mundo1 = new Mundo(2.2f);
            currentDisparoState = DisparoState.Quieto;
            currentGravedadState = GravedadState.Aire;
            borrarBala = false;
            filas = 20;
            columnas = 0;
            count = 0;

            spriteList = new List<AutomatedSprite>();
            kiel = new List<JefeFinal>();
            disparoList = new List<Disparo>();
            lluviaList = new List<Disparo>();
            lluviaList2 = new List<Disparo>();
            mapaList = new List<Mapa>();
            terrenoList = new List<Mapa>();
            terrenoList2 = new List<Mapa>();
            terrenoListSuelo = new List<Mapa>();
            fondoList1 = new List<Mapa>();
            fondoList2 = new List<Mapa>();
            map = new List<Point>();
            disparoJefeList = new List<Mapa>();
            jefeList = new List<Jefe>();
            vidaList = new List<Mapa>();
            vidaList2 = new List<Mapa>();
            vidaListKiel = new List<Mapa>();
            vidaList2Kiel = new List<Mapa>();
            tigres = new List<Mapa>();
            maxPosBloque = 0f;
            terrenoTiempoPasado = 0;
            terrenoDireccion = 0;
            terrenoDireccionAnterior = 4;
            terrenoTiempo = 100;
            temblorTerrenoTime = 0;
            temblorTerrenoElec = 0;
            temblorTerrenoNivel = 3;
            arrastre = 0;
            //Lluvia
            densidadLluvia = 500; //milisegundos entre mota
            velocidadLluvia = 6f;
            viento = -2;

            enemySpawnMinMilliseconds = 1000;
            enemySpawnMaxMilliseconds = 2000;
            enemyMinSpeed = 2;
            enemyMaxSpeed = 6;

            nextSpawnTime = 0;

            //Variables jefe
            respawnJefeMin = 0;
            respawnJefeMax = 1;

            //Posiciones
            posicionTerrenoX = 0;
            posicionTerrenoY = 700;
            posicionTerrenoBase = 0;
            posicionFondo1 = -1000;
            posicionFondo2 = -1000;

            //Disparo
            delayDisparo = 200;
            delayDisparoCurrent = 900;
            animaDisparo = 0;
            //Monsturo
            heridoDelayCurrent = 0;
            //Mapa
            cambioMapaDelay = 8000;
            cambioMapaDelayCurrent = 0;
            hastaMapa1 = 8000;
            hastaMapa2 = 15000;
            currentParteMapa = ParteMapa.Suelo;
            numFondo1 = 0;
            numFondo2 = 0;
            numTerreno1 = 0;
            capaTerreno = 0.5f;

            //Muerte
            caido = false;
            puntosControl = new List<float>();
            puntoControl = 0;
            vidaPos = 0;
            vidaPosKiel = 0;
            tiempoHerido = 500f;
            tiempoMuerto = 0;
            muertoDelTo = false;


            for (int n = 0; n < 200; n++)
                map.Add(new Point(2, 0));

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
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
            //disparoJefeList.Add(new Mapa(
            //                Game.Content.Load<Texture2D>(@"Images/DisparoJefe"),
            //                new Vector2(kiel.getPlayerPosition.X + posxDisparoKiel, kiel.getPlayerPosition.Y),
            //                new Point(0, 0),
            //                new Point(200, 200),
            //                new Point(0, 0),
            //                new Point(4, 1),
            //                new Vector2(0f, 0),
            //                0f,
            //                1000, 0.89f));

            complete = new Mapa(
                Game.Content.Load<Texture2D>(@"Images/LevelComplete"),
                new Vector2(120, 120),
                new Point(1140, 496),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f);

            vidaList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSprite"),
                new Vector2(50 + vidaPos, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.89f));
            vidaPos += 7;
            for (int i = 0; i < 28; i++)
            {
                vidaList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSprite"),
                new Vector2(50 + vidaPos, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(1, 0),
                new Point(1, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.89f));
                vidaPos += 7;
            }
            vidaList.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSprite"),
                new Vector2(50 + vidaPos, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(2, 0),
                new Point(2, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.89f));
            vidaPos = 0;
            vidaList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSprite"),
                new Vector2(50 + vidaPos, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(0, 1),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));
            vidaPos += 7;
            for (int i = 0; i < 28; i++)
            {
                vidaList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSprite"),
                new Vector2(50 + vidaPos, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(1, 1),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));
                vidaPos += 7;
            }
            vidaList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSprite"),
                new Vector2(50 + vidaPos, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(2, 1),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));
            vidaPos += 7;

            //Vida de kiel

            vidaListKiel.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSpriteKiel"),
                new Vector2(500 + vidaPosKiel, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.89f));
            vidaPosKiel += 7;
            for (int i = 0; i < 98; i++)
            {
                vidaListKiel.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSpriteKiel"),
                new Vector2(500 + vidaPosKiel, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(1, 0),
                new Point(1, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.89f));
                vidaPosKiel += 7;
            }
            vidaListKiel.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSpriteKiel"),
                new Vector2(500 + vidaPosKiel, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(2, 0),
                new Point(2, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.89f));
            vidaPosKiel = 0;
            vidaList2Kiel.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSpriteKiel"),
                new Vector2(500 + vidaPosKiel, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(0, 1),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));
            vidaPosKiel += 7;
            for (int i = 0; i < 98; i++)
            {
                vidaList2Kiel.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSpriteKiel"),
                new Vector2(500 + vidaPosKiel, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(1, 1),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));
                vidaPosKiel += 7;
            }
            vidaList2Kiel.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/vidaSpriteKiel"),
                new Vector2(500 + vidaPosKiel, 30),
                new Point(10, 23),
                new Point(0, 0),
                new Point(2, 1),
                new Point(0, 0),
                new Vector2(0f, 0),
                0f,
                1000, 0.99f));
            vidaPosKiel += 7;

            player = new UserControlledSprite(
            Game.Content.Load<Texture2D>(@"Images/pj"),
            new Vector2((1366 / 2) - 152, 300),
            new Point(194, 178),
            new Point(collisionOffSet.X, collisionOffSet.Y),
            new Point(0, 0),
            new Point(0, 0),
            new Vector2(0f, 0),
            0f,
            70, 0.80f, 30);


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


            

            reproducirMusica("kiel");
            reproducirAmbiente("viento");
            trackCue = soundBank.GetCue("caido");
            trackCue2 = soundBank2.GetCue("bicho1");
            //Suelo(-100, posicionTerrenoY, 0, -600, terrenoList);
            SueloGrande(0, posicionTerrenoY, 0, -100, terrenoList);
            SueloGrande(posicionTerrenoX + 1000, posicionTerrenoY, 0, 0, terrenoList);
            Escaleras(-400 , posicionTerrenoY - 200, 600, 0, 2, terrenoList2, 0.210f, true);


            kiel.Add(new JefeFinal(
            Game.Content.Load<Texture2D>(@"Images/kiel"),
            new Vector2(500, 20),
            new Point(180, 200),
            new Point(80, 30),
            new Point(0, 0),
            new Point(4, 2),
            new Vector2(5, 5),
            0f,
            400, 0.95f, 100));

            
            final = new Mapa(
            Game.Content.Load<Texture2D>(@"Images/final"),
            new Vector2(posicionTerrenoX + 2000, posicionTerrenoY - 146),
            new Point(171, 156),
            new Point(0, 0),
            new Point(0, 1),
            new Point(4, 3),
            new Vector2(0f, 0),
            0f,
            200, 0.9f);
            posicionTerrenoX = 0;
            posicionTerrenoY = 0;

            
            //Escaleras(posicionTerrenoX + 600, posicionTerrenoY, 100, 10, 10, terrenoList2, 0.210f, true);
            //SegundoPlano();

            foreach (Sprite s in terrenoList2)
            {
                s.getEscala = 0.8f;
                s.GetCollisionOffSet = new Point(60, 60);
            }

            puntuacionFuente = Game.Content.Load<SpriteFont>(@"Fuentes\fuente");

            player.setTamAura = 300;
            foreach (Sprite s in jefeList)
                s.setTamAura = 400;
            

        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            MouseState currMouseState = Mouse.GetState();


            final.Draw(gameTime, spriteBatch);
            foreach (Sprite s in tigres)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in terrenoList)
                s.Draw(gameTime, spriteBatch);
            foreach (Sprite s in terrenoList2)
                s.Draw(gameTime, spriteBatch);
            foreach (Sprite s in terrenoListSuelo)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in mapaList)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in fondoList1)
                s.Draw2(gameTime, spriteBatch);

            foreach (Sprite s in fondoList2)
                s.Draw2(gameTime, spriteBatch);

            foreach (Sprite s in disparoList)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in spriteList)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in lluviaList)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in lluviaList2)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in vidaList)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in vidaList2)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in vidaListKiel)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in vidaList2Kiel)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in disparoJefeList)
                s.Draw(gameTime, spriteBatch);

            if (player.getFin)
            {
                if (tiempoFin > 3000)
                {
                    complete.Draw(gameTime, spriteBatch);
                    if (tiempoFin > 6000)
                        completoDelTo = true;
                    else
                        tiempoFin += gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                    tiempoFin += gameTime.ElapsedGameTime.Milliseconds;


            }
            player.Draw(gameTime, spriteBatch);
            pointer.Draw(gameTime, spriteBatch);
            foreach(Sprite s in kiel)
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in jefeList)
                s.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(puntuacionFuente, texto, new Vector2(0, 40), Color.Red);

            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {

            if (!player.getFin)
            {
                if (!player.getMuerto)
                {
                    if (kielMuerto)
                    {
                        
                        kielMuerto = false;
                        tiempoMuertoKiel = 0;
                        kielMuertoSprite = true;
                        kiel[0].getVida = 100;

                        kiel[0].getEstadoKiel = JefeFinal.JefeState.Muerto;
                        kiel[0].Desde = new Point(0, 3);
                        kiel[0].Hasta = new Point(4, 3);
                        reproducirSonido("gritokiel");
                            reproducirSonido2("terremoto");
                        kiel[0].getMilisegundosPorFrame = 2000;
                    }
                    else
                    {
                        if (tiempoMuertoKiel > tiempoMuertoKielTotal && kielMuertoSprite == true)
                        {
                            kiel.RemoveAt(0);
                            kielMuertoSprite = false;
                            if (player.direction.X < 0)
                                arrastre = player.direction.X - parada - comodidad;
                            else
                                arrastre = player.direction.X - parada + comodidad;
                        }
                        else if (kielMuertoSprite == true)
                        {
                            tiempoMuertoKiel += gameTime.ElapsedGameTime.Milliseconds;
                            kiel[0].getPlayerPosition += new Vector2(0, 10);
                            Temblor(gameTime);
                            temblorTerrenoNivel = 20;
                        }
                        if (!kielMuertoSprite && tiempoMuertoKiel > tiempoMuertoKielTotal)
                            VolverAlCentro();
                    }


                    Disparo(gameTime);
                    
                        //Lluvia(gameTime, densidadLluvia, velocidadLluvia, viento);
                    
                    Lluvia2(gameTime, densidadLluvia2, velocidadLluvia, viento);
                    Fondo2(gameTime);
                    Fondo1(gameTime);
                    //GenerarTerreno(gameTime);
                    //Fisica(gameTime);
                    //RespawnJefe(gameTime);
                    ConstitucionMon(gameTime);

                    ConstitucionKiel(gameTime);
                    Inteligencia1(gameTime);


                    //Mapa1(gameTime);

                    Salto(gameTime);
                    Fisica2(gameTime);
                    DisparaJefeFinal(gameTime);

                    foreach (Mapa s in terrenoList)
                    {

                        s.Update(gameTime, Game.Window.ClientBounds);
                    }
                    foreach (Mapa s in terrenoList2)
                    {
                        s.getMapaPosition -= new Vector2(arrastre / 2, 0);
                        s.Update(gameTime, Game.Window.ClientBounds);
                    }

                    if (player.collisionRect.Intersects(final.collisionRectDerecha))
                        player.getFin = true;

                    foreach(Sprite s in kiel)
                        s.Update(gameTime, Game.Window.ClientBounds);
                }


                foreach (float t in puntosControl)
                {
                    if (t > terrenoList[0].getMapaPosition.X && player.getEfecto == SpriteEffects.FlipHorizontally && puntoControl > t)
                    {
                        puntoControl = t;

                    }
                }

                if (player.getPlayerPosition.Y > Game.Window.ClientBounds.Height - 40 && caido == false)
                {
                    
                        if (!trackCue.IsPlaying)
                            reproducirSonido("caido");
                    
                }
                if (player.getPlayerPosition.Y > Game.Window.ClientBounds.Height + 500 && caido == false)
                {
                    player.getVida -= 3;
                    for (int i = 0; i < 3; i++)
                    {
                        if (vidaList2.Count > 0)
                            vidaList2.RemoveAt(vidaList2.Count - 1);
                    }
                    caido = true;

                }

                if (caido)
                {
                    if (terrenoList[0].getMapaPosition.X < puntoControl - 30)
                    {
                        foreach (Mapa t in terrenoList)
                            t.getMapaPosition += new Vector2(30, 0);
                        foreach (Mapa t in terrenoList2)
                            t.getMapaPosition += new Vector2(15, 0);
                        foreach (Jefe t in jefeList)
                            t.getPlayerPosition += new Vector2(30, 0);
                        foreach (Mapa t in tigres)
                            t.getMapaPosition += new Vector2(30, 0);
                        final.getMapaPosition += new Vector2(30, 0);
                        foreach (Mapa t in fondoList1)
                            t.getMapaPosition += new Vector2(30 / 3, 0);
                        foreach (Mapa t in fondoList2)
                            t.getMapaPosition += new Vector2(30 / 8, 0);


                    }
                    else if (terrenoList[0].getMapaPosition.X > puntoControl + 30)
                    {
                        foreach (Mapa t in terrenoList)
                            t.getMapaPosition -= new Vector2(30, 0);
                        foreach (Mapa t in terrenoList2)
                            t.getMapaPosition -= new Vector2(15, 0);
                        foreach (Jefe t in jefeList)
                            t.getPlayerPosition -= new Vector2(30, 0);
                        foreach (Mapa t in tigres)
                            t.getMapaPosition -= new Vector2(30, 0);
                        final.getMapaPosition -= new Vector2(30, 0);
                        foreach (Mapa t in fondoList1)
                            t.getMapaPosition -= new Vector2(30 / 3, 0);
                        foreach (Mapa t in fondoList2)
                            t.getMapaPosition -= new Vector2(30 / 8, 0);
                    }
                    else
                    {
                        gravedad = 0;
                        player.getPlayerPosition = new Vector2((Game.Window.ClientBounds.Width / 2) - (player.getFrameSize.X / 2), 0 - player.getFrameSize.Y);
                        caido = false;
                    }
                }

                if (player.getVida <= 0 && player.getMuerto == false)
                {
                    player.Desde = new Point(0, 5);
                    player.Hasta = new Point(4, 6);
                    pararMusica(musicCue);
                    reproducirMusica("muerto");
                    player.getMuerto = true;
                }
                if (player.getMuerto)
                {
                    if (tiempoMuerto < 4000)
                        tiempoMuerto += gameTime.ElapsedGameTime.Milliseconds;
                    else
                    {
                        pararMusica(ambientCue);
                        muertoDelTo = true;
                    }
                }

                if (ambientCue.IsStopped)
                    reproducirAmbiente("viento");

               
                final.getMapaPosition -= new Vector2(arrastre, 0);

                if (player.direction.X < 0)
                    arrastre = player.direction.X - parada - comodidad;
                else
                    arrastre = player.direction.X - parada + comodidad;

                ks = Keyboard.GetState();
                
                pointer.Update(gameTime, Game.Window.ClientBounds);
            }
            if (player.getFin)
            {
                player.getPlayerPosition = final.getMapaPosition + new Vector2(-30, 20);
                final.getMapaPosition += new Vector2(2, -2);
                final.getEfecto = SpriteEffects.FlipHorizontally;
            }
            final.Update(gameTime, Game.Window.ClientBounds);
            player.Update(gameTime, Game.Window.ClientBounds);
            
            
        }
        protected override void LoadContent()
        {

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
            if (ambientCue.IsPlaying)
                ambientCue.Pause();
        }
        public Random rnd { get; private set; }
        private void Lluvia(GameTime gameTime, int densidad, float velocidad, int viento)
        {
            
                tiempoLluvia += gameTime.ElapsedGameTime.Milliseconds;
                if (tiempoLluvia > densidad)
                {
                    lluviaList.Add(new Disparo(
                            Game.Content.Load<Texture2D>(@"Images/meteorito"),
                            new Vector2(rnd.Next(0, 1500), -200),
                            new Point(200, 200),
                            new Point(80, 80),
                            new Point(0, 0),
                            new Point(0, 0),
                            Vector2.Zero,
                            5.8f,
                            100, 0.8f));
                    tiempoLluvia = 0;
                }
                Temblor(gameTime);
                temblorTerrenoNivel = 5;
            
            for (int e = 0; e < lluviaList.Count; e++)
            {
                if (lluviaList[e].collisionRect.Intersects(player.collisionRect) && fondo == false)
                {
                    if (tiempoTocaRayoKiel > 400)
                    {
                        player.Desde = new Point(0, 4);
                        player.Hasta = new Point(4, 4);
                        for (int n = 0; n < 1; n++)
                        {
                            if (vidaList2.Count > 0)
                                vidaList2.RemoveAt(vidaList2.Count - 1);
                            player.getVida -= 1;
                        }
                        tiempoHerido = 0;
                        player.getHerido = true;
                        if (rnd.Next(0, 2) == 0)
                            reproducirSonido("au1");
                        else
                            reproducirSonido("au2");
                        tiempoTocaRayoKiel = 0;
                    }
                    else
                        tiempoTocaRayoKiel += gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                {
                    VolverDeHerido(gameTime);
                }

                if (lluviaList[e].getEstaMuerto)
                {
                    if (lluviaList[e].getTiempoMuerteLluvia > 360)
                    {
                        lluviaList.RemoveAt(e);
                    }
                    else
                        lluviaList[e].getTiempoMuerteLluvia += gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                    lluviaList[e].getDisparoPosition += new Vector2(viento, velocidad);

                foreach (JefeFinal s in kiel)
                {
                    if (s.getVida < 1)
                        lluviaList[e].getDisparoPosition -= new Vector2(arrastre, 0);
                }
                for (int e2 = 0; e2 < terrenoList.Count; e2++)
                {
                    if (lluviaList[e].collisionRect.Intersects(terrenoList[e2].collisionRect) && lluviaList[e].getEstaMuerto == false)
                    {
                        lluviaList[e].Desde = new Point(0,0);
                        lluviaList[e].Hasta = new Point(4,1);
                        lluviaList[e].getEstaMuerto = true;
                        
                    }

                }
                if (lluviaList[e].getDisparoPosition.X < -200)
                    lluviaList.RemoveAt(e);

                else if (lluviaList[e].getDisparoPosition.Y > 1000)
                    lluviaList.RemoveAt(e);

                lluviaList[e].Update(gameTime, Game.Window.ClientBounds);
            }
        }
        private void Lluvia2(GameTime gameTime, int densidad, float velocidad, int viento)
        {
            tiempoLluvia2 += gameTime.ElapsedGameTime.Milliseconds;
            if (tiempoLluvia2 > densidad)
            {
                lluviaList2.Add(new Disparo(
                        Game.Content.Load<Texture2D>(@"Images/meteorito"),
                        new Vector2(rnd.Next(0, 1500), -200),
                        new Point(200, 200),
                        new Point(80, 80),
                        new Point(0, 0),
                        new Point(0, 0),
                        Vector2.Zero,
                        5.8f,
                        100, 0.201f));
                tiempoLluvia2 = 0;
                lluviaList2[lluviaList2.Count - 1].getEscala = 0.8f;
                lluviaList2[lluviaList2.Count - 1].GetCollisionOffSet = new Point(100, 80);
            }
            for (int e = 0; e < lluviaList2.Count; e++)
            {
                if (lluviaList2[e].collisionRect.Intersects(player.collisionRect) && fondo)
                {
                    if (tiempoTocaRayoKiel > 400)
                    {
                        player.Desde = new Point(0, 4);
                        player.Hasta = new Point(4, 4);
                        for (int n = 0; n < 1; n++)
                        {
                            if (vidaList2.Count > 0)
                                vidaList2.RemoveAt(vidaList2.Count - 1);
                            player.getVida -= 1;
                        }
                        tiempoHerido = 0;
                        player.getHerido = true;
                        if (rnd.Next(0, 2) == 0)
                            reproducirSonido("au1");
                        else
                            reproducirSonido("au2");
                        tiempoTocaRayoKiel = 0;
                    }
                    else
                        tiempoTocaRayoKiel += gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                {
                    VolverDeHerido(gameTime);
                }

                if (lluviaList2[e].getEstaMuerto)
                {
                    if (lluviaList2[e].getTiempoMuerteLluvia > 360)
                    {
                        lluviaList2.RemoveAt(e);
                    }
                    else
                        lluviaList2[e].getTiempoMuerteLluvia += gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                    lluviaList2[e].getDisparoPosition += new Vector2(viento, velocidad);

                foreach (JefeFinal s in kiel)
                {
                    if (s.getVida < 1)
                        lluviaList2[e].getDisparoPosition -= new Vector2(arrastre, 0);
                }
                for (int e2 = 0; e2 < terrenoList2.Count; e2++)
                {
                    if (lluviaList2[e].collisionRect.Intersects(terrenoList2[e2].collisionRect) && lluviaList2[e].getEstaMuerto == false)
                    {
                        lluviaList2[e].Desde = new Point(0, 0);
                        lluviaList2[e].Hasta = new Point(4, 1);
                        lluviaList2[e].getEstaMuerto = true;

                    }

                }
                if (lluviaList2[e].getDisparoPosition.X < -200)
                    lluviaList2.RemoveAt(e);

                else if (lluviaList2[e].getDisparoPosition.Y > 1000)
                    lluviaList2.RemoveAt(e);

            }
            foreach(Sprite s in lluviaList2)
                s.Update(gameTime, Game.Window.ClientBounds);
        }
        private void Fisica(GameTime gameTime)
        {
            foreach (Mapa s in mapaList)
            {


                if (s.collisionRect.Intersects(player.collisionRectPies))
                {
                    currentGravedadState = GravedadState.Suelo;
                    player.getPlayerPosition = new Vector2(player.getPlayerPosition.X, (s.collisionRect.Y) - player.collisionRect.Height);
                }
                if (s.collisionRect.Intersects(player.collisionRectArriba))
                {
                    player.getPlayerPosition = new Vector2(player.getPlayerPosition.X, s.getMapaPosition.Y + s.collisionRect.Height);
                    player.getSaltando = false;
                }

                if (s.collisionRect.Intersects(player.collisionRectDerecha))
                    colisiond++;
                if (s.collisionRect.Intersects(player.collisionRectIzquierda))
                    colisioni++;

                if (s.getMapaPosition.X > maxPosBloque)
                    maxPosBloque = s.getMapaPosition.X;

                s.getMapaVelocidad = new Vector2(arrastre, 0);

                s.Update(gameTime, Game.Window.ClientBounds);
            }

            for (int i = 0; i < mapaList.Count; i++)
            {
                if (mapaList[i].getMapaPosition.X < 0)
                    mapaList.RemoveAt(i);
            }

            if (colisiond > 0)
                parada = player.getPersonajeVelocidad.X;
            else if (colisioni > 0)
                parada = -player.getPersonajeVelocidad.X;
            else
                parada = 0;

            colision = 0;
            colisiond = 0;
            colisioni = 0;
        }
        private void Fisica2(GameTime gameTime)
        {



            lastMouseState = currMouseState;
            currMouseState = Mouse.GetState();
            if (currMouseState.RightButton.Equals(ButtonState.Pressed) && lastMouseState.RightButton.Equals(ButtonState.Released))
            {
                arrastre = 0;
                if (fondo)
                    fondo = false;
                else
                    fondo = true;
            }

            if (fondo)
            {
                player.getEscala = 0.8f;
                player.GetCollisionOffSet = new Point(50, 8);
                player.getCapa = 0.34f;
                foreach (Mapa s in terrenoList2)
                {

                    if (s.collisionRectTerreno.Intersects(player.collisionRectPies) && gravedad >= 0)
                    {
                        currentGravedadState = GravedadState.Suelo;
                        player.getPlayerPosition = new Vector2(player.getPlayerPosition.X, (s.collisionRect.Y) - player.collisionRect.Height);
                        gravedad = 0;
                    }
                }
            }
            else if (fondo == false)
            {
                player.getEscala = 1f;
                player.GetCollisionOffSet = new Point(25, 5);
                player.getCapa = 0.89f;
                foreach (Mapa s in terrenoList)
                {
                    if (s.collisionRectTerreno.Intersects(player.collisionRectPies) && gravedad >= 0)
                    {
                        currentGravedadState = GravedadState.Suelo;
                        gravedad = 0;
                        player.getPlayerPosition = new Vector2(player.getPlayerPosition.X, (s.collisionRect.Y) - player.collisionRect.Height);

                    }
                    //if (s.collisionRect.Intersects(player.collisionRectArriba))
                    //{
                    //    player.getPlayerPosition = new Vector2(player.getPlayerPosition.X, s.getMapaPosition.Y + s.collisionRect.Height);
                    //    player.getSaltando = false;
                    //}

                    if (s.collisionRect.Intersects(player.collisionRectDerecha))
                        colisiond++;
                    if (s.collisionRect.Intersects(player.collisionRectIzquierda))
                        colisioni++;


                    s.getMapaVelocidad = new Vector2(arrastre, 0);

                }


                if (colisiond > 0)
                    parada = player.getPersonajeVelocidad.X;
                else if (colisioni > 0)
                    parada = -player.getPersonajeVelocidad.X;
                else
                    parada = 0;

                //if (colisiond > 0)
                //    parada = player.getPersonajeVelocidad.X;
                //else if (colisioni > 0)
                //    parada = -player.getPersonajeVelocidad.X;
                //else
                //    parada = 0;

                colision = 0;
                colisiond = 0;
                colisioni = 0;
            }
            foreach (Mapa s in terrenoList)
                s.getMapaVelocidad = new Vector2(arrastre, 0);

        }
        private void Disparo(GameTime gameTime)
        {
            MouseState currMouseState = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();
            if (currMouseState.LeftButton == ButtonState.Pressed)
            {

                if (delayDisparoCurrent >= delayDisparo)
                {
                    if (currentDisparoState != DisparoState.Disparando)
                    {
                        player.Desde = new Point(0, 3);
                        player.Hasta = new Point(4, 3);
                        delayDisparoCurrent = 1500;
                    }

                    currentDisparoState = DisparoState.Disparando;
                    posaux = new Vector2((float)Math.Cos(player.getAngulo), (float)Math.Sin(player.getAngulo));
                    disparoList.Add(new Disparo(
                        Game.Content.Load<Texture2D>(@"Images/disparo"),
                        new Vector2(0, 0),
                        new Point(150, 200),
                        new Point(50, 100),
                        new Point(0, 0),
                        new Point(4, 1),
                        Vector2.Zero,
                        0f,
                        100, 1f));
                    int sonidoDisparo = rnd.Next(0, 2);
                    if (sonidoDisparo == 0)
                        reproducirSonido("disparo1");
                    else if (sonidoDisparo == 1)
                        reproducirSonido("disparo2");



                    disparoList[disparoList.Count - 1].getDisparoPosition = new Vector2((player.getPlayerPosition.X + (player.getFrameSize.X / 2)) - (disparoList[disparoList.Count - 1].getFrameSize.X / 2), (player.getPlayerPosition.Y + (player.getFrameSize.Y / 2)) - (disparoList[disparoList.Count - 1].getFrameSize.Y / 2));
                    disparoList[disparoList.Count - 1].getAngulo = posaux - new Vector2(0.17f, 0.15f);
                    //disparoList[disparoList.Count - 1].getRotacion = player.getAngulo + MathHelper.ToRadians(240);
                    delayDisparoCurrent = 0;
                }
                else
                    delayDisparoCurrent += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (currMouseState.LeftButton == ButtonState.Released)
            {

                if (currentDisparoState != DisparoState.Quieto)
                {
                    delayDisparoCurrent = 1500;
                    if (keyboard.IsKeyDown(Keys.D))
                    {
                        player.getEfecto = SpriteEffects.FlipHorizontally;
                        player.Desde = new Point(0, 1);
                        player.Hasta = new Point(4, 1);
                    }
                    else if (keyboard.IsKeyDown(Keys.A))
                    {
                        player.getEfecto = SpriteEffects.None;
                        player.Desde = new Point(0, 1);
                        player.Hasta = new Point(4, 1);
                    }

                }
                currentDisparoState = DisparoState.Quieto;
            }
            for (int e = 0; e < disparoList.Count; e++)
            {


                //disparoList[e].getDisparoPosition += (disparoList[e].getAngulo * 10);
                disparoList[e].getDisparoPosition += new Vector2((disparoList[e].getAngulo.X * 13), (disparoList[e].getAngulo.Y * 10));
                for (int e2 = 0; e2 < mapaList.Count; e2++)
                {
                    if (disparoList[e].collisionRect.Intersects(mapaList[e2].collisionRect))
                    {
                        mapaList.RemoveAt(e2);
                        borrarBala = true;
                    }

                }
                disparoList[e].Update(gameTime, Game.Window.ClientBounds);
                if (borrarBala)
                    disparoList.RemoveAt(e);
                else if (disparoList[e].EstaFuera(Game.Window.ClientBounds))
                    disparoList.RemoveAt(e);
                borrarBala = false;
            }
        }
        private void Salto(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && ks.IsKeyUp(Keys.Space) && player.getSaltando == false)
            {
                if (currentGravedadState == GravedadState.Suelo && gravedad < 15)
                {
                    player.getSaltando = true;
                    reproducirSonido("jump");
                    int saltoSonido = rnd.Next(0, 3);
                    if (saltoSonido == 0)
                        reproducirSonido2("salto1");
                    else if (saltoSonido == 1)
                        reproducirSonido2("salto2");
                    else
                        reproducirSonido2("salto3");
                    currentGravedadState = GravedadState.Aire;
                    gravedad = 0;
                }
            }


            if (player.getSaltando)
            {

                tiempoVelocidadSalto += gameTime.ElapsedGameTime.Milliseconds;


                jumpTimer += 1;
                gravedad = jumpTimer;
                if (currentGravedadState == GravedadState.Suelo)
                    player.getSaltando = false;



            }
            else
            {

                gravedad = 15;
                jumpTimer = -23;
                player.getSaltando = false;
            }
            if (gravedad > 15)
                gravedad = 15;

            if (player.getPlayerPosition.Y > Game.Window.ClientBounds.Height + 500)
            {
                player.getPersonajeVelocidad = Vector2.Zero;
            }
            if (!player.getFin)
                player.getPlayerPosition = new Vector2(player.getPlayerPosition.X - parada - arrastre, player.getPlayerPosition.Y + gravedad);

        }
        private void GenerarTerreno(GameTime gameTime)
        {
            if (maxPosBloque + 32 < 1366)
            {
                for (int i = alturaTerreno; i > 1; i--)
                {
                    mapaList.Add(new Mapa(
                    Game.Content.Load<Texture2D>(@"Images/escenarios"),
                    new Vector2(maxPosBloque + 29, (750 - (32 * i))),
                    new Point(32, 32),
                    new Point(0, 0),
                    new Point(0, 5),
                    new Point(0, 3),
                    new Vector2(0, 0),
                    0f,
                    0, 0.6f));
                }

                if (terrenoTiempoPasado > terrenoTiempo)
                {
                    terrenoDireccion = rnd.Next(0, 3);
                    terrenoTiempoPasado = 0;

                    if (terrenoDireccion == 0 || terrenoDireccion == 1)
                        terrenoTiempo = rnd.Next(0, 10);
                    else
                        terrenoTiempo = rnd.Next(0, 100);
                    countTerreno++;
                }
                else
                    terrenoTiempoPasado += gameTime.ElapsedGameTime.Milliseconds;

                if (terrenoDireccion == 0 && alturaTerreno < 15)
                    alturaTerreno++;
                else if (terrenoDireccion == 1 && alturaTerreno > 2)
                    alturaTerreno--;




            }
            maxPosBloque = 0;
        }
        private void RespawnJefe(GameTime gameTime)
        {

            if (respawnJefeMin < respawnJefeMax)
                respawnJefeMin += gameTime.ElapsedGameTime.Milliseconds;
            else
            {
                jefeList.Add(new Jefe(
                Game.Content.Load<Texture2D>(@"Images/mon"),
                new Vector2(Game.Window.ClientBounds.Width, rnd.Next(0, 500)),
                new Point(117, 156),
                new Point(60, 15),
                new Point(0, 0),
                new Point(4, 1),
                new Vector2(0.8f, 0.8f),
                0f,
                50,
                0.8f,
                5));
                respawnJefeMin = 0;
                respawnJefeMax = rnd.Next(1000, 10000);
            }
        }
        private void Fondo1(GameTime gameTime)
        {
            if (posicionFondo1 <= Game.Window.ClientBounds.Width)
            {
                fondoList1.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/fondo1sin"),
                new Vector2(posicionFondo1 - 10, Game.Window.ClientBounds.Height - 350),
                new Point(1536, 1024),
                new Point(0, 50),
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                0, 0.11f)
                );
                numFondo1++;
                if (numFondo1 % 2 == 0)
                {
                    fondoList1[fondoList1.Count - 1].getEfecto = SpriteEffects.None;
                }
                else
                {
                    fondoList1[fondoList1.Count - 1].getEfecto = SpriteEffects.FlipHorizontally;
                }

            }
            posicionFondo1 = fondoList1[fondoList1.Count - 1].getMapaPosition.X + fondoList1[fondoList1.Count - 1].getMapaFrameSize.X;

            if (!kielMuertoSprite && tiempoMuertoKiel > tiempoMuertoKielTotal)
                {
                    foreach (Mapa s in fondoList1)
                    {
                        s.getMapaPosition -= new Vector2(arrastre / 3, 0);
                    }
                }
            
        }
        private void Fondo2(GameTime gameTime)
        {
            if (posicionFondo2 <= Game.Window.ClientBounds.Width)
            {
                fondoList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/fondo2sin"),
                new Vector2(posicionFondo2 - 10, Game.Window.ClientBounds.Height - 900),
                new Point(1536, 743),
                new Point(0, 50),
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                0, 0.10f)
                );
                numFondo2++;
                if (numFondo2 % 2 == 0)
                {
                    fondoList2[fondoList2.Count - 1].getEfecto = SpriteEffects.None;
                }
                else
                {
                    fondoList2[fondoList2.Count - 1].getEfecto = SpriteEffects.FlipHorizontally;
                }
            }
            posicionFondo2 = fondoList2[fondoList2.Count - 1].getMapaPosition.X + fondoList2[fondoList2.Count - 1].getMapaFrameSize.X;
            
                if (!kielMuertoSprite && tiempoMuertoKiel > tiempoMuertoKielTotal)
                {
                    foreach (Mapa s in fondoList2)
                    {
                        s.getMapaPosition -= new Vector2(arrastre / 8, 0);
                    }
                }
            
        }
        private void Inteligencia1(GameTime gameTime)
        {

            for (int i = 0; i < jefeList.Count; i++)
            {
                if (jefeList[i].getAgresivo && jefeList[i].getHerido == false)
                {
                    if (jefeList[i].getPlayerPosition.Y + (jefeList[i].getFrameSize.Y / 2) > player.getPlayerPosition.Y + (player.getFrameSize.Y / 2))
                    {
                        if (jefeList[i].getPlayerPosition.X + (jefeList[i].getFrameSize.X / 2) > player.getPlayerPosition.X + (player.getFrameSize.X / 2))
                        {
                            jefeList[i].Direccion = new Vector2(-1, -1);
                            jefeList[i].getEfecto = SpriteEffects.None;
                        }
                        else if (jefeList[i].getPlayerPosition.X + (jefeList[i].getFrameSize.X / 2) < player.getPlayerPosition.X + (player.getFrameSize.X / 2))
                        {
                            jefeList[i].Direccion = new Vector2(1, -1);
                            jefeList[i].getEfecto = SpriteEffects.FlipHorizontally;
                        }
                    }
                    else if (jefeList[i].getPlayerPosition.Y + (jefeList[i].getFrameSize.Y / 2) < player.getPlayerPosition.Y + (player.getFrameSize.Y / 2))
                    {
                        if (jefeList[i].getPlayerPosition.X + (jefeList[i].getFrameSize.X / 2) > player.getPlayerPosition.X + (player.getFrameSize.X / 2))
                        {
                            jefeList[i].Direccion = new Vector2(-1, 1);
                            jefeList[i].getEfecto = SpriteEffects.None;
                        }
                        else if (jefeList[i].getPlayerPosition.X + (jefeList[i].getFrameSize.X / 2) < player.getPlayerPosition.X + (player.getFrameSize.X / 2))
                        {
                            jefeList[i].Direccion = new Vector2(1, 1);
                            jefeList[i].getEfecto = SpriteEffects.FlipHorizontally;
                        }
                    }
                    //else
                    //    jefeList[i].Direccion = new Vector2(0, 0); //Para que se queden más tiempo quietos

                }
                if (jefeList[i].collisionRect.Intersects(player.collisionRectAura))
                    jefeList[i].getAgresivo = true;
                else
                    jefeList[i].getAgresivo = false;

                if (jefeList[i].collisionRect.Intersects(player.collisionRect) && player.getHerido == false)
                {
                    player.Desde = new Point(0, 4);
                    player.Hasta = new Point(4, 4);
                    for (int n = 0; n < 5; n++)
                    {
                        if (vidaList2.Count > 0)
                            vidaList2.RemoveAt(vidaList2.Count - 1);
                        player.getVida -= 1;
                    }
                    tiempoHerido = 0;
                    player.getHerido = true;
                    jefeList[i].getTocado = true;
                    if (rnd.Next(0, 2) == 0)
                        reproducirSonido("au1");
                    else
                        reproducirSonido("au2");
                    int sonidoBicho = rnd.Next(0, 3);
                    if (sonidoBicho == 0)
                        reproducirSonido2("bicho1");
                    else if (sonidoBicho == 1)
                        reproducirSonido2("bicho2");
                    else
                        reproducirSonido2("bicho3");
                }



                if (jefeList[i].getTocado)
                {
                    if (jefeList[i].getPlayerPosition.X + (jefeList[i].getFrameSize.X / 2) > player.getPlayerPosition.X + (player.getFrameSize.X / 2))
                    {
                        jefeList[i].getPlayerPosition += new Vector2(15, 0);
                    }
                    else if (jefeList[i].getPlayerPosition.X + (jefeList[i].getFrameSize.X / 2) < player.getPlayerPosition.X + (player.getFrameSize.X / 2))
                    {
                        jefeList[i].getPlayerPosition -= new Vector2(15, 0);
                    }
                    if (jefeList[i].getPlayerPosition.Y + (jefeList[i].getFrameSize.Y / 2) > player.getPlayerPosition.Y + (player.getFrameSize.Y / 2))
                    {
                        player.getPlayerPosition -= new Vector2(0, 5); //toca al enemigo por arriba
                    }
                }


                if (tiempoHerido < 1000 && player.getHerido)
                {
                    tiempoHerido += gameTime.ElapsedGameTime.Milliseconds;

                }
                else
                {
                    if (player.getHerido)
                    {
                        player.Desde = new Point(0, 0);
                        player.Hasta = new Point(4, 1);
                        player.getHerido = false;
                    }
                    jefeList[i].getTocado = false;

                }

            }

        }
        private void ConstitucionMon(GameTime gameTime)
        {
            for (int i = 0; i < jefeList.Count; i++)
            {
                for (int e = 0; e < disparoList.Count; e++)
                {

                    if (disparoList[e].collisionRect.Intersects(jefeList[i].collisionRect))
                    {
                        if (jefeList[i].getVida > 0)
                        {
                            jefeList[i].getVida -= 1;
                            disparoList.RemoveAt(e);
                            jefeList[i].getCurrentTiempoHerido = 400;
                            jefeList[i].getHerido = true;

                        }


                    }
                }
                if (jefeList[i].getVida <= 0)
                {
                    if (jefeList[i].getMuerto == false)
                    {
                        jefeList[i].Desde = new Point(0, 2);
                        jefeList[i].Hasta = new Point(5, 2);
                        jefeList[i].getMilisegundosPorFrame = 200;
                        jefeList[i].getCurrentTiempoHerido = 980;
                        jefeList[i].getMuerto = true;
                    }


                }
                else
                {
                    if (jefeList[i].getHerido)
                    {
                        jefeList[i].Desde = new Point(0, 3);
                        jefeList[i].Hasta = new Point(0, 3);
                    }
                    if (jefeList[i].getHerido && jefeList[i].getCurrentTiempoHerido <= 0)
                    {
                        if (jefeList[i].getAgresivo)
                        {
                            jefeList[i].Desde = new Point(0, 1);
                            jefeList[i].Hasta = new Point(4, 2);
                        }
                        else
                        {
                            jefeList[i].Desde = new Point(0, 0);
                            jefeList[i].Hasta = new Point(4, 0);
                        }
                        jefeList[i].getHerido = false;
                    }
                    else
                        jefeList[i].getCurrentTiempoHerido -= gameTime.ElapsedGameTime.Milliseconds;
                }
                foreach (JefeFinal s in kiel)
                {
                    if (s.getVida < 1)
                        jefeList[i].getPlayerPosition -= new Vector2(arrastre, 0);
                }
                jefeList[i].Update(gameTime, Game.Window.ClientBounds);
                if (jefeList[i].getCurrentTiempoHerido <= 0 && jefeList[i].getMuerto)
                {
                    jefeList.RemoveAt(i);
                }
                else
                    jefeList[i].getCurrentTiempoHerido -= gameTime.ElapsedGameTime.Milliseconds;
            }



        }
        private void Mapa1(GameTime gameTime)
        {
            //terrenoListSuelo.Add(new Mapa(
            //        Game.Content.Load<Texture2D>(@"Images/terreno1"),
            //        new Vector2(posicionTerrenoBase + rnd.Next(0, 30), Game.Window.ClientBounds.Height - 100),
            //        new Point(1160, 185),
            //        new Point(0, 50),
            //        new Point(0, 0),
            //        new Point(0, 0),
            //        Vector2.Zero,
            //        0f,
            //        0, 0.5f)
            //     );
            //posicionTerrenoBase = terrenoListSuelo[terrenoListSuelo.Count - 1].getMapaPosition.X + terrenoListSuelo[terrenoListSuelo.Count - 1].getMapaFrameSize.X;
            if (currentParteMapa.Equals(ParteMapa.Suelo))
            {

                if (posicionTerrenoX <= Game.Window.ClientBounds.Width + 475)
                {
                    terrenoList.Add(new Mapa(
                    Game.Content.Load<Texture2D>(@"Images/terreno3"),
                    new Vector2(posicionTerrenoX - 233 + rnd.Next(0, 465), rnd.Next(300, 650)),
                    new Point(467, 166),
                    new Point(0, 30),
                    new Point(0, 0),
                    new Point(0, 0),
                    Vector2.Zero,
                    0f,
                    0, 0.5f)
                    );
                    numTerreno1++;
                    if (numTerreno1 % 2 == 0)
                        terrenoList[terrenoList.Count - 1].getCapa = 0.5f;
                    else
                        terrenoList[terrenoList.Count - 1].getCapa = 0.6f;
                }

            }
            else if (currentParteMapa.Equals(ParteMapa.Saltitos))
            {
                if (posicionTerrenoX <= Game.Window.ClientBounds.Width)
                {
                    terrenoList.Add(new Mapa(
                    Game.Content.Load<Texture2D>(@"Images/terreno2"),
                    new Vector2(posicionTerrenoX + rnd.Next(0, 20), rnd.Next(300, 650)),
                    new Point(67, 97),
                    new Point(-10, 30),
                    new Point(0, rnd.Next(0, 3)),
                    new Point(0, 0),
                    Vector2.Zero,
                    0f,
                    0, 0.4f)
                    );

                }

            }
            posicionTerrenoX = terrenoList[terrenoList.Count - 1].getMapaPosition.X + terrenoList[terrenoList.Count - 1].getMapaFrameSize.X;


            if (terrenoList[0].getMapaPosition.X < -3000 && terrenoList[0].getMapaPosition.X >= -4000)
                currentParteMapa = ParteMapa.Saltitos;
            else if (terrenoList[0].getMapaPosition.X < -4000 && terrenoList[0].getMapaPosition.X >= -6000)
                currentParteMapa = ParteMapa.Suelo;
            else if (terrenoList[0].getMapaPosition.X < -6000 && terrenoList[0].getMapaPosition.X >= -7000)
                currentParteMapa = ParteMapa.Saltitos;
            else if (terrenoList[0].getMapaPosition.X < -7000 && terrenoList[0].getMapaPosition.X >= -10000)
                currentParteMapa = ParteMapa.Suelo;


            //foreach (Mapa s in terrenoListSuelo)
            //{
            //    s.getMapaPosition -= new Vector2(arrastre, 0);
            //    s.Update(gameTime, Game.Window.ClientBounds);

            //}
        }
        private void SegundoPlano()
        {
            terrenoList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/terrenolargo"),
                new Vector2(1148, 100),
                new Point(548, 800),
                new Point(83, 60),
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.31f)
                );

            terrenoList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/terrenolargo"),
                new Vector2(850, 300),
                new Point(548, 800),
                new Point(83, 60),
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.32f)
                );

            terrenoList2.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/terrenolargo"),
                new Vector2(650, 450),
                new Point(548, 800),
                new Point(83, 60),
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.33f)
                );
        }
        private void Escaleras(float posicionTerrenoX, float posicionTerrenoY, int distanciaEscalones, int alturaEscalones, int escalones, List<Mapa> lista, float capa, Boolean encima)
        {
            for (int i = 0; i < escalones; i++)
            {
                lista.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/terrenolargo"),
                new Vector2(posicionTerrenoX + distanciaEscalones, posicionTerrenoY - alturaEscalones),
                new Point(548, 800),
                new Point(40, 35),
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, capa)
                );
                posicionTerrenoX = lista[lista.Count - 1].getMapaPosition.X;
                posicionTerrenoY = lista[lista.Count - 1].getMapaPosition.Y;
                if (encima)
                {
                    if (alturaEscalones > 0)
                        capa += 0.001f;
                    else
                        capa -= 0.001f;
                }
                else if (!encima)
                {
                    if (alturaEscalones < 0)
                        capa += 0.001f;
                    else
                        capa -= 0.001f;
                }
                lista[lista.Count - 1].getCapa = capa;
            }
            this.posicionTerrenoX = posicionTerrenoX;
            this.posicionTerrenoY = posicionTerrenoY;
        }
        private void Escaleras2(float posicionTerrenoX, float posicionTerrenoY, int distanciaEscalones, int alturaEscalones, int escalones, List<Mapa> lista, float capa, Boolean encima)
        {
            for (int i = 0; i < escalones; i++)
            {
                lista.Add(new Mapa(
                    Game.Content.Load<Texture2D>(@"Images/terreno3"),
                    new Vector2(posicionTerrenoX + distanciaEscalones, posicionTerrenoY - alturaEscalones),
                    new Point(467, 166),
                    new Point(0, 30),
                    new Point(0, 0),
                    new Point(0, 0),
                    Vector2.Zero,
                    0f,
                    0, capa)
                    );
                posicionTerrenoX = lista[lista.Count - 1].getMapaPosition.X;
                posicionTerrenoY = lista[lista.Count - 1].getMapaPosition.Y;
                if (encima)
                {
                    if (alturaEscalones > 0)
                        capa += 0.001f;
                    else
                        capa -= 0.001f;
                }
                else if (!encima)
                {
                    if (alturaEscalones < 0)
                        capa += 0.001f;
                    else
                        capa -= 0.001f;
                }
                lista[lista.Count - 1].getCapa = capa;
            }
            this.posicionTerrenoX = posicionTerrenoX;
            this.posicionTerrenoY = posicionTerrenoY;
        }
        private void Suelo(float posicionTerrenoX, float posicionTerrenoY, int distancia, int altura, List<Mapa> lista)
        {
            lista.Add(new Mapa(
                Game.Content.Load<Texture2D>(@"Images/terrenolargo"),
                new Vector2(posicionTerrenoX + distancia, posicionTerrenoY + altura),
                new Point(548, 800),
                new Point(40, 35),
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                0f,
                1000, 0.5f)
                );
            posicionTerrenoX = lista[lista.Count - 1].getMapaPosition.X;
            posicionTerrenoY = lista[lista.Count - 1].getMapaPosition.Y;
            lista[terrenoList.Count - 1].getCapa = capaTerreno;
            this.posicionTerrenoX = posicionTerrenoX;
            this.posicionTerrenoY = posicionTerrenoY;
        }
        private void SueloChico(float posicionTerrenoX, float posicionTerrenoY, int distancia, int altura, int tipo, List<Mapa> lista)
        {
            lista.Add(new Mapa(
                    Game.Content.Load<Texture2D>(@"Images/terreno2"),
                    new Vector2(posicionTerrenoX + distancia, posicionTerrenoY + altura),
                    new Point(67, 100),
                    new Point(-10, 35),
                    new Point(0, tipo),
                    new Point(0, 0),
                    Vector2.Zero,
                    0f,
                    1000, 0.49f)
                    );
            posicionTerrenoX = lista[lista.Count - 1].getMapaPosition.X;
            posicionTerrenoY = lista[lista.Count - 1].getMapaPosition.Y;
            if (altura > 0)
                capaTerreno += 0.001f;
            else
                capaTerreno -= 0.001f;
            lista[lista.Count - 1].getCapa = capaTerreno;
            this.posicionTerrenoX = posicionTerrenoX;
            this.posicionTerrenoY = posicionTerrenoY;
        }
        private void SueloGrande(float posicionTerrenoX, float posicionTerrenoY, int distancia, int altura, List<Mapa> lista)
        {
            lista.Add(new Mapa(
                    Game.Content.Load<Texture2D>(@"Images/terrenoRecto"),
                    new Vector2(posicionTerrenoX, posicionTerrenoY + altura),
                    new Point(1000, 512),
                    new Point(-10, 50),
                    new Point(0, 0),
                    new Point(0, 0),
                    Vector2.Zero,
                    0f,
                    1000, capaTerreno)
            );
            capaTerreno -= 0.001f;
            lista[terrenoList.Count - 1].getEfecto = SpriteEffects.None;

            posicionTerrenoX = lista[lista.Count - 1].getMapaPosition.X + lista[lista.Count - 1].getFrameSize.X- 5;
            posicionTerrenoY = lista[lista.Count - 1].getMapaPosition.Y;
            capaTerreno = lista[lista.Count - 1].getCapa - 0.01f;
            lista.Add(new Mapa(
                    Game.Content.Load<Texture2D>(@"Images/terrenoRecto"),
                    new Vector2(posicionTerrenoX, posicionTerrenoY),
                    new Point(1000, 512),
                    new Point(-10, 50),
                    new Point(0, 0),
                    new Point(0, 0),
                    Vector2.Zero,
                    0f,
                    1000, capaTerreno - 0.001f)
            );
            lista[lista.Count - 1].getEfecto = SpriteEffects.FlipHorizontally;
            posicionTerrenoX = lista[lista.Count - 1].getMapaPosition.X;
            posicionTerrenoY = lista[lista.Count - 1].getMapaPosition.Y;

            lista[lista.Count - 1].getCapa = capaTerreno;
            this.posicionTerrenoX = posicionTerrenoX;
            this.posicionTerrenoY = posicionTerrenoY;
        }
        private void PuntoControlTigre(float posicionX, float posicionY)
        {
            tigres.Add(new Mapa(
            Game.Content.Load<Texture2D>(@"Images/growlie"),
            new Vector2(posicionX, posicionY),
            new Point(87, 88),
            new Point(0, 0),
            new Point(0, 0),
            new Point(2, 1),
            Vector2.Zero,
            0f,
            1000, 0.99f));
        }
        public Boolean getMuertoDelTo
        {
            get
            {
                return muertoDelTo;
            }
            set
            {
                muertoDelTo = value;
            }
        }
        public Boolean getCompletoDelTo
        {
            get
            {
                return completoDelTo;
            }
            set
            {
                completoDelTo = value;
            }
        }
        private void VolverDeHerido(GameTime gameTime)
        {
            if (tiempoHerido < 1000 && player.getHerido)
            {
                tiempoHerido += gameTime.ElapsedGameTime.Milliseconds;

            }
            else
            {
                if (player.getHerido)
                {
                    player.Desde = new Point(0, 0);
                    player.Hasta = new Point(4, 1);
                    player.getHerido = false;
                }

            }
        }
        private void DisparaJefeFinal(GameTime gameTime)
        {

            if (!kielMuertoSprite && tiempoMuertoKiel > tiempoMuertoKielTotal)
            {
                foreach (Disparo s in lluviaList2)
                {
                    s.getDisparoPosition -= new Vector2(arrastre / 2, 0);
                }
            }

            foreach (JefeFinal k in kiel)
            {
                if (k.getDisparaKiel)
                {


                    if (k.getEfecto == SpriteEffects.FlipHorizontally)
                    {
                        if (k.getPlayerPosition.X + k.getFrameSize.X + posxDisparoKiel < Game.Window.ClientBounds.Width)
                        {
                            disparoJefeList.Add(new Mapa(
                            Game.Content.Load<Texture2D>(@"Images/DisparoJefe"),
                            new Vector2(k.getPlayerPosition.X + k.getFrameSize.X + posxDisparoKiel, k.getPlayerPosition.Y),
                            new Point(200, 200),
                            new Point(0, 0),
                            new Point(0, 0),
                            new Point(4, 1),
                            new Vector2(0f, 0),
                            0f,
                            100, 0.99f));
                            posxDisparoKiel += 100;
                        }
                    }
                    else
                    {
                        if (k.getPlayerPosition.X + k.getFrameSize.X + posxDisparoKiel > 0)
                        {
                            disparoJefeList.Add(new Mapa(
                            Game.Content.Load<Texture2D>(@"Images/DisparoJefe"),
                            new Vector2(k.getPlayerPosition.X + posxDisparoKiel - 200, k.getPlayerPosition.Y),
                            new Point(200, 200),
                            new Point(0, 0),
                            new Point(0, 0),
                            new Point(4, 1),
                            new Vector2(0f, 0),
                            0f,
                            100, 0.99f));
                            posxDisparoKiel -= 100;
                        }
                    }


                }
                else
                {
                    BorrarLista(disparoJefeList);
                    posxDisparoKiel = 0;
                }
                foreach (Mapa s in disparoJefeList)
                {
                    
                    s.getMapaPosition += new Vector2(0, k.getPersonajeVelocidad.Y);
                    s.getEfecto = k.getEfecto;
                    if (s.collisionRect.Intersects(player.collisionRect) && fondo == false)
                    {
                        Dolor(gameTime,800);
                    }
                    else
                    {
                        VolverDeHerido(gameTime);
                    }

                    s.Update(gameTime, Game.Window.ClientBounds);
                }
            }
        }
        private void BorrarLista(List<Mapa> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                lista.RemoveAt(i);
            }
        }
        private void ConstitucionKiel(GameTime gameTime)
        {
            foreach (JefeFinal k in kiel)
            {
                for (int i = 0; i < disparoList.Count; i++)
                {
                    if (disparoList[i].collisionRect.Intersects(k.collisionRect) && fondo == false)
                    {
                        if (k.getVida > 0 && k.getEstadoKiel != JefeFinal.JefeState.Normal)
                        {
                            for (int n = 0; n < 1; n++)
                            {
                                if (vidaList2Kiel.Count > 0)
                                    vidaList2Kiel.RemoveAt(vidaList2Kiel.Count - 1);
                                k.getVida -= 1;
                            }
                            
                            k.getCurrentTiempoHerido = 400;
                            k.getHerido = true;

                        }
                        disparoList.RemoveAt(i);
                    }
                }
                if (k.getVida > 0)
                    arrastre = 0;
                else
                {
                    

                    kiel[0].getDisparaKiel = false;
                        kielMuerto = true;

                        
                }
               if(k.collisionRect.Intersects(player.collisionRect) && fondo == false)
               {
                   Dolor(gameTime, 100);
               }
            }
        }
        private void Temblor(GameTime gameTime)
        {
            if (temblorTerrenoTime > 100)
            {
                temblorTerrenoElec++;
                if (temblorTerrenoElec % 2 == 0)
                {
                    foreach (Mapa s in terrenoList)
                        s.getMapaPosition += new Vector2(temblorTerrenoNivel, 0);
                    foreach (Mapa s in terrenoList2)
                        s.getMapaPosition += new Vector2(temblorTerrenoNivel, 0);
                }
                else
                {
                    foreach (Mapa s in terrenoList)
                        s.getMapaPosition -= new Vector2(temblorTerrenoNivel, 0);
                    foreach (Mapa s in terrenoList2)
                        s.getMapaPosition -= new Vector2(temblorTerrenoNivel, 0);
                }
                temblorTerrenoTime = 0;
                
            }
            else
                temblorTerrenoTime += gameTime.ElapsedGameTime.Milliseconds;
        }
        private void VolverAlCentro()
        {
            if (player.getPlayerPosition.X + (player.getFrameSize.X / 2) < (Game.Window.ClientBounds.Width  / 2)-6 && !estaEnSuSitio)
            {
                player.getPlayerPosition += new Vector2(10, 0);
                foreach (Mapa s in terrenoList)
                    s.getMapaPosition += new Vector2(10, 0);
                foreach (Mapa s in terrenoList2)
                    s.getMapaPosition += new Vector2(5, 0);
                foreach (Mapa s in fondoList1)
                    s.getMapaPosition += new Vector2(10 / 3, 0);
                foreach (Mapa s in fondoList2)
                    s.getMapaPosition += new Vector2(10 / 8, 0);
            }
            else if (player.getPlayerPosition.X + (player.getFrameSize.X / 2) > (Game.Window.ClientBounds.Width / 2) + 6 && !estaEnSuSitio)
            {
                player.getPlayerPosition -= new Vector2(10, 0);
                foreach (Mapa s in terrenoList)
                    s.getMapaPosition -= new Vector2(10, 0);
                foreach (Mapa s in terrenoList2)
                    s.getMapaPosition -= new Vector2(5, 0);
                foreach (Mapa s in fondoList1)
                    s.getMapaPosition -= new Vector2(10 / 3, 0);
                foreach (Mapa s in fondoList2)
                    s.getMapaPosition -= new Vector2(10 / 8, 0);
            }
            else
                estaEnSuSitio = true;
        }
        public void setVolumen(float musicVolume)
        {
            musicCategory.SetVolume(musicVolume);
        }
        private void Dolor(GameTime gameTime, float tiempoDolor)
        {
            if (tiempoTocaRayoKiel > tiempoDolor)
            {
                player.Desde = new Point(0, 4);
                player.Hasta = new Point(4, 4);
                for (int n = 0; n < 1; n++)
                {
                    if (vidaList2.Count > 0)
                        vidaList2.RemoveAt(vidaList2.Count - 1);
                    player.getVida -= 1;
                }
                tiempoHerido = 0;
                player.getHerido = true;
                if (rnd.Next(0, 2) == 0)
                    reproducirSonido("au1");
                else
                    reproducirSonido("au2");
                tiempoTocaRayoKiel = 0;
            }
            else
                tiempoTocaRayoKiel += gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}
