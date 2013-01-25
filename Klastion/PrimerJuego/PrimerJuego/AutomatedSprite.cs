using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PrimerJuego
{
    class AutomatedSprite : Sprite
    {
        Vector2 angulo;

        //PRIMER CONSTRUCTOR
        public AutomatedSprite(
            Texture2D textureImage, 
            Vector2 position, 
            Point frameSize,
            Point collisionOffset, 
            Point currentFrame, //Desde
            Point sheetSize,    //Hasta
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
            rotacion
            )
        { }
        //SEGUNDO CONSTRUCTOR
        public AutomatedSprite(
            Texture2D textureImage, 
            Vector2 position, 
            Point frameSize,
            Point collisionOffset, 
            Point currentFrame, 
            Point sheetSize, 
            Vector2 speed, 
            float rotacion,
            int millisecondsPerFrame,
            float capa
            )
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
            capa
            )
        { }

        //------------------------------------------------------------------------------------------------------
        //---------------------------------------------------GET/SET--------------------------------------------
        //------------------------------------------------------------------------------------------------------



        public override Vector2 direction
        {
            get { return speed; }
        }

        public float PosicionX
        {
            set
            {
                position.X = value;
            }
            get
            {
                return position.X;
            }
        }

        public float PosicionY
        {
            set
            {
                position.Y = value;
            }
            get
            {
                return position.Y;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;
            base.Update(gameTime, clientBounds);
        }

        public Vector2 BichoPosition
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

        public Vector2 BichoAngulo
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
    }
}
