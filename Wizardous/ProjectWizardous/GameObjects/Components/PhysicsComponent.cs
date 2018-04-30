using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace ProjectWizardous
{
    public class PhysicsComponent
    {
        public PhysicsComponent(Game currentScene)
        {
        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
            parent.Velocity += parent.Acceleration * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            parent.Position += parent.Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
        }

        public virtual void Reset() { }

        public virtual void ReceiveMessage(int message) { }

        #region Collision
        public bool IsTouching(GameObject g, GameObject parent)
        {
            return IsTouchingLeft(g, parent) ||
                IsTouchingTop(g, parent) ||
                IsTouchingRight(g, parent) ||
                IsTouchingBottom(g, parent);
        }

        public bool IsTouchingLeft(GameObject g, GameObject parent)
        {
            return parent.Rectangle.Right > g.Rectangle.Left &&
                         parent.Rectangle.Left < g.Rectangle.Left &&
                         parent.Rectangle.Bottom > g.Rectangle.Top &&
                         parent.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingRight(GameObject g, GameObject parent)
        {
            return parent.Rectangle.Right > g.Rectangle.Right &&
                         parent.Rectangle.Left < g.Rectangle.Right &&
                         parent.Rectangle.Bottom > g.Rectangle.Top &&
                         parent.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingTop(GameObject g, GameObject parent)
        {
            return parent.Rectangle.Right > g.Rectangle.Left &&
                         parent.Rectangle.Left < g.Rectangle.Right &&
                         parent.Rectangle.Bottom > g.Rectangle.Top &&
                         parent.Rectangle.Top < g.Rectangle.Top;
        }

        public bool IsTouchingBottom(GameObject g, GameObject parent)
        {
            return parent.Rectangle.Right > g.Rectangle.Left &&
                         parent.Rectangle.Left < g.Rectangle.Right &&
                         parent.Rectangle.Bottom > g.Rectangle.Bottom &&
                         parent.Rectangle.Top < g.Rectangle.Bottom;
        }
        #endregion
    }
}
