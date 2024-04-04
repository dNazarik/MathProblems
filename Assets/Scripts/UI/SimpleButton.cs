using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SimpleButton : MonoBehaviour
    {
        [SerializeField] protected Button button;

        public void Init(Action action) => button.onClick.AddListener(new UnityAction(action));
    }
}