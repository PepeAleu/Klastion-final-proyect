using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PrimerJuego
{
    class Mapa : Sprite
    {
         MouseState prevMouseState;
        public Mapa(
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
        public Mapa(Texture2D textureImage, Vector2 position, Point frameSize,
        Point collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, float rotacion,
        int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed,rotacion,  millisecondsPerFrame)
        { }
        public Mapa(Texture2D textureImage, Vector2 position, Point frameSize,
        Point collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, float rotacion,
        int millisecondsPerFrame,float capa)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed,rotacion,  millisecondsPerFrame, capa)
        { }

        public override Vector2 direction
        {
            get
            {
            Vector2 inputDirection = Vector2.Zero;
            inputDirection.X -= 1;
            return inputDirection * speed; 
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {

            position += direction;
            base.Update(gameTime, clientBounds);
        }

        public Vector2 getMapaPosition
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

        public Point getMapaFrameSize
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

        public Vector2 getMapaVelocidad
        {
            set
            {
                speed = value;
            }
            get
            {
                return speed;
            }
        }

        public Rectangle collisionRectTerreno
        {
            get
            {
                return new Rectangle(
                (int)position.X + GetCollisionOffSet.X,
                (int)position.Y + GetCollisionOffSet.Y,
                frameSize.X-(GetCollisionOffSet.X*2),
                60);
            }
        }
        
    }
}
