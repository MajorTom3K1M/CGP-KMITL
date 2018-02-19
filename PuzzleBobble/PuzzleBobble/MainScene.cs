using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBobble
{
	public class MainScene : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

        Texture2D rectTexture;
        Texture2D bobble_red, bobble_green, bobble_blue, bobble_yellow;
        Texture2D shooter;
        Texture2D cave;

        List<GameObject> gameObjects = new List<GameObject>();
        int numObj;

        public MainScene()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = Singleton.MAINSCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Singleton.MAINSCREEN_HEIGHT;
            graphics.ApplyChanges();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

            rectTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            Color[] dataBrick = new Color[1 * 1];
            for (int i = 0; i < dataBrick.Length; ++i) dataBrick[i] = Color.White;
            rectTexture.SetData(dataBrick);

            //Import Sprite for GameScreen BG
            //Reference: https://www.reddit.com/r/PixelArt/comments/61xvdq/ocwipcc_a_parallax_cave_background_i_made/
            cave = this.Content.Load<Texture2D>("cave");

            //Import Sprite of Bobble
            bobble_red = this.Content.Load<Texture2D>("bobble_red");
            bobble_blue = this.Content.Load<Texture2D>("bobble_blue");
            bobble_green = this.Content.Load<Texture2D>("bobble_green");
            bobble_yellow = this.Content.Load<Texture2D>("bobble_yellow");

            //Import Sprite of Shooter
            shooter = this.Content.Load<Texture2D>("arrow");

            switch (Singleton.Instance.currentGameScene)
            {
                case Singleton.GameScene.TitleScene:
                    //TODO: Add Title Graphics, maybe a splash screen

                    break;
                case Singleton.GameScene.MenuScene:
                    //TODO: Add 'New Game' Button

                    //TODO: Add 'Option' Button

                    //TODO: Add 'History' Button

                    //TODO: Add 'Exit' Button

                    break;
                case Singleton.GameScene.OptionScene:
                    //TODO: Add 'BGM' Button/Slider

                    //TODO: Add 'FX' Button/Slider

                    //TODO: Add 'Skip Tutorial' Checkbox

                    break;
                case Singleton.GameScene.HistoryScene:
                    //TODO: Show Game History, like High Score or other Statistical Data

                    break;
                case Singleton.GameScene.GameScene:

                    //Add Shooter
                    gameObjects.Add(
                        new BobbleShooter(shooter)
                        {
                            Name = "Shooter",
                            Position = new Vector2(Singleton.MAINSCREEN_WIDTH / 2, Singleton.MAINSCREEN_HEIGHT - 50)
                        }
                    );

                    //Add First Bobble
                    gameObjects.Add(
                        new Bobble(bobble_green)
                        {
                            Name = "Test",
                            Position = new Vector2(Singleton.MAINSCREEN_WIDTH / 2 - 25, Singleton.MAINSCREEN_HEIGHT - 75),
                            Speed = 500
                        }
                    );


                    //Initial Bobble Pattern
                    for (int i = 0; i < 4; ++i)
                    {
                        for (int j = (i % 2); j < 15; j += 2)
                        {
                            int jOffset = 6 * i;
                            if (i < 2)
                            {
                                if (j < 4)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_red)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - jOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Red
                                        }
                                    );
                                }
                                else if (j < 8)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_yellow)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - jOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Yellow
                                        }
                                    );
                                }
                                else if (j < 12)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_blue)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - jOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Blue
                                        }
                                    );
                                }
                                else
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_green)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - jOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Green
                                        }
                                    );
                                }
                            }
                            //Row 3-4
                            else if (i < 4)
                            {
                                if (j < 3)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_blue)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - jOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Blue
                                        }
                                    );
                                }
                                else if (j < 7)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_green)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - jOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Green
                                        }
                                    );
                                }
                                else if (j < 11)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_red)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - jOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Red
                                        }
                                    );
                                }
                                else
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_yellow)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - jOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Yellow
                                        }
                                    );
                                }
                            }
                        }
                    }
                    switch (Singleton.Instance.currentGameState)
                    {
                        case Singleton.GameSceneState.Tutorial:
                            //TODO: Logic for 'Skip Tutorial'

                            break;
                        case Singleton.GameSceneState.Start:
                            //TODO: Logic on First Move

                            break;
                        case Singleton.GameSceneState.Playing:
                            //TODO: Playing Logic on each update

                            break;
                        case Singleton.GameSceneState.End:
                            switch (Singleton.Instance.currentPlayerStatus)
                            {
                                case Singleton.PlayerStatus.Won:
                                    //TODO: Showing winning graphics
                                    break;
                                case Singleton.PlayerStatus.Lost:
                                    //TODO: Showing losing graphics

                                    break;
                            }
                            //TODO: Showing current score, and submit to statistic data

                            //TODO: Showing high score, in statistic data

                            //TODO: Add 'Play Again' Button

                            //TODO: Add 'Back to Title' Button

                            break;
                    }
                    break;
            }

		}

		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

            numObj = gameObjects.Count;

            for (int i = 0; i < numObj; i++)
            {
                if (gameObjects[i].IsActive) gameObjects[i].Update(gameTime, gameObjects);
            }

			// TODO: Add your update logic here
            switch(Singleton.Instance.currentGameScene){
                case Singleton.GameScene.TitleScene:
                    //TODO: Add Title Graphics, maybe a splash screen

                    //TODO: Sleep for 5 second

                    //Change Scene
                    Singleton.Instance.currentGameScene = Singleton.GameScene.MenuScene;

                    break;
                case Singleton.GameScene.MenuScene:
                    //TODO: Add 'New Game' button logic, move to GameScene
                    //TODO:Adding some animation for represent the scene transistion
                    Singleton.Instance.currentGameScene = Singleton.GameScene.GameScene;

                    //TODO: Add 'Option' Button

                    //TODO: Add 'History' Button

                    //TODO: Add 'Exit' Button

                    break;
                case Singleton.GameScene.OptionScene:
                    //TODO: Add 'BGM' Button/Slider

                    //TODO: Add 'FX' Button/Slider

                    //TODO: Add 'Skip Tutorial' Checkbox

                    break;
                case Singleton.GameScene.HistoryScene:
                    //TODO: Show Game History, like High Score or other Statistical Data

                    break;
                case Singleton.GameScene.GameScene:
                    switch(Singleton.Instance.currentGameState){
                        case Singleton.GameSceneState.Tutorial:
                            //TODO: Logic for 'Skip Tutorial'

                            break;
                        case Singleton.GameSceneState.Start:
                            //TODO: Logic on First Move

                            break;
                        case Singleton.GameSceneState.Playing:
                            //TODO: Playing Logic on each update

                            break;
                        case Singleton.GameSceneState.End:
                            switch(Singleton.Instance.currentPlayerStatus){
                                case Singleton.PlayerStatus.Won:
                                    //TODO: Showing winning graphics
                                    break;
                                case Singleton.PlayerStatus.Lost:
                                    //TODO: Showing losing graphics

                                    break;
                            }
                            //TODO: Showing current score, and submit to statistic data

                            //TODO: Showing high score, in statistic data

                            //TODO: Add 'Play Again' Button

                            //TODO: Add 'Back to Title' Button

                            break;
                    }
                    break;
            }

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);

            numObj = gameObjects.Count;
             
            //TODO: Add your drawing code here
            spriteBatch.Begin();

            //Draw GameScreen
            spriteBatch.Draw(cave, new Vector2((Singleton.MAINSCREEN_WIDTH - Singleton.GAMESCREEN_WIDTH) / 2, 0f), Color.White);

            //Draw GameObject
            for (int i = 0; i < numObj; i++)
            {
                if (gameObjects[i].IsActive) gameObjects[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            graphics.BeginDraw();

			base.Draw(gameTime);
		}

        protected void Reset()
        {
            //Reset all GameObject
            foreach (GameObject obj in gameObjects)
            {
                obj.Reset();
            }
        }
	}
}
