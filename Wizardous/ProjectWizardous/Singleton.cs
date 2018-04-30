using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ProjectWizardous
{
    public class Singleton
    {
        public const int MAINSCREEN_WIDTH = 1366;
        public const int MAINSCREEN_HEIGHT = 768;

        public const float SPLASH_TIME = 7f;

        public KeyboardState PreviousKey, CurrentKey;

        public List<GameObject> gameObjects = new List<GameObject>();

        private Singleton() { }

        private static Singleton instance;

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
