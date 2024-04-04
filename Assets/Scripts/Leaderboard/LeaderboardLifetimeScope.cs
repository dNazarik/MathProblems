using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Leaderboard
{
    public class LeaderboardLifetimeScope : LifetimeScope
    {
        [SerializeField] private LeaderboardView _view;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LeaderboardController>();
            builder.RegisterComponentInNewPrefab(_view, Lifetime.Singleton).AsSelf();
        }
    }
}