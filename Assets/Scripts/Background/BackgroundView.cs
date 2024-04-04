using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    [SerializeField] private Image _bg;
    public void SetBackground(Sprite sprite) => _bg.sprite = sprite;
}