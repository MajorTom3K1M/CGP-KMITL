using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBobble
{
    public class BobbleShooter : GameObject
    {

        public NormalBobble normalBobble;
        Point mousePosition;
        MouseState mouseClickedState, previousMouseState, mouseState;
        public float Speed;
        public float Angle;
        public double mouseAngle;

        NormalBobble bobble;

        private float timer;

        enum shooterState
        {
            shooterReload,
            shooterReady
        }

        private shooterState currentShooterState;

        public BobbleShooter(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            previousMouseState = mouseClickedState;
            mouseState = Mouse.GetState();
            mousePosition = mouseState.Position;
            mouseAngle = MathHelper.ToDegrees((float)Math.Atan2(((Singleton.MAINSCREEN_HEIGHT - 20) - mousePosition.Y), (float)(mousePosition.X - (Singleton.MAINSCREEN_WIDTH / 2))));

            if (mouseAngle < 0)
            {
                mouseAngle = 180 + (180 + mouseAngle);
            }
            var lbound = 8;
            var ubound = 172;
            if (mouseAngle > 90 && mouseAngle < 270)
            {
                // Left
                if (mouseAngle > ubound)
                {
                    mouseAngle = ubound;
                }
            }
            else
            {
                // Right
                if (mouseAngle < lbound || mouseAngle >= 270)
                {
                    mouseAngle = lbound;
                }
            }
            switch (currentShooterState)
            {
                case shooterState.shooterReload:
                    //TODO: Reload when the latest bobble is collided

                    timer += (float) gameTime.ElapsedGameTime.TotalSeconds;
                    if (timer > 1)
                    {
                        bobble = normalBobble.Clone() as NormalBobble;
                        bobble.Name = "NormalBobble";
                        bobble.Position = new Vector2(Singleton.MAINSCREEN_WIDTH / 2 - 25, Singleton.MAINSCREEN_HEIGHT - 75);
                        gameObjects.Add(bobble);
                        timer = 0;
                        currentShooterState = shooterState.shooterReady;
                    }
                    break;
                case shooterState.shooterReady:


                    previousMouseState = mouseClickedState;
                    mouseClickedState = Mouse.GetState();

                    Velocity.X = (float)Math.Cos(MathHelper.ToRadians(Angle)) * Speed;
                    Velocity.Y = -1 * (float)Math.Sin(MathHelper.ToRadians(Angle)) * Speed;
                    Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

                    if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    {
                        bobble.Angle = (float) mouseAngle;
                        bobble.Speed = 700;
                        currentShooterState = shooterState.shooterReload;
                    }

                    break;
            }
            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.Yellow, (float)MathHelper.ToRadians((float)-(mouseAngle + 90)), new Vector2(5, 20), 1f, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            this.IsActive = true;
            base.Reset();
        }
    }
}
