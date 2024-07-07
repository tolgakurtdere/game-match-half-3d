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
        private const string LEVEL_INDEX_KEY = "com.tk.reachedLevelIndex";
        public static event Action<int> OnLevelLoaded;
        public static event Action OnLevelStarted;
        public static event Action<bool> OnLevelStopped;

        [SerializeField] private List<Level> levelPrefabs = new();
        private static int? s_reachedLevelIndex;

        private static int ReachedLevelIndex
        {
            get { return s_reachedLevelIndex ??= PlayerPrefs.GetInt(LEVEL_INDEX_KEY); }
            set
            {
                s_reachedLevelIndex = value;
                PlayerPrefs.SetInt(LEVEL_INDEX_KEY, value);
                PlayerPrefs.Save();
            }
        }

        public static int HighestLevelNo => ReachedLevelIndex + 1;

        public static Level CurrentLevel { get; private set; }

        public static int TotalLevelCount => Instance.levelPrefabs.Count;

        private void Start()
        {
            LoadLevel(false);
        }

        public static void LoadLevel(bool nextLevel)
        {
            if (!Instance.levelPrefabs.Any()) return;
            if (nextLevel) ReachedLevelIndex++;

            if (CurrentLevel) Destroy(CurrentLevel.gameObject);

            var levelToLoad = Instance.levelPrefabs[ReachedLevelIndex % TotalLevelCount];
            CurrentLevel = Instantiate(levelToLoad, Vector3.zero, Quaternion.identity, Instance.transform);

            UIManager.HomeUI.Show();
            OnLevelLoaded?.Invoke(HighestLevelNo);
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
    }
}