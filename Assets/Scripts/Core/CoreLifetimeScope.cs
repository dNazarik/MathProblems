using Audio;
using Background;
using Core.Utils;
using Core.Utils.SceneLoader;
using Gameplay.Common.MainMenu;
using Gameplay.Configs;
using Leaderboard;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class CoreLifetimeScope : LifetimeScope
    {
        [SerializeField] private BackgroundView _backgroundView;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private AudioConfig _audioConfig;

        /// <summary>
        /// FPS limit to avoid hardware usage for no reason
        /// </summary>
        private void Start() => Application.targetFrameRate = 30;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MainMenuController>();

            builder.Register<BackgroundController>(Lifetime.Singleton).AsSelf();
            builder.Register<BackgroundModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInNewPrefab(_backgroundView, Lifetime.Singleton).AsSelf();

            builder.Register<AudioController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<CommonFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<MainMenuModel>(Lifetime.Singleton).AsSelf();
            builder.Register<SceneLoaderUtility>(Lifetime.Singleton).AsSelf();
            builder.Register<LeaderboardModel>(Lifetime.Singleton);

            builder.RegisterInstance(_gameSettings);
            builder.RegisterInstance(_audioConfig);
        }
    }
}