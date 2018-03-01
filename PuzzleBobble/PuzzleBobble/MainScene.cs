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

        public static Texture2D bobble_red, bobble_green, bobble_blue, bobble_yellow;
        Texture2D rectTexture;

        Texture2D shooter, loseCollider;

        Texture2D cave, splashScreen;

        Texture2D menuBG, menuParallax, menuTitle;
        Texture2D buttonNew, buttonOption, buttonExtras, buttonExit;

        List<GameObject> gameObjects;
        int numObj;

        Queue<GameObject> q = new Queue<GameObject>();

        Random rnd = new Random();

        //Splash Screen Waiting
        double currentGameTime;
        bool isFirstTime;

        //Menu Screen Parallax
        int parallaxHelper;
        bool isAscend;

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

            Singleton.Instance.currentGameScene = Singleton.GameScene.TitleScene;
            Singleton.Instance.currentGameState = Singleton.GameSceneState.None;


            //Splash Screen
            isFirstTime = true;

            parallaxHelper = 0;
            isAscend = true;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
            gameObjects = new List<GameObject>();
            rectTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);

            Color[] dataRect = new Color[1 * 1];
            for (int i = 0; i < dataRect.Length; ++i) dataRect[i] = Color.White;
            rectTexture.SetData(dataRect);

            //Splash Screen
            splashScreen = this.Content.Load<Texture2D>("splashScreen");

            //Menu Screen
            menuBG = this.Content.Load<Texture2D>("menu_bg");
            menuTitle = this.Content.Load<Texture2D>("menu_title");
            menuParallax = this.Content.Load<Texture2D>("menu_parallax");

            //Import Option Sprite
            buttonNew = this.Content.Load<Texture2D>("button_new");
            buttonOption = this.Content.Load<Texture2D>("button_option");
            buttonExtras = this.Content.Load<Texture2D>("button_ext");
            buttonExit = this.Content.Load<Texture2D>("button_exit");

            //Import Sprite for GameScreen BG
            //Reference: https://www.reddit.com/r/PixelArt/comments/61xvdq/ocwipcc_a_parallax_cave_background_i_made/
            cave = this.Content.Load<Texture2D>("cave");

            //Import Sprite of Bobble
            bobble_red = this.Content.Load<Texture2D>("bobble_red");
            bobble_blue = this.Content.Load<Texture2D>("bobble_blue");
            bobble_green = this.Content.Load<Texture2D>("bobble_green");
            bobble_yellow = this.Content.Load<Texture2D>("bobble_yellow");

            //Import GameFont
            Singleton.Instance.gameFont = this.Content.Load<SpriteFont>("GameFont");

            //Import Sprite of Shooter
            shooter = this.Content.Load<Texture2D>("arrow");
            loseCollider = this.Content.Load<Texture2D>("lose_collider");

            switch (Singleton.Instance.currentGameScene)
            {
                case Singleton.GameScene.TitleScene:
                    //TODO: Add Title Graphics, maybe a splash screen

                    break;
                case Singleton.GameScene.MenuScene:
                    //TODO: Add 'New Game' Button
                    gameObjects.Add(
                        new Button(buttonNew)
                        {
                            Name = "NewGameButton",
                            Position = new Vector2(100, 300)
                        }
                    );

                    //TODO: Add 'Option' Button
                    gameObjects.Add(
                        new Button(buttonOption)
                        {
                            Name = "OptionButton",
                            Position = new Vector2(100, 330)
                        }
                    );

                    //TODO: Add 'Extras' Button
                    gameObjects.Add(
                        new Button(buttonExtras)
                        {
                            Name = "ExtrasButton",
                            Position = new Vector2(100, 360)
                        }
                    );

                    //TODO: Add 'Exit' Button
                    gameObjects.Add(
                        new Button(buttonExit)
                        {
                            Name = "ExitButton",
                            Position = new Vector2(100, 450),
                            ColorHovered = Color.DarkRed
                        }
                    );

                    break;
                case Singleton.GameScene.OptionScene:
                    //TODO: Add 'BGM' Button/Slider
                    gameObjects.Add(
                        new Button(buttonExtras)
                        {
                            Name = "BGMButton",
                            Position = new Vector2(100, 360)
                        }
                    );

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

                    //Add Lose Collider
                    gameObjects.Add(
                        new LoseCollider(loseCollider)
                        {
                            Name = "LoseCollider",
                            Position = new Vector2(200, 500)
                        }
                    );

                    //Initial Bobble Pattern
                    for (int i = 0; i < 4; ++i)
                    {
                        for (int j = (i % 2); j < 15; j += 2)
                        {
                            int iOffset = 6 * i;
                            //int jOffset = 0;

                            if (i < 2)
                            {
                                if (j < 4)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_red)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - iOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Red,
                                            isInitialized = true
                                        }
                                    );
                                }
                                else if (j < 8)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_yellow)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - iOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Yellow,
                                            isInitialized = true
                                        }
                                    );
                                }
                                else if (j < 12)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_blue)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - iOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Blue,
                                            isInitialized = true
                                        }
                                    );
                                }
                                else
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_green)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - iOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Green,
                                            isInitialized = true
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
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - iOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Blue,
                                            isInitialized = true
                                        }
                                    );
                                }
                                else if (j < 7)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_green)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - iOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Green,
                                            isInitialized = true
                                        }
                                    );
                                }
                                else if (j < 11)
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_red)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - iOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Red,
                                            isInitialized = true
                                        }
                                    );
                                }
                                else
                                {
                                    gameObjects.Add(
                                        new NormalBobble(bobble_yellow)
                                        {
                                            Name = "NormalBobble",
                                            Position = new Vector2(j * Singleton.BOBBLE_SIZE / 2 + 200, i * Singleton.BOBBLE_SIZE - iOffset),
                                            bobbleColor = NormalBobble.BobbleColor.Yellow,
                                            isInitialized = true
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
            Reset();

		}

		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

            Singleton.GameScene gameScene = Singleton.Instance.currentGameScene;

            numObj = gameObjects.Count;

            for (int i = 0; i < numObj; i++)
            {
                if (gameObjects[i].IsActive) gameObjects[i].Update(gameTime, gameObjects);
            }
			// TODO: Add your update logic here
            switch(Singleton.Instance.currentGameScene){
                case Singleton.GameScene.TitleScene:
                    //TODO: Splash Screen

                    //TODO: Sleep for 7 seconds
                    if (isFirstTime){
                        currentGameTime = gameTime.TotalGameTime.TotalSeconds;
                        isFirstTime = false;

                        //DEBUG LOG
                        //Console.WriteLine("ENTER >> " + currentGameTime);
                    } 

                    //DEBUG LOG
                    //Console.WriteLine(gameTime.TotalGameTime.TotalSeconds - currentGameTime);

                    if (gameTime.TotalGameTime.TotalSeconds - currentGameTime >= Singleton.SPLASH_TIME){
                        //Change Scene
                        Singleton.Instance.currentGameScene = Singleton.GameScene.MenuScene;
                    }

                    break;
                case Singleton.GameScene.MenuScene:
                    //TODO: Add 'New Game' button logic, move to GameScene
                    //TODO: Adding some animation for represent the scene transistion

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
                            Singleton.Instance.currentGameState = Singleton.GameSceneState.Start;

                            break;
                        case Singleton.GameSceneState.Start:
                            //TODO: Logic on Each Move
                            Singleton.Instance.currentGameState = Singleton.GameSceneState.Playing;

                            break;
                        case Singleton.GameSceneState.Playing:
                            //TODO: Playing Logic on each update
                            int count = 0;

                            foreach(GameObject g in gameObjects){
                                if(g.Name.Equals("NormalBobble") && g.Position.Y == 0f && g.IsActive) count++;
                            }

                            if (count == 0){
                                Singleton.Instance.currentPlayerStatus = Singleton.PlayerStatus.Won;
                                Singleton.Instance.currentGameState = Singleton.GameSceneState.End;
                            } 


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

            if (gameScene != Singleton.Instance.currentGameScene){
                Console.WriteLine("Load Content");
                LoadContent();
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
            switch(Singleton.Instance.currentGameScene){
                case Singleton.GameScene.TitleScene:
                    //TODO: Draw Splash Screen
                    spriteBatch.Draw(splashScreen, Vector2.Zero, new Color(Color.White, parallaxHelper));
                    break;
                case Singleton.GameScene.MenuScene:
                    spriteBatch.Draw(menuBG, Vector2.Zero, Color.White);

                    //Parallax Function
                    if(parallaxHelper > 250) isAscend = false;
                    else isAscend |= parallaxHelper < 1;
                    
                    if (gameTime.TotalGameTime.Milliseconds % 1 == 0){
                        if(isAscend) parallaxHelper++;
                        else parallaxHelper--;
                    } 
                    spriteBatch.Draw(menuParallax, Vector2.Zero, new Color(Color.White, parallaxHelper));

                    spriteBatch.Draw(menuTitle, Vector2.Zero, Color.White);

                    //Button Showing

                    break;
                case Singleton.GameScene.GameScene:
                    spriteBatch.Draw(cave, new Vector2((Singleton.MAINSCREEN_WIDTH - Singleton.GAMESCREEN_WIDTH) / 2, 0f), Color.White);

                    switch(Singleton.Instance.currentGameState){
                        case Singleton.GameSceneState.End:
                            switch(Singleton.Instance.currentPlayerStatus){
                                case Singleton.PlayerStatus.Won:
                                    //TODO: Show Win Window

                                    break;
                                case Singleton.PlayerStatus.Lost:
                                    //TODO: Show Lose Window

                                    break;
                            }
                            break;
                    }
                    break;
            }

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
