using TMPro;
using UnityEngine;

namespace Gameplay.Common
{
    /// <summary>
    /// Not big class so it makes no sense creating a Controller and a Model for it
    /// </summary>
    public class MiddlePanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _question;

        public void SetQuestion(string question) => _question.text = question;
    }
}
