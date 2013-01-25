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
    class UserControlledSprite : Sprite
    {
        MouseState prevMouseState;
        enum WalkState { Arriba, Derecha, Abajo, Izquierda };
        WalkState currentWalkState = WalkState.Arriba;
        enum ActionState { Quieto, Andando, Disparando };
        ActionState currentActionState = ActionState.Quieto;
        Boolean bloqueo = false;
        Boolean saltando = false;
        Boolean herido;
        Boolean muerto;
        float velocidad = 0;
        float aceleracion = 0;
        Boolean fin = false;
        /*========================================
* ========== CONSTRUCTOR 1 ==============
* =====================================*/
        public UserControlledSprite(
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
        public UserControlledSprite(
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
        public UserControlledSprite(
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
        public UserControlledSprite(
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
                Vector2 inputDirection = Vector2.Zero;
                MouseState currMouseState = Mouse.GetState();
                
                if (!muerto && !fin)
                {
                    //if (Keyboard.GetState().IsKeyDown(Keys.W))
                    //    inputDirection.Y -= 1;

                    if ((Keyboard.GetState().IsKeyDown(Keys.D) && currentWalkState.Equals(WalkState.Izquierda)) || (Keyboard.GetState().IsKeyDown(Keys.A) && currentWalkState.Equals(WalkState.Derecha)))
                        speed.X = 0;
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {

                        if (currentActionState.Equals(ActionState.Quieto))
                        {
                            if (currMouseState.LeftButton.Equals(ButtonState.Released))
                            {
                                Desde = new Point(0, 1);
                                Hasta = new Point(4, 1);
                            }
                            getEfecto = SpriteEffects.FlipHorizontally;
                            currentActionState = ActionState.Andando;
                            currentWalkState = WalkState.Derecha;
                        }

                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.D) && currentWalkState.Equals(WalkState.Derecha))
                    {
                        if (currentActionState.Equals(ActionState.Andando) && currMouseState.LeftButton.Equals(ButtonState.Released))
                        {
                            Desde = new Point(0, 0);
                            Hasta = new Point(4, 0);
                        }
                        currentActionState = ActionState.Quieto;
                    }



                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {

                        if (currentActionState.Equals(ActionState.Quieto))
                        {
                            if (currMouseState.LeftButton.Equals(ButtonState.Released))
                            {
                                Desde = new Point(0, 1);
                                Hasta = new Point(4, 1);
                            }
                            getEfecto = SpriteEffects.None;
                            currentActionState = ActionState.Andando;
                            currentWalkState = WalkState.Izquierda;
                        }

                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.A) && currentWalkState.Equals(WalkState.Izquierda))
                    {
                        if (currentActionState.Equals(ActionState.Andando) && currMouseState.LeftButton.Equals(ButtonState.Released))
                        {
                            Desde = new Point(0, 0);
                            Hasta = new Point(4, 0);
                        }
                        currentActionState = ActionState.Quieto;
                    }

                    if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                    {
                        if (currentActionState.Equals(ActionState.Andando) && currMouseState.LeftButton.Equals(ButtonState.Released))
                        {
                            Desde = new Point(0, 0);
                            Hasta = new Point(4, 0);
                            currentActionState = ActionState.Quieto;
                        }

                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        velocidad = 12f;
                        aceleracion = 0.3f;
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.LeftShift))
                    {
                        velocidad = 7f;
                        aceleracion = 0.1f;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        

                        if (speed.X <= velocidad)
                            speed.X += aceleracion;
                    }

                    if (saltando == false && currentActionState == ActionState.Quieto)
                    {

                        if (speed.X > 0f)
                            speed.X -= aceleracion;
                        else
                            speed.X = 0;
                    }

                    

                    if (currentWalkState.Equals(WalkState.Izquierda))
                    {
                        inputDirection.X = -1;
                    }
                    else if (currentWalkState.Equals(WalkState.Derecha))
                    {
                        inputDirection.X = 1;
                    }
                    //if (Keyboard.GetState().IsKeyDown(Keys.W))
                    //{
                    //    inputDirection.Y -= 1;
                    //    if (currentActionState.Equals(ActionState.Quieto))
                    //    {
                    //        Desde = new Point(0, 18);
                    //        Hasta = new Point(8, 18);
                    //        currentActionState = ActionState.Andando;
                    //        currentWalkState = WalkState.Arriba;
                    //    }

                    //}
                    //if (Keyboard.GetState().IsKeyUp(Keys.W) && currentWalkState.Equals(WalkState.Arriba))
                    //{
                    //    Hasta = new Point(0, 18);
                    //    currentActionState = ActionState.Quieto;
                    //}

                    //if (Keyboard.GetState().IsKeyDown(Keys.S))
                    //{
                    //    inputDirection.Y += 1;
                    //    if (currentActionState.Equals(ActionState.Quieto))
                    //    {
                    //        Desde = new Point(0, 0);
                    //        Hasta = new Point(8, 0);
                    //        currentActionState = ActionState.Andando;
                    //        currentWalkState = WalkState.Abajo;
                    //    }

                    //}
                    //if (Keyboard.GetState().IsKeyUp(Keys.S) && currentWalkState.Equals(WalkState.Abajo))
                    //{
                    //    Hasta = new Point(0, 0);
                    //    currentActionState = ActionState.Quieto;
                    //}
                }
                    return inputDirection * speed;
            }
        }


        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            
            if(position.Y < 1366)
            position += direction;
            
            MouseState currMouseState = Mouse.GetState();
            angulo = (float)Math.Atan2(currMouseState.Y - position.Y, currMouseState.X - position.X);
            
            prevMouseState = currMouseState;
           
            if (position.X < 0)
                position.X = 0;

            if (position.X > clientBounds.Width - frameSize.X)
            position.X = (clientBounds.Width - frameSize.X);


            base.Update(gameTime, clientBounds);
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


        public Boolean getSaltando
        {
            get
            {
                return saltando;
            }
            set
            {
                saltando = value;
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

        public float getRotacion
        {
            get
            {
                return rotacion;
            }
        }
        public Boolean Bloqueo
        {
            get
            {
                return bloqueo;
            }
            set
            {
                bloqueo = value;
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

        public Boolean getFin
        {
            get
            {
                return fin;
            }
            set
            {
                fin = value;
            }
        }

        public float getAngulo
        {
            get
            {
                return angulo;
            }  
        }

        public Vector2 getPersonajeVelocidad
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
    }
}
