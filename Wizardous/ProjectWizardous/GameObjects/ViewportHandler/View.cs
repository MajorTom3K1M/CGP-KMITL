using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectWizardous
{
    public class PlayerGraphicsComponent : GraphicsComponent
    {
        private bool jump = true;
        private bool isTouchingShopKepper = false;
        public int health = 100;
        public int chargeBar = 0;
        public bool charging = false;
        public int chargeToggle;

        public Texture2D healthTexture;
        public Rectangle healthRectangle;

        public Texture2D chargeTexture;
        public Vector2 chargeposition;
        public Rectangle rectangle;

        public Texture2D arrowTexture;
        public Vector2 arrowPosition;

        public Texture2D bgBar;
        public Vector2 bgPostion;
        public Rectangle bgRectangle;

        public Texture2D shopTexture;
        public Rectangle shopRect;
        public Vector2 shopPosition;

        public Texture2D selectTexture;
        public Vector2 selectPosition;

        public Texture2D selectExitTexture;
        public Vector2 selectExitPosition;

        public int moveSelectorX = 0;
        public int moveSelectorY = 0;
        //public Rectangle bgRectangle;

        public float angle;
        public float power;
        enum ControlState {
            NormalState,
            ActiveState,
            ShopState
        }

        private ControlState currentControlState;
        public PlayerGraphicsComponent(GameScene currentScene) : base(currentScene)
        {
            Texture2D playerTexture = currentScene.SceneManager.Content.Load<Texture2D>("Images/Actor_Spritesheet");

            _animations = new Dictionary<string, Animation>()
                            {
                                { "Alive", new Animation(playerTexture, new Rectangle(0, 0, 64, 64), 1) },
                                { "Dead", new Animation(playerTexture, new Rectangle(448, 448, 512, 512), 1) },
                            };

            _texture = playerTexture;
            healthTexture = currentScene.SceneManager.Content.Load<Texture2D>("Images/Health");
            chargeTexture = currentScene.SceneManager.Content.Load<Texture2D>("Images/ChargeBar");
            arrowTexture = currentScene.SceneManager.Content.Load<Texture2D>("Images/ArrowHead");
            bgBar = currentScene.SceneManager.Content.Load<Texture2D>("Images/BgBar");
            shopTexture = currentScene.SceneManager.Content.Load<Texture2D>("Images/Store");
            selectTexture = currentScene.SceneManager.Content.Load<Texture2D>("Images/Select1");
            selectExitTexture = currentScene.SceneManager.Content.Load<Texture2D>("Images/SelectExit");
            angle = 75;
            rectangle = new Rectangle(0, 0, 0, chargeTexture.Height);
            bgRectangle = new Rectangle(0, 0, bgBar.Width, bgBar.Height);
            shopRect = new Rectangle(0, 0, shopTexture.Width, shopTexture.Height);
            arrowPosition = new Vector2(50, 50);
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
            switch (currentControlState) {
                case ControlState.ActiveState:
                    spriteBatch.Draw(arrowTexture, arrowPosition, null, Color.White, MathHelper.ToRadians(angle), new Vector2(arrowTexture.Width / 2, 143), 1f, SpriteEffects.None, 1f);
                   //spriteBatch.Draw(bgBar,bgPostion, bgRectangle, canSee , 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(chargeTexture, chargeposition, rectangle, Color.White, 0f, Vector2.Zero , 1f, SpriteEffects.None, 0f);
                    break;
                case ControlState.ShopState:
                    if (moveSelectorY >= 305) {
                        spriteBatch.Draw(selectExitTexture, selectExitPosition, null, Color.White * 0.5f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    } else {
                        spriteBatch.Draw(selectTexture, selectPosition, null, Color.White * 0.5f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    spriteBatch.Draw(shopTexture,shopPosition,shopRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                    break;
            }
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);

            base.Draw(spriteBatch, parent);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
            chargeposition = new Vector2(parent.Position.X - 100, parent.Position.Y + parent.Rectangle.Height + 2);
            switch (currentControlState) {
                case ControlState.NormalState:
                    if (jump == true) {
                        float i = 1;
                        parent.Velocity.Y += 0.15f * i;
                    }

                    if (parent.Position.Y + _texture.Height >= 1150) {
                        parent.SendMessage(101, this);
                        jump = false;
                    }

                    if (jump == false) {
                        parent.Velocity.Y = 0f;
                    }
                    if (parent.Position.X <= Singleton.MAINSCREEN_WIDTH / 2) {
                        healthRectangle = new Rectangle(50, 20, health, 20);
                    } else {
                        healthRectangle = new Rectangle((int)parent.Position.X - 633, 20, health, 20);
                    }
                    if (health > 0) {

                    }
                    break;
                case ControlState.ActiveState:
                    angle = MathHelper.Clamp(angle, 15, 75);
                    //bgPostion = new Vector2(parent.Position.X - 103, parent.Position.Y + parent.Rectangle.Height - 3);
                    arrowPosition = new Vector2((parent.Position.X + parent.Viewport.Width / 2 - 32), (parent.Position.Y + parent.Viewport.Height / 2 - 32));
                    chargeposition = new Vector2((parent.Position.X - 100), (parent.Position.Y + 35));
                    //CHARGE
                    if (charging) {
                        if (rectangle.Width <= 0) {
                            chargeToggle = 7;
                        } else if (rectangle.Width >= chargeTexture.Width) {
                            chargeToggle = -7;
                        }
                        rectangle.Width = rectangle.Width + chargeToggle;
                        parent.ShootPower = rectangle.Width;

                        parent.SendMessage(700,this);
                    }
                break;
              case ControlState.ShopState:
                    if (moveSelectorX >= 450 && moveSelectorY >= 300) {
                        moveSelectorX = MathHelper.Clamp(moveSelectorX, 0, 450);
                        moveSelectorY = MathHelper.Clamp(moveSelectorY, 0, 450);
                    } else {
                        moveSelectorX = MathHelper.Clamp(moveSelectorX, 0, 450);
                        moveSelectorY = MathHelper.Clamp(moveSelectorY, 0, 300);
                    }
                    if (parent.Position.X < Singleton.MAINSCREEN_WIDTH / 2) {
                        shopPosition = new Vector2(350, 100);
                        selectPosition = new Vector2(420 + moveSelectorX, 150 + moveSelectorY);
                        selectExitPosition = new Vector2(865, 590);
                    } else {
                        shopPosition = new Vector2(parent.Position.X - 333,100);
                        selectPosition = new Vector2((parent.Position.X - 263) + moveSelectorX, 150 + moveSelectorY);
                        selectExitPosition = new Vector2(parent.Position.X + 182, 590);
                    }
                    break;


            }

            base.Update(gameTime, gameObjects, parent);
        }

        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
            if (sender.Equals(this)) return;

            if (message == 101) {
                jump = false;
            } else if (message == 100) {
                jump = true;
            } else if (message == 200) {
                health -= 10;
            } else if (message == 900) {
                charging = true;
            } else if (message == 901) {
                charging = false;
            } else if (message == 800) {
                angle++;
            } else if (message == 801) {
                angle--;
            } else if (message == 700) {
                currentControlState = ControlState.ActiveState;
            } else if (message == 500) {
                rectangle.Width = 0;
            } else if (message == 701) {
                currentControlState = ControlState.NormalState;
            } else if (message == 702) {
                currentControlState = ControlState.ShopState;
            } else if (message == 902) {
                isTouchingShopKepper = true;
            } else if (message == 903) {
                isTouchingShopKepper = false;
            } else if (message == 300) {
                moveSelectorX += 150;
            } else if (message == 301) {
                moveSelectorX -= 150;
            } else if (message == 302) {
                moveSelectorY += 150;
            } else if (message == 303) {
                moveSelectorY -= 150;
            }
        }
    }
}
