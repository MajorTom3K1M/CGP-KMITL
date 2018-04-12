using Microsoft.Xna.Framework.Graphics;

namespace ProjectWiz
{
    public class Singleton
    {
        public const int MAINSCREEN_WIDTH = 1366;
        public const int MAINSCREEN_HEIGHT = 768;

        public const float SPLASH_TIME = 7f;

        public enum GameScene
        {
            None,
            Splash,
            Menu,
            Option,
            Game,
            End
        }

        public enum GameState
        {
            None,
            Normal,        
            Active,
            Menu
        }

        public enum PlayerStatus
        {
            None,
            Actor1,     //Play as Actor1
            Actor2      //Play as Actor2
        }   

        public GameScene curScene;
        public GameState curState;
        public PlayerStatus curPlayerState;

        private static Singleton instance;

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
