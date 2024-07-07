using TK.Manager;
using MoreMountains.NiceVibrations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MatchHalf3D
{
    public class MatchController : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] private int MatchingObjectCount { get; set; }
        [ShowInInspector, ReadOnly] private MatchingObject _currentObject;

        private void OnEnable()
        {
            MatchingObject.OnAnyClicked += OnMatchingObjectClicked;
        }

        private void OnDisable()
        {
            MatchingObject.OnAnyClicked -= OnMatchingObjectClicked;
        }

        private void Awake()
        {
            MatchingObjectCount = GetComponentsInChildren<MatchingObject>().Length / 2;
        }

        private void OnMatchingObjectClicked(MatchingObject obj)
        {
            if (_currentObject)
            {
                if (_currentObject == obj)
                {
                    //return;
                }
                else if (_currentObject.ID == obj.ID)
                {
                    _currentObject.Match(MatchObjects);
                    obj.Match();
                    _currentObject = null;
                }
                else
                {
                    _currentObject.Release();
                    _currentObject = obj;
                }
            }
            else
            {
                _currentObject = obj;
            }

            AudioManager.Instance.Play(AudioManager.ClickSfx);
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }

        private void MatchObjects(Vector3 matchPos)
        {
            MatchingObjectCount--;
            if (MatchingObjectCount == 0)
            {
                LevelManager.StopLevel(true);
            }

            GameManager.GameSettings.MatchFx.PlayFX(matchPos, Quaternion.identity);
            AudioManager.Instance.Play(AudioManager.MatchSfx);
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
    }
}