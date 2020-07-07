#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace ProjectWizardous
{
    public class MainGame : Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        SceneManager sceneManager;

        #endregion

        #region Initialization

        public MainGame()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = Singleton.MAINSCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Singleton.MAINSCREEN_HEIGHT;

            // Create the screen manager component.
            sceneManager = new SceneManager(this);

            Components.Add(sceneManager);

            //TODO: Add new screen here
            //sceneManager.AddScreen(new SplashScreen());
            sceneManager.AddScreen(new SplashScreen());
        }

        #endregion

        #region Draw

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        #endregion
    }
}
