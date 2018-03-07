using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBobble
{
    public class Button : GameObject
    {
        Point mousePosition;
        MouseState mouseClickedState, previousMouseState, mouseState;

        Color colorDisplay = Color.White;
        public Color ColorHovered = Color.Gray;
        public bool InitialActivated = true;

        public Button(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            previousMouseState = mouseClickedState;
            mouseState = Mouse.GetState();
            mousePosition = mouseState.Position;

            if(mousePosition.X < Rectangle.Right && mousePosition.X > Rectangle.Left && mousePosition.Y < Rectangle.Bottom && mousePosition.Y > Rectangle.Top){
                colorDisplay = ColorHovered;
                switch(Name){
                    case "NightmarePanel":
                        foreach(GameObject g in gameObjects){
                            if (g.Name.Equals("WarningPopup") && !g.IsActive) g.IsActive = true;
                        }
                        break;
                }
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released){
                    switch(Name){
                        case "NewGameButton":
                            Singleton.Instance.score = 0;
                            Singleton.Instance.IsBlindMode = false;
                            Singleton.Instance.currentGameScene = Singleton.GameScene.GameScene;
                            Singleton.Instance.currentPlayerStatus = Singleton.PlayerStatus.None;
                            Singleton.Instance.currentGameState = Singleton.GameSceneState.Start;
                            break;
                        case "OptionButton":
                            Singleton.Instance.currentGameScene = Singleton.GameScene.OptionScene;
                            break;
                        case "ExtrasButton":
                            //Debug the consequently click bug
                            Mouse.SetPosition(50,400);

                            Singleton.Instance.currentGameScene = Singleton.GameScene.ExtrasScene;
                            break;
                        case "BackMenuButton":
                        case "BackButton":
                            Singleton.Instance.currentGameScene = Singleton.GameScene.MenuScene;
                            break;
                        case "HardcorePanel":
                            Singleton.Instance.score = 0;
                            Singleton.Instance.ceilingTime = 15;
                            Singleton.Instance.colorVariety = 8;
                            Singleton.Instance.IsBlindMode = false;
                            Singleton.Instance.bombTime = 10;
                            Singleton.Instance.currentGameScene = Singleton.GameScene.GameScene;
                            Singleton.Instance.currentPlayerStatus = Singleton.PlayerStatus.None;
                            Singleton.Instance.currentGameState = Singleton.GameSceneState.Start;
                            break;
                        case "BlindPanel":
                            Singleton.Instance.score = 0;
                            Singleton.Instance.ceilingTime = 20;
                            Singleton.Instance.colorVariety = 8;
                            Singleton.Instance.IsBlindMode = true;
                            Singleton.Instance.bombTime = 10;
                            Singleton.Instance.currentGameScene = Singleton.GameScene.GameScene;
                            Singleton.Instance.currentPlayerStatus = Singleton.PlayerStatus.None;
                            Singleton.Instance.currentGameState = Singleton.GameSceneState.Start;
                            break;
                        case "NightmarePanel":
                            Singleton.Instance.score = 0;
                            Singleton.Instance.ceilingTime = 15;
                            Singleton.Instance.colorVariety = 8;
                            Singleton.Instance.IsBlindMode = true;
                            Singleton.Instance.bombTime = 999;
                            Singleton.Instance.currentGameScene = Singleton.GameScene.GameScene;
                            Singleton.Instance.currentPlayerStatus = Singleton.PlayerStatus.None;
                            Singleton.Instance.currentGameState = Singleton.GameSceneState.Start;
                            break;
                        case "ExitButton":
                            //Game Exit
                            new MainScene().Exit();
                            break;
                    }
                }
            }
            else{
                colorDisplay = Color.White;
                switch (Name){
                    case "NightmarePanel":
                        foreach (GameObject g in gameObjects){
                            if (g.Name.Equals("WarningPopup") && g.IsActive) g.IsActive = false;
                        }
                        break;
                }
            }

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, colorDisplay, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.05f);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            this.IsActive = InitialActivated;
            base.Reset();
        }
    }
}
