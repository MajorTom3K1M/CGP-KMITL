using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectWizardous
{
    class Level01 : GameScene
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        Texture2D backgroundTexture, majoxLogo;

        #endregion

        #region Variables

        //TODO: Add Variables Here

        List<GameObject> gameObjects = Singleton.Instance.gameObjects;
        int objCount;

        #endregion

        #region Initialization

        public Level01()
        {
            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(SceneManager.Game.Services, "Content");

            //Load Content Here
            backgroundTexture = content.Load<Texture2D>("rectTexture");
            majoxLogo = content.Load<Texture2D>("majox_logo");

        }

        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            Singleton.Instance.CurrentKey = Keyboard.GetState();
            objCount = gameObjects.Count;

            //TODO: Update here


            for (int i = 0; i < objCount; i++)
            {
                if (gameObjects[i].IsActive) gameObjects[i].Update(gameTime, gameObjects);
            }

            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SceneManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            SpriteBatch spriteBatch = SceneManager.SpriteBatch;
            Viewport viewport = SceneManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
            byte fade = TransitionAlpha;

            spriteBatch.Begin();

            //spriteBatch.Draw(majoxLogo, new Vector2(viewport.Width / 2, viewport.Height / 2), null, new Color(fade, fade, fade), 0f, new Vector2(majoxLogo.Width / 2, majoxLogo.Height / 2), 1f, SpriteEffects.None, 0f);

            spriteBatch.End();
        }

        #endregion
    }
}
