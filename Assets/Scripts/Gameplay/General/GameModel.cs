using System;
using Core.Utils;
using Gameplay.Configs;

namespace Gameplay.General
{
    public class GameModel
    {
        public const float VfxDuration = 0.5f;
        public const float BgMusicVolume = 0.5f;
        private readonly int _timerEntityId;
        private readonly ITimerController _timerController;
        private readonly GameSettings _gameSettings;

        public event Action OnAbilityReady;
        public event Action OnLevelUp;

        public Difficulty CurrentDifficulty { get; private set; }

        public bool IsGameOver = false;
        public bool IsGamePaused = false; //also used for VFX, it is no sense to create another IsVfxPlaying field

        private bool _isAbilityUsed = false;
        private int _timer;
        private int _abilityCounter;
        private int _correctAnswersAmount;

        public int Score { get; private set; } = 0;
        public bool IsAudioEnabled() => _gameSettings.AudioEnabled;
        private int GetTimeBonus() => _gameSettings.TimerBonuses[(int)CurrentDifficulty];
        private int GetScoreValue() => _gameSettings.ScoreValues[(int)CurrentDifficulty];

        public GameModel(ITimerController timerController, GameSettings gameSettings)
        {
            _timerController = timerController;
            _gameSettings = gameSettings;

            CurrentDifficulty = Difficulty.Easy;

            Score = 0;
            _abilityCounter = 0;
            _correctAnswersAmount = 0;
            _timer = _gameSettings.DefaultSessionTime;

            _timerEntityId = _timerController.CreateTimeEntity();
        }

        public float GetTimer() => _timerController.ElapsedTimeReverse(_timerEntityId, _timer);
        public void UseAbility() => _isAbilityUsed = true;

        public void Boost()
        {
            _timer += GetTimeBonus();

            Score += GetScoreValue();

            CheckDifficulty();

            if (!_isAbilityUsed)
                return;

            _abilityCounter++;

            if (_abilityCounter == _gameSettings.AbilityResetCooldown)
            {
                OnAbilityReady?.Invoke();

                _abilityCounter = 0;

                _isAbilityUsed = false;
            }
        }

        private void CheckDifficulty()
        {
            if (CurrentDifficulty == Difficulty.Hard)
                return;

            _correctAnswersAmount++;

            if (_correctAnswersAmount < _gameSettings.CorrectAnswersForNextLevel)
                return;

            _correctAnswersAmount = 0;

            CurrentDifficulty++;

            OnLevelUp?.Invoke();
        }
    }
}