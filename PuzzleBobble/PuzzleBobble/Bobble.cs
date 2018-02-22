using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PuzzleBobble;

namespace PuzzleBobble
{
    public class Bobble : GameObject
    {
        public float Speed;
        public float Angle;
        public float dt;
        MouseState mouseState, previousMouseState;
        double tileWidth;
        double tileHeight;

        public Bobble(Texture2D texture) : base(texture)
        {
            tileWidth = texture.Width;
            tileHeight = texture.Height;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            Velocity.X = dt * (float)Math.Cos(MathHelper.ToRadians(Angle)) * Speed;
            Velocity.Y = dt * -1 * (float)Math.Sin(MathHelper.ToRadians(Angle)) * Speed;
            Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && Singleton.Instance.currentGameState == Singleton.GameSceneState.Playing)
            {
                Angle = (float)Singleton.Instance.shootAngle;
                dt = 1;
            }
            if (Position.X <= 200)
            {
                Angle = 180 - Angle;

            }

            if (Position.X >= 550)
            {
                Angle = 180 - Angle;

            }
            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);

            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            this.IsActive = true;
            base.Reset();
        }
    }
}
