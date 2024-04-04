using _3rdParty.Core;
using Gameplay.Common;
using Gameplay.General;
using Gameplay.Math;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private MiddlePanelView _middlePanelView;
        [SerializeField] private BottomPanelView _bottomPanelView;
        [SerializeField] private TopPanelView _topPanelView;
        [SerializeField] private PausePanelView _pausePanelView;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private SaveRecordPanelView _saveRecordPanelView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameController>();

            builder.Register<TimerController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<GameModel>(Lifetime.Singleton).AsSelf();
            builder.Register<ProblemsGenerator>(Lifetime.Singleton).AsSelf();
            builder.Register<GameUIDataContainer>(Lifetime.Singleton).AsSelf();
            
            builder.RegisterComponentInNewPrefab(_topPanelView, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_middlePanelView, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_bottomPanelView, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_pausePanelView, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_gameOverView, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_saveRecordPanelView, Lifetime.Singleton);
        }
    }
}