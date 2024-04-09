using System;
using Background;
using Core.Utils;
using Core.Utils.SceneLoader;
using VContainer.Unity;

namespace Gameplay.Common.MainMenu
{
    public class MainMenuController : IInitializable, IDisposable
    {
        private readonly MainMenuModel _model;
        private readonly ICommonFactory _factory;
        private readonly BackgroundController _backgroundController;

        private MainMenuPanelView _view;

        public MainMenuController(MainMenuModel model, ICommonFactory factory,
            BackgroundController backgroundController)
        {
            _model = model;
            _factory = factory;
            _backgroundController = backgroundController;
        }

        public void Initialize()
        {
            _backgroundController.Init();

            CreateView();
        }

        public void Dispose()
        {
            _view.ChangeBgBtn.onClick.RemoveAllListeners();
            _view.LeaderboardBtn.onClick.RemoveAllListeners();
            _view.NewGameBtn.onClick.RemoveAllListeners();
        }

        private async void CreateView()
        {
            var prefab = await _model.GetPrefab();

            _view = _factory.InstantiateObject<MainMenuPanelView>(prefab);
            _view.ChangeBgBtn.onClick.AddListener(_backgroundController.SetNextBackground);
            _view.LeaderboardBtn.onClick.AddListener(ShowLeaderboards);
            _view.NewGameBtn.onClick.AddListener(StartNewGame);
        }

        private void StartNewGame()
        {
            SceneLoaderUtility.LoadGameScene();

            _view.gameObject.SetActive(false);
        }

        private void ShowLeaderboards()
        {
            SceneLoaderUtility.LoadLeadersScene();

            _view.HideMiddleButtons();
        }
    }
}