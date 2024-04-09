using Gameplay.General;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Create/Create Game settings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public bool AudioEnabled;
        public int DefaultSessionTime;
        public int AbilityResetCooldown;
        public int CorrectAnswersForNextLevel = 10;
        public int[] TimerBonuses = { 1, 2, 3 };
        public int[] ScoreValues = { 10, 20, 30 };
    }
}
