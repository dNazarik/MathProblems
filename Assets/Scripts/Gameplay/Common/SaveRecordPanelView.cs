using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.Common
{
    /// <summary>
    /// Not big class so it makes no sense creating a Controller and a Model for it
    /// </summary>
    public class SaveRecordPanelView : MonoBehaviour, IDisposable
    {
        private const string DefaultName = "Unnamed";

        [SerializeField] private TMP_InputField _usernameInput;
        [SerializeField] private Button _saveBtn, _cancelBtn;
        [SerializeField] private GameObject _popup;

        public void Init(Action<string> saveAction, Action cancelAction)
        {
            _popup.SetActive(true);

            _saveBtn.onClick.AddListener(() => saveAction?.Invoke(GetValidatedInput()));
            _cancelBtn.onClick.AddListener(new UnityAction(cancelAction));
        }

        private string GetValidatedInput()
        {
            var nickname = _usernameInput.text;
            nickname = nickname.Trim();

            if (string.IsNullOrWhiteSpace(nickname))
                nickname = DefaultName;

            return nickname;
        }

        public void Dispose()
        {
            _saveBtn.onClick.RemoveAllListeners();
            _cancelBtn.onClick.RemoveAllListeners();
        }
    }
}