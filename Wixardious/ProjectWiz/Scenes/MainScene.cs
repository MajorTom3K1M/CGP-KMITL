using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectWiz
{
	public class MainScene : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

        MainScene splash, menu;

        public MainScene()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
            Window.Title = "Wixardous BETA";
		}

		protected override void Initialize()
		{
            //Set Native Size
            graphics.PreferredBackBufferWidth = Singleton.MAINSCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Singleton.MAINSCREEN_HEIGHT;
            graphics.ApplyChanges();

            Singleton.Instance.curScene = Singleton.GameScene.Splash;
            Singleton.Instance.curState = Singleton.GameState.None;
            Singleton.Instance.curPlayerState = Singleton.PlayerStatus.None;

            splash = new SplashScreen();
            menu = new MenuScene();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

            switch (Singleton.Instance.curScene)
            {
                case Singleton.GameScene.Splash: splash.LoadContent(); break;
                case Singleton.GameScene.Menu: menu.LoadContent(); break;
                //case Singleton.GameScene.Option: Splash.UpdateSplash(gameTime); break;
                //case Singleton.GameScene.Game: Splash.UpdateSplash(gameTime); break;
                //case Singleton.GameScene.End: Splash.UpdateSplash(gameTime); break;
            }

		}

		protected override void Update(GameTime gameTime)
		{
			//Scene Update
            switch(Singleton.Instance.curScene){
                case Singleton.GameScene.Splash: splash.Update(gameTime); break;
                case Singleton.GameScene.Menu: menu.Update(gameTime); break;
                //case Singleton.GameScene.Option: Splash.UpdateSplash(gameTime); break;
                //case Singleton.GameScene.Game: Splash.UpdateSplash(gameTime); break;
                //case Singleton.GameScene.End: Splash.UpdateSplash(gameTime); break;
            }

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
            graphics.GraphicsDevice.Clear(Color.Black);

            //Scene Draw
            switch (Singleton.Instance.curScene)
            {
                case Singleton.GameScene.Splash: splash.Draw(gameTime); break;
                case Singleton.GameScene.Menu: menu.Draw(gameTime); break;
                //case Singleton.GameScene.Option: Splash.UpdateSplash(gameTime); break;
                //case Singleton.GameScene.Game: Splash.UpdateSplash(gameTime); break;
                //case Singleton.GameScene.End: Splash.UpdateSplash(gameTime); break;
            }

			base.Draw(gameTime);
		}

        public void WaitForSecond(float second, GameTime gameTime)
        {
            double currentGameTime = gameTime.TotalGameTime.TotalSeconds;
            while (gameTime.TotalGameTime.TotalSeconds - currentGameTime <= second) ;
        }
    }
}
