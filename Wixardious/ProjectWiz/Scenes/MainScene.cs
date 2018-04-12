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

        public MainScene()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
            this.Window.Title = "Wixardous BETA";
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

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//TODO: use this.Content to load your game content here 
		}

		protected override void Update(GameTime gameTime)
		{
			//Scene Update
            switch(Singleton.Instance.curScene){
                case Singleton.GameScene.Splash: Splash.UpdateSplash(gameTime); break;
                case Singleton.GameScene.Menu: Splash.UpdateSplash(gameTime); break;
                case Singleton.GameScene.Option: Splash.UpdateSplash(gameTime); break;
                case Singleton.GameScene.Game: Splash.UpdateSplash(gameTime); break;
                case Singleton.GameScene.End: Splash.UpdateSplash(gameTime); break;
            }

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            //Scene Draw
            switch (Singleton.Instance.curScene)
            {
                case Singleton.GameScene.Splash: Splash.DrawSplash(gameTime); break;
                case Singleton.GameScene.Menu: Splash.UpdateSplash(gameTime); break;
                case Singleton.GameScene.Option: Splash.UpdateSplash(gameTime); break;
                case Singleton.GameScene.Game: Splash.UpdateSplash(gameTime); break;
                case Singleton.GameScene.End: Splash.UpdateSplash(gameTime); break;
            }

			base.Draw(gameTime);
		}
	}
}
