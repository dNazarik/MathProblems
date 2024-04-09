using UnityEngine.SceneManagement;

namespace Core.Utils.SceneLoader
{
    public class SceneLoaderUtility
    {
        private const int SceneMenuId = 0;
        private const int SceneGameId = 1;
        private const int SceneLeadersId = 2;

        public static void LoadGameScene()
        {
            var leadersScene = SceneManager.GetSceneByBuildIndex(SceneLeadersId);

            if (leadersScene.IsValid() && leadersScene.isLoaded)
                SceneManager.UnloadSceneAsync(SceneLeadersId);

            SceneManager.LoadSceneAsync(SceneGameId, LoadSceneMode.Additive);
        }

        public static void LoadLeadersScene() =>
            SceneManager.LoadSceneAsync(SceneLeadersId, LoadSceneMode.Additive);

        public static void LoadMenuScene() => SceneManager.LoadSceneAsync(SceneMenuId);
    }
}