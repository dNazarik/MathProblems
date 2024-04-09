using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Utils;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardModel
    {
        private const int TopPlayerAmount = 10;
        private const int MockScoreFactor = 200;

        private const string LoadingErrorMessageFormat =
            "Something went wrong during leaderboard loading, gonna create a new one. Error: {0}";

        private readonly string[] _mockLeaders =
            { "Sam", "Ana", "Nick", "Valera", "Goose", "Kate", "Dan", "Ben Dover", "Antony", "admin" };

        private readonly string _filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");

        public LeaderboardData LeaderboardData { get; private set; }

        public LeaderboardModel()
        {
            LeaderboardData = new LeaderboardData();

            TryLoadLeaderboard();
        }

        public bool IsNewRecordSet(int score) => LeaderboardData.entries.Any(e => e.score < score);

        public void SaveLeaderboard()
        {
            var json = JsonUtility.ToJson(LeaderboardData);

            File.WriteAllText(_filePath, json);
        }

        public void SaveNewRecord(string name, int score)
        {
            var id = -1;

            for (var i = 0; i < LeaderboardData.entries.Count; i++)
            {
                if (LeaderboardData.entries[i].score < score)
                    id = i + 1;
                else
                    break;
            }

            LeaderboardData.entries.Insert(id, new LeaderboardEntry { playerName = name, score = score });

            if (LeaderboardData.entries.Count > TopPlayerAmount)
                LeaderboardData.entries.Remove(LeaderboardData.entries.First());

            SaveLeaderboard();
        }

        private void TryLoadLeaderboard()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    var json = File.ReadAllText(_filePath);

                    LeaderboardData = JsonUtility.FromJson<LeaderboardData>(json);
                }
                catch (Exception e)
                {
                    Debug.Log(string.Format(LoadingErrorMessageFormat, e.Message));
                }
            }
            else
                GenerateMockLeadersTable();
        }

        private void GenerateMockLeadersTable()
        {
            LeaderboardData = new LeaderboardData { entries = new List<LeaderboardEntry>() };

            _mockLeaders.Shuffle();

            for (var i = 0; i < _mockLeaders.Length; i++)
            {
                AddEntry(_mockLeaders[i],
                    i * LeaderboardModel.MockScoreFactor + LeaderboardModel.MockScoreFactor);
            }
        }

        private void AddEntry(string playerName, int score)
        {
            var newEntry = new LeaderboardEntry
            {
                playerName = playerName,
                score = score
            };

            LeaderboardData.entries.Add(newEntry);

            SaveLeaderboard();
        }
    }
}