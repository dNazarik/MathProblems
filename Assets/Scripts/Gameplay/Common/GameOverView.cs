using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Common
{
    /// <summary>
    /// Not big class so it makes no sense creating a Controller and a Model for it
    /// </summary>
    public class GameOverView : MonoBehaviour
    {
        private const float AnimationDuration = 2.0f;
        private const int StaticTextTimeMs = 1000;

        [SerializeField] private GameObject _gameOverInputBlocker;
        [SerializeField] private Transform _gameOverTransform;

        public async Task PlayAnimation()
        {
            _gameOverInputBlocker.SetActive(true);

            await _gameOverTransform.DOLocalMoveY(0.0f, AnimationDuration, true).AsyncWaitForCompletion();

            await Task.Delay(StaticTextTimeMs);

            _gameOverInputBlocker.SetActive(false);
        }
    }
}