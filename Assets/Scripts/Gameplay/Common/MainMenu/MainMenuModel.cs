using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay.Common.MainMenu
{
    public class MainMenuModel
    {
        private const string PrefabPath = "Assets/Prefabs/Panels/UIMenuPanel.prefab";

        public async Task<GameObject> GetPrefab()
        {
            var loader = Addressables.LoadAssetAsync<GameObject>(PrefabPath);

            while (!loader.IsDone)
                await Task.Delay(1);

            return loader.Result;
        }
    }
}