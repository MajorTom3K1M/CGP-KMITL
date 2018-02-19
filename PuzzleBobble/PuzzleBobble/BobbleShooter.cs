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
        Point mousePosition;
        MouseState mouseClickedState, previousMouseState, mouseState;
        public float Speed;
        public float Angle;
        public double mouseAngle;
        public BobbleShooter(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            previousMouseState = mouseClickedState;
            mouseState = Mouse.GetState();
            mousePosition = mouseState.Position;
            mouseAngle = MathHelper.ToDegrees((float)Math.Atan2(((Singleton.MAINSCREEN_HEIGHT - 20) - mousePosition.Y), (float)(mousePosition.X - (Singleton.MAINSCREEN_WIDTH / 2))));
            //System.Console.WriteLine(mousePosition.X+" "+mousePosition.Y);
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

            Singleton.Instance.shootAngle = (float)mouseAngle;
            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //float centerx = Singleton.MAINSCREEN_WIDTH / 2;
            //float centery = Singleton.MAINSCREEN_HEIGHT - 20;
            //spriteBatch.Draw(_texture, Position, Color.White);
            //new Vector2((float)(centerx + 1.5 * 50 * (float)Math.Cos(MathHelper.ToRadians((float)mouseAngle))), (float)(centery - 1.5 * 50 * Math.Sin(MathHelper.ToRadians((float)mouseAngle))))
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
