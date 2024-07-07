using System;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.NiceVibrations;
using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    public class LevelManager : SingletonBehaviour<LevelManager>
    {
        public static event Action OnLevelLoaded;
        public static event Action OnLevelStarted;
        public static event Action<bool> OnLevelStopped;

        [SerializeField] private List<Level> levelPrefabs = new();

        public static Level CurrentLevel { get; private set; }
        public static Transform Thrash { get; private set; }
        public static int TotalLevelCount => Instance.levelPrefabs.Count;

        protected override void Awake()
        {
            base.Awake();

            //Create the thrash
            Thrash = new GameObject("Thrash").transform;
        }

        private void Start()
        {
            LoadLevel(false);
        }

        public static void LoadLevel(bool nextLevel)
        {
            if (!Instance.levelPrefabs.Any()) return;
            if (nextLevel) PrefsManager.Instance.IncrementLevelIndex();

            ClearThrash();

            var levelToLoad = Instance.levelPrefabs[PrefsManager.Instance.GetLevelIndex() % TotalLevelCount];
            CurrentLevel = Instantiate(levelToLoad, Vector3.zero, Quaternion.identity, Thrash);

            UIManager.GameUI.SetLevelCountText("Level " + (PrefsManager.Instance.GetLevelIndex() + 1));
            UIManager.HomeUI.Show();
            OnLevelLoaded?.Invoke();
        }

        public static void StartLevel()
        {
            if (GameManager.IsPlaying) return;
            GameManager.IsPlaying = true;

            OnLevelStarted?.Invoke();
        }

        public static void StopLevel(bool isSuccess)
        {
            if (!GameManager.IsPlaying) return;
            GameManager.IsPlaying = false;

            if (isSuccess) //level succeed
            {
                MMVibrationManager.Haptic(HapticTypes.Success);
                UIManager.LevelCompletedUI.Show();
            }
            else //level failed
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
                UIManager.LevelFailedUI.Show();
            }

            OnLevelStopped?.Invoke(isSuccess);
        }

        private static void ClearThrash()
        {
            var count = Thrash.childCount;
            for (var i = count - 1; i >= 0; i--)
            {
                Destroy(Thrash.GetChild(i).gameObject);
            }
        }
    }
}