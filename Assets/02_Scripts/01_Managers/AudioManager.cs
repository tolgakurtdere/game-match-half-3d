using Sirenix.OdinInspector;
using TK.Utility;
using UnityEngine;

namespace TK.Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : SingletonBehaviour<AudioManager>
    {
        [SerializeField, Required] private AudioClip clickSfx;
        [SerializeField, Required] private AudioClip matchSfx;
        [SerializeField, Required] private AudioClip successSfx;
        [SerializeField, Required] private AudioClip failSfx;
        private AudioSource _audioSource;

        public static AudioClip ClickSfx => Instance.clickSfx;
        public static AudioClip MatchSfx => Instance.matchSfx;
        public static AudioClip SuccessSfx => Instance.successSfx;
        public static AudioClip FailSfx => Instance.failSfx;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
        }

        private void OnLevelLoaded()
        {
            _audioSource.Stop();
        }

        public void Play(AudioClip audioClip)
        {
            //_audioSource.Stop();
            _audioSource.PlayOneShot(audioClip);
        }
    }
}