using TK.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TK.UI
{
    public class HomeUI : UIBase
    {
        [SerializeField] private Button playButton;

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
        }

        private void Play()
        {
            Hide();
            LevelManager.StartLevel();
        }
    }
}