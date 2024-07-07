using System;
using TK.Manager;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TK.Utility
{
    [Serializable]
    public class FXPlayer
    {
        [SerializeField] private ParticleSystem fx;

        public void PlayFX(Transform target)
        {
            PlayFX(target.position, target.rotation);
        }

        public void PlayFX(Vector3 position)
        {
            if (!fx) return;
            Object.Instantiate(fx, position, fx.transform.rotation, LevelManager.Thrash);
        }

        public void PlayFX(Vector3 position, Quaternion rotation)
        {
            if (!fx) return;
            Object.Instantiate(fx, position, rotation, LevelManager.Thrash);
        }
    }
}