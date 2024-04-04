using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Answer
{
    public class OptionButton : SimpleButton, IDisposable
    {
        [SerializeField] private TMP_Text _optionNumber;
        [SerializeField] private Image _buttonImage;

        public bool IsCorrect { get; set; }
        public void Init(Action<OptionButton> onClick) => button.onClick.AddListener(() => onClick.Invoke(this));
        public void Dispose() => button.onClick.RemoveAllListeners();
        public void SetNumber(int number) => _optionNumber.text = number.ToString();
        public void SetCorrectState(bool isCorrect) => IsCorrect = isCorrect;
        public void SetButtonActivity(bool isActive) => button.interactable = isActive;

        public async Task HighlightButton(Color color, float duration)
        {
            button.interactable = false;

            var sequence = DOTween.Sequence();
            sequence.Append(_buttonImage.DOColor(color, duration));
            sequence.AppendInterval(duration);
            sequence.Append(_buttonImage.DOColor(Color.white, duration));
            await sequence.Play().AsyncWaitForCompletion();

            button.interactable = true;
        }
    }
}