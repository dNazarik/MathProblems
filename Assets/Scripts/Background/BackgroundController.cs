using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Background
{
    public class BackgroundController
    {
        private readonly BackgroundModel _model;
        private readonly BackgroundView _view;

        public BackgroundController(BackgroundModel model, BackgroundView view) => (_model, _view) = (model, view);

        public void Init() => CheckSavedBackgroundImage();

        public void SetNextBackground()
        {
            _model.SetNextBgId();

            LoadAndSetBg();
        }

        private void ChangeBackground(AsyncOperationHandle<Sprite> loader)
        {
            _view.SetBackground(loader.Result);

            loader.Completed -= ChangeBackground;
        }

        private void CheckSavedBackgroundImage()
        {
            _model.TryGetSavedBackgroundId();

            LoadAndSetBg();
        }

        private void LoadAndSetBg()
        {
            var spriteLoader =
                Addressables.LoadAssetAsync<Sprite>(string.Format(BackgroundModel.BackgroundImagePathFormat,
                    _model.BgId));

            spriteLoader.Completed += ChangeBackground;
        }
    }
}