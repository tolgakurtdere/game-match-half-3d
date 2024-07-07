using Sirenix.OdinInspector;
using TK.Settings;
using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField, Required] private GameSettings gameSettings;

        public static bool IsPlaying { get; set; }
        public static GameSettings GameSettings => Instance.gameSettings;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }
    }
}