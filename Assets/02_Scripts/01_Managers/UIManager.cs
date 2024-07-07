using TK.UI;
using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    public class UIManager : SingletonBehaviour<UIManager>
    {
        [SerializeField] private HomeUI homeUI;
        [SerializeField] private GameUI gameUI;
        [SerializeField] private LevelCompletedUI levelCompletedUI;
        [SerializeField] private LevelFailedUI levelFailedUI;

        public static HomeUI HomeUI => Instance.homeUI;
        public static GameUI GameUI => Instance.gameUI;
        public static LevelCompletedUI LevelCompletedUI => Instance.levelCompletedUI;
        public static LevelFailedUI LevelFailedUI => Instance.levelFailedUI;
    }
}