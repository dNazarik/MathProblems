using System;
using UI;
using UnityEngine;

namespace Gameplay.Common
{
    /// <summary>
    /// Not big class so it makes no sense creating a Controller and a Model for it
    /// </summary>
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] private SimpleButton _pauseButton;
        [SerializeField] private CircleBlock _timer, _score;

        public void SetPauseAction(Action pauseAction) => _pauseButton.Init(pauseAction);
        public void SetTimer(string timer) => _timer.Value.text = timer;
        public void SetScore(int score) => _score.Value.text = score.ToString();
    }
}