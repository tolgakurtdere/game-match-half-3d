using TK.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TK.UI
{
    public class LevelFailedUI : UIBase
    {
        [SerializeField] private Button retryButton;

        private void Awake()
        {
            retryButton.onClick.AddListener(Retry);
        }

        private void Retry()
        {
            Hide();
            LevelManager.LoadLevel(false);
        }
    }
}