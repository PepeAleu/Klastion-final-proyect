using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PrimerJuego 
{
    class StaticSprite : Sprite
    {
        MouseState prevMouseState;
        public StaticSprite(
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
        public StaticSprite(Texture2D textureImage, Vector2 position, Point frameSize,
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
            if (currMouseState.X != prevMouseState.X ||
            currMouseState.Y != prevMouseState.Y)
            {
                position = new Vector2(currMouseState.X, currMouseState.Y);
            }
            prevMouseState = currMouseState;

            base.Update(gameTime, clientBounds);

        }
    }
}
