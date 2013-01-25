using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PrimerJuego
{
    class JefeFinal : Sprite
    {
        Vector2 direccion;
        float monDelayMovimiento = 3000;
        float monDelayMovimientoCurrent = 0;
        Random rnd = new Random();
        Boolean herido = false;
        Boolean muerto = false;
        float currentTiempoHerido = 0;
        float preparandoseSpriteTime = 0;
        float agresivoSpriteTime = 0;
        float normalSpriteTime = 0;
        Boolean preparandoseSprite = false;
        Boolean agresivoSprite = false;
        Boolean normalSprite = false;
        Boolean tocado = false;
        float currentTiempoAgresivo = 0;
        public enum JefeState { Normal, Preparandose, Agresivo, Muerto };
        JefeState currentJefeState = JefeState.Preparandose;
        Boolean dispara = false;
        /*========================================
* ========== CONSTRUCTOR 1 ==============
* =====================================*/
        public JefeFinal(
        Texture2D textureImage,
        Vector2 position,
        Point frameSize,
        Point collisionOffset,
        Point currentFrame,
        Point sheetSize,
        Vector2 speed,
        float rotacion)
            : base(
                textureImage,
                position,
                frameSize,
                collisionOffset,
                currentFrame,
                sheetSize,
                speed,
                rotacion) { }
        /*========================================
        * ========== CONSTRUCTOR 2 ==============
        * =====================================*/
        public JefeFinal(
        Texture2D textureImage,
        Vector2 position,
        Point frameSize,
        Point collisionOffset,
        Point currentFrame,
        Point sheetSize,
        Vector2 speed,
        float rotacion,
        int millisecondsPerFrame)
            : base(
                textureImage,
                position,
                frameSize,
                collisionOffset,
                currentFrame,
                sheetSize,
                speed,
                rotacion,
                millisecondsPerFrame) { }

        /*========================================
       * ========== CONSTRUCTOR 3 ==============
       * =====================================*/
        public JefeFinal(
        Texture2D textureImage,
        Vector2 position,
        Point frameSize,
        Point collisionOffset,
        Point currentFrame,
        Point sheetSize,
        Vector2 speed,
        float rotacion,
        int millisecondsPerFrame,
            float capa)
            : base(
                textureImage,
                position,
                frameSize,
                collisionOffset,
                currentFrame,
                sheetSize,
                speed,
                rotacion,
                millisecondsPerFrame,
                capa) { }

        /*========================================
        * ========== CONSTRUCTOR 4 ==============
        * =====================================*/
        public JefeFinal(
        Texture2D textureImage,
        Vector2 position,
        Point frameSize,
        Point collisionOffset,
        Point currentFrame,
        Point sheetSize,
        Vector2 speed,
        float rotacion,
        int millisecondsPerFrame,
            float capa,
            int vida)
            : base(
                textureImage,
                position,
                frameSize,
                collisionOffset,
                currentFrame,
                sheetSize,
                speed,
                rotacion,
                millisecondsPerFrame,
                capa,
                vida) { }

        public override Vector2 direction
        {

            get
            {
                KeyboardState keyboard = Keyboard.GetState();
                Vector2 inputDirection = direccion;

                return inputDirection * speed;
            }
        }


        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {

            if (currentJefeState != JefeState.Muerto)
            {
                if (currentTiempoAgresivo < 15000)
                {
                    currentTiempoAgresivo += gameTime.ElapsedGameTime.Milliseconds;
                    Inteligencia2(gameTime, clientBounds);
                }
                else if (currentTiempoAgresivo >= 15000 && currentTiempoAgresivo < 20000)
                {
                    currentTiempoAgresivo += gameTime.ElapsedGameTime.Milliseconds;
                    Inteligencia3(gameTime);
                }
                else if (currentTiempoAgresivo >= 20000 && currentTiempoAgresivo < 28000)
                {
                    currentTiempoAgresivo += gameTime.ElapsedGameTime.Milliseconds;
                    Inteligencia4(gameTime);
                }
                else if (currentTiempoAgresivo >= 28000)
                {
                    currentTiempoAgresivo = 0;
                    speed = new Vector2(5, 5);
                }
            }
            position += direction;

            if (position.X +(frameSize.X / 2) < clientBounds.Width / 2)
                getEfecto = SpriteEffects.FlipHorizontally;
            else
                getEfecto = SpriteEffects.None;
           
            if (position.Y < 0)
                position.Y = 0;
            else if (position.Y > clientBounds.Height - frameSize.Y -50)
                position.Y = (clientBounds.Height - frameSize.Y - 50);
            if (position.X < 0)
                position.X = 0;
            else if (position.X > clientBounds.Width - frameSize.X)
                position.X = (clientBounds.Width - frameSize.X);
            if(!currentJefeState.Equals(JefeState.Agresivo))
            base.Update(gameTime, clientBounds);
        }

        private void Inteligencia2(GameTime gameTime, Rectangle clientBounds)
        {

            if (!currentJefeState.Equals(JefeState.Normal))
            {
                Desde = new Point(0, 2);
                Hasta = new Point(4, 2);
                dispara = false;
                currentJefeState = JefeState.Normal;
            }
                if (normalSpriteTime > 1600)
                {
                    Desde = new Point(0, 3);
                    Hasta = new Point(0, 3);
                    normalSpriteTime = 0;
                    float dirx = -1 + rnd.Next(0, 3);
                    float diry = -1 + rnd.Next(0, 3);

                    direccion = new Vector2(dirx, diry);
                    if (dirx < 0)
                        getEfecto = SpriteEffects.None;
                    else if (dirx > 0)
                        getEfecto = SpriteEffects.FlipHorizontally;
                    
                    
                }
                else
                    normalSpriteTime += gameTime.ElapsedGameTime.Milliseconds;

                
        }

        private void Inteligencia3(GameTime gameTime)
        {
            int lado = 0;
            if (currentJefeState.Equals(JefeState.Normal))
            {
                lado = rnd.Next(0, 2);
                Desde = new Point(0, 4);
                Hasta = new Point(4, 4);
                if (lado == 0)
                {
                    direccion = new Vector2(-1, -1);
                    getEfecto = SpriteEffects.None;
                }
                else
                {
                    direccion = new Vector2(1, -1);
                    getEfecto = SpriteEffects.FlipHorizontally;
                }
                currentJefeState = JefeState.Preparandose;
            }
            if (preparandoseSpriteTime <= 1600)
            {
                preparandoseSpriteTime += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                Desde = new Point(0, 0);
                Hasta = new Point(0, 0);
                preparandoseSpriteTime = 0;
            }

        }

        private void Inteligencia4(GameTime gameTime)
        {
            if (currentJefeState.Equals(JefeState.Preparandose))
            {
                Desde = new Point(0, 0);
                Hasta = new Point(3, 1);
                direccion = new Vector2(0, 1);
                speed = new Vector2(1f, 1f);
                dispara = false;
                currentJefeState = JefeState.Agresivo;
            }
            if (agresivoSpriteTime < 1600)
                agresivoSpriteTime += gameTime.ElapsedGameTime.Milliseconds;
            else
            {
                agresivoSpriteTime = 0;
                dispara = true;
                Desde = new Point(3, 1);
                Hasta = new Point(3, 1);
            }
        }

        public Vector2 getPlayerPosition
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Boolean getDisparaKiel
        {
            get
            {
                return dispara;
            }
            set
            {
                dispara = value;
            }
        }

        public Boolean getHerido
        {
            get
            {
                return herido;
            }
            set
            {
                herido = value;
            }
        }

        public Boolean getTocado
        {
            get
            {
                return tocado;
            }
            set
            {
                tocado = value;
            }
        }

        public Vector2 Direccion
        {
            get
            {
                return direccion;
            }
            set
            {
                direccion = value;
            }
        }

        public Point getFrameSize
        {
            get
            {
                return frameSize;
            }
            set
            {
                frameSize = value;
            }
        }

        public float getAngulo
        {
            get
            {
                return angulo;
            }
        }

        public float getCurrentTiempoHerido
        {
            get
            {
                return currentTiempoHerido;
            }
            set
            {
                currentTiempoHerido = value;
            }
        }

        public Vector2 getPersonajeVelocidad
        {
            get
            {
                return speed;
            }
        }

        public JefeState getEstadoKiel
        {
            get
            {
                return currentJefeState;
            }
            set
            {
                currentJefeState = value;
            }
        }
    }
}
