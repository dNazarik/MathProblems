using System;
using System.Collections.Generic;
using Core.Utils;
using Leaderboard.ListEntity;
using UnityEngine;
using VContainer;

namespace Leaderboard
{
    public class LeaderboardView : MonoBehaviour, IDisposable
    {
        [SerializeField] private LeaderListEntity _leaderListEntityPrefab;
        [SerializeField] private Transform _listParent;

        private ICommonFactory _factory;
        private List<LeaderListEntity> _listEntities;

        [Inject]
        public void Construct(ICommonFactory factory) => _factory = factory;

        public void SetLeaderboard(List<LeaderboardEntry> entries)
        {
            _listEntities = new List<LeaderListEntity>(entries.Count);

            for (var index = entries.Count - 1; index > -1; index--)
            {
                var entry = entries[index];
                var entity =
                    _factory.InstantiateObject<LeaderListEntity>(_leaderListEntityPrefab.gameObject, _listParent);
                entity.SetEntity(entry.playerName, entry.score);
            }
        }

        public void Dispose()
        {
            foreach (var entity in _listEntities)
                Destroy(entity.gameObject);

            _listEntities.Clear();
            _listEntities = null;
        }
    }
}