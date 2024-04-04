using System;
using Background;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;

namespace Gameplay.Common
{
    /// <summary>
    /// Not big class so it makes no sense creating a Controller and a Model for it
    /// </summary>
    public class PausePanelView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _continueButton, _changeBgButton;
        [SerializeField] private GameObject _pauseScreen;

        private BackgroundController _backgroundController;

        [Inject]
        public void Construct(BackgroundController bgController) => _backgroundController = bgController;

        public void Init(Action continueAction)
        {
            _continueButton.onClick.AddListener(new UnityAction(continueAction));
            _changeBgButton.onClick.AddListener(_backgroundController.SetNextBackground);
        }

        public void ShowPauseScreen(bool isShow) => _pauseScreen.SetActive(isShow);

        public void Dispose()
        {
            _continueButton.onClick.RemoveAllListeners();
            _changeBgButton.onClick.RemoveAllListeners();
        }
    }
}