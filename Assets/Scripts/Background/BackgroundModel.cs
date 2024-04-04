using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Background
{
    public class BackgroundModel
    {
        private const int BackgroundsAmount = 3;

        private const string BackgroundPrefabPath = "Assets/Prefabs/Panels/BackgroundCanvas.prefab";
        public const string BackgroundImagePathFormat = "Assets/Art/Backgrounds/{0}.jpg";
        private const string SavedBgKey = "BgId";

        public int BgId { get; private set; }

        public static async Task<GameObject> GetPrefab()
        {
            var loader = Addressables.LoadAssetAsync<GameObject>(BackgroundPrefabPath);
            
            while (!loader.IsDone)
            {
                await Task.Delay(1);
            }

            return loader.Result;
        }

        public void TryGetSavedBackgroundId()
        {
            var savedBgId = PlayerPrefs.GetInt(SavedBgKey);

            BgId = savedBgId == 0 ? 1 : savedBgId;
        }

        public void SetNextBgId()
        {
            BgId++;

            if (BgId > BackgroundsAmount)
                BgId = 1;

            PlayerPrefs.SetInt(SavedBgKey, BgId);
        }
    }
}