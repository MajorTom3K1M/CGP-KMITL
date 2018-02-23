using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBobble
{
    public class NormalBobble : Bobble
    {

        MouseState mouseState, previousMouseState;
        public float Speed;
        public float Angle;
        public bool isNeverShoot = true;
        public bool areBobbleSelected = false;
        public enum BobbleColor { Red, Green, Blue, Yellow }

        public NormalBobble(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            int s = gameObjects.Count;
            GameObject a = gameObjects[s - 2];
            int j = (int)Math.Round(Position.Y / 44) % 2;
            float yGrid = (float)Math.Round(Position.Y / 44) * 44;
            float xGrid = j * (Singleton.BOBBLE_SIZE / 2) + (float)Math.Round(Position.X / 50) * 50;
            foreach (GameObject g in gameObjects)
            {

                if (!g.Equals(this) && g.Name.Equals("NormalBobble") && this.circleCollide(g) && isNeverShoot && g.IsActive)
                {
                    this.Speed = 0;
                    isNeverShoot = false;
                    if (j == 1)
                    {
                        xGrid = j * (Singleton.BOBBLE_SIZE / 2) + (float)(Math.Floor((Position.X) / 50)) * 50;
                    }
                    Position = new Vector2(xGrid, yGrid);

                    if (Position.Equals(g.Position))
                    {
                        xGrid -= 50;
                        Position = new Vector2(xGrid, yGrid);
                    }
                }
            }

            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            Velocity.X = (float)Math.Cos(MathHelper.ToRadians(Angle)) * Speed;
            Velocity.Y = -1 * (float)Math.Sin(MathHelper.ToRadians(Angle)) * Speed;
            Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            if (Position.X <= 200) Angle = 180 - Angle;
            if (Position.X >= 550) Angle = 180 - Angle;

            if (Position.Y < 0)
            {
                Speed = 0;
                xGrid = (float)Math.Round(Position.X / 50) * 50;
                yGrid = (float)Math.Round(Position.Y / 44);
                Position = new Vector2(j * (Singleton.BOBBLE_SIZE / 2) + xGrid, yGrid * 44);
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
