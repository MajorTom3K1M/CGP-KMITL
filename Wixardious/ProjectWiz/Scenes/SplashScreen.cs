using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectWiz
{
    public class SplashScreen : MainScene
	{
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Content

        Texture2D teamLogo;

        #endregion

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            teamLogo = base.Content.Load<Texture2D>("majox_logo");
        }

        protected override void Update(GameTime gameTime)
        {

        }

        protected override void Draw(GameTime gameTime)
        {

        }
	}
}
