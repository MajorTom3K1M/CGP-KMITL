#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace ProjectWiz
{
    class SplashScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        Texture2D backgroundTexture, majoxLogo;

        #endregion

        #region Initialization

        public SplashScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            //TODO: Load content here
            backgroundTexture = content.Load<Texture2D>("rectTexture");
            majoxLogo = content.Load<Texture2D>("majox_logo");
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
            byte fade = TransitionAlpha;

            spriteBatch.Begin();

            //spriteBatch.Draw(backgroundTexture, fullscreen, new Color(fade, fade, fade));
            spriteBatch.Draw(majoxLogo, new Vector2(viewport.Width / 2, viewport.Height / 2), null, new Color(fade, fade, fade), 0f, new Vector2(majoxLogo.Width / 2, majoxLogo.Height / 2), 1f, SpriteEffects.None, 0f);

            spriteBatch.End();
        }


        #endregion
    }
}
