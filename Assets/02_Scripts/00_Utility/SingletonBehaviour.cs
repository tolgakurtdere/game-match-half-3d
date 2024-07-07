using UnityEngine;

namespace TK.Utility
{
    /// <summary>
    /// Inherit from this base class to create a singleton. 
    /// </summary>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        [SerializeField] private bool dontDestroyOnLoad;

        /// <summary>
        /// Lock used to not allow simultaneous operations on this singleton by multiple sources.
        /// </summary>
        private static readonly object s_lock = new();

        /// <summary>
        /// Reference to the singleton instance of type <see cref="T"/>.
        /// </summary>
        private static T s_instance;

        /// <summary>
        /// Returns the reference to the singleton instance of type <see cref="T"/>.
        /// </summary>
        public static T Instance
        {
            get
            {
                // Lock preventing from simultaneous access by multiple sources.
                lock (s_lock)
                {
                    // If it's the first time accessing this singleton Instance, _instance will always be null
                    // Searching for an active instance of type T in the scene.
                    if (!s_instance)
                    {
                        s_instance = FindObjectOfType<T>();

                        if (!s_instance)
                        {
                            Debug.LogError($"The singleton object could not found! : {typeof(T)}");
                        }
                    }

                    return s_instance;
                }
            }
        }

        /// <summary>
        /// Checking if an instance of <see cref="SingletonBehaviour{T}"/> already exists in the scene.
        /// If it exists, destroy this object.
        /// </summary> 
        protected virtual void Awake()
        {
            if (Instance != this)
            {
                Destroy(this);
                Debug.LogError($"There are more than one singleton object! : {typeof(T)}. Destroyed!");
                return;
            }

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary>
        /// Removes the reference to this object on destroy.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                s_instance = null;
            }
        }
    }
}