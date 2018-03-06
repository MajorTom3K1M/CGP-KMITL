using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBobble
{
    public class BGMIndicator : Indicator
    {
        public BGMIndicator(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            Singleton.Instance.bgmSound = indicatorIndex * 33;
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
