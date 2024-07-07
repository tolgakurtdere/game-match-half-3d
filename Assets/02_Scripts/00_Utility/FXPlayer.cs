using System;
using Lean.Pool;
using UnityEngine;

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
            LeanPool.Spawn(fx, position, fx.transform.rotation);
        }

        public void PlayFX(Vector3 position, Quaternion rotation)
        {
            if (!fx) return;
            LeanPool.Spawn(fx, position, rotation);
        }
    }
}