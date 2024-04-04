using System;
using System.Linq;
using Core;
using Core.SceneLoader;
using Gameplay.Common;
using Gameplay.Math;
using Leaderboard;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay.General
{
    public class GameController : IInitializable, ITickable, IDisposable
    {
        private readonly ProblemsGenerator _problemsGenerator;
        private readonly GameModel _gameModel;
        private readonly LeaderboardModel _leaderboardModel;
        private readonly SaveRecordPanelView _saveRecordPanelView;
        private readonly GameUIDataContainer _gameUIDataContainer;
        private readonly IAudioController _audioController;
        private readonly AudioConfig _audioConfig;

        public GameController(ProblemsGenerator problemsGenerator, GameModel gameModel,
            LeaderboardModel leaderboardModel, SaveRecordPanelView saveRecordPanelView,
            GameUIDataContainer gameUIDataContainer, IAudioController audioController, AudioConfig audioConfig)
        {
            _problemsGenerator = problemsGenerator;
            _gameModel = gameModel;
            _leaderboardModel = leaderboardModel;
            _saveRecordPanelView = saveRecordPanelView;
            _gameUIDataContainer = gameUIDataContainer;
            _audioController = audioController;
            _audioConfig = audioConfig;
        }

        public void Initialize()
        {
            InitGameUI();

            _gameModel.OnAbilityReady += ResetAbility;

            ShowNextQuestion();

            if (_gameModel.IsAudioEnabled())
                _audioController.PlayEffect(_audioConfig.GetClipByType(AudioType.BackgroundMusic),
                    volume: GameModel.BgMusicVolume, isLoop: true);
        }

        private void InitGameUI()
        {
            _gameUIDataContainer.BottomPanelView.Init(_gameModel.UseAbility);
            _gameUIDataContainer.BottomPanelView.InitOptionsButtons(OnUserAnswer);
            _gameUIDataContainer.TopPanelView.SetPauseAction(Pause);
            _gameUIDataContainer.PausePanelView.Init(Continue);
        }

        public void Dispose()
        {
            _gameModel.OnAbilityReady -= ResetAbility;

            _gameUIDataContainer.BottomPanelView.Dispose();
        }

        public void Tick()
        {
            if (_gameModel.IsGameOver || _gameModel.IsGamePaused)
                return;

            var timer = _gameModel.GetTimer();

            _gameUIDataContainer.TopPanelView.SetTimer(((int)timer).ToString());

            if (timer < 0.0f)
                GameOver();
        }

        private void OnUserAnswer(bool isCorrect)
        {
            if (isCorrect)
            {
                _gameModel.Boost();

                ShowNextQuestion();

                _gameUIDataContainer.TopPanelView.SetScore(_gameModel.Score);
            }
            else
                GameOver();
        }

        private void ShowNextQuestion()
        {
            var q = _problemsGenerator.GetGeneratedQuestion();

            _gameUIDataContainer.MiddlePanelView.SetQuestion(q.ProblemString);

            var options = q.GetShuffledOptions();

            _gameUIDataContainer.BottomPanelView.SetOptions(options);
            _gameUIDataContainer.BottomPanelView.SetCorrectButton(options.ToList().IndexOf(q.CorrectAnswer));
        }

        private void ResetAbility() => _gameUIDataContainer.BottomPanelView.SetAbilityButtonActivity(true);

        private void Pause()
        {
            if (_gameModel.IsGamePaused)
                return;

            _gameModel.IsGamePaused = true;

            Time.timeScale = 0;

            _gameUIDataContainer.PausePanelView.ShowPauseScreen(true);
        }

        private void Continue()
        {
            _gameModel.IsGamePaused = false;

            Time.timeScale = 1;

            _gameUIDataContainer.PausePanelView.ShowPauseScreen(false);
        }

        private async void GameOver()
        {
            _gameModel.IsGameOver = true;

            _audioController.PlayEffect(_audioConfig.GetClipByType(AudioType.GameOver));

            await _gameUIDataContainer.GameOverView.PlayAnimation();

            if (_leaderboardModel.IsNewRecordSet(_gameModel.Score))
            {
                if (_gameModel.IsAudioEnabled())
                    _audioController.PlayEffect(_audioConfig.GetClipByType(AudioType.NewRecord));

                _saveRecordPanelView.Init(playerName =>
                {
                    _leaderboardModel.SaveNewRecord(playerName, _gameModel.Score);

                    SceneLoaderController.LoadMenuScene();
                }, SceneLoaderController.LoadMenuScene);
            }
            else
                SceneLoaderController.LoadMenuScene();
        }
    }
}