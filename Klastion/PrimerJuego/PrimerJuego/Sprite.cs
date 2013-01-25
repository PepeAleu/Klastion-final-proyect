using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PrimerJuego
{
    abstract class Sprite
    {
        protected Texture2D textureImage;
        protected Vector2 position;
        protected Point frameSize;
        Point collisionOffset;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        protected Vector2 speed;
        const int defaultMillisecondsPerFrame = 16;
        public float rotacion;
        public float angulo;
        float capa = 0f;
        int vida;
        SpriteEffects efecto = SpriteEffects.None;
        int tamAura = 0;
        float escala = 1f;
        
        /*========================================
        * ========== CONSTRUCTOR 1 ==============
        * =====================================*/
        public Sprite(
        Texture2D textureImage,
        Vector2 position,
        Point frameSize,
        Point collisionOffset,
        Point currentFrame,
        Point sheetSize,
        Vector2 speed,
            float rotacion)
            : this(
                textureImage,
                position,
                frameSize,
                collisionOffset,
                currentFrame,
                sheetSize,
                speed,
                rotacion,
                defaultMillisecondsPerFrame
                ) { }

        /*========================================
        * ========== CONSTRUCTOR 2 ==============
        * =====================================*/
        public Sprite(
        Texture2D textureImage,
        Vector2 position,
        Point frameSize,
        Point collisionOffset,
        Point currentFrame,
        Point sheetSize,
        Vector2 speed,
        float rotacion,
        int millisecondsPerFrame
        )
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.rotacion = rotacion;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        /*========================================
        * ========== CONSTRUCTOR 3 ==============
        * =====================================*/
        public Sprite(
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
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.rotacion = rotacion;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.capa = capa;
        }

        /*========================================
        * ========== CONSTRUCTOR 4 ==============
        * =====================================*/
        public Sprite(
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
        int vida
        )
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.rotacion = rotacion;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.capa = capa;
            this.vida = vida;
        }

        /*========================================
        * ========== CONSTRUCTOR 5 ==============
        * =====================================*/
        public Sprite(
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
        int vida,
        SpriteEffects efecto
        )
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.rotacion = rotacion;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.capa = capa;
            this.vida = vida;
            this.efecto = efecto;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            Point inicialFrame = new Point(currentFrame.X, currentFrame.Y);

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = inicialFrame.Y;

                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
            textureImage,
            position,
            new Rectangle(
            currentFrame.X * frameSize.X,
            currentFrame.Y * frameSize.Y,
            frameSize.X,
            frameSize.Y
            ),
            Color.White,
            rotacion,
            Vector2.Zero,
            escala,
            efecto,
            capa);
        }
        public virtual void Draw2(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
            textureImage,
            position,
            new Rectangle(
            currentFrame.X,
            currentFrame.Y,
            frameSize.X,
            frameSize.Y
            ),
            Color.White,
            0,
            Vector2.Zero,
            1f,
            efecto,
            capa);
        }


        public abstract Vector2 direction
        {
            get;
        }

        public int getMilisegundosPorFrame
        {
            get
            {
                return millisecondsPerFrame;
            }
            set
            {
                millisecondsPerFrame = value;
            }
        }

        public Point Hasta
        {
            get
            {
                return sheetSize;
            }
            set
            {
                sheetSize = value;
            }
        }

        public Point Desde
        {
            get
            {
                return currentFrame;
            }
            set
            {
                currentFrame = value;
            }
        }

        public int getVida
        {
            get
            {
                return vida;
            }
            set
            {
                vida = value;
            }
        }

        public SpriteEffects getEfecto
        {
            get
            {
                return efecto;
            }
            set
            {
                efecto = value;
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

        public float getCapa
        {
            get
            {
                return capa;
            }
            set
            {
                capa = value;
            }
        }

        public float getEscala
        {
            get
            {
                return escala;
            }
            set
            {
                escala = value;
            }
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                (int)position.X + collisionOffset.X,
                (int)position.Y + collisionOffset.Y,
                frameSize.X  - (collisionOffset.X * 2),
                frameSize.Y  - (collisionOffset.Y * 2));
            }
        }

        public Rectangle collisionRectPies
        {
            get
            {
                return new Rectangle(
                (int)(position.X + (frameSize.X/2))-1,
                (int)((position.Y + frameSize.Y)- (collisionOffset.Y * 2))-1,
                2,
                1);
            }
        }

        public Rectangle collisionRectDerecha
        {
            get
            {
                return new Rectangle(
                (int)((position.X + frameSize.X) - collisionOffset.X)-44,
                (int)position.Y + (frameSize.Y/2)-5,
                4,
                10);
            }
        }

        public Rectangle collisionRectIzquierda
        {
            get
            {
                return new Rectangle(
                (int)position.X + collisionOffset.X+40,
                (int)position.Y + (frameSize.Y / 2) - 5,
                12,
                10);
            }
        }

        public Rectangle collisionRectArriba
        {
            get
            {
                return new Rectangle(
                (int)position.X + (frameSize.X/2) -1,
                (int)position.Y+collisionOffset.Y,
                2,
                4);
            }
        }

        public Rectangle collisionRectAura
        {
            get
            {
                return new Rectangle(
                (int)position.X - tamAura,
                (int)position.Y - tamAura,
                frameSize.X + (tamAura*2),
                frameSize.Y + (tamAura * 2));
            }
        }

        public Rectangle collisionRectCaida
        {
            get
            {
                return new Rectangle(
                (int)position.X + collisionOffset.X,
                (int)((position.Y + frameSize.Y) - (collisionOffset.Y * 2)) ,
                frameSize.X - (collisionOffset.X * 2),
                frameSize.Y );
            }
        }

        public int setTamAura
        {
            set
            {
                tamAura = value;
            }
        }

        public Point GetCollisionOffSet
        {
            get
            {
                return collisionOffset;
            }
            set
            {
                collisionOffset = value;
            }
        }

        

        public bool EstaFuera(Rectangle clientRect)
        {
            if (position.X < 0-frameSize.X ||
            position.X > clientRect.Width ||
            position.Y < 0-frameSize.Y ||
            position.Y > clientRect.Height)
            {
                return true;
            }
            return false;
        }

    }
}
