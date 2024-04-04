using UnityEngine;

namespace Background
{
    public class BackgroundModel
    {
        private const int BackgroundsAmount = 3;

        public const string BackgroundImagePathFormat = "Assets/Art/Backgrounds/{0}.jpg";
        private const string SavedBgKey = "BgId";

        public int BgId { get; private set; }

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