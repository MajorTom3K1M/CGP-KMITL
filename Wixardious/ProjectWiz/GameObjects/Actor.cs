using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectWiz.Models;

namespace ProjectWiz
{
    public class Enemy : GameObject
    {
        public int Score;

        public enum EnemyState
        {
            Alive,
            Dead
        }
        public EnemyState CurrentEnemyState;

        public Enemy(Dictionary<string, Animation> animations) : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
