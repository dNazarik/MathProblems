using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Common
{
    public class MainMenuPanelView : MonoBehaviour
    {
        [field: SerializeField] public Button NewGameBtn { get; private set; }
        [field: SerializeField] public Button LeaderboardBtn { get; private set; }
        [field: SerializeField] public Button ChangeBgBtn { get; private set; }

        public void HideMiddleButtons()
        {
            LeaderboardBtn.gameObject.SetActive(false);
            ChangeBgBtn.gameObject.SetActive(false);
        }
    }
}