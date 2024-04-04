using UnityEngine.SceneManagement;

namespace Core.SceneLoader
{
    public class SceneLoaderController
    {
        public static void LoadGameScene()
        {
            var leadersScene = SceneManager.GetSceneByBuildIndex(SceneLoaderModel.SceneLeadersId);

            if (leadersScene.IsValid() && leadersScene.isLoaded)
                SceneManager.UnloadSceneAsync(SceneLoaderModel.SceneLeadersId);

            SceneManager.LoadSceneAsync(SceneLoaderModel.SceneGameId, LoadSceneMode.Additive);
        }

        public static void LoadLeadersScene() =>
            SceneManager.LoadSceneAsync(SceneLoaderModel.SceneLeadersId, LoadSceneMode.Additive);

        public static void LoadMenuScene() => SceneManager.LoadSceneAsync(SceneLoaderModel.SceneMenuId);
    }
}