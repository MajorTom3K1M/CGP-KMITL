using System;

namespace ProjectWiz.Managers
{
    public class SceneManager
	{
        public static void SetCurrentScene(Singleton.GameScene scene)
        {
            Singleton.Instance.curScene = scene;
        }
    }
}
