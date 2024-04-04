using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Leaderboard
{
    [Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public int score;
    }

    [Serializable]
    public class LeaderboardData
    {
        public List<LeaderboardEntry> entries;
    }

    public class LeaderboardController : IInitializable, IDisposable
    {
        private readonly LeaderboardModel _model;
        private readonly LeaderboardView _view;

        public LeaderboardController(LeaderboardModel model, LeaderboardView view) => (_model, _view) = (model, view);
        public void Initialize() => _view.SetLeaderboard(GetAllEntries());
        public void Dispose() => _model.SaveLeaderboard();
        private List<LeaderboardEntry> GetAllEntries() => _model.LeaderboardData.entries;
    }
}