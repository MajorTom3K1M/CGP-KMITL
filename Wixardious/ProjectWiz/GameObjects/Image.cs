using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectWiz.Models;

namespace ProjectWiz
{
    public class Image : GameObject
    {
        bool isCentered;

        public Image(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(isCentered)
            {
                spriteBatch.Draw(_texture, Position, null, Color.White, 0f, new Vector2(_texture.Width / 2, _texture.Height / 2), 1f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(_texture, Position, Viewport, Color.White);
            }

            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
