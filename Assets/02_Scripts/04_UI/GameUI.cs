using TK.Manager;
using TMPro;
using UnityEngine;

namespace TK.UI
{
    public class GameUI : UIBase
    {
        [SerializeField] private TextMeshProUGUI levelCountText;

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
        }

        private void OnLevelLoaded(int levelNo)
        {
            levelCountText.text = $"Level {levelNo}";
        }
    }
}