using TMPro;
using UnityEngine;

namespace Leaderboard.ListEntity
{
    public class LeaderListEntity : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name, _score;

        public void SetEntity(string playerName, int playerScore)
        {
            _name.text = playerName;
            _score.text = playerScore.ToString();
        }
    }
}
