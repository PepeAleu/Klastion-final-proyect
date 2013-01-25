using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PrimerJuego
{
    class Disparo : Sprite
    {
        Vector2 angulo;
        Boolean muerto = false;
        float tiempoMuerteLluvia = 0;

        public Disparo(
            Texture2D textureImage, 
            Vector2 position, 
            Point frameSize,
            Point collisionOffset, 
            Point currentFrame, //Desde
            Point sheetSize,    //Hasta
            Vector2 speed,
            float rotacion)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, rotacion)
        { }
        public Disparo(Texture2D textureImage, Vector2 position, Point frameSize,
        Point collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, float rotacion,
        int millisecondsPerFrame, float capa)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed,rotacion,  millisecondsPerFrame, capa)
        { }

        public override Vector2 direction
        {
            get { return speed; }
        }

       
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            
            base.Update(gameTime, clientBounds);
        }

        public Vector2 getDisparoPosition
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

        public Vector2 getAngulo
        {
            get
            {
                return angulo;
            }
            set
            {
                angulo = value;
            }
        }

        public float getRotacion
        {
            get
            {
                return rotacion;
            }
            set
            {
                rotacion = value;
            }
        }

        public Boolean getEstaMuerto
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

        public float getTiempoMuerteLluvia
        {
            get
            {
                return tiempoMuerteLluvia;
            }
            set
            {
                tiempoMuerteLluvia = value;
            }
        }
    }
}
