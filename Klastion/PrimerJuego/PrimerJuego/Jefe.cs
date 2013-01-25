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
    class Jefe : Sprite
    {
        Vector2 direccion;
        float monDelayMovimiento = 3000;
        float monDelayMovimientoCurrent = 0;
        Random rnd = new Random();
        Boolean herido = false;
        Boolean muerto = false;
        float currentTiempoHerido = 0;
        Boolean agresivo = false;
        Boolean agresivoSprite = false;
        Boolean tocado = false;
        /*========================================
* ========== CONSTRUCTOR 1 ==============
* =====================================*/
        public Jefe(
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
        public Jefe(
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
        public Jefe(
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
        public Jefe(
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
            Inteligencia2(gameTime);
            position += direction;

           
            if (position.Y < 0)
                position.Y = 0;


            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = (clientBounds.Height - frameSize.Y);
            base.Update(gameTime, clientBounds);
        }

        private void Inteligencia2(GameTime gameTime)
        {
            if (agresivo == false)
            {
                if (agresivoSprite == false)
                {
                    Desde = new Point(0, 0);
                    Hasta = new Point(4, 0);
                    agresivoSprite = true;
                }
                speed = new Vector2(0.8f, 0.8f);
                if (monDelayMovimientoCurrent >= monDelayMovimiento)
                {

                    monDelayMovimientoCurrent = 0;
                    float dirx = -1 + rnd.Next(0, 3);
                    float diry = -1 + rnd.Next(0, 3);
                    direccion = new Vector2(dirx, diry);
                    if (dirx < 0)
                        getEfecto = SpriteEffects.None;
                    else if (dirx > 0)
                        getEfecto = SpriteEffects.FlipHorizontally;

                }
                else
                    monDelayMovimientoCurrent += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                speed = new Vector2(5, 3);
                if (agresivoSprite)
                {
                    Desde = new Point(0, 1);
                    Hasta = new Point(4, 2);
                    agresivoSprite = false;
                }
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

        public Boolean getAgresivo
        {
            get
            {
                return agresivo;
            }
            set
            {
                agresivo = value;
            }
        }

        public Boolean getMuerto
        {
            get
            {
                return muerto;
            }
            set
            {
                muerto = value;
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
    }
}
