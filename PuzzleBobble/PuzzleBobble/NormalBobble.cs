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
    public class NormalBobble : Bobble
    {

        MouseState mouseState, previousMouseState;
        public float Speed;
        public float Angle;
        public bool isNeverShoot = true;
        //public int radius = 25;
        public enum BobbleColor { Red, Green, Blue, Yellow }
        public BobbleColor bobbleColor;

        public NormalBobble(Texture2D texture) : base(texture)
        {
            isNeverShoot = false;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            foreach (GameObject g in gameObjects)
            {
                if (!g.Equals(this) && g.Name.Equals("NormalBobble") && this.circleCollide(g) && isNeverShoot)
                {
                    this.Speed = 0;
                    Console.WriteLine(Position.X + " " + Position.Y);
                    isNeverShoot = false;
                    int j = (int)Math.Round(Position.Y / 44) % 2;
                    int xGrid = (int)(Math.Round((Position.X / 50))) * 50;
                    int yGrid = (int)Math.Round(Position.Y / 44);
                    Position = new Vector2(j * (Singleton.BOBBLE_SIZE / 2) + xGrid, yGrid * 44);
                    /*if (this.Position.X > g.Position.X) {

                    }*/
                    if (this.Position == g.Position)
                    {
                        xGrid -= 50;
                        Position = new Vector2(j * (Singleton.BOBBLE_SIZE / 2) + xGrid, yGrid * 44);
                    }
                    Console.WriteLine(Position.X + " " + Position.Y);


                }
            }

            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            Velocity.X = (float)Math.Cos(MathHelper.ToRadians(Angle)) * Speed;
            Velocity.Y = -1 * (float)Math.Sin(MathHelper.ToRadians(Angle)) * Speed;
            Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            if (Position.X <= 200)
            {
                Angle = 180 - Angle;

            }

            if (Position.X >= 550)
            {
                Angle = 180 - Angle;
            }

            if (Position.Y < 0)
            {
                Speed = 0;
                //System.Console.WriteLine(Position.X + " " + Position.Y + " ");
            }

            for (int i = 0; i < 4; ++i)
            {
                for (int j = (i % 2); j < 15; j += 2)
                {
                    int jOffset = 6 * i;
                    //if () { }
                }
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
