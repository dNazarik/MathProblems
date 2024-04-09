using System;
using System.Linq;
using Audio;
using Core;
using Core.Utils;
using Gameplay.Answer;
using Gameplay.General;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Gameplay.Common
{
    /// <summary>
    /// Not big class so it makes no sense creating a Controller and a Model for it
    /// </summary>
    public class BottomPanelView : MonoBehaviour, IDisposable
    {
        [SerializeField] private OptionButton[] _buttons;
        [SerializeField] private Button _ability;

        private GameModel _gameModel;
        private IAudioController _audioController;
        private AudioConfig _audioConfig;
        private Action _onAbilityBtnClick;
        private Action<bool> _onAnswerSelected;
        private int _currentCorrectButtonId = -1;

        [Inject]
        public void Construct(GameModel gameModel, IAudioController audioController, AudioConfig audioConfig)
        {
            _gameModel = gameModel;
            _audioConfig = audioConfig;
            _audioController = audioController;
        }

        public void Init(Action onAbilityClick)
        {
            _onAbilityBtnClick = onAbilityClick;

            _ability.onClick.AddListener(ApplyAbility);
        }

        public void Dispose()
        {
            _ability.onClick.RemoveAllListeners();

            foreach (var t in _buttons)
                t.Dispose();
        }

        public void InitOptionsButtons(Action<bool> onClick)
        {
            _onAnswerSelected = onClick;

            foreach (var t in _buttons)
                t.Init(PlaySfxOnOptionClick);
        }

        private async void PlaySfxOnOptionClick(OptionButton clickedOption)
        {
            _gameModel.IsGamePaused = true;

            SetButtonsInteractable(false);

            if (clickedOption.IsCorrect)
            {
                if (_gameModel.IsAudioEnabled())
                    _audioController.PlayEffect(_audioConfig.GetClipByType(AudioType.CorrectAnswer));

                await clickedOption.HighlightButton(Color.green, GameModel.VfxDuration);
            }
            else
            {
                if (_gameModel.IsAudioEnabled())
                    _audioController.PlayEffect(_audioConfig.GetClipByType(AudioType.IncorrectAnswer));

                var correctButton = _buttons.First(b => b.IsCorrect);

                // not awaited to play same green and red transition
                correctButton.HighlightButton(Color.green, GameModel.VfxDuration);
                await clickedOption.HighlightButton(Color.red, GameModel.VfxDuration);
            }

            SetButtonsInteractable(true);

            _gameModel.IsGamePaused = false;

            _onAnswerSelected?.Invoke(clickedOption.IsCorrect);
        }

        public void SetOptions(int[] numbers)
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].SetNumber(numbers[i]);
                _buttons[i].SetCorrectState(false);
            }

            SetButtonsInteractable(true);
        }

        public void SetCorrectButton(int id)
        {
            _currentCorrectButtonId = id;

            _buttons[_currentCorrectButtonId].SetCorrectState(true);
        }

        public void SetAbilityButtonActivity(bool isActive) => _ability.interactable = isActive;

        private void SetButtonsInteractable(bool isInteractable)
        {
            foreach (var btn in _buttons)
                btn.SetButtonActivity(isInteractable);
        }

        private void ApplyAbility()
        {
            if (_gameModel.IsGamePaused)
                return;

            if (_gameModel.IsAudioEnabled())
                _audioController.PlayEffect(_audioConfig.GetClipByType(AudioType.CorrectAnswer));

            var incorrectIdToKeep =
                Randomizer.GetNumberInRangeExcluding(0, _buttons.Length, new[] { _currentCorrectButtonId });

            for (var i = 0; i < _buttons.Length; i++)
            {
                if (i == incorrectIdToKeep || i == _currentCorrectButtonId)
                    continue;

                _buttons[i].SetButtonActivity(false);
            }

            SetAbilityButtonActivity(false);

            _onAbilityBtnClick?.Invoke();
        }
    }
}