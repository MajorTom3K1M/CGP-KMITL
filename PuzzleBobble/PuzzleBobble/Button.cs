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

        public Button(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            previousMouseState = mouseClickedState;
            mouseState = Mouse.GetState();
            mousePosition = mouseState.Position;

            if(mousePosition.X < Rectangle.Right && mousePosition.Y > Rectangle.Left && mousePosition.Y < Rectangle.Bottom && mousePosition.Y > Rectangle.Top){
                colorDisplay = ColorHovered;
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released){
                    switch(Name){
                        case "NewGameButton":
                            Singleton.Instance.currentGameScene = Singleton.GameScene.GameScene;
                            break;
                    }
                }
            }
            else{
                colorDisplay = Color.White;
            }

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, colorDisplay);

            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            this.IsActive = true;
            base.Reset();
        }
    }
}
