using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PrimerJuego
{
    class Boton : Sprite
    {
        MouseState prevMouseState;
        Boolean pulsado = false;
        String opcion = "";

        public Boton(
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
        public Boton(Texture2D textureImage, Vector2 position, Point frameSize,
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
            MouseState currMouseState = Mouse.GetState();
            if (currMouseState.X >= position.X && currMouseState.X <= (position.X + frameSize.X) && currMouseState.Y >= position.Y && currMouseState.Y <= (position.Y + frameSize.Y))
                pulsado = true;
            else
                pulsado = false;

            base.Update(gameTime, clientBounds);

        }

        public Boolean getPulsado
        {
            get
            {
                return pulsado;
            }
            set
            {
                pulsado = value;
            }
        }

        public Vector2 getPosicionBotones
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
        
        public String getOpcion
        {
            get
            {
                return opcion;
            }
            set
            {
                opcion = value;
            }
        }
    }
}
