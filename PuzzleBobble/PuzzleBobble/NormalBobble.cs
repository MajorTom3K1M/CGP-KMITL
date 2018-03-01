using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBobble
{
    public class NormalBobble : Bobble
    {

        public NormalBobble(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (bobbleColor)
            {
                case BobbleColor.Blue:
                    spriteBatch.Draw(MainScene.bobble_blue, Position, Color.White);
                    break;
                case BobbleColor.Green:
                    spriteBatch.Draw(MainScene.bobble_green, Position, Color.White);
                    break;
                case BobbleColor.Red:
                    spriteBatch.Draw(MainScene.bobble_red, Position, Color.White);
                    break;
                case BobbleColor.Yellow:
                    spriteBatch.Draw(MainScene.bobble_yellow, Position, Color.White);
                    break;
            }
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            this.IsActive = true;
            base.Reset();
        }
    }
}
