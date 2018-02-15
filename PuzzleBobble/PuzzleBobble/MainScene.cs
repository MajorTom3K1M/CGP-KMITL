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

            //Initial Bobble Pattern
            for (int i = 0; i < 4; ++i)
            {
                for (int j = (i % 2); j < 15; j += 2)
                {
                    int jOffset = 6 * i;
                    if(i < 2){
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
                    else if(i < 4){
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

		}

		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			// TODO: Add your update logic here

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
