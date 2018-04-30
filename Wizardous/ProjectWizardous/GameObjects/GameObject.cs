using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace ProjectWizardous
{
    public class GameObject : ICloneable
    {
        #region PUBLIC_VARIABLES

        public Dictionary<string, SoundEffectInstance> SoundEffects;

        //Transformation
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;

        //Basic Physics
        public Vector2 Velocity;
        public Vector2 Acceleration;

        public string Name;
        public bool IsActive;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Viewport.Width, Viewport.Height);
            }
        }

        public Rectangle Viewport;

        #endregion

        #region PROTECTED_VARIABLES

        protected Texture2D _texture;

        protected InputComponent _input;
        protected PhysicsComponent _physics;
        protected GraphicsComponent _graphics;

        #endregion

        public GameObject(InputComponent input, PhysicsComponent physics, GraphicsComponent graphics)
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
            Rotation = 0f;
            IsActive = true;

            _input = input;
            _physics = physics;
            _graphics = graphics;
        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            if (_input != null) _input.Update(gameTime, gameObjects, this);
            if (_physics != null) _physics.Update(gameTime, gameObjects, this);
            if (_graphics != null) _graphics.Update(gameTime, gameObjects, this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_graphics != null) _graphics.Draw(spriteBatch, this);
        }

        public virtual void Reset()
        {
            if (_input != null) _input.Reset();
            if (_physics != null) _physics.Reset();
            if (_graphics != null) _graphics.Reset();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void SendMessage(int message)
        {
            if (_input != null) _input.ReceiveMessage(message);
            if (_physics != null) _physics.ReceiveMessage(message);
            if (_graphics != null) _graphics.ReceiveMessage(message);
        }
    }
}
