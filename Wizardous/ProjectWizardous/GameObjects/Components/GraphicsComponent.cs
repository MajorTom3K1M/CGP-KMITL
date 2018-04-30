using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectWizardous
{
    public class GraphicsComponent
    {
        protected Dictionary<string, Animation> _animations;
        protected AnimationManager _animationManager;
        protected Texture2D _texture;

        public GraphicsComponent(Game currentScene)
        {
        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
            if (_animationManager != null)
            {
                _animationManager.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            if (_animationManager == null)
            {
                spriteBatch.Draw(_texture,
                parent.Position,
                parent.Viewport,
                Color.White,
                parent.Rotation,
                parent.Viewport.Center.ToVector2(),
                parent.Scale,
                SpriteEffects.None,
                0);
            }
            else
            {
                _animationManager.Draw(spriteBatch, parent.Position, parent.Rotation, parent.Scale);
            }
        }

        public virtual void Reset()
        {
            if (_animations != null)
            {
                _animationManager = new AnimationManager(_animations.First().Value);
            }
        }

        public virtual void ReceiveMessage(int message) { }
    }
}
