using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBobble
{
    public class Ceiling : GameObject
    {
        public Ceiling(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            this.IsActive = true;
            base.Reset();
        }
    }
}
