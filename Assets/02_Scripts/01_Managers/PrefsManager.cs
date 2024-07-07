using Sirenix.OdinInspector;
using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    public class PrefsManager : SingletonBehaviour<PrefsManager>
    {
        [SerializeField, ReadOnly] [BoxGroup("Game")]
        private int levelIndex;

        private const string LevelIndexKey = "com.tk.levelindex";

        protected override void Awake()
        {
            base.Awake();
            levelIndex = PlayerPrefs.GetInt(LevelIndexKey);
        }

        /// <summary>
        /// In order to play specific level on the editor
        /// </summary>
        /// <param name="index"></param>
        [Button, DisableInPlayMode]
        private void SetLevelIndex(int index)
        {
            this.levelIndex = index;
            PlayerPrefs.SetInt(LevelIndexKey, this.levelIndex);
        }

        public int GetLevelIndex()
        {
            return levelIndex;
        }

        public void IncrementLevelIndex()
        {
            SetLevelIndex(levelIndex + 1);
        }
    }
}