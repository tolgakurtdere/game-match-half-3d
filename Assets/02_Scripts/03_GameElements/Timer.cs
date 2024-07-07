using TK.Manager;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MatchHalf3D
{
    public class Timer : MonoBehaviour
    {
        [SerializeField, Required] private TextMeshProUGUI timerText;
        private float _remainingTime;
        private bool _isRunning;

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            LevelManager.OnLevelStarted += OnLevelStarted;
            LevelManager.OnLevelStopped += OnLevelStopped;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
            LevelManager.OnLevelStarted -= OnLevelStarted;
            LevelManager.OnLevelStopped -= OnLevelStopped;
        }

        void Update()
        {
            if (!_isRunning) return;

            _remainingTime -= Time.deltaTime;
            if (_remainingTime > 0)
            {
                DisplayTime(_remainingTime);
            }
            else
            {
                _remainingTime = 0;
                _isRunning = false;

                LevelManager.StopLevel(false);
            }
        }

        private void OnLevelLoaded()
        {
            _remainingTime = LevelManager.CurrentLevel.TimerDuration;

            DisplayTime(_remainingTime);
        }

        private void OnLevelStarted()
        {
            _isRunning = true;
        }

        private void OnLevelStopped(bool isSuccess)
        {
            _isRunning = false;
        }

        private void DisplayTime(float time)
        {
            var minutes = Mathf.FloorToInt(time / 60);
            var seconds = Mathf.FloorToInt(time % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}