using TMPro;
using UnityEngine;

namespace TK.UI
{
    public class GameUI : UIBase
    {
        [SerializeField] private TextMeshProUGUI levelCountText;

        public void SetLevelCountText(string levelText)
        {
            levelCountText.text = levelText;
        }
    }
}