using TK.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TK.UI
{
    public class LevelCompletedUI : UIBase
    {
        [SerializeField] private Button nextButton;

        private void Awake()
        {
            nextButton.onClick.AddListener(Next);
        }

        private void Next()
        {
            Hide();
            LevelManager.LoadLevel(true);
        }
    }
}